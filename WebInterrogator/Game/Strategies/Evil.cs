namespace Interrogator.Game.Strategies
{
    public class Evil : IStrategy
    {
        public Move GetNextMove(int turn, List<Move> yourMoves, List<Move> otherMoves)
        {
            return Move.Betray;
        }
    }
}