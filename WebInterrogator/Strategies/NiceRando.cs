namespace Interrogator.Strategies
{
    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: Randomly cooperate or betray, but cooperate is more likely
    /// </summary>
    public class NiceRando : IStrategy
    {
        private System.Random _randomizer;
        
        public NiceRando()
        {
            this._randomizer = new System.Random();
        }
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            return this._randomizer.NextDouble() > 0.7 ? Move.Betray : Move.Cooperate;
        }
    }
}