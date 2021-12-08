namespace Interrogator.Game
{
    public interface IStrategy
    {
        Move GetNextMove(int turn, List<Move> ownPreviousMoves, List<Move> opponentPreviousMoves);
    }
}