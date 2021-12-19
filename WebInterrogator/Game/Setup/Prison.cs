namespace Interrogator.Game.Setup
{
    public class Prison
    {
        private const int MIN_TURN_COUNT = 50;
        private const int MAX_TURN_COUNT = 200;

        public Prison(PrisonConfig prisonConfig, IEnumerable<Prisoner> prisoners)
        {
            this.Prisoners = prisoners.ToList();
            this.NumberOfTurns = prisonConfig.Turns ?? this.GetRandomTurnCount();
            this.ConfigureSpeed(prisonConfig.MsPerTurn);
            this.Name = prisonConfig.Name;
            
            Console.WriteLine($"Points: {prisonConfig.BothCooperate}");
            this.PayoffCalculator = new PayoffCalculator(
                prisonConfig.BothCooperate,
                prisonConfig.Betrayed,
                prisonConfig.Betraying,
                prisonConfig.BothBetrayed);
        }

        private void ConfigureSpeed(double msPerTurn)
        {
            switch (msPerTurn)
            {
                case >= 1:
                    this.MsPerTurn = (int)msPerTurn;
                    break;
                case > 0:
                    this.MsPerTurn = 1;
                    this.TurnsPerMs = (int)(1.0 / msPerTurn);
                    break;
                default:
                    this.MsPerTurn = 0;
                    this.TurnsPerMs = this.NumberOfTurns;
                    break;
            }
        }

        public string Name { get; private set; }
        public PayoffCalculator PayoffCalculator { get; }

        public List<Prisoner> Prisoners { get; }

        public int MsPerTurn { get; private set; }
        
        public int TurnsPerMs { get; private set; }

        public int NumberOfTurns { get; private set; }
        
        public void ResetStrategies()
        {
            this.Prisoners.ForEach(p => p.ResetStrategy());
        }

        private int GetRandomTurnCount()
        {
            var r = new Random();
            return r.Next(Prison.MIN_TURN_COUNT, Prison.MAX_TURN_COUNT);
        }
        
        public void SetRandomTurnCount(int seed)
        {
            var r = new Random(seed);
            this.NumberOfTurns = r.Next(Prison.MIN_TURN_COUNT, Prison.MAX_TURN_COUNT);
        }
        
        public List<Round> CreateRounds()
        {
            Console.WriteLine("creating rounds");
            var rounds = new List<Round>();
            var odd = false;
            if (this.Prisoners.Count % 2 == 1)
            {
                odd = true;
                this.Prisoners.Insert(0, null);
            }
            Console.WriteLine($"Prisoners count {Prisoners.Count}");
            var half = this.Prisoners.Count / 2;
            
            for (var roundCount = 1; roundCount < this.Prisoners.Count; roundCount++)
            {
                var round = new Round(roundCount);
                for (var i = odd ? 1 : 0; i < half; i++)
                {
                    var p1 = this.Prisoners[i];
                    var p2 = this.Prisoners[this.Prisoners.Count - i - 1];
                    round.Duels.Add(new Duel(p1, p2));
                }
                rounds.Add(round);
                
                var last = this.Prisoners.Last();
                this.Prisoners.RemoveAt(this.Prisoners.Count - 1);
                this.Prisoners.Insert(1, last);
                
            }
            if (odd)
            {
                Prisoners.RemoveAt(0);
            }
            return rounds;
        }
    }
}


   
    
   
    
