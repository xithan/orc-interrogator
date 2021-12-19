namespace Interrogator.Strategies
{
    using Interrogator.Game;
    
    public class PK : IStrategy
    {
        const int EVAL_PERIOD = 6;
        enum Strategy
        {
            TitForTat,
            AlwaysBetray,
            TitForTwoTats,
        }

        private Strategy _strategy = Strategy.TitForTat;

        private int _evalCounter = 0;
        
        private int _betrayCounter = 0;
        
        private int _fuckedCounter = 0;
        
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            _evalCounter++;
            if (_evalCounter == EVAL_PERIOD)
            {
                _evalCounter = 0;
            }
            
            if (turn == 1)
            {
                return Move.Cooperate;
            }
            
            var lastMove = otherMoves[turn - 2];
            if (lastMove == Move.Betray)
            {
                _betrayCounter++;
                if (yourMoves[turn - 2] == Move.Cooperate)
                {
                    _fuckedCounter++;
                }
            }

            if (turn == EVAL_PERIOD || (_evalCounter == 0 && _fuckedCounter >= 2))
            {
                Evaluate();    
            }

            return this._strategy switch
            {
                Strategy.AlwaysBetray => Move.Betray,
                Strategy.TitForTat => lastMove,
                Strategy.TitForTwoTats => (lastMove == Move.Betray && 
                                           otherMoves[turn - 3] == Move.Betray && 
                                            yourMoves.Last() == Move.Cooperate)
                    ? Move.Betray
                    : Move.Cooperate,
                _ => Move.Betray
            };
        }

        private void Evaluate()
        {
            this._strategy = _betrayCounter switch
            {
                > 4 => Strategy.AlwaysBetray,
                3 => Strategy.TitForTwoTats,
                0 => Strategy.TitForTat,
                _ => Strategy.AlwaysBetray
            };

            _fuckedCounter = 0;
            _betrayCounter = 0;
        }
    }
}