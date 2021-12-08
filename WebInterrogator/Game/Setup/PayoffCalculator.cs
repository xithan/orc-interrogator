namespace Interrogator.Game
{
    public class PayoffCalculator
    {
        private readonly int _bothCooperate;
        private readonly int _betrayed;
        private readonly int _betraying;
        private readonly int _bothBetrayed;

        public PayoffCalculator(int bothCooperate = 2, int betrayed = 6, int betraying = 1, int bothBetrayed = 4)
        {
            this._bothCooperate = bothCooperate;
            this._betrayed = betrayed;
            this._betraying = betraying;
            this._bothBetrayed = bothBetrayed;
        }
        
        #region Public Methods and Operators

        public (int, int) CalculatePayoff(Move move1, Move move2)
        {
            if (move1 == Move.Cooperate && move2 == Move.Cooperate)
            {
                return (this._bothCooperate, this._bothCooperate);
            }

            if (move1 == Move.Cooperate && move2 == Move.Betray)
            {
                return (this._betrayed, this._betraying);
            }

            if (move1 == Move.Betray && move2 == Move.Cooperate)
            {
                return (this._betraying, this._betrayed);
            }

            return (this._bothBetrayed, this._bothBetrayed);
        }

        #endregion
    }
}