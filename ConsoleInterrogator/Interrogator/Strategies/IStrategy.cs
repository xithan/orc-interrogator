namespace Interrogator.Strategies
{
    using System.Collections.Generic;

    using Interrogator.Game;

    public interface IStrategy
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the next move.
        /// </summary>
        /// <param name="currentTurn">Current duel turn (first round has index 1).</param>
        /// <param name="previousOwnMoves">The list of previous own moves.</param>
        /// <param name="previousPartnerMoves">The list of previous partner moves.</param>
        /// <returns>The next move.</returns>
        Move GetNextMove(int currentTurn, IList<Move> previousOwnMoves, IList<Move> previousPartnerMoves);
        
        #endregion
    }
}