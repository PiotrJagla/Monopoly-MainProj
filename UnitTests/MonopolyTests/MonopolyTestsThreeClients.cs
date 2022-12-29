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
        public void PlayersMoneyOwnedAfterTurn1To5Test()
        {
            int TurnsToTest = 5;
            for (int turn = 1; turn <= TurnsToTest; turn++)
            {
                List<MonopolyService> Clients = new List<MonopolyService>();
                Clients.Add(new MonopolyGameLogic());
                Clients.Add(new MonopolyGameLogic());
                Clients.Add(new MonopolyGameLogic());
                List<MoneyFlow> ClientsMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(turn, ref Clients);

                List<PlayerUpdateData> ActualMoney = Clients[0].GetUpdatedData().PlayersData;
                List<int> ExpectedMoney = MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow);
                Assert.IsTrue(MonopolyDataPrepare.CompareMoneyAmount(ExpectedMoney, ActualMoney));
            }
        }

        [TestMethod]
        public void BankrupcyTest1()
        {

            MonopolyService FirstClient = null;
            MonopolyService SecoundClient = null;
            MonopolyService ThirdClient = null;
            List<MonopolyService> Clients = null;
            List<MoneyFlow> PlayersMoneyFlow = null;

            for (int i = 1; ; i++)
            {
                FirstClient = new MonopolyGameLogic();
                SecoundClient = new MonopolyGameLogic();
                ThirdClient = new MonopolyGameLogic();

                Clients = new List<MonopolyService>();
                Clients.Add(FirstClient);
                Clients.Add(SecoundClient);
                Clients.Add(ThirdClient);
                PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients);

                if (PlayersMoneyFlow[2].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[2].Loss)
                {
                    break;
                }
            }

            MonopolyUpdateMessage CheckBankrupcy = ThirdClient.GetUpdatedData();
            
            Assert.IsTrue(CheckBankrupcy.BankruptPlayer == PlayerKey.Third);
        }

        [TestMethod]
        public void BankrupcyTest2()
        {

            MonopolyService FirstClient = null;
            MonopolyService SecoundClient = null;
            MonopolyService ThirdClient = null;
            List<MonopolyService> Clients = null;
            List<MoneyFlow> PlayersMoneyFlow = null;

            for (int i = 1; ; i++)
            {
                FirstClient = new MonopolyGameLogic();
                SecoundClient = new MonopolyGameLogic();
                ThirdClient = new MonopolyGameLogic();

                Clients = new List<MonopolyService>();
                Clients.Add(FirstClient);
                Clients.Add(SecoundClient);
                Clients.Add(ThirdClient);
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
        public void WhosTurnTest()
        {
            MonopolyService FirstClient = null;
            MonopolyService SecoundClient = null;
            MonopolyService ThirdClient = null;
            List<MonopolyService> Clients = null;
            List<MoneyFlow> PlayersMoneyFlow = null;

            FirstClient = new MonopolyGameLogic();
            SecoundClient = new MonopolyGameLogic();
            ThirdClient = new MonopolyGameLogic();

            Clients = new List<MonopolyService>();
            Clients.Add(FirstClient);
            Clients.Add(SecoundClient);
            Clients.Add(ThirdClient);

            MonopolyDataPrepare.PrepareClientsData(ref Clients);

            while(true)
            {
                Clients[0].ExecuteTurn(1);
                Clients[0].BuyCellIfPossible();

                MonopolyService CurrentClient = Clients[0];
                MonopolyDataPrepare.UpdateOthers(ref Clients, ref CurrentClient);

                Clients[1].ExecuteTurn(1);

                CurrentClient = Clients[1];
                MonopolyDataPrepare.UpdateOthers(ref Clients, ref CurrentClient);

                if (Clients[0].GetUpdatedData().PlayersData.Count == 2)
                    break;

            }
            Clients[1].UpdateData(Clients[1].GetUpdatedData());

            Assert.IsTrue(Clients[2].IsYourTurn() == true);
            Assert.IsTrue(Clients[1].IsYourTurn() == false);
            Assert.IsTrue(Clients[0].IsYourTurn() == false);

            Clients[0].NextTurn();
            Clients[2].NextTurn();

            

            Assert.IsTrue(Clients[2].IsYourTurn() == false);
            Assert.IsTrue(Clients[1].IsYourTurn() == false);
            Assert.IsTrue(Clients[0].IsYourTurn() == true);
        }

        [TestMethod]
        public void WinnerTest()
        {
            MonopolyService FirstClient = null;
            MonopolyService SecoundClient = null;
            MonopolyService ThirdClient = null;
            List<MonopolyService> Clients = null;
            List<MoneyFlow> PlayersMoneyFlow = null;

            for (int i = 1; ; i++)
            {
                FirstClient = new MonopolyGameLogic();
                SecoundClient = new MonopolyGameLogic();
                ThirdClient = new MonopolyGameLogic();

                Clients = new List<MonopolyService>();
                Clients.Add(FirstClient);
                Clients.Add(SecoundClient);
                Clients.Add(ThirdClient);
                PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients);

                if (PlayersMoneyFlow[2].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[2].Loss &&
                    PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[1].Loss)
                {
                    break;
                }
            }

            SecoundClient.UpdateData(SecoundClient.GetUpdatedData());
            ThirdClient.UpdateData(ThirdClient.GetUpdatedData());

            Assert.IsTrue(FirstClient.WhoWon() == PlayerKey.First);
            Assert.IsTrue(SecoundClient.WhoWon() == PlayerKey.First);
            Assert.IsTrue(ThirdClient.WhoWon() == PlayerKey.First);
        }

        
    }
}
  
  