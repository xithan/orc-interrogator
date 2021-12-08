namespace Interrogator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Interrogator.Game;
    using Interrogator.Strategies;



    public static class Program
    {
        #region Static Fields

        // TODO Remove demo strategies and add participant's strategies
        private static readonly Type[] Strategies =
            { 
                typeof(Evil),
                typeof(Nice),
                typeof(Grudge),
                typeof(Rando),
                typeof(TitForTat),
            };

        #endregion

        #region Methods

        static void Main(string[] args)
        {
            var gamePoints = Program.Strategies.ToDictionary(x => x, x => 0);
            var duelRounds = Program.GetRandomNumber();

            Program.RunAllDuelsAndUpdateGamePoints(gamePoints, duelRounds);

            Program.PrintResults(gamePoints);
        }

        private static void RunAllDuelsAndUpdateGamePoints(Dictionary<Type, int> gamePoints, int duelRounds)
        {
            for (var i = 0; i < Program.Strategies.Length; i++)
            {
                for (var j = i + 1; j < Program.Strategies.Length; j++)
                {
                    var (strategy1, strategy2) = (Program.Strategies[i], Program.Strategies[j]);
                    var payoffs = new Duel(duelRounds).RunDuel(Program.Strategies[i], Program.Strategies[j]);
                    Program.UpdateGamePoints(strategy1, strategy2, payoffs, gamePoints);
                }
            }
        }

        private static void UpdateGamePoints(
            Type strategy1,
            Type strategy2,
            (int Payoff1, int Payoff2) payoffs,
            Dictionary<Type, int> gamePoints)
        {
            gamePoints[strategy1] = gamePoints.GetValueOrDefault(strategy1) + payoffs.Payoff1;
            gamePoints[strategy2] = gamePoints.GetValueOrDefault(strategy2) + payoffs.Payoff2;
        }

        private static void PrintResults(IDictionary<Type, int> gamePoints)
        {
            Console.WriteLine("Final results:");

            foreach (var (key, value) in gamePoints
                .OrderBy(x => x.Value)
                .ThenBy(x => x.Key.Name))
            {
                Console.WriteLine(key.Name + ": " + value);
            }
        }

        private static int GetRandomNumber()
        {
            // TODO Change range and seed for actual tournament run.
            const int Seed = 0;
            return new Random(Seed).Next(500, 1000);
        }

        #endregion
    }
}