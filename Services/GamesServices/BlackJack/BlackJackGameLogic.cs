using Models;
using Models.BlackJack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.BlackJack
{
    public class BlackJackGameLogic : BlackJackService
    {
        private static List<Card> DrawnCards = new List<Card>();
        private static List<Card> DealerCards = new List<Card>();

        public BlackJackGameLogic()
        {
            
        }

        public void DrawCard()
        {
            DrawnCards.Add(new Card());
        }

        public List<Card> GetDealerCards()
        {
            return DealerCards;
        }

        public List<Card> GetUserCards()
        {
            return DrawnCards;
        }

        public int GetUserPoints()
        {
            int result = 0;

            foreach (Card card in DrawnCards)
                result += (int)card.type + 2;

            return result;
        }

        public int GetDealerPoints()
        {
            int result = 0;

            foreach (Card card in DealerCards)
                result += (int)card.type + 2;

            return result;
        }

        public void StartGame()
        {
            DrawnCards.Clear();
            DealerCards.Clear();
        }

        public bool UserWon()
        {
            if (GetUserPoints() > Constants.BlackJackMaxPoints)
                return false;

            if (GetDealerPoints() > Constants.BlackJackMaxPoints)
                return true;

            if (GetUserPoints() > GetDealerPoints())
                return true;

            return false;
        }

        public void DealerTurn()
        {
            DealerCardsDraw();
        }

        private void DealerCardsDraw()
        {
            while(true)
            {
                DealerCards.Add(new Card());
                if (GetDealerPoints() >= Constants.BlackJackMinPoints)
                    return;
            }
        }
    }
}
