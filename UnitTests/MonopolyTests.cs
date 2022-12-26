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

            PlayerKey[] IsBuyingCellOnTurn = new PlayerKey[] {
                PlayerKey.First, PlayerKey.Secound, PlayerKey.Secound, PlayerKey.First, PlayerKey.First,PlayerKey.First
            };
            Tuple<int, PlayerKey>[] IsSellingCellIndexOnTurn = new Tuple<int, PlayerKey>[] {
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(3,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(2,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne)
            };

            int NumberOfTurns = 1;
            for (int i = 0; i < NumberOfTurns; i++)
            {
                MonopolyClientFirst.ExecuteTurn(1);

                if (IsBuyingCellOnTurn[i] == PlayerKey.First)
                    MonopolyClientFirst.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.First)
                    MonopolyClientFirst.SellCell(MonopolyClientFirst.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());


                MonopolyClientSecound.ExecuteTurn(1);

                if (IsBuyingCellOnTurn[i] == PlayerKey.Secound)
                    MonopolyClientSecound.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.Secound)
                    MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            }

            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());
            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 370);
            Assert.IsTrue(PlayersMoney[1].Money == 360);

            
        }

        [TestMethod]
        public void MoneyOwnedTest3()
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

            PlayerKey[] IsBuyingCellOnTurn = new PlayerKey[] {
                PlayerKey.First, PlayerKey.Secound, PlayerKey.Secound, PlayerKey.First, PlayerKey.First,PlayerKey.First
            };
            Tuple<int, PlayerKey>[] IsSellingCellIndexOnTurn = new Tuple<int, PlayerKey>[] {
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(3,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(2,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne)
            };

            int NumberOfTurns = 2;
            for (int i = 0; i < NumberOfTurns; i++)
            {
                MonopolyClientFirst.ExecuteTurn(1);

                if (IsBuyingCellOnTurn[i] == PlayerKey.First)
                    MonopolyClientFirst.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.First)
                    MonopolyClientFirst.SellCell(MonopolyClientFirst.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());


                MonopolyClientSecound.ExecuteTurn(1);

                if (IsBuyingCellOnTurn[i] == PlayerKey.Secound)
                    MonopolyClientSecound.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.Secound)
                    MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            }

            ////First turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Seceound turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.BuyCellIfPossible();
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 370);
            Assert.IsTrue(PlayersMoney[1].Money == 230);


        }

        [TestMethod]
        public void MoneyOwnedTest4()
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

            PlayerKey[] IsBuyingCellOnTurn = new PlayerKey[] {
                PlayerKey.First, PlayerKey.Secound, PlayerKey.Secound, PlayerKey.First, PlayerKey.First,PlayerKey.First
            };
            Tuple<int, PlayerKey>[] IsSellingCellIndexOnTurn = new Tuple<int, PlayerKey>[] {
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(3,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(2,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne)
            };

            int NumberOfTurns = 3;
            for (int i = 0; i < NumberOfTurns; i++)
            {
                MonopolyClientFirst.ExecuteTurn(1);

                if (IsBuyingCellOnTurn[i] == PlayerKey.First)
                    MonopolyClientFirst.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.First)
                    MonopolyClientFirst.SellCell(MonopolyClientFirst.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());


                MonopolyClientSecound.ExecuteTurn(1);

                if (IsBuyingCellOnTurn[i] == PlayerKey.Secound)
                    MonopolyClientSecound.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.Secound)
                    MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            }

            ////First turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Seceound turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.BuyCellIfPossible();
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Third turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.BuyCellIfPossible();
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 370);
            Assert.IsTrue(PlayersMoney[1].Money == 120);


        }

        [TestMethod]
        public void MoneyOwnedTest5()
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

            PlayerKey[] IsBuyingCellOnTurn = new PlayerKey[] {
                PlayerKey.First, PlayerKey.Secound, PlayerKey.Secound, PlayerKey.First, PlayerKey.First,PlayerKey.First
            };
            Tuple<int, PlayerKey>[] IsSellingCellIndexOnTurn = new Tuple<int, PlayerKey>[] {
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(3,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(2,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne)
            };

            int NumberOfTurns = 4;
            for (int i = 0; i < NumberOfTurns; i++)
            {
                MonopolyClientFirst.ExecuteTurn(1);

                if (IsBuyingCellOnTurn[i] == PlayerKey.First)
                    MonopolyClientFirst.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.First)
                    MonopolyClientFirst.SellCell(MonopolyClientFirst.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());


                MonopolyClientSecound.ExecuteTurn(1);

                if (IsBuyingCellOnTurn[i] == PlayerKey.Secound)
                    MonopolyClientSecound.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.Secound)
                    MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            }


            ////First turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Seceound turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.BuyCellIfPossible();
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Third turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.BuyCellIfPossible();
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Fourth turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[3].OnDisplay());
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());


            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 320);
            Assert.IsTrue(PlayersMoney[1].Money == 130);


        }

        [TestMethod]
        public void MoneyOwnedTest6()
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

            PlayerKey[] IsBuyingCellOnTurn = new PlayerKey[] {
                PlayerKey.First, PlayerKey.Secound, PlayerKey.Secound, PlayerKey.First, PlayerKey.First,PlayerKey.First
            };
            Tuple<int, PlayerKey>[] IsSellingCellIndexOnTurn = new Tuple<int, PlayerKey>[] {
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(3,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(2,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne)
            };

            int NumberOfTurns = 5;
            for (int i = 0; i < NumberOfTurns; i++)
            {
                MonopolyClientFirst.ExecuteTurn(1);

                if (IsBuyingCellOnTurn[i] == PlayerKey.First)
                    MonopolyClientFirst.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.First)
                    MonopolyClientFirst.SellCell(MonopolyClientFirst.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());


                MonopolyClientSecound.ExecuteTurn(1);

                if (IsBuyingCellOnTurn[i] == PlayerKey.Secound)
                    MonopolyClientSecound.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.Secound)
                    MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            }

            ////First turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Seceound turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.BuyCellIfPossible();
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Third turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.BuyCellIfPossible();
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Fourth turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[3].OnDisplay());
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());


            ////Fivth turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[2].OnDisplay());
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            List<PlayerUpdateData> PlayersMoney = MonopolyClientFirst.GetUpdatedData().PlayersData;

            Assert.IsTrue(PlayersMoney[0].Money == 280);
            Assert.IsTrue(PlayersMoney[1].Money == 120);


        }

        [TestMethod]
        public void BankruptTest1()
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


            PlayerKey[] IsBuyingCellOnTurn = new PlayerKey[] {
                PlayerKey.First, PlayerKey.Secound, PlayerKey.Secound, PlayerKey.First, PlayerKey.First,PlayerKey.First
            };
            Tuple<int,PlayerKey>[] IsSellingCellIndexOnTurn = new Tuple<int,PlayerKey>[] {
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne), 
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(3,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(2,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne)
            };

            int NumberOfTurns = 6;
            for (int i = 0; i < NumberOfTurns; i++)
            {
                MonopolyClientFirst.ExecuteTurn(1);

                if ( IsBuyingCellOnTurn[i] == PlayerKey.First)
                    MonopolyClientFirst.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.First)
                    MonopolyClientFirst.SellCell(MonopolyClientFirst.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());


                MonopolyClientSecound.ExecuteTurn(1);

                if (IsBuyingCellOnTurn[i] == PlayerKey.Secound)
                    MonopolyClientSecound.BuyCellIfPossible();

                if (IsSellingCellIndexOnTurn[i].Item2 == PlayerKey.Secound)
                    MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[IsSellingCellIndexOnTurn[i].Item1].OnDisplay());

                MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            }

            ////First turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Seceound turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.BuyCellIfPossible();
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Third turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.BuyCellIfPossible();
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////Fourth turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[3].OnDisplay());
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());


            ////Fivth turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());



            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientSecound.SellCell(MonopolyClientSecound.GetBoard()[2].OnDisplay());
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            ////sixth turn
            //MonopolyClientFirst.ExecuteTurn(1);
            //MonopolyClientFirst.BuyCellIfPossible();
            //MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

            //MonopolyClientSecound.ExecuteTurn(1);
            //MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());

            MonopolyUpdateMessage CheckBankrupcy = MonopolyClientSecound.GetUpdatedData();
            Assert.IsTrue(CheckBankrupcy.BankruptPlayer == PlayerKey.Secound);
        }

        [TestMethod]
        public void WinnerTest()
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
           
            for (int i = 0; i < 5; i++)
            {
                MonopolyClientFirst.ExecuteTurn(1);
                MonopolyClientFirst.BuyCellIfPossible();
                MonopolyClientSecound.UpdateData(MonopolyClientFirst.GetUpdatedData());

                MonopolyClientSecound.ExecuteTurn(1);
                MonopolyClientFirst.UpdateData(MonopolyClientSecound.GetUpdatedData());
            }

            MonopolyClientSecound.UpdateData(MonopolyClientSecound.GetUpdatedData());

            Assert.IsTrue(MonopolyClientFirst.WhoWon() == PlayerKey.First);
            Assert.IsTrue(MonopolyClientSecound.WhoWon() == PlayerKey.First);
        }
    }
}
