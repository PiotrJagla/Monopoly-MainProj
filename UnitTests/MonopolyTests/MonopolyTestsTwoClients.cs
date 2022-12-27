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
            MonopolyService MonopolyClientFirst = new MonopolyGameLogic();
            MonopolyService MonopolyClientSecound = new MonopolyGameLogic();
            
            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(MonopolyClientFirst);
            Clients.Add(MonopolyClientSecound);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(1, ref Clients);

            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 370);
            Assert.IsTrue(PlayersMoney[1].Money == 360);


        }

        [TestMethod]
        public void MoneyOwnedAfterTurn2Test()
        {
            MonopolyService MonopolyClientFirst = new MonopolyGameLogic();
            MonopolyService MonopolyClientSecound = new MonopolyGameLogic();

            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(MonopolyClientFirst);
            Clients.Add(MonopolyClientSecound);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(2, ref Clients);

            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 370);
            Assert.IsTrue(PlayersMoney[1].Money == 230);


        }

        [TestMethod]
        public void MoneyOwnedAfterTurn3Test()
        {
            MonopolyService MonopolyClientFirst = new MonopolyGameLogic();
            MonopolyService MonopolyClientSecound = new MonopolyGameLogic();

            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(MonopolyClientFirst);
            Clients.Add(MonopolyClientSecound);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(3, ref Clients);

            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 370);
            Assert.IsTrue(PlayersMoney[1].Money == 120);


        }

        [TestMethod]
        public void MoneyOwnedAfterTurn4Test()
        {
            MonopolyService MonopolyClientFirst = new MonopolyGameLogic();
            MonopolyService MonopolyClientSecound = new MonopolyGameLogic();

            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(MonopolyClientFirst);
            Clients.Add(MonopolyClientSecound);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(4, ref Clients);

            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 320);
            Assert.IsTrue(PlayersMoney[1].Money == 130);


        }

        [TestMethod]
        public void MoneyOwnedAfterTurn5Test()
        {
            MonopolyService MonopolyClientFirst = new MonopolyGameLogic();
            MonopolyService MonopolyClientSecound = new MonopolyGameLogic();

            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(MonopolyClientFirst);
            Clients.Add(MonopolyClientSecound);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(5, ref Clients);

            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 280);
            Assert.IsTrue(PlayersMoney[1].Money == 120);


        }

        [TestMethod]
        public void BankruptTest()
        {
            MonopolyService MonopolyClientFirst = new MonopolyGameLogic();
            MonopolyService MonopolyClientSecound = new MonopolyGameLogic();

            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(MonopolyClientFirst);
            Clients.Add(MonopolyClientSecound);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(6, ref Clients);

            MonopolyUpdateMessage CheckBankrupcy = MonopolyClientSecound.GetUpdatedData();
            Assert.IsTrue(CheckBankrupcy.BankruptPlayer == PlayerKey.Secound);
        }

        [TestMethod]
        public void WinnerTest()
        {
            MonopolyService MonopolyClientFirst = new MonopolyGameLogic();
            MonopolyService MonopolyClientSecound = new MonopolyGameLogic();

            List<MonopolyService> Clients = new List<MonopolyService>();
            Clients.Add(MonopolyClientFirst);
            Clients.Add(MonopolyClientSecound);
            MonopolyDataPrepareForTests.ExecuteTurnsNumber(6, ref Clients);

            MonopolyClientSecound.UpdateData(MonopolyClientSecound.GetUpdatedData());

            Assert.IsTrue(MonopolyClientFirst.WhoWon() == PlayerKey.First);
            Assert.IsTrue(MonopolyClientSecound.WhoWon() == PlayerKey.First);
        }
    }
}
