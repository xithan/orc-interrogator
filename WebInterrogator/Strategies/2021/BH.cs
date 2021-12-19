namespace Interrogator.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using Interrogator.Game;

    public class BH : IStrategy
    {
        #region Fields

        private readonly OpponentModel _opponentModel = new();

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public Move GetNextMove(int currentTurn, IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
        {
            this._opponentModel.Update(previousOwnMoves, previousOpponentMoves);

            var betrayalRisk = this._opponentModel.EstimateBetrayalRisk(previousOwnMoves, previousOpponentMoves);
            return BH.GetNextMoveDependingOnIterationAndBetrayalRisk(currentTurn, betrayalRisk);
        }

        #endregion

        #region Methods

        private static Move GetNextMoveDependingOnIterationAndBetrayalRisk(int currentTurn, double betrayalRisk)
        {
            if (currentTurn > 10 && betrayalRisk is > 1d / 3 and < 2d / 3)
            {
                return Move.Betray;
            }

            var expectedYearsForBetray = BH.CalculateExpectedYearsInJail(Move.Betray, betrayalRisk);
            var expectedYearsForCooperate = BH.CalculateExpectedYearsInJail(Move.Cooperate, betrayalRisk);

            return expectedYearsForBetray < expectedYearsForCooperate ? Move.Betray : Move.Cooperate;
        }

        private static double CalculateExpectedYearsInJail(Move ownMove, double betrayalRisk)
        {
            if (ownMove == Move.Betray)
            {
                return betrayalRisk * 4 + (1 - betrayalRisk) * 6;
            }
            else
            {
                return betrayalRisk * 6 + (1 - betrayalRisk) * 2;
            }
        }

        #endregion

        private class OpponentModel
        {
            #region Fields

            private readonly Dictionary<(Move, Move), OpponentMoveDistribution> _moveDistributionsByPreviousPlay = new();

            private readonly Dictionary<Move, OpponentMoveDistribution> _moveDistributionsByPreviousOwnMove = new();

            private readonly Dictionary<Move, OpponentMoveDistribution> _moveDistributionsByPreviousOpponentMove = new();

            private readonly OpponentMoveDistribution _totalMoveDistribution = new();

            #endregion

            #region Public Methods and Operators

            public void Update(IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
            {
                if (!previousOpponentMoves.Any())
                {
                    return;
                }

                var lastOpponentMove = previousOpponentMoves.Last();
                this._totalMoveDistribution.Update(lastOpponentMove);

                if (previousOpponentMoves.Count >= 2 && previousOwnMoves.Count >= 2)
                {
                    var secondToLastPlay = OpponentModel.GetSecondToLastPlay(previousOwnMoves, previousOpponentMoves);
                    var (secondToLastOwnMove, secondToLastOpponentMove) = secondToLastPlay;

                    OpponentModel.UpdateDictionary(this._moveDistributionsByPreviousPlay, secondToLastPlay, lastOpponentMove);
                    OpponentModel.UpdateDictionary(this._moveDistributionsByPreviousOwnMove, secondToLastOwnMove, lastOpponentMove);
                    OpponentModel.UpdateDictionary(this._moveDistributionsByPreviousOpponentMove, secondToLastOpponentMove, lastOpponentMove);
                }
            }

            public double EstimateBetrayalRisk(IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
            {
                if (previousOpponentMoves.Count < 2)
                {
                    return this._totalMoveDistribution.GetBetrayalRisk();
                }

                var secondToLastPlay = OpponentModel.GetSecondToLastPlay(previousOwnMoves, previousOpponentMoves);
                if (this._moveDistributionsByPreviousPlay.TryGetValue(secondToLastPlay, out var opponentMoveDistribution))
                {
                    return opponentMoveDistribution.GetBetrayalRisk();
                }

                var (secondToLastOwnMove, secondToLastOpponentMove) = secondToLastPlay;
                var moveDistributionByPreviousOwnMove = this._moveDistributionsByPreviousOwnMove.GetValueOrDefault(secondToLastOwnMove);
                var moveDistributionByPreviousOpponentMove =
                    this._moveDistributionsByPreviousOpponentMove.GetValueOrDefault(secondToLastOpponentMove);

                return this.EstimateBetrayalRisk(moveDistributionByPreviousOwnMove, moveDistributionByPreviousOpponentMove);
            }

            #endregion

            #region Methods

            private static void UpdateDictionary<T>(IDictionary<T, OpponentMoveDistribution> dictionary, T key, Move opponentMove)
            {
                if (!dictionary.TryGetValue(key, out var distribution))
                {
                    distribution = new OpponentMoveDistribution();
                    dictionary[key] = distribution;
                }

                distribution.Update(opponentMove);
            }

            private double EstimateBetrayalRisk(
                OpponentMoveDistribution moveDistributionByPreviousOwnMove,
                OpponentMoveDistribution moveDistributionByPreviousOpponentMove)
            {
                if (moveDistributionByPreviousOwnMove == null && moveDistributionByPreviousOpponentMove == null)
                {
                    return this._totalMoveDistribution.GetBetrayalRisk();
                }

                if (moveDistributionByPreviousOwnMove == null)
                {
                    return moveDistributionByPreviousOpponentMove.GetBetrayalRisk();
                }

                return moveDistributionByPreviousOwnMove.GetMergedBetrayalRisk(moveDistributionByPreviousOpponentMove);
            }

            private static (Move, Move) GetSecondToLastPlay(IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
            {
                var secondToLastOwnMove = previousOwnMoves[^2];
                var secondToLastOpponentMove = previousOpponentMoves[^2];

                return (secondToLastOwnMove, secondToLastOpponentMove);
            }

            #endregion
        }

        private class OpponentMoveDistribution
        {
            #region Constants

            private const double InitialBetrayalRisk = 0.5;

            #endregion

            #region Fields

            private int _numberBetrayals;

            private int _totalNumber;

            #endregion

            #region Public Methods and Operators

            public void Update(Move opponentMove)
            {
                this._totalNumber++;
                if (opponentMove == Move.Betray)
                {
                    this._numberBetrayals++;
                }
            }

            public double GetBetrayalRisk()
            {
                return OpponentMoveDistribution.GetBetrayalRisk(this._numberBetrayals, this._totalNumber);
            }

            public double GetMergedBetrayalRisk(OpponentMoveDistribution otherDistribution)
            {
                return OpponentMoveDistribution.GetBetrayalRisk(
                    this._numberBetrayals + (otherDistribution?._numberBetrayals ?? 0),
                    this._totalNumber + (otherDistribution?._totalNumber ?? 0));
            }

            #endregion

            #region Methods

            private static double GetBetrayalRisk(int betrayals, int tries)
            {
                return tries == 0 ? OpponentMoveDistribution.InitialBetrayalRisk : ((double)betrayals / tries);
            }

            #endregion
        }
    }
}