namespace Interrogator.Game.Setup
{
    public class Prison
    {
        private static int MIN_TURN_COUNT = 50;
        private static int MAX_TURN_COUNT = 200;
        
        public Prison(PrisonConfig prisonConfig, IEnumerable<Prisoner> prisoners)
        {
            this.Prisoners = prisoners.ToList();
            this.NumberOfTurns = prisonConfig.Turns ?? this.GetRandomTurnCount();
            this.MsPerTurn = prisonConfig.MsPerTurn;
            Console.WriteLine($"Points: {prisonConfig.BothCooperate}");
            this.PayoffCalculator = new PayoffCalculator(
                prisonConfig.BothCooperate,
                prisonConfig.Betrayed,
                prisonConfig.Betraying,
                prisonConfig.BothBetrayed);
        }

        public PayoffCalculator PayoffCalculator { get; }

        public List<Prisoner> Prisoners { get; }

        public int MsPerTurn { get; }

        public int NumberOfTurns { get; }

        public void ResetStrategies()
        {
            Prisoners.ForEach(p => p.ResetStrategy());
        }

        private int GetRandomTurnCount()
        {
            Random r = new Random();
            return r.Next(MIN_TURN_COUNT, MAX_TURN_COUNT);
        }
        
        public List<Round> CreateRounds()
        {
            var rounds = new List<Round>();
            var odd = false;
            if (Prisoners.Count % 2 == 1)
            {
                odd = true;
                Prisoners.Insert(0, null);
            }
            var half = Prisoners.Count / 2;
            
            for (var roundCount = 1; roundCount < Prisoners.Count; roundCount++)
            {
                var round = new Round(roundCount);
                for (var i = odd ? 1 : 0; i < half; i++)
                {
                    var p1 = Prisoners[i];
                    var p2 = Prisoners[Prisoners.Count - i - 1];
                    round.Duels.Add(new Duel(p1, p2));
                }
                rounds.Add(round);
                
                var last = Prisoners.Last();
                Prisoners.RemoveAt(Prisoners.Count - 1);
                Prisoners.Insert(1, last);
                
            }
            if (odd)
            {
                Prisoners.RemoveAt(0);
            }
            return rounds;
        }
    }
}


   
    
   
    
