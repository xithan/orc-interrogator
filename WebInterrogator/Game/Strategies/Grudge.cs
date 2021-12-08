namespace Interrogator.Game.Strategies
{
    public class Grudge : IStrategy
    {
        private bool _betrayed;
        public Move GetNextMove(int turn, List<Move> yourMoves, List<Move> otherMoves)
        {
            if (turn == 0)
                return Move.Cooperate;
            this._betrayed = this._betrayed || otherMoves.Last() == Move.Betray;
            return _betrayed ? Move.Betray : Move.Cooperate;
        }
    }
}