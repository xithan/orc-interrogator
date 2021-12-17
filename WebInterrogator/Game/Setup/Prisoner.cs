namespace Interrogator.Game.Setup
{
    using Interrogator.Strategies;

    public class Prisoner
    {
        private Type _strategyType;
        
        public Prisoner(string name, string strategyName)
        {
            this.Name = name;
            this._strategyType = Type.GetType($"Interrogator.Strategies.{strategyName}") ??
                                 throw new ArgumentException($"Couldn't find type {strategyName}");
            this.ResetStrategy();
        }

        public void ResetStrategy()
        {
            this.Strategy = (IStrategy)Activator.CreateInstance(this._strategyType) ??
                            throw new ArgumentException($"Couldn't load {this._strategyType}");
        }
        public string Name { get; }

        public int TotalSentence { get; set; }
        public double DuelWins { get; set; }
        
        public IStrategy Strategy { get; private set; }
    }
}