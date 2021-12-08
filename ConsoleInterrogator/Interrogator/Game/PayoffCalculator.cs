namespace Interrogator.Game
{
    public static class PayoffCalculator
    {
        #region Static Fields

        public static readonly int BothCooperate = 2;

        public static readonly int BeingBetrayed = 6;

        public static readonly int Betraying = 1;

        public static readonly int BothBetray = 4;

        #endregion

        #region Public Methods and Operators

        public static (int, int) CalculatePayoff(Move move1, Move move2)
        {
            if (move1 == Move.Cooperate && move2 == Move.Cooperate)
            {
                return (PayoffCalculator.BothCooperate, PayoffCalculator.BothCooperate);
            }

            if (move1 == Move.Cooperate && move2 == Move.Betray)
            {
                return (PayoffCalculator.BeingBetrayed, PayoffCalculator.Betraying);
            }

            if (move1 == Move.Betray && move2 == Move.Cooperate)
            {
                return (PayoffCalculator.Betraying, PayoffCalculator.BeingBetrayed);
            }

            return (PayoffCalculator.BothBetray, PayoffCalculator.BothBetray);
        }

        #endregion
    }
}