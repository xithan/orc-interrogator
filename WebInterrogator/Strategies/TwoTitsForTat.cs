namespace Interrogator.Strategies
{
    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: This strategy betrays twice in response to betray and otherwise cooperates
    /// </summary>
    public class TwoTitsForTat : IStrategy
    {
        private bool _betrayAgain;
        
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            if (turn == 1)
            {
                return Move.Cooperate;
            }

            if (otherMoves.Last() == Move.Betray)
            {
                _betrayAgain = true;
                return Move.Betray;
            }
            if (_betrayAgain)
            {
                _betrayAgain = false;
                return Move.Betray;
            }

            return Move.Cooperate;
        }
    }
}