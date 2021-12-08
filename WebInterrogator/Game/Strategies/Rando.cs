namespace Interrogator.Game.Strategies
{
    public class Rando : IStrategy
    {
        private System.Random _randomizer;
        
        public Rando()
        {
            this._randomizer = new System.Random();
        }
        public Move GetNextMove(int turn, List<Move> yourMoves, List<Move> otherMoves)
        {
            return _randomizer.NextDouble() > 0.5 ? Move.Betray : Move.Cooperate;
        }
    }
}