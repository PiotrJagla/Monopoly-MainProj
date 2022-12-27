using Models.Monopoly;
using Models.MultiplayerConnection;
using Org.BouncyCastle.Crypto;
using Services.GamesServices.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MonopolyTests
{
    [TestClass]
    public class MonopolyTestsThreeClients
    {
        [TestMethod]
        public void MoneyOwnedAfterTurn1()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            MonopolyService ThirdClient = new MonopolyGameLogic();


            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(1, ref Clients);

            List<PlayerUpdateData> PlayersMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 410);
            Assert.IsTrue(PlayersMoney[1].Money == 360);
            Assert.IsTrue(PlayersMoney[2].Money == 360);
        }

        [TestMethod]
        public void MoneyOwnedAfterTurn2()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            MonopolyService ThirdClient = new MonopolyGameLogic();


            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(2, ref Clients);

            List<PlayerUpdateData> PlayersMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 410);
            Assert.IsTrue(PlayersMoney[1].Money == 300);
            Assert.IsTrue(PlayersMoney[2].Money == 290);
        }

        [TestMethod]
        public void MoneyOwnedAfterTurn3()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            MonopolyService ThirdClient = new MonopolyGameLogic();


            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(3, ref Clients);

            List<PlayerUpdateData> PlayersMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 410);
            Assert.IsTrue(PlayersMoney[1].Money == 240);
            Assert.IsTrue(PlayersMoney[2].Money == 240);
        }

        
    }
}
