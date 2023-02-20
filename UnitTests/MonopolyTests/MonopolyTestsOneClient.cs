using Services.GamesServices.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.MultiplayerConnection;
using Enums.Monopoly;
using Services.GamesServices.Monopoly.Board.Cells;
using Models.Monopoly;

namespace UnitTests.MonopolyTests
{
    [TestClass]
    public class MonopolyTestsOneClient
    {
        private MonopolyService Client;

        [TestInitialize]
        public void TestsSetup()
        {
            Client = MonopolyDataPrepare.InitClients(1)[0];
        }


        [TestMethod]
        public void TestBuyingOwnCell()
        {
            Client.ExecutePlayerMove(1);

            MonopolyModalParameters parameters = Client.GetModalParameters();
            Client.ModalResponse(
                MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent),
                parameters.Identifier
            );

            for (int i = 2; Client.GetBoard()[i].GetBuyingBehavior().GetOwner() != PlayerKey.First ; i = (++i)%Client.GetBoard().Count)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
            }
            Client.ExecutePlayerMove(1);
            Assert.IsTrue(Client.GetModalParameters().Identifier == ModalResponseIdentifier.NoResponse);
        }

        [TestMethod]
        public void NationMonopolTest()
        {
            int PolandCostsWithoutMonopol = Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
            
            for (int i = 1;  ; i++)
            {
                Client.ExecutePlayerMove(1);

                MonopolyModalParameters parameters = Client.GetModalParameters();
                Client.ModalResponse(
                    MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent),
                    parameters.Identifier
                );

                if (Client.GetBoard()[i].GetNation() != Client.GetBoard()[i + 1].GetNation())
                    break;
            }
            
            Assert.IsTrue(Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay == PolandCostsWithoutMonopol*Consts.Monopoly.MonopolMultiplayer);
        }

        [TestMethod]
        public void BeachMonopolTest_TwoBeachesOwned()
        {
            List<MonopolyCell> BeachCells = Client.GetBoard().FindAll(p => p is MonopolyBeachCell);
            int FirstBeachStayCost = BeachCells[0].GetBuyingBehavior().GetCosts().Stay;

            for (int i = 1; ; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);

                if (Client.GetBoard()[i] is MonopolyBeachCell)
                {
                    MonopolyModalParameters parameters = Client.GetModalParameters();
                    Client.ModalResponse(
                        MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent),
                        parameters.Identifier
                    );
                }

                if (Client.GetBoard()[i].GetBeachName() == BeachCells[1].GetBeachName())
                    break;
            }
            int ExpectedValue = (int)(FirstBeachStayCost * Consts.Monopoly.BeachesOwnedMultiplayer[2]);
            int ActualValue = Client.GetBoard().FirstOrDefault(
                b => b.GetBeachName() == BeachCells[0].GetBeachName()
            ).GetBuyingBehavior().GetCosts().Stay;

            Assert.IsTrue(ActualValue == ExpectedValue);
        }

        [TestMethod]
        public void BeachMonopolTest_ThreeBeachesOwned()
        {
            List<MonopolyCell> BeachCells = Client.GetBoard().FindAll(p => p is MonopolyBeachCell);
            int FirstBeachStayCost = BeachCells[0].GetBuyingBehavior().GetCosts().Stay;

            for (int i = 1; ; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);

                if (Client.GetBoard()[i] is MonopolyBeachCell)
                {
                    MonopolyModalParameters parameters = Client.GetModalParameters();
                    Client.ModalResponse(
                        MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent),
                        parameters.Identifier
                    );
                }

                if (Client.GetBoard()[i].GetBeachName() == BeachCells[2].GetBeachName())
                    break;
            }
            int ExpectedValue = (int)(FirstBeachStayCost * Consts.Monopoly.BeachesOwnedMultiplayer[3]);
            int ActualValue = Client.GetBoard().FirstOrDefault(
                b => b.GetBeachName() == BeachCells[0].GetBeachName()
            ).GetBuyingBehavior().GetCosts().Stay;

            Assert.IsTrue(ActualValue == ExpectedValue);
        }

        [TestMethod]
        public void LosingMonopolAfterSellingBeachCell_Had2Sold1()
        {
            List<MonopolyCell> BeachCells = Client.GetBoard().FindAll(p => p is MonopolyBeachCell);
            int FirstBeachStayCost = BeachCells[0].GetBuyingBehavior().GetCosts().Stay;

            for (int i = 1; ; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);

                if (Client.GetBoard()[i] is MonopolyBeachCell)
                {
                    MonopolyModalParameters parameters = Client.GetModalParameters();
                    Client.ModalResponse(
                        MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent),
                        parameters.Identifier
                    );
                }

                if (Client.GetBoard()[i].GetBeachName() == BeachCells[1].GetBeachName())
                    break;
            }
            MonopolyCell? CellToSell = Client.GetBoard().FirstOrDefault(
                b => b.GetBeachName() == BeachCells[1].GetBeachName()
            );
            Client.SellCell(CellToSell.OnDisplay());
            int ActualValue = Client.GetBoard().FirstOrDefault(
                b => b.GetBeachName() == BeachCells[0].GetBeachName()
            ).GetBuyingBehavior().GetCosts().Stay;

            Assert.IsTrue(ActualValue == FirstBeachStayCost);
        }

        [TestMethod]
        public void LosingMonopolAfterSellingBeachCell_Had3Sold1()
        {
            List<MonopolyCell> BeachCells = Client.GetBoard().FindAll(p => p is MonopolyBeachCell);
            int SecoundBeachCellStayCost = BeachCells[1].GetBuyingBehavior().GetCosts().Stay;

            for (int i = 1; ; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);

                if (Client.GetBoard()[i] is MonopolyBeachCell)
                {
                    MonopolyModalParameters parameters = Client.GetModalParameters();
                    Client.ModalResponse(
                        MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent),
                        parameters.Identifier
                    );
                }

                if (Client.GetBoard()[i].GetBeachName() == BeachCells[2].GetBeachName())
                    break;
            }
            MonopolyCell? CellToSell = Client.GetBoard().FirstOrDefault(
                b => b.GetBeachName() == BeachCells[2].GetBeachName()
            );
            Client.SellCell(CellToSell.OnDisplay());

            int ExpectedValue = (int)(SecoundBeachCellStayCost * Consts.Monopoly.BeachesOwnedMultiplayer[2]);

            int ActualValue = Client.GetBoard().FirstOrDefault(
                b => b.GetBeachName() == BeachCells[1].GetBeachName()
            ).GetBuyingBehavior().GetCosts().Stay;

            Assert.IsTrue(ExpectedValue == ActualValue);
        }

        [TestMethod]
        public void IsAbleToBuyStartCell_ShouldBeNo()
        {
            for (int i = 1; i <= Client.GetBoard().Count; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
            }

            Assert.IsTrue(Client.GetModalParameters().Identifier == ModalResponseIdentifier.NoResponse);
        }

        [TestMethod]
        public void IsntAbleToMoveAfterStepingOnIsland()
        {
            int BoardSize = Client.GetBoard().Count;

            for (int i = 1; i < Client.GetBoard().Count; i++)
            {
                Client.ExecutePlayerMove(1);
                if (Client.GetBoard()[i].OnDisplay() == Consts.Monopoly.IslandDiaplsy)
                {
                    break;
                }
            }
            Client.ExecutePlayerMove(BoardSize - 2);

            Assert.IsTrue(Client.GetUpdatedData().PlayersData[0].Money == Consts.Monopoly.StartMoneyAmount);
        }

        [TestMethod]
        public void IsAbleToMoveFromIslandAfter3Turns()
        {
            int BoardSize = Client.GetBoard().Count;

            for (int i = 1; i < Client.GetBoard().Count; i++)
            {
                Client.ExecutePlayerMove(1);
                if (Client.GetBoard()[i] is MonopolyIslandCell)
                {
                    break;
                }
            }

            Client.ExecutePlayerMove(1);
            Client.ExecutePlayerMove(1);
            Client.ExecutePlayerMove(1);

            Client.ExecutePlayerMove(BoardSize - 2);

            Assert.IsTrue(Client.GetUpdatedData().PlayersData[0].Money == Consts.Monopoly.StartMoneyAmount + Consts.Monopoly.OnStartCrossedMoneyGiven);
        }

        [TestMethod]
        public void IsAbleToMoveFromIslandAfterPaying()
        {
            int BoardSize = Client.GetBoard().Count;

            for (int i = 1; i < Client.GetBoard().Count; i++)
            {
                Client.ExecutePlayerMove(1);
                if (Client.GetBoard()[i] is MonopolyIslandCell)
                {
                    break;
                }
            }
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent, ModalResponseIdentifier.Island);

            Client.ExecutePlayerMove(BoardSize - 2);

            int ExpectedMoney = Consts.Monopoly.StartMoneyAmount + Consts.Monopoly.OnStartCrossedMoneyGiven - Consts.Monopoly.IslandEscapeCost;
            int ActualMoney = Client.GetUpdatedData().PlayersData[0].Money;
            Assert.IsTrue(ExpectedMoney == ActualMoney);
            
        }

        [TestMethod]
        public void IsAbleToMoveFromIslandAfterPaying_DontHaveMoney()
        {
            int BoardSize = Client.GetBoard().Count;

            for (int i = 1; i < Client.GetBoard().Count; i++)
            {
                Client.ExecutePlayerMove(1);
                MonopolyModalParameters parameters = Client.GetModalParameters();
                Client.ModalResponse(
                    MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent),
                    parameters.Identifier
                );
                if (Client.GetBoard()[i] is MonopolyIslandCell)
                {
                    break;
                }
            }
            //i am called many times because one time is not enough to test whether client is able to pay
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent,ModalResponseIdentifier.Island);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent,ModalResponseIdentifier.Island);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent,ModalResponseIdentifier.Island);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent,ModalResponseIdentifier.Island);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent,ModalResponseIdentifier.Island);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent,ModalResponseIdentifier.Island);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent,ModalResponseIdentifier.Island);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent,ModalResponseIdentifier.Island);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent,ModalResponseIdentifier.Island);

            Client.ExecutePlayerMove(BoardSize - 2);

            int ActualMoney = Client.GetUpdatedData().PlayersData[0].Money;
            Assert.IsTrue(ActualMoney >= 0);

        }

        [TestMethod]
        public void WorldChampionshipSettingTest_TwoChampinships()
        {
            int ExpectedStayCost1 = Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
            int ExpectedStayCost2 = Client.GetBoard()[2].GetBuyingBehavior().GetCosts().Stay;
            Client.ModalResponse(Client.GetBoard()[1].OnDisplay(), ModalResponseIdentifier.Championship);

            int ActualStayCost1 = Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
            Assert.IsTrue(ExpectedStayCost1 * Consts.Monopoly.ChampionshipMultiplayer == ActualStayCost1);
            Assert.IsTrue(Client.GetBoard()[1].OnDisplay().Contains(Consts.Monopoly.ChampionshipInfo));


            Client.ModalResponse(Client.GetBoard()[2].OnDisplay(), ModalResponseIdentifier.Championship);
            ActualStayCost1 = Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
            int ActualStayCost2 = Client.GetBoard()[2].GetBuyingBehavior().GetCosts().Stay;
            Assert.IsTrue(ExpectedStayCost2 * Consts.Monopoly.ChampionshipMultiplayer == ActualStayCost2);
            Assert.IsTrue(ExpectedStayCost1 == ActualStayCost1);
            Assert.IsTrue(Client.GetBoard()[1].OnDisplay().Contains(Consts.Monopoly.ChampionshipInfo) == false);
            Assert.IsTrue(Client.GetBoard()[2].OnDisplay().Contains(Consts.Monopoly.ChampionshipInfo));
        }

        [TestMethod]
        public void WorldChampionshipSettingTest_OneChampionship()
        {
            int ExpectedStayCost1 = Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
            Client.ModalResponse(Client.GetBoard()[1].OnDisplay(), ModalResponseIdentifier.Championship);

            int ActualStayCost1 = Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
            Assert.IsTrue(ExpectedStayCost1 * Consts.Monopoly.ChampionshipMultiplayer == ActualStayCost1);

        }

        [TestMethod]
        public void FlyingFromAirportTest()
        {
            for (int i = 1; ; i++)
            {
                Client.ExecutePlayerMove(1);

                if (Client.GetBoard()[i] is AirportCell)
                {
                    break;
                }
            }

            Client.ModalResponse(Client.GetBoard()[1].OnDisplay(), ModalResponseIdentifier.Airport);
            int ActualPlayerMoney = Client.GetUpdatedData().PlayersData[0].Money;
            int ExpectedMoney = Consts.Monopoly.StartMoneyAmount + Consts.Monopoly.OnStartCrossedMoneyGiven;

            Assert.IsTrue(ActualPlayerMoney == ExpectedMoney);
        }


    }

}
