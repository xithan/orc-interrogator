using System.Collections.Generic;

namespace Interrogator.Strategies
{
    using Interrogator.Game;

    public class RK : IStrategy
    {
        public Move GetNextMove(int currentTurn, IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
        {
            switch (currentTurn)
            {
                case 1:
                    return Move.Cooperate;
                case 2:
                    return previousOpponentMoves[^1];
                case 3:
                    return previousOpponentMoves[^1] == previousOpponentMoves[^2] ? previousOpponentMoves[^1] : Move.Cooperate;
            }

            if (previousOpponentMoves[^1] == previousOpponentMoves[^2])
            {
                return previousOpponentMoves[^1];
            }
            
            return previousOpponentMoves[^1] == previousOpponentMoves[^3] ? previousOpponentMoves[^1] : previousOpponentMoves[^2];
        }
    }
}
