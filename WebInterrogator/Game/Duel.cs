using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using Interrogator.Game.Setup;

namespace Interrogator.Game
{
    /// <summary>
    /// Interrogation duel with two prisoners
    /// </summary>
    public class Duel
    {
        public Duel(Prisoner p1, Prisoner p2)
        {
            this.Participants = (new Participant(p1), new Participant(p2));
        }
        
        public (Participant, Participant) Participants { get; set; }

        public void Interrogate(int turn, PayoffCalculator payoffCalculator)
        {
            var p1 = Participants.Item1;
            var p2 = Participants.Item2;
            var p1Move = p1.Interrogate(turn, Participants.Item2);
            var p2Move = p2.Interrogate(turn, Participants.Item1);
            p1.Moves.Add(p1Move);
            p2.Moves.Add(p2Move);
            var (p1Points, p2Points) = payoffCalculator.CalculatePayoff(p1Move, p2Move);
            p1.Sentence += p1Points;
            p2.Sentence += p2Points;
        }
    }
    
    
    
}