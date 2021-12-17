namespace Interrogator.Strategies
{
    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: Randomly cooperate or betray, but betray is more likely
    /// </summary>
    public class EvilRando : IStrategy
    {
        private System.Random _randomizer;
        
        public EvilRando()
        {
            this._randomizer = new System.Random();
        }
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            return this._randomizer.NextDouble() > 0.3 ? Move.Betray : Move.Cooperate;
        }
    }
}