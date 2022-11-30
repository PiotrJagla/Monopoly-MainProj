using Models.BlackJack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.BlackJack
{
    public interface BlackJackService
    {
        void StartGame();

        bool UserWon();

        void DrawCard();

        void DealerTurn();

        List<Card> GetUserCards();
        List<Card> GetDealerCards();

        int GetUserPoints();
        int GetDealerPoints();

    }
}
