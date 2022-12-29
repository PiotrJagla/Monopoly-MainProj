using Services.GamesServices.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.MultiplayerConnection;
using Enums.Monopoly;

namespace UnitTests.MonopolyTests
{
    [TestClass]
    public class MonopolyClientsNumberIndependentTests
    {
        [TestMethod]
        public void TestBuyingOwnCell()
        {
            MonopolyService Client = new MonopolyGameLogic();
            List<Player> Players = new List<Player>();
            Players.Add(new Player());
            Client.StartGame(Players);
            Client.SetMainPlayerIndex(0);

            Client.ExecuteTurn(1);
            Client.BuyCellIfPossible();

            for (int i = 1; i < Client.GetBoard().Count; i++)
            {
                Client.ExecuteTurn(1);
            }

            Assert.IsTrue(Client.ExecuteTurn(1) == MonopolyTurnResult.CannotBuyCell);
        }

        [TestMethod]
        public void NationMonopolTest()
        {
            MonopolyService Client = new MonopolyGameLogic();
            List<Player> Players = new List<Player>();
            Players.Add(new Player());
            Client.StartGame(Players);
            Client.SetMainPlayerIndex(0);

            int PolandCostsWithoutMonopol = Client.GetBoard()[1].GetCosts().Stay;
            
            for (int i = 1;  ; i++)
            {
                Client.ExecuteTurn(1);
                Client.BuyCellIfPossible();

                if (Client.GetBoard()[i].GetNation() != Client.GetBoard()[i + 1].GetNation())
                    break;
            }
            
            Assert.IsTrue(Client.GetBoard()[1].GetCosts().Stay == PolandCostsWithoutMonopol*2);
        }
    }
}
