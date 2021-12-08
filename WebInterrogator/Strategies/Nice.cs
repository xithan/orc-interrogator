namespace Interrogator.Strategies
{
    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: Always cooperate
    /// </summary>
    public class Nice : IStrategy
    {
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            return Move.Cooperate;
        }
    }
}