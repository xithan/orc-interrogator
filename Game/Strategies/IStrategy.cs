namespace Interrogator.Game
{
    public interface IStrategy
    {
        Move GetNextMove(int turn, List<Move> yourMoves, List<Move> otherMoves);
    }
}