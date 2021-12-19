using Interrogator.Game;
using Interrogator.Strategies;

namespace Interrogator.Strategies
{
    public class Fortress4 : IStrategy
    {
        private int _state = 1;

        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            if (turn == 1)
            {
                return Move.Betray;
            }

            var lastMove = otherMoves[turn - 2];
            
            switch (_state)
            {
                case 1:
                    if (lastMove == Move.Betray)
                    {
                        _state = 2;
                    }
                    return Move.Betray;
                case 2:
                    _state = lastMove == Move.Cooperate ? 1 : 3;
                    return Move.Betray;
                case 3:
                    _state = lastMove == Move.Cooperate ? 1 : 4;
                    return Move.Betray;
                case 4:
                    if (lastMove == Move.Betray)
                    {
                        _state = 1;
                        return Move.Betray;
                    }
                    return Move.Cooperate;
            }

            return Move.Betray;
        }
    }
}