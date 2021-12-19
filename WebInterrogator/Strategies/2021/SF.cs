namespace Interrogator.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using Interrogator.Game;

    /// <summary>
    /// Very clever strategy that no-one can beat.
    /// </summary>
    public class SF : IStrategy
    {
        public Move GetNextMove(int currentTurn, IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
        {
            return currentTurn == 1 ? Move.Cooperate : previousOpponentMoves.Last();
        }
    }
}