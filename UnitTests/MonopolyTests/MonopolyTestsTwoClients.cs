using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.GamesServices.Monopoly;
using Services.GamesServices.Monopoly.Board;
using Services.GamesServices.Monopoly.Update;
using Models.Monopoly;
using Models.MultiplayerConnection;
using Enums.Monopoly;

namespace UnitTests.MonopolyTests
{
    [TestClass]
    public class MonopolyTestsTwoClients
    {

        [TestMethod]
        public void MoneyOwnedAfterTurn1Test()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();
            
            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            List<MoneyFlow> PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(1, ref Clients);

            List<PlayerUpdateData> ActualMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(ActualMoney[0].Money == MonopolyDataPrepare.GetExpectedMoney(PlayersMoneyFlow)[0]);
            Assert.IsTrue(ActualMoney[1].Money == MonopolyDataPrepare.GetExpectedMoney(PlayersMoneyFlow)[1]);
        }

        [TestMethod]
        public void MoneyOwnedAfterTurn2Test()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();

            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            List<MoneyFlow> PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(2, ref Clients);

            List<PlayerUpdateData> ActualMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(ActualMoney[0].Money == MonopolyDataPrepare.GetExpectedMoney(PlayersMoneyFlow)[0]);
            Assert.IsTrue(ActualMoney[1].Money == MonopolyDataPrepare.GetExpectedMoney(PlayersMoneyFlow)[1]);
        }

        [TestMethod]
        public void MoneyOwnedAfterTurn3Test()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();

            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            List<MoneyFlow> PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(3, ref Clients);

            List<PlayerUpdateData> ActualMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(ActualMoney[0].Money == MonopolyDataPrepare.GetExpectedMoney(PlayersMoneyFlow)[0]);
            Assert.IsTrue(ActualMoney[1].Money == MonopolyDataPrepare.GetExpectedMoney(PlayersMoneyFlow)[1]);


        }

        [TestMethod]
        public void MoneyOwnedAfterTurn4Test()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();

            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            List<MoneyFlow> PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(4, ref Clients);

            List<PlayerUpdateData> ActualMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(ActualMoney[0].Money == MonopolyDataPrepare.GetExpectedMoney(PlayersMoneyFlow)[0]);
            Assert.IsTrue(ActualMoney[1].Money == MonopolyDataPrepare.GetExpectedMoney(PlayersMoneyFlow)[1]);


        }

        [TestMethod]
        public void MoneyOwnedAfterTurn5Test()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();

            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            List<MoneyFlow> PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(5, ref Clients);

            List<PlayerUpdateData> ActualMoney = FirstClient.GetUpdatedData().PlayersData;

            Assert.IsTrue(ActualMoney[0].Money == MonopolyDataPrepare.GetExpectedMoney(PlayersMoneyFlow)[0]);
            Assert.IsTrue(ActualMoney[1].Money == MonopolyDataPrepare.GetExpectedMoney(PlayersMoneyFlow)[1]);


        }

        [TestMethod]
        public void BankruptTest()
        {
            MonopolyService FirstClient = null;
            MonopolyService SecoundClient = null;
            List<MonopolyService> Clients = null;
            List<MoneyFlow> PlayersMoneyFlow = null;

            for (int i = 1; ; i++)
            {
                FirstClient = new MonopolyGameLogic();
                SecoundClient = new MonopolyGameLogic();

                Clients = new List<MonopolyService>();
                Clients.Add(FirstClient);
                Clients.Add(SecoundClient);
                PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients);

                if(PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[1].Loss)
                    break;
            }

            MonopolyUpdateMessage CheckBankrupcy = SecoundClient.GetUpdatedData();
            Assert.IsTrue(CheckBankrupcy.BankruptPlayer == PlayerKey.Secound);
        }

       // [TestMethod]
        public void WinnerTest()
        {
            MonopolyService FirstClient = new MonopolyGameLogic();
            MonopolyService SecoundClient = new MonopolyGameLogic();

            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            MonopolyDataPrepare.ExecuteTurnsNumber(6, ref Clients);

            SecoundClient.UpdateData(SecoundClient.GetUpdatedData());

            Assert.IsTrue(FirstClient.WhoWon() == PlayerKey.First);
            Assert.IsTrue(SecoundClient.WhoWon() == PlayerKey.First);
        }
    }
}
