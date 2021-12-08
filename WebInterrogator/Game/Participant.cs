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
        
        public Prisoner Prisoner { get; set; }
        
        public int Sentence { get; set; }
        
        public List<Move> Moves { get; set; }

        public Move Interrogate(int turn, Participant other)
        {
            return this.Prisoner.Strategy.GetNextMove(turn, this.Moves, other.Moves);
        }
    }
}