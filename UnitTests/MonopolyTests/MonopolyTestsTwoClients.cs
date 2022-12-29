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
        public void PlayersMoneyOwnedAfter1To5TurnsTest()
        {
            int TurnsToTest = 5;
            for (int turn = 1; turn <= TurnsToTest; turn++)
            {
                List<MonopolyService> Clients = new List<MonopolyService>();
                Clients.Add(new MonopolyGameLogic());
                Clients.Add(new MonopolyGameLogic());
                List<MoneyFlow> ClientsMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(TurnsToTest, ref Clients);

                List<PlayerUpdateData> ActualMoneyFirst = Clients[0].GetUpdatedData().PlayersData;
                List<int> ExpectedMoney = MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow);
                Assert.IsTrue(MonopolyDataPrepare.CompareMoneyAmount(ExpectedMoney, ActualMoneyFirst));
            }
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

                if (PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[1].Loss)
                {
                    break;
                }
            }

            MonopolyUpdateMessage CheckBankrupcy = SecoundClient.GetUpdatedData();

            Assert.IsTrue(CheckBankrupcy.BankruptPlayer == PlayerKey.Secound);
        }

       [TestMethod]
        public void WinnerTest()
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

                if (PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[1].Loss)
                {
                    break;
                }
            }

            SecoundClient.UpdateData(SecoundClient.GetUpdatedData());

            Assert.IsTrue(FirstClient.WhoWon() == PlayerKey.First);
            Assert.IsTrue(SecoundClient.WhoWon() == PlayerKey.First);
        }
    }
}
