namespace Interrogator.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: If betrayed once, then always betray.
    /// </summary>
    public class Grudge : IStrategy
    {
        private bool _betrayed;

        /// <inheritdoc />
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            if (turn == 1)
                return Move.Cooperate;
            this._betrayed = this._betrayed || otherMoves.Last() == Move.Betray;
            return this._betrayed ? Move.Betray : Move.Cooperate;
        }
    }
}