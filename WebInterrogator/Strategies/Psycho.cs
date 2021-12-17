namespace Interrogator.Strategies
{
    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: Repeat the opposite of the last move of your opponent
    /// </summary>
    public class Psycho : IStrategy
    {
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            if (turn == 1)
            {
                return Move.Cooperate;
            }

            return otherMoves.Last() == Move.Cooperate ? Move.Betray : Move.Cooperate;
        }
    }
}