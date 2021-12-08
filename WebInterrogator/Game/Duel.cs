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
        
        public (Participant, Participant) Participants { get; }

        public void Interrogate(int turn, PayoffCalculator payoffCalculator)
        {
            var p1 = this.Participants.Item1;
            var p2 = this.Participants.Item2;
            var p1NextMove = p1.Interrogate(turn, p2.Moves);
            var p2NextMove = p2.Interrogate(turn, p1.Moves);
            
            // Important: Don't update moves before both participants are interrogated
            p1.Moves.Add(p1NextMove);  
            p2.Moves.Add(p2NextMove);
            
            var (p1Sentence, p2Sentence) = payoffCalculator.CalculatePayoff(p1NextMove, p2NextMove);
            p1.AddSentence(p1Sentence);
            p2.AddSentence(p2Sentence);
        }
    }
    
    
    
}