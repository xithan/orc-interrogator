namespace Interrogator.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: If betrayed once, then always betray.
    /// </summary>
    public class PSt : IStrategy
    {
        private bool _yellowCard;

        private bool _redCard;

        /// <inheritdoc />
        public Move GetNextMove(int currentTurn, IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
        {
            if (currentTurn == 1)
            {
                return Move.Cooperate;
            }

            this._yellowCard = this._yellowCard || previousOpponentMoves.Last() == Move.Betray;
            this._redCard = this._redCard || (this._yellowCard && previousOpponentMoves.Last() == Move.Betray);

            return this._redCard
                       ? Move.Betray
                       : Move.Cooperate;
        }

    }
}