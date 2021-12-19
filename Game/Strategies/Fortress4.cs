namespace Interrogator.Game.Strategies
{
    public class Fortress4 : IStrategy
    {
        private Move _answer;

        public Move GetNextMove(int turn, List<Move> yourMoves, List<Move> otherMoves)
        {
            turn += 1;
            switch (turn)
            {
                case 1:
                case 2:
                case 3:
                    return Move.Betray;
                case 4:
                    return Move.Cooperate;
                case 5:
                    if ((otherMoves[0] == Move.Betray) &&
                        (otherMoves[1] == Move.Betray) &&
                        (otherMoves[2] == Move.Betray) &&
                        (otherMoves[3] == Move.Cooperate))
                    {
                        this._answer = Move.Cooperate;
                    }
                    else
                    {
                        this._answer = Move.Betray;
                    }

                    break;
                default:
                    if (this._answer == Move.Cooperate)
                    {
                        this._answer = otherMoves[turn - 2];
                    }
                    else if (otherMoves[turn - 2] == Move.Betray &&
                             otherMoves[turn - 3] == Move.Betray &&
                             otherMoves[turn - 4] == Move.Betray)
                    {
                        this._answer = Move.Cooperate;
                    }

                    break;
            }
            return this._answer;
        }
    }
}