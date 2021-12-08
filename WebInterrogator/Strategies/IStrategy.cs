namespace Interrogator.Strategies
{
    using Interrogator.Game;
    
    public interface IStrategy
    {
        /// <summary>
        /// Gets the next move.
        /// </summary>
        /// <param name="currentTurn">Current duel turn (first turn has index 1).</param>
        /// <param name="previousOwnMoves">The list of previous own moves.</param>
        /// <param name="previousOpponentMoves">The list of previous partner moves.</param>
        /// <returns>The next move.</returns>
        Move GetNextMove(int currentTurn, IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves);
    }
}