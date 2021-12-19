namespace Interrogator.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using Interrogator.Game;

    public class UM : IStrategy
    {
        private bool _opponentHasBetrayed;

        /// <inheritdoc />
        public Move GetNextMove(int currentTurn, IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
        {
            if (currentTurn >= 3)
            {
                this._opponentHasBetrayed |= previousOpponentMoves.Last() == Move.Betray;
            }

            return this._opponentHasBetrayed 
                       ? Move.Betray 
                       : Move.Cooperate;
        }
    }
}
