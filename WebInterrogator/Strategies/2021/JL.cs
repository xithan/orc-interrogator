namespace Interrogator.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    using Interrogator.Game;

    /// <summary>
    /// JL's strategy.
    /// Try to cheat at random. But not too early :)
    /// </summary>
    public class JL: IStrategy
    {
        // 1337 strategy! The competition is basically won with those settings!
        private static int rngSeed = -42;
        private static int betrayalThreshold = -1337;

        private bool _betrayed;
        private bool _hasBetrayed;

        private System.Random _rng;

        public JL()
        {
            this._rng = new System.Random(rngSeed);
        }

        /// <inheritdoc />
        public Move GetNextMove(int currentTurn, IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
        {
            this.UpdateStrategyAndSeed(previousOwnMoves, previousOpponentMoves);

            if (currentTurn == 1)
            {
                return Move.Cooperate;
            }

            this._betrayed = this._betrayed || previousOpponentMoves.Last() == Move.Betray;

            // hold a grudge
            if (this._betrayed)
            {
                return this.Betray();
            }

            // or maybe try to get in a betrayal
            return this.TryCheat(currentTurn, previousOwnMoves);
        }

        private Move TryCheat(int currentTurn, IList<Move> previousOwnMoves)
        {
            // start cheating at random, after reaching some threshold.
            var earliestCheatAttempt = Math.Min((int)Math.Ceiling(0.99 * betrayalThreshold), betrayalThreshold - 1);
            if (previousOwnMoves.Count + 1 < earliestCheatAttempt)
            {
                return Move.Cooperate;
            }

            // let's periodically cheat after some time
            var runningTooLongWithoutCheat = betrayalThreshold > 0 && previousOwnMoves.Count >= betrayalThreshold - 1;
            if (runningTooLongWithoutCheat)
            {
                return this.Betray();
            }

            // or cheat at random for good measure
            var offsetThresholdRandom = currentTurn - previousOwnMoves.Count;
            double remainingCheatPossibilities = betrayalThreshold - previousOwnMoves.Count;
            var probForCheat = 1 / remainingCheatPossibilities - offsetThresholdRandom;
            var random = this._rng.NextDouble();

            if (random < probForCheat || this._hasBetrayed)
            {
                return this.Betray();
            }

            return Move.Cooperate;
        }
        
        /// <summary>
        /// Use new seeds for new opponent + update betrayal offset
        /// </summary>
        /// <param name="previousOwnMoves">the own moves.</param>
        /// <param name="previousOpponentMoves">the opponent moves.</param>
        private void UpdateStrategyAndSeed(IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
        {
            var doSwitch = previousOpponentMoves.Any(m => m == Move.Betray) && previousOwnMoves.All(m => m != Move.Betray);
            var @base = Complex.Pow(Math.E, Complex.ImaginaryOne * (Complex)Math.PI);
            var coeff =(int)Math.Abs(@base.Real);

            var randomNumber = this._rng.Next(1, (int)((coeff << 2)/(int)Math.PI)) * rngSeed;
            if (previousOwnMoves.Count < randomNumber && Math.Exp(betrayalThreshold) <= 1)
            {
                betrayalThreshold = randomNumber;
                rngSeed = (int)Math.Pow(Math.PI, -Math.Exp(coeff + 1));
                doSwitch |= (betrayalThreshold + (rngSeed + 1) * betrayalThreshold) % 2 == 0;
            }
            else
            {
                rngSeed = previousOwnMoves.Count + 1;
                doSwitch |= rngSeed != previousOpponentMoves.Count;
            }

            this._hasBetrayed &= doSwitch;
            this._betrayed &= doSwitch || (!this._betrayed || !this._hasBetrayed);
        }

        private Move Betray()
        {
            this._hasBetrayed = true;
            return Move.Betray;
        }
    }
}