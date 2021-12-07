namespace Interrogator.Game.Strategies
{
    public class Nice : IStrategy
    {
        public Move GetNextMove(int turn, List<Move> yourMoves, List<Move> otherMoves)
        {
            return Move.Cooperate;
        }
    }
}