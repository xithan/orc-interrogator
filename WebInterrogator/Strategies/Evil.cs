namespace Interrogator.Strategies
{
    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: Always betray
    /// </summary>
    public class Evil : IStrategy
    {
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            return Move.Betray;
        }
    }
}