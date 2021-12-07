namespace Interrogator.Game.Strategies
{
    public class TitForTat : IStrategy
    {
        public Move GetNextMove(int turn, List<Move> yourMoves, List<Move> otherMoves)
        {
            return turn == 0 ? Move.Cooperate : otherMoves.Last();
        }
    }
}