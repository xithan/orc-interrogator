namespace Interrogator.Strategies
{
    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: Betray if opponent betrayed two times in a row, otherwise cooperate
    /// </summary>
    public class TitForTwoTats : IStrategy
    {
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            if (turn == 1)
            {
                return Move.Cooperate;
            }

            return otherMoves.TakeLast(2).All(m => m == Move.Betray) 
                   && yourMoves.Last() == Move.Cooperate
                ? Move.Betray
                : Move.Cooperate;
        }
    }
}