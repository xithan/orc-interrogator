namespace Interrogator.Game
{
    public class PayoffCalculator
    {
        public readonly int BothCooperate;
        public readonly int Betrayed;
        public readonly int Betraying;
        public readonly int BothBetray;

        public PayoffCalculator(int bothCooperate = 2, int betrayed = 6, int betraying = 1, int bothBetray = 4)
        {
            this.BothCooperate = bothCooperate;
            this.Betrayed = betrayed;
            this.Betraying = betraying;
            this.BothBetray = bothBetray;
        }
        
        #region Public Methods and Operators

        public (int, int) CalculatePayoff(Move move1, Move move2)
        {
            if (move1 == Move.Cooperate && move2 == Move.Cooperate)
            {
                return (this.BothCooperate, this.BothCooperate);
            }

            if (move1 == Move.Cooperate && move2 == Move.Betray)
            {
                return (this.Betrayed, this.Betraying);
            }

            if (move1 == Move.Betray && move2 == Move.Cooperate)
            {
                return (this.Betraying, this.Betrayed);
            }

            return (this.BothBetray, this.BothBetray);
        }

        #endregion
    }
}