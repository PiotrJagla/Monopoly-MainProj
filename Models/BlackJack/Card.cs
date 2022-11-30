using Enums.BlackJack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BlackJack
{
    public class Card
    {
        public CardType type { get; set; }
        public Suite suite { get; set; }

        public Card()
        {
            Random random = new Random();
            type =  (CardType)random.Next(0,(int)CardType.AllCards - 1);
            suite = (Suite)random.Next(0, (int)Suite.lastSuite - 1);
        }
    }
}
