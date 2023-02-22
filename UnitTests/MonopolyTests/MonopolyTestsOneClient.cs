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
            for (int i = 0; ; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);

                if (Client.GetBoard()[(i + 1) % Client.GetBoard().Count] is MonopolyBeachCell)
                    break;
            }

            MonopolyModalParameters parameters = Client.GetModalParameters();
            Client.ModalResponse(
                MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent));

            for (int i = 0; i < Client.GetBoard().Count; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
            }

            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is MonopolyBeachCell);
            Assert.IsTrue(Client.GetModalParameters().WhenShowModal == ModalShow.Never);
        }

        [TestMethod]
        public void NationMonopolTest()
        {
            int PolandCostsWithoutMonopol = 0;
            bool CheckCostOnce = true;
            
            for (int i = 1;  ; i++)
            {
                Client.ExecutePlayerMove(1);

                MonopolyModalParameters parameters = Client.GetModalParameters();
                Client.ModalResponse(
                    MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent)
                );

                if(CheckCostOnce)
                {
                    PolandCostsWithoutMonopol = Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
                    CheckCostOnce = false;
                }

                if (Client.GetBoard()[i].GetName() != Client.GetBoard()[i + 1].GetName())
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
                        MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent));
                }

                if (Client.GetBoard()[i].GetName() == BeachCells[1].GetName())
                    break;
            }
            int ExpectedValue = (int)(FirstBeachStayCost * Consts.Monopoly.BeachesOwnedMultiplier[2]);
            int ActualValue = Client.GetBoard().FirstOrDefault(
                b => b.GetName() == BeachCells[0].GetName()
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
                        MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent));
                }

                if (Client.GetBoard()[i].GetName() == BeachCells[2].GetName())
                    break;
            }
            int ExpectedValue = (int)(FirstBeachStayCost * Consts.Monopoly.BeachesOwnedMultiplier[3]);
            int ActualValue = Client.GetBoard().FirstOrDefault(
                b => b.GetName() == BeachCells[0].GetName()
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
                        MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent));
                }

                if (Client.GetBoard()[i].GetName() == BeachCells[1].GetName())
                    break;
            }
            MonopolyCell? CellToSell = Client.GetBoard().FirstOrDefault(
                b => b.GetName() == BeachCells[1].GetName()
            );
            Client.SellCell(CellToSell.OnDisplay());
            int ActualValue = Client.GetBoard().FirstOrDefault(
                b => b.GetName() == BeachCells[0].GetName()
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
                        MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent));
                }

                if (Client.GetBoard()[i].GetName() == BeachCells[2].GetName())
                    break;
            }
            MonopolyCell? CellToSell = Client.GetBoard().FirstOrDefault(
                b => b.GetName() == BeachCells[2].GetName()
            );
            Client.SellCell(CellToSell.OnDisplay());

            int ExpectedValue = (int)(SecoundBeachCellStayCost * Consts.Monopoly.BeachesOwnedMultiplier[2]);

            int ActualValue = Client.GetBoard().FirstOrDefault(
                b => b.GetName() == BeachCells[1].GetName()
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

            Assert.IsTrue(Client.GetModalParameters().WhenShowModal == ModalShow.Never);
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
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent);

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
                    MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent));
                if (Client.GetBoard()[i] is MonopolyIslandCell)
                {
                    break;
                }
            }

            int ihdg = 0;

            //i am called many times because one time is not enough to test whether client is able to pay
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent);
            Client.ModalResponse(Consts.Monopoly.PayToEscapeIslandCellButtonContent);

            Client.ExecutePlayerMove(BoardSize - 2);

            int ActualMoney = Client.GetUpdatedData().PlayersData[0].Money;
            Assert.IsTrue(ActualMoney >= 0);

        }

        [TestMethod]
        public void WorldChampionshipSettingTest_TwoChampinships()
        {
            bool BuyTwoCells = true;
            for (int i = 0; Client.GetBoard()[i] is not ChampionshipCell; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);

                

                if (BuyTwoCells)
                {
                    MonopolyModalParameters parameters = Client.GetModalParameters();
                    Client.ModalResponse(
                        MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent)
                    );
                }

                if (Client.GetBoard()[i].GetName() != Client.GetBoard()[i + 1].GetName())
                    BuyTwoCells = false;
            }

            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is ChampionshipCell);

            int ExpectedStayCost1 = Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
            int ExpectedStayCost2 = Client.GetBoard()[2].GetBuyingBehavior().GetCosts().Stay;
            Client.ModalResponse(Client.GetBoard()[1].OnDisplay());

            int ActualStayCost1 = Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
            Assert.IsTrue(ExpectedStayCost1 * Consts.Monopoly.ChampionshipMultiplayer == ActualStayCost1);
            Assert.IsTrue(Client.GetBoard()[1].OnDisplay().Contains(Consts.Monopoly.ChampionshipInfo));


            Client.ModalResponse(Client.GetBoard()[2].OnDisplay());
            ActualStayCost1 = Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
            int ActualStayCost2 = Client.GetBoard()[2].GetBuyingBehavior().GetCosts().Stay;
            Assert.IsTrue(ExpectedStayCost2 * Consts.Monopoly.ChampionshipMultiplayer == ActualStayCost2);
            Assert.IsTrue(ExpectedStayCost1 == ActualStayCost1);
            Assert.IsTrue(Client.GetBoard()[1].OnDisplay().Contains(Consts.Monopoly.ChampionshipInfo) == false);
            Assert.IsTrue(Client.GetBoard()[2].OnDisplay().Contains(Consts.Monopoly.ChampionshipInfo) == true);
        }

        [TestMethod]
        public void WorldChampionshipSettingTest_OneChampionship()
        {
            for (int i = 0; Client.GetBoard()[i] is not ChampionshipCell; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
            }

            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is ChampionshipCell);

            int ExpectedStayCost1 = Client.GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
            Client.ModalResponse(Client.GetBoard()[1].OnDisplay());

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

            Client.ModalResponse(Client.GetBoard()[1].OnDisplay());
            Client.ExecutePlayerMove(1);
            int ActualPlayerMoney = Client.GetUpdatedData().PlayersData[0].Money;
            int ExpectedMoney = Consts.Monopoly.StartMoneyAmount + Consts.Monopoly.OnStartCrossedMoneyGiven;

            Assert.IsTrue(ActualPlayerMoney == ExpectedMoney);
        }

        [TestMethod]
        public void TestPossibilityOfBuyingCellWithoutMoney_Nation()
        {
            
            for (int i = 0; i< 10; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
                MonopolyModalParameters parameters = Client.GetModalParameters();
                Client.ModalResponse(
                    MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent));

                if (i >= 10 && Client.GetBoard()[i + 1] is MonopolyNationCell)
                    break;
            }
            
            Client.ExecutePlayerMove(1);
            MonopolyModalParameters ModalParameters = Client.GetModalParameters();

            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is MonopolyNationCell);
            Assert.IsTrue(ModalParameters.Parameters.ButtonsContent.IndexOf(Consts.Monopoly.Field) == -1);
            Assert.IsTrue(ModalParameters.Parameters.ButtonsContent.IndexOf(Consts.Monopoly.OneHouse) == -1);
            Assert.IsTrue(ModalParameters.Parameters.ButtonsContent.IndexOf(Consts.Monopoly.TwoHouses) == -1);
        }

        [TestMethod]
        public void TestPossibilityOfBuyingCellWithoutMoney_Beach()
        {
            for (int i = 1; i < Client.GetBoard().Count; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
                MonopolyModalParameters parameters = Client.GetModalParameters();
                Client.ModalResponse(
                    MonopolyDataPrepare.FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent));

                if (i >= 10 && Client.GetBoard()[i + 1] is MonopolyBeachCell)
                    break;
            }

            Client.ExecutePlayerMove(1);
            MonopolyModalParameters ModalParameters = Client.GetModalParameters();

            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is MonopolyBeachCell);
            Assert.IsTrue(ModalParameters.WhenShowModal == ModalShow.Never);
            
        }

        [TestMethod]
        public void TestPossibilityOfBuyingThreeHouses_FirstLap()
        {
            for (int i = 1; i < Client.GetBoard().Count; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
                if (i >= 5 && Client.GetBoard()[i + 1] is MonopolyNationCell)
                    break;
            }

            Client.ExecutePlayerMove(1);
            MonopolyModalParameters ModalParameters = Client.GetModalParameters();

            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is MonopolyNationCell);
            Assert.IsTrue(ModalParameters.Parameters.ButtonsContent.IndexOf(Consts.Monopoly.ThreeHouses) == -1);

        }

        [TestMethod]
        public void TestPossibilityOfBuyingThreeHouses_SecoundLap()
        {
            for (int i = 0; i < Client.GetBoard().Count; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
        

                if (i >= Client.GetBoard().Count &&
                    Client.GetBoard()[(i + 1)% Client.GetBoard().Count] is MonopolyNationCell)
                    break;
            }

            Client.ExecutePlayerMove(1);
            MonopolyModalParameters ModalParameters = Client.GetModalParameters();

            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is MonopolyNationCell);
            Assert.IsTrue(ModalParameters.Parameters.ButtonsContent.IndexOf(Consts.Monopoly.ThreeHouses) != -1);

        }

        [TestMethod]
        public void TestPossibilityOfBuyingHotel_NotBoughtCell()
        {
            for (int i = 0; i < Client.GetBoard().Count; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);


                if (i >= Client.GetBoard().Count &&
                    Client.GetBoard()[(i + 1) % Client.GetBoard().Count] is MonopolyNationCell)
                    break;
            }

            Client.ExecutePlayerMove(1);
            MonopolyModalParameters ModalParameters = Client.GetModalParameters();

            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is MonopolyNationCell);
            Assert.IsTrue(ModalParameters.Parameters.ButtonsContent.IndexOf(Consts.Monopoly.Hotel) == -1);

        }

        [TestMethod]
        public void TestPossibilityOfBuyingHotel_TwoHousesBought()
        {
            for (int i = 0; i < Client.GetBoard().Count; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);


                if (Client.GetBoard()[i] is MonopolyNationCell)
                    break;
            }

            MonopolyModalParameters parameters = Client.GetModalParameters();
            Client.ModalResponse(
                Consts.Monopoly.TwoHouses);


            for (int i = 0; i < Client.GetBoard().Count; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
            }

            parameters = Client.GetModalParameters();

            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is MonopolyNationCell);
            Assert.IsTrue(parameters.Parameters.ButtonsContent.IndexOf(Consts.Monopoly.Hotel) == -1);

        }

        [TestMethod]
        public void TestPossibilityOfBuyingHotel_ThreeHousesBought()
        {
            for (int i = 0; ; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);


                if (i >= Client.GetBoard().Count &&
                    Client.GetBoard()[(i + 1) % Client.GetBoard().Count] is MonopolyNationCell)
                    break;
            }

            MonopolyModalParameters parameters = Client.GetModalParameters();
            Client.ModalResponse(
                Consts.Monopoly.ThreeHouses);


            for (int i = 0; i < Client.GetBoard().Count; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
            }

            parameters = Client.GetModalParameters();

            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is MonopolyNationCell);
            Assert.IsTrue(parameters.Parameters.ButtonsContent.IndexOf(Consts.Monopoly.Hotel) != -1);

        }

        [TestMethod]
        public void TestEnhancingCell_CellEnhanced()
        {
            for (int i = 0; ; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
                if (Client.GetBoard()[(i + 1) % Client.GetBoard().Count] is MonopolyNationCell)
                    break;
            }

            MonopolyModalParameters parameters = Client.GetModalParameters();
            Client.ModalResponse(
                Consts.Monopoly.Field);

            int MainPlayerPos = Client.GetUpdatedData().PlayersData[0].Position;
            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is MonopolyNationCell);
            Assert.IsTrue(Client.GetBoard()[MainPlayerPos].OnDisplay().Contains(Consts.Monopoly.Field));

            parameters = Client.GetModalParameters();
            Client.ModalResponse(
                Consts.Monopoly.TwoHouses);

            Assert.IsTrue(Client.GetBoard()[MainPlayerPos].OnDisplay().Contains(Consts.Monopoly.TwoHouses));

        }

        [TestMethod]
        public void TestEnhancingCell_OptionsToBuy()
        {
            for (int i = 0; ; i++)
            {
                MonopolyDataPrepare.ExecuteClientTestTurn(ref Client, i);
                if (Client.GetBoard()[(i + 1) % Client.GetBoard().Count] is MonopolyNationCell)
                    break;
            }

            MonopolyModalParameters parameters = Client.GetModalParameters();
            Client.ModalResponse(
                Consts.Monopoly.Field);

            int MainPlayerPos = Client.GetUpdatedData().PlayersData[0].Position;
            Assert.IsTrue(Client.GetBoard()[Client.GetUpdatedData().PlayersData[0].Position] is MonopolyNationCell);
            Assert.IsTrue(Client.GetBoard()[MainPlayerPos].OnDisplay().Contains(Consts.Monopoly.Field));

            parameters = Client.GetModalParameters();

            Assert.IsTrue(parameters.Parameters.ButtonsContent.Contains(Consts.Monopoly.TwoHouses));

        }
    }

}
