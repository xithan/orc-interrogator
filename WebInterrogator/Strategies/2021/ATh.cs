#region Copyright (c) OPTANO GmbH 
// ////////////////////////////////////////////////////////////////////////////////
// 
//        OPTANO GmbH Source Code
//        Copyright (c) 2010-2021 OPTANO GmbH
//        ALL RIGHTS RESERVED.
// 
//    The entire contents of this file is protected by German and
//    International Copyright Laws. Unauthorized reproduction,
//    reverse-engineering, and distribution of all or any portion of
//    the code contained in this file is strictly prohibited and may
//    result in severe civil and criminal penalties and will be
//    prosecuted to the maximum extent possible under the law.
// 
//    RESTRICTIONS
// 
//    THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES
//    ARE CONFIDENTIAL AND PROPRIETARY TRADE SECRETS OF
//    OPTANO GMBH.
// 
//    THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED
//    FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE
//    COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE
//    AVAILABLE TO OTHER INDIVIDUALS WITHOUT WRITTEN CONSENT
//    AND PERMISSION FROM OPTANO GMBH.
// 
// ////////////////////////////////////////////////////////////////////////////////
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interrogator.Strategies
{
    using System.Diagnostics;

    using Interrogator.Game;

    public class AThStrategy: IStrategy
    {
        private readonly System.Random _moodCreator;

        private double _moodThreshold;

        private double _currentMoodReaction;

        private const double DefaultMoodReaction = 0.1;

        private const double WorstMood = 0;

        private const double BestMood = 1;

        private const double DefaultThreshold = 0.8;

        private const double ChanceToBecomeEvil = 0.0005;

        private const int MovesToAnalyze = 3;

        private bool _theWorldIsEvilAndSoAmI;

        public AThStrategy()
        {
            this._moodCreator = new System.Random();
            this._currentMoodReaction = AThStrategy.DefaultMoodReaction;
            this._moodThreshold = AThStrategy.DefaultThreshold;
            this._theWorldIsEvilAndSoAmI = false;
        }
        
        /// <inheritdoc />
        public Move GetNextMove(int currentTurn, IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
        {
            this.ReactToOpponentsMoves(previousOwnMoves, previousOpponentMoves);
            var mood = this.GetStrategyMood();
            return mood > this._moodThreshold ? Move.Betray : Move.Cooperate;
        }

        private double GetStrategyMood()
        {
            var random = this._moodCreator.NextDouble();
            if (!this._theWorldIsEvilAndSoAmI && random < AThStrategy.ChanceToBecomeEvil)
            {
                this._theWorldIsEvilAndSoAmI = true;
            }

            return this._theWorldIsEvilAndSoAmI ? AThStrategy.BestMood : random;
        }

        private void ReactToOpponentsMoves(IList<Move> previousOwnMoves, IList<Move> previousOpponentMoves)
        {
            if (previousOpponentMoves.Any())
            {
                var aggregatedMove = this.AggregatePreviousMoves(previousOpponentMoves);
                
                this._moodThreshold += aggregatedMove == Move.Cooperate ? this._currentMoodReaction : -this._currentMoodReaction;
                this._moodThreshold = this._moodThreshold > AThStrategy.BestMood ? AThStrategy.BestMood : this._moodThreshold;
                this._moodThreshold = this._moodThreshold < AThStrategy.WorstMood ? AThStrategy.WorstMood : this._moodThreshold;
            }
        }

        private Move AggregatePreviousMoves(IList<Move> previousOpponentMoves)
        {
            if (previousOpponentMoves.Count < AThStrategy.MovesToAnalyze)
            {
                return previousOpponentMoves.Last();
            }

            var lastMoves = new List<Move>();
            for (var i = 0; i < AThStrategy.MovesToAnalyze - 1; i++)
            {
                lastMoves.Add(previousOpponentMoves[previousOpponentMoves.Count - i - 1]);
            }

            if (lastMoves.All(m => m == Move.Betray)
                || lastMoves.All(m => m == Move.Cooperate))
            {
                this._currentMoodReaction += AThStrategy.DefaultMoodReaction;
            }
            else
            {
                this._currentMoodReaction -= AThStrategy.DefaultMoodReaction;
                if (this._currentMoodReaction < AThStrategy.DefaultMoodReaction)
                {
                    this._currentMoodReaction = AThStrategy.DefaultMoodReaction;
                }
            }

            return lastMoves.Count(m => m == Move.Betray) >= lastMoves.Count(m => m == Move.Cooperate) ? Move.Betray : Move.Cooperate;
        }
    }
}