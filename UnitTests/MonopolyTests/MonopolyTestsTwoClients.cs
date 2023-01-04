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
            List<MonopolyService> Clients = null;
            List<MoneyFlow> PlayersMoneyFlow = null;

            for (int i = 1; ; i++)
            {
                Clients = new List<MonopolyService>();
                Clients.Add(new MonopolyGameLogic());
                Clients.Add(new MonopolyGameLogic());
                PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients);

                if (PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[1].Loss)
                {
                    break;
                }
            }

            MonopolyUpdateMessage CheckBankrupcy = Clients[1].GetUpdatedData();

            Assert.IsTrue(CheckBankrupcy.BankruptPlayer == PlayerKey.Secound);
        }

       [TestMethod]
        public void WinnerTest()
        {
            List<MonopolyService> Clients = null;
            List<MoneyFlow> PlayersMoneyFlow = null;

            for (int i = 1; ; i++)
            {
                Clients = new List<MonopolyService>();
                Clients.Add(new MonopolyGameLogic());
                Clients.Add(new MonopolyGameLogic());
                PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients);

                if (PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[1].Loss)
                {
                    break;
                }
            }

            Clients[1].UpdateData(Clients[1].GetUpdatedData());

            Assert.IsTrue(Clients[0].WhoWon() == PlayerKey.First);
            Assert.IsTrue(Clients[1].WhoWon() == PlayerKey.First);
        }
    }
}
