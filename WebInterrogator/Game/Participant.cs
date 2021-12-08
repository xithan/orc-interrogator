using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Interrogator.Game.Setup;

namespace Interrogator.Game
{
    public class Participant
    {
        public Participant(Prisoner p)
        {
            this.Prisoner = p;
            this.Sentence = 0;
            this.Moves = new List<Move>();
        }

        public void AddSentence(int value)
        {
            this.Sentence += value;
        }
        
        public Prisoner Prisoner { get; }
        
        public int Sentence { get; private set; }
        
        public List<Move> Moves { get; }

        public Move Interrogate(int turn, IList<Move> opponentMoves)
        {
            return this.Prisoner.Strategy.GetNextMove(turn, this.Moves, opponentMoves);
        }
    }
}