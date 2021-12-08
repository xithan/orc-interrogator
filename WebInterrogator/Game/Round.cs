using System.Collections.Generic;

namespace Interrogator.Game
{
    public class Round
    {
        public Round(int number)
        {
            this.Duels = new List<Duel>();
            this.Number = number;
        }
        
        public int Number { get;  }
        
        public IList<Duel> Duels { get; set; }

    }
}