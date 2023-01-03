using Services.GamesServices.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.MultiplayerConnection;
using Enums.Monopoly;
using Models.Monopoly;

namespace UnitTests.MonopolyTests
{
    [TestClass]
    public class MonopolyClientsNumberIndependentTests
    {
        [TestMethod]
        public void TestBuyingOwnCell()
        {
            MonopolyService Client = new MonopolyGameLogic();
            List<Player> Players = new List<Player>();
            Players.Add(new Player());
            Client.StartGame(Players);
            Client.SetMainPlayerIndex(0);

            Client.ExecuteTurn(1);
            Client.BuyCellIfPossible();

            for (int i = 1; i < Client.GetBoard().Count; i++)
            {
                Client.ExecuteTurn(1);
            }

            Assert.IsTrue(Client.ExecuteTurn(1) == MonopolyTurnResult.CannotBuyCell);
        }

        [TestMethod]
        public void NationMonopolTest()
        {
            MonopolyService Client = new MonopolyGameLogic();
            List<Player> Players = new List<Player>();
            Players.Add(new Player());
            Client.StartGame(Players);
            Client.SetMainPlayerIndex(0);

            int PolandCostsWithoutMonopol = Client.GetBoard()[1].GetCosts().Stay;
            
            for (int i = 1;  ; i++)
            {
                Client.ExecuteTurn(1);
                Client.BuyCellIfPossible();

                if (Client.GetBoard()[i].GetNation() != Client.GetBoard()[i + 1].GetNation())
                    break;
            }
            
            Assert.IsTrue(Client.GetBoard()[1].GetCosts().Stay == PolandCostsWithoutMonopol*Consts.Monopoly.MonopolMultiplayer);
        }

        [TestMethod]
        public void BeachMonopolTest_TwoBeachesOwned()
        {
            MonopolyService Client = new MonopolyGameLogic();
            List<Player> Players = new List<Player>();
            Players.Add(new Player());
            Client.StartGame(Players);
            Client.SetMainPlayerIndex(0);

            List<MonopolyCell> BeachCells = Client.GetBoard().FindAll(p => p.GetBeachName() != Beach.NoBeach);
            int FirstBeachStayCost = BeachCells[0].GetCosts().Stay;

            for (int i = 1; ; i++)
            {
                Client.ExecuteTurn(1);

                if (Client.GetBoard()[i].GetBeachName() != Beach.NoBeach)
                    Client.BuyCellIfPossible();

                if (Client.GetBoard()[i].GetBeachName() == BeachCells[1].GetBeachName())
                    break;
            }
            int ExpectedValue = (int)(FirstBeachStayCost * Consts.Monopoly.BeachesOwnedMultiplayer[2]);
            int ActualValue = Client.GetBoard().FirstOrDefault(
                b => b.GetBeachName() == BeachCells[0].GetBeachName()
            ).GetCosts().Stay;

            Assert.IsTrue(ActualValue == ExpectedValue);
        }

        [TestMethod]
        public void BeachMonopolTest_ThreeBeachesOwned()
        {
            MonopolyService Client = new MonopolyGameLogic();
            List<Player> Players = new List<Player>();
            Players.Add(new Player());
            Client.StartGame(Players);
            Client.SetMainPlayerIndex(0);

            List<MonopolyCell> BeachCells = Client.GetBoard().FindAll(p => p.GetBeachName() != Beach.NoBeach);
            int FirstBeachStayCost = BeachCells[0].GetCosts().Stay;

            for (int i = 1; ; i++)
            {
                Client.ExecuteTurn(1);
                
                if (Client.GetBoard()[i].GetBeachName() != Beach.NoBeach)
                    Client.BuyCellIfPossible();

                if (Client.GetBoard()[i].GetBeachName() == BeachCells[2].GetBeachName())
                    break;
            }
            int ExpectedValue = (int)(FirstBeachStayCost * Consts.Monopoly.BeachesOwnedMultiplayer[3]);
            int ActualValue = Client.GetBoard().FirstOrDefault(
                b => b.GetBeachName() == BeachCells[0].GetBeachName()
            ).GetCosts().Stay;

            Assert.IsTrue(ActualValue == ExpectedValue);
        }

        [TestMethod]
        public void IsAbleToBuyStartCell_ShouldBeNo()
        {
            MonopolyService Client = new MonopolyGameLogic();
            List<Player> Players = new List<Player>();
            Players.Add(new Player());
            Client.StartGame(Players);
            Client.SetMainPlayerIndex(0);


            for (int i = 1; i < Client.GetBoard().Count; i++)
            {
                Client.ExecuteTurn(1);
            }
            

            Assert.IsTrue(Client.ExecuteTurn(1) == MonopolyTurnResult.CannotBuyCell);
        }

        [TestMethod]
        public void IsntAbleToMoveAfterStepingOnIsland()
        {
            MonopolyService Client = new MonopolyGameLogic();
            List<Player> Players = new List<Player>();
            Players.Add(new Player());
            Client.StartGame(Players);
            Client.SetMainPlayerIndex(0);

            int BoardSize = Client.GetBoard().Count;

            for (int i = 1; i < Client.GetBoard().Count; i++)
            {
                Client.ExecuteTurn(1);
                if (Client.GetBoard()[i].OnDisplay() == Consts.Monopoly.IslandDiaplsy)
                {
                    Client.ModalResponse("Wait");
                    break;
                }
            }

            Client.ExecuteTurn(BoardSize - 2);

            Assert.IsTrue(Client.GetUpdatedData().PlayersData[0].Money == Consts.Monopoly.StartMoneyAmount);
        }

        [TestMethod]
        public void IsAbleToMoveFromIslandAfter3Turns()
        {
            MonopolyService Client = new MonopolyGameLogic();
            List<Player> Players = new List<Player>();
            Players.Add(new Player());
            Client.StartGame(Players);
            Client.SetMainPlayerIndex(0);

            int BoardSize = Client.GetBoard().Count;

            for (int i = 1; i < Client.GetBoard().Count; i++)
            {
                Client.ExecuteTurn(1);
                if (Client.GetBoard()[i].OnDisplay() == Consts.Monopoly.IslandDiaplsy)
                {
                    Client.ModalResponse("Wait");
                    break;
                }
            }

            Client.ExecuteTurn(1);
            Client.ExecuteTurn(1);
            Client.ExecuteTurn(1);
            Client.ExecuteTurn(BoardSize - 2);

            Assert.IsTrue(Client.GetUpdatedData().PlayersData[0].Money == Consts.Monopoly.StartMoneyAmount + Consts.Monopoly.OnStartCrossedMoneyGiven);
        }
    }

}
