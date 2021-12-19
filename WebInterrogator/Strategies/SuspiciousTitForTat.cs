namespace Interrogator.Strategies
{
    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: Start with betray. Repeat the last move of your opponent
    /// </summary>
    public class SuspiciousTitForTat : IStrategy
    {
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            return turn == 1 ? Move.Betray : otherMoves.Last();
        }
    }
}