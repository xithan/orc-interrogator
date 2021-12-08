namespace Interrogator.Strategies
{
    using System.Collections.Generic;

    using Interrogator.Game;

    /// <summary>
    ///  Demo strategy - will be removed for the actual tournament.
    /// </summary>
    public class AlwaysCooperate : IStrategy
    {
        #region Public Methods and Operators

        /// <inheritdoc />
        public Move GetNextMove(int currentRound, IList<Move> previousOwnMoves, IList<Move> previousPartnerMoves)
        {
            return Move.Cooperate;
        }

        #endregion
    }
}