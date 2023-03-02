using Enums.Monopoly;
using Models.Monopoly;
using Models.MultiplayerConnection;
using MySqlX.XDevAPI;
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
            PlayerKey.First, PlayerKey.First, PlayerKey.First, PlayerKey.First, PlayerKey.First,PlayerKey.First,PlayerKey.First,
            PlayerKey.First,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,
            PlayerKey.First,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne

        };

        private PlayerKey[] BuyingOrder_ForWhosTurnTest = new PlayerKey[]
        {
            PlayerKey.Secound, PlayerKey.Third, PlayerKey.First, PlayerKey.First, PlayerKey.First,PlayerKey.First, PlayerKey.First,
            PlayerKey.First, PlayerKey.First, PlayerKey.First, PlayerKey.First, PlayerKey.First,PlayerKey.First,PlayerKey.First,
            PlayerKey.First,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,
            PlayerKey.First,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne

        };

        private PlayerKey[] BuyingOrderFirstWinner = new PlayerKey[]
        {
            PlayerKey.First, PlayerKey.First, PlayerKey.First, PlayerKey.First, PlayerKey.First,PlayerKey.First, PlayerKey.First,
            PlayerKey.First, PlayerKey.First, PlayerKey.First, PlayerKey.First, PlayerKey.First,PlayerKey.First,PlayerKey.First,
            PlayerKey.First, PlayerKey.First, PlayerKey.First, PlayerKey.NoOne, PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne,
            PlayerKey.NoOne,PlayerKey.NoOne,PlayerKey.NoOne

        };

        private List<MonopolyService> Clients;

        [TestInitialize]
        public void TestsSetup()
        {
            ResetClients();
        }

        private void ResetClients()
        {
            Clients = MonopolyDataPrepare.InitClients(3);
        }

        [TestMethod]
        public void PlayersMoneyOwnedAfterTurn1To5Test()
        {
            int TurnsToTest = 5;
            for (int turn = 1; turn <= TurnsToTest; turn++)
            {
                
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
            int g = 0;
            for (int i = 1; ; i++)
            {
                g = i;
                ResetClients();
                PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients, BuyingOrder);

                if (PlayersMoneyFlow[2].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[2].Loss)
                {
                    break;
                }
            }

            MonopolyUpdateMessage CheckBankrupcy = Clients[0].GetUpdatedData();
            
            //Assert.IsTrue(CheckBankrupcy.BankruptPlayer == PlayerKey.Third);
            Assert.IsTrue(CheckBankrupcy.PlayersData.FirstOrDefault(p => p.PlayerIndex == 3) == null);
        }

        [TestMethod]
        public void BankrupcyTest2()
        {
            List<MoneyFlow> PlayersMoneyFlow = null;
            int g = 0;
            for (int i = 1; ; i++)
            {
                g = i;
                ResetClients();
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
        public void WinnerTest()
        {
            List<MoneyFlow> PlayersMoneyFlow = null;
            for (int i = 1; ; i++)
            {


                ResetClients();
                PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients, BuyingOrderFirstWinner);

                if ((PlayersMoneyFlow[2].Income + Consts.Monopoly.StartMoneyAmount) < PlayersMoneyFlow[2].Loss &&
                    (PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount) < PlayersMoneyFlow[1].Loss)
                {
                    break;
                }

            }


            Assert.IsTrue(Clients[0].WhoWon() == PlayerKey.First ||
                Clients[1].WhoWon() == PlayerKey.First ||
                Clients[2].WhoWon() == PlayerKey.First);
            
        }

        
    }
}
  
  