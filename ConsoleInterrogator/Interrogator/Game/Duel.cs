namespace Interrogator.Game
{
    using System;
    using System.Collections.Generic;

    using Interrogator.Strategies;

    /// <summary>
    /// Class to run a single duel between two strategies. Aggregates and returns payoffs from all duel rounds.
    /// </summary>
    public class Duel
    {
        #region Fields

        private readonly int _duelRounds;

        #endregion

        #region Constructors and Destructors

        public Duel(int duelRounds)
        {
            this._duelRounds = duelRounds;
        }

        #endregion

        #region Public Methods and Operators

        public (int, int) RunDuel(Type strategyType1, Type strategyType2)
        {
            var (moves1, moves2) = (new List<Move>(), new List<Move>());
            var (payoff1, payoff2) = (0, 0);

            var strategy1 = Duel.CreateStrategyInstance(strategyType1);
            var strategy2 = Duel.CreateStrategyInstance(strategyType2);
            
            for (var currentRound = 1; currentRound <= this._duelRounds; currentRound++)
            {
                // Get and update moves.
                var (move1, move2) = (strategy1.GetNextMove(currentRound, moves1, moves2),
                                         strategy2.GetNextMove(currentRound, moves2, moves1));
                moves1.Add(move1);
                moves2.Add(move2);

                // Calculate and update payoffs.
                var (payoff1ForCurrentRound, payoff2ForCurrentRound) = PayoffCalculator.CalculatePayoff(move1, move2);
                payoff1 += payoff1ForCurrentRound;
                payoff2 += payoff2ForCurrentRound;
            }

            Console.WriteLine(
                "Duel result for " + strategy1.GetType().Name + " vs. " + strategy2.GetType().Name + ": "
                + payoff1 + " / " + payoff2);

            return (payoff1, payoff2);
        }

        private static IStrategy CreateStrategyInstance(Type strategyType)
        {
            return (IStrategy)Activator.CreateInstance(strategyType) ?? throw new ArgumentException($"Invalid type for a strategy: {strategyType}");
        }

        #endregion
    }
}