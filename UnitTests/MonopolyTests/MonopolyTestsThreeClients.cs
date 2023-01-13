using Enums.Monopoly;
using Models.Monopoly;
using Models.MultiplayerConnection;
using Org.BouncyCastle.Crypto;
using Services.GamesServices.Monopoly;
using Services.GamesServices.Monopoly.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace UnitTests.MonopolyTests
{
    [TestClass]
    public class MonopolyTestsThreeClients
    {
        private PlayerKey[] BuyingOrder = new PlayerKey[]
        {
            PlayerKey.Secound, PlayerKey.Third, PlayerKey.First, PlayerKey.First, PlayerKey.First,PlayerKey.First, PlayerKey.First,
            PlayerKey.First, PlayerKey.First, PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne
        };

        private List<MonopolyService> Clients;

        [TestInitialize]
        public void TestsSetup()
        {
            Clients = ResetClients();
        }

        private List<MonopolyService> ResetClients()
        {
            List<MonopolyService> Result = new List<MonopolyService>();
            int ClientsNumber = 3;
            for (int i = 0; i < ClientsNumber; i++)
            {
                Result.Add(new MonopolyGameLogic());
            }
            return Result;
        }

        [TestMethod]
        public void PlayersMoneyOwnedAfterTurn1To5Test()
        {
            int TurnsToTest = 5;
            for (int turn = 1; turn <= TurnsToTest; turn++)
            {
                Clients = ResetClients();
                List<MoneyFlow> ClientsMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(turn, ref Clients, BuyingOrder);

                List<PlayerUpdateData> ActualMoney = Clients[0].GetUpdatedData().PlayersData;
                List<int> ExpectedMoney = MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow);
                Assert.IsTrue(MonopolyDataPrepare.CompareMoneyAmount(ExpectedMoney, ActualMoney));
            }
        }

        [TestMethod]
        public void BankrupcyTest1()
        {
            List<MoneyFlow> PlayersMoneyFlow = null;

            for (int i = 1; ; i++)
            {
                Clients = ResetClients();
                PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients, BuyingOrder);

                if (PlayersMoneyFlow[2].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[2].Loss)
                {
                    break;
                }
            }

            MonopolyUpdateMessage CheckBankrupcy = Clients[2].GetUpdatedData();
            
            Assert.IsTrue(CheckBankrupcy.BankruptPlayer == PlayerKey.Third);
        }

        [TestMethod]
        public void BankrupcyTest2()
        {

            List<MonopolyService> Clients = null;
            List<MoneyFlow> PlayersMoneyFlow = null;

            for (int i = 1; ; i++)
            {
                Clients = ResetClients();
                PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients, BuyingOrder);

                if (PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[1].Loss)
                {
                    break;
                }
            }

            MonopolyUpdateMessage CheckBankrupcy = Clients[1].GetUpdatedData();

            Assert.IsTrue(CheckBankrupcy.BankruptPlayer == PlayerKey.Secound);
        }

        [TestMethod]
        public void WhosTurnTest()
        {
            List<MoneyFlow> PlayersMoneyFlow = null;

            Clients = ResetClients();

            MonopolyDataPrepare.PrepareClientsData(ref Clients);

            int i = 0;
            while(true)
            {
                Clients[0].ExecutePlayerMove(1);
                Clients[0].BuyCellIfPossible();

                MonopolyService CurrentClient = Clients[0];
                MonopolyDataPrepare.UpdateOthers(ref Clients, ref CurrentClient);

                Clients[1].ExecutePlayerMove(1);

                CurrentClient = Clients[1];
                MonopolyDataPrepare.UpdateOthers(ref Clients, ref CurrentClient);

                Clients[2].ExecutePlayerMove(0);

                CurrentClient = Clients[2];
                MonopolyDataPrepare.UpdateOthers(ref Clients, ref CurrentClient);

                if (Clients[0].GetUpdatedData().PlayersData.Count == 2)
                    break;
                ++i;
            }

            Clients[1].UpdateData(Clients[1].GetUpdatedData());
            

            Assert.IsTrue(Clients[2].IsYourTurn() == false);
            Assert.IsTrue(Clients[1].IsYourTurn() == false);
            Assert.IsTrue(Clients[0].IsYourTurn() == true);

            Clients[0].NextTurn();
            Clients[2].NextTurn();

            

            Assert.IsTrue(Clients[2].IsYourTurn() == true);
            Assert.IsTrue(Clients[1].IsYourTurn() == false);
            Assert.IsTrue(Clients[0].IsYourTurn() == false);
        }

        [TestMethod]
        public void WinnerTest()
        {
            List<MoneyFlow> PlayersMoneyFlow = null;

            for (int i = 1; ; i++)
            {
                Clients = ResetClients();
                PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients, BuyingOrder);

                if (PlayersMoneyFlow[2].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[2].Loss &&
                    PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[1].Loss)
                {
                    break;
                }
            }

            Clients[1].UpdateData(Clients[1].GetUpdatedData());
            Clients[2].UpdateData(Clients[2].GetUpdatedData());

            Assert.IsTrue(Clients[0].WhoWon() == PlayerKey.First);
            Assert.IsTrue(Clients[1].WhoWon() == PlayerKey.First);
            Assert.IsTrue(Clients[2].WhoWon() == PlayerKey.First);
        }

        
    }
}
  
  