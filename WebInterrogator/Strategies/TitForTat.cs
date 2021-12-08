namespace Interrogator.Strategies
{
    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: Repeat the last move of your opponent
    /// </summary>
    public class TitForTat : IStrategy
    {
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            return turn == 1 ? Move.Cooperate : otherMoves.Last();
        }
    }
}