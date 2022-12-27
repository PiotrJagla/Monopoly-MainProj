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

            Client.ExecuteTurn(1);
            Client.ExecuteTurn(1);
            Client.ExecuteTurn(1);
            Client.ExecuteTurn(1);
            Client.ExecuteTurn(1);
            Client.ExecuteTurn(1);
            Client.ExecuteTurn(1);


            Assert.IsTrue(Client.ExecuteTurn(1) == MonopolyTurnResult.CannotBuyCell);
                      

        }
    }
}
