using Enums.Monopoly;
using Models.Monopoly;
using Models.MultiplayerConnection;
using Org.BouncyCastle.Crypto;
using Services.GamesServices.Monopoly;
using Services.GamesServices.Monopoly.Update;
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

            Assert.IsTrue(PlayersMoney[0].Money == 400);
            Assert.IsTrue(PlayersMoney[1].Money == 370);
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

            Assert.IsTrue(PlayersMoney[0].Money == 400);
            Assert.IsTrue(PlayersMoney[1].Money == 370);
            Assert.IsTrue(PlayersMoney[2].Money == 230);
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

            Assert.IsTrue(PlayersMoney[0].Money == 390);
            Assert.IsTrue(PlayersMoney[1].Money == 320);
            Assert.IsTrue(PlayersMoney[2].Money == 180);
        }

        [TestMethod]
        public void MoneyOwnedAfterTurn4()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            MonopolyService ThirdClient = new MonopolyGameLogic();


            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(4, ref Clients);

            List<PlayerUpdateData> PlayersMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 440);
            Assert.IsTrue(PlayersMoney[1].Money == 220);
            Assert.IsTrue(PlayersMoney[2].Money == 80);
        }

        [TestMethod]
        public void MoneyOwnedAfterTurn5()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            MonopolyService ThirdClient = new MonopolyGameLogic();


            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(5, ref Clients);

            List<PlayerUpdateData> PlayersMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 540);
            Assert.IsTrue(PlayersMoney[1].Money == 80);
            Assert.IsTrue(PlayersMoney[2].Money == 70);
        }

        [TestMethod]
        public void BankrupcyTest1()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            MonopolyService ThirdClient = new MonopolyGameLogic();


            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(5, ref Clients);

            MonopolyUpdateMessage CheckSecound = SecoundClient.GetUpdatedData();
            MonopolyUpdateMessage CheckThird = ThirdClient.GetUpdatedData();

            Assert.IsTrue(CheckSecound.BankruptPlayer == PlayerKey.Secound);
            Assert.IsTrue(CheckThird.BankruptPlayer == PlayerKey.Third);
        }

        [TestMethod]
        public void WhosTurnTest()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            MonopolyService ThirdClient = new MonopolyGameLogic();


            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(5, ref Clients);

            FirstClient.NextTurn();
            ThirdClient.NextTurn();

            Assert.IsTrue(ThirdClient.IsYourTurn() == true);
        }

        [TestMethod]
        public void WinnerTest()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            MonopolyService ThirdClient = new MonopolyGameLogic();


            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(5, ref Clients);

            PlayerKey Winner = FirstClient.WhoWon();

            Assert.IsTrue(Winner == PlayerKey.First);
        }

        
    }
}
