namespace Interrogator.Strategies
{
    using System.Collections.Generic;

    using Interrogator.Game;

    /// <summary>
    /// Demo strategy: Randomly cooperate or betray
    /// </summary>
    public class Rando : IStrategy
    {
        private System.Random _randomizer;
        
        public Rando()
        {
            this._randomizer = new System.Random();
        }
        public Move GetNextMove(int turn, IList<Move> yourMoves, IList<Move> otherMoves)
        {
            return this._randomizer.NextDouble() > 0.5 ? Move.Betray : Move.Cooperate;
        }
    }
}