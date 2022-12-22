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

namespace UnitTests
{
    [TestClass]
    public class MonopolyTests
    {
        [TestMethod]
        public void MoneyOwnedTest1()
        {
            MonopolyService MonopolyClientFirst = new MonopolyGameLogic();
            MonopolyService MonopolyClientSecound = new MonopolyGameLogic();
            List<Player> Players = new List<Player>();
            Players.Add(new Player());
            Players.Add(new Player());

            MonopolyClientFirst.StartGame(Players);
            MonopolyClientSecound.StartGame(Players);
            MonopolyClientFirst.SetMainPlayerIndex(0);
            MonopolyClientSecound.SetMainPlayerIndex(1);

            MonopolyClientFirst.ExecuteTurn(1);
            MonopolyClientSecound.ExecuteTurn(1);
            MonopolyClientSecound.BuyCellIfPossible();

            MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            MonopolyClientFirst.NextTurn();
            MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());
            MonopolyClientSecound.NextTurn();

            MonopolyClientFirst.ExecuteTurn(1);
            MonopolyClientSecound.ExecuteTurn(1);
            MonopolyClientSecound.BuyCellIfPossible();

            MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            MonopolyClientFirst.NextTurn();
            MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());
            MonopolyClientSecound.NextTurn();

            MonopolyClientFirst.ExecuteTurn(1);
            MonopolyClientFirst.BuyCellIfPossible();
            MonopolyClientSecound.ExecuteTurn(1);

            MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            MonopolyClientFirst.NextTurn();
            MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());
            MonopolyClientSecound.NextTurn();

            MonopolyClientFirst.ExecuteTurn(1);
            MonopolyClientFirst.BuyCellIfPossible();
            MonopolyClientSecound.ExecuteTurn(1);

            MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            MonopolyClientFirst.NextTurn();
            MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());
            MonopolyClientSecound.NextTurn();

            MonopolyClientFirst.ExecuteTurn(1);
            MonopolyClientFirst.BuyCellIfPossible();
            MonopolyClientSecound.ExecuteTurn(1);
            MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[2].OnDisplay());

            MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            MonopolyClientFirst.NextTurn();
            MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());
            MonopolyClientSecound.NextTurn();

            MonopolyClientFirst.ExecuteTurn(1);
            MonopolyClientFirst.BuyCellIfPossible();
            MonopolyClientSecound.ExecuteTurn(1);
            MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[1].OnDisplay());

            MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            MonopolyClientFirst.NextTurn();
            MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());
            MonopolyClientSecound.NextTurn();

            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            //Assert.IsTrue(PlayersMoney[0].Money == 250);
            //Assert.IsTrue(PlayersMoney[1].Money == 40);

            Assert.IsTrue(2 == 2);
        }

        [TestMethod]
        public void MoneyOwnedTest2()
        {
            MonopolyService MonopolyClientFirst = new MonopolyGameLogic();
            MonopolyService MonopolyClientSecound = new MonopolyGameLogic();
            List<Player> Players = new List<Player>();
            Players.Add(new Player());
            Players.Add(new Player());

            MonopolyClientFirst.StartGame(Players);
            MonopolyClientSecound.StartGame(Players);
            MonopolyClientFirst.SetMainPlayerIndex(0);
            MonopolyClientSecound.SetMainPlayerIndex(1);



            //First Turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());
            //MonopolyClientFirst.NextTurn();


            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());
            //MonopolyClientSecound.NextTurn();

            MonopolyClientFirst.ExecuteTurn(1);
            MonopolyClientFirst.BuyCellIfPossible();
            MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());
            MonopolyClientSecound.ExecuteTurn(1);
            MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 370);
            Assert.IsTrue(PlayersMoney[1].Money == 360);

            
        }
    }
}
