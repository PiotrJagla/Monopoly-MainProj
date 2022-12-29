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
            List<MoneyFlow> ClientsMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(1, ref Clients);

            List<PlayerUpdateData> ActualMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(ActualMoney[0].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[0]);
            Assert.IsTrue(ActualMoney[1].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[1]);
            Assert.IsTrue(ActualMoney[2].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[2]);
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
            List<MoneyFlow> ClientsMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(2, ref Clients);

            List<PlayerUpdateData> ActualMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(ActualMoney[0].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[0]);
            Assert.IsTrue(ActualMoney[1].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[1]);
            Assert.IsTrue(ActualMoney[2].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[2]);
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
            List<MoneyFlow> ClientsMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(3, ref Clients);

            List<PlayerUpdateData> ActualMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(ActualMoney[0].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[0]);
            Assert.IsTrue(ActualMoney[1].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[1]);
            Assert.IsTrue(ActualMoney[2].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[2]);
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
            List<MoneyFlow> ClientsMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(4, ref Clients);

            List<PlayerUpdateData> ActualMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(ActualMoney[0].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[0]);
            Assert.IsTrue(ActualMoney[1].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[1]);
            Assert.IsTrue(ActualMoney[2].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[2]);
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
            List<MoneyFlow> ClientsMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(5, ref Clients);

            List<PlayerUpdateData> ActualMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(ActualMoney[0].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[0]);
            Assert.IsTrue(ActualMoney[1].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[1]);
            Assert.IsTrue(ActualMoney[2].Money == MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow)[2]);
        }

        //[TestMethod]
        public void BankrupcyTest1()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            MonopolyService ThirdClient = new MonopolyGameLogic();


            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);
            MonopolyDataPrepare.ExecuteTurnsNumber(6, ref Clients);

            MonopolyUpdateMessage CheckSecound = SecoundClient.GetUpdatedData();
            MonopolyUpdateMessage CheckThird = ThirdClient.GetUpdatedData();

            Assert.IsTrue(CheckSecound.BankruptPlayer == PlayerKey.Secound);
            Assert.IsTrue(CheckThird.BankruptPlayer == PlayerKey.Third);
        }

        //[TestMethod]
        public void WhosTurnTest()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            MonopolyService ThirdClient = new MonopolyGameLogic();


            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);
            MonopolyDataPrepare.ExecuteTurnsNumber(6, ref Clients);

            FirstClient.NextTurn();
            ThirdClient.NextTurn();

            Assert.IsTrue(ThirdClient.IsYourTurn() == true);
        }

        //[TestMethod]
        public void WinnerTest()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            MonopolyService ThirdClient = new MonopolyGameLogic();


            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);
            MonopolyDataPrepare.ExecuteTurnsNumber(6, ref Clients);

            PlayerKey Winner = FirstClient.WhoWon();

            Assert.IsTrue(Winner == PlayerKey.First);
        }

        
    }
}
  
  