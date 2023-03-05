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
using MySqlX.XDevAPI;
using Services.GamesServices.Monopoly.Board.Cells;

namespace UnitTests.MonopolyTests;

[TestClass]
public class MonopolyTestsTwoClients
{
    private PlayerKey[] BuyingOrder = new PlayerKey[]
    {
        PlayerKey.First, PlayerKey.Secound, PlayerKey.Secound, PlayerKey.First, PlayerKey.First,PlayerKey.First,PlayerKey.First,
        PlayerKey.First,PlayerKey.First,PlayerKey.First,PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne,
        PlayerKey.First,PlayerKey.First,PlayerKey.First,PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne,
        PlayerKey.NoOne
    };


    private PlayerKey[] BuyingOrderInLosingMonopolCheck = new PlayerKey[]
    {
        PlayerKey.Secound, PlayerKey.Secound, PlayerKey.First, PlayerKey.First, PlayerKey.First,PlayerKey.First,PlayerKey.First,
        PlayerKey.First,PlayerKey.First,PlayerKey.First,PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne,
        PlayerKey.First,PlayerKey.First,PlayerKey.First,PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne,
        PlayerKey.NoOne
    };

    private List<MonopolyService> Clients;

    [TestInitialize]
    public void TestsSetup()
    {
        ResetClients();
    }

    private void ResetClients()
    {
        Clients = MonopolyDataPrepare.InitClients(2);
    }

    [TestMethod]
    public void PlayersMoneyOwnedAfter1To5TurnsTest()
    {
        int TurnsToTest = 5;
        for (int turn = 1; turn <= TurnsToTest; turn++)
        {
            ResetClients();
            List<MoneyFlow> ClientsMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(turn, ref Clients, BuyingOrder);

            List<PlayerUpdateData> ActualMoneyFirst = Clients[0].GetUpdatedData().PlayersData;
            List<int> ExpectedMoney = MonopolyDataPrepare.GetExpectedMoney(ClientsMoneyFlow);
            Assert.IsTrue(MonopolyDataPrepare.CompareMoneyAmount(ExpectedMoney, ActualMoneyFirst));
        }
    }

    [TestMethod]
    public void BankruptTest()
    {
        
        List<MoneyFlow> PlayersMoneyFlow = null;

        for (int i = 1; ; i++)
        {
            ResetClients();
            PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients,BuyingOrder);

            if (PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[1].Loss)
            {
                break;
            }
        }

        Assert.IsTrue(Clients[0].GetUpdatedData().PlayersData.FirstOrDefault(p => p.PlayerIndex == 1) == null);
    }

   [TestMethod]
    public void WinnerTest()
    {
        List<MoneyFlow> PlayersMoneyFlow = null;

        for (int i = 1; ; i++)
        {
            ResetClients();
            PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients, BuyingOrder);

            if (PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[1].Loss)
            {
                break;
            }
        }
        

        Assert.IsTrue(Clients[0].WhoWon() == PlayerKey.First);
    }

    [TestMethod]
    public void LosingNationMonopolAfterSellingCellTest()
    {
        List<MoneyFlow> PlayersMoneyFlow = null;
        int StayCostWithoutMonopolExpected = Clients[0].GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;

        for (int i = 3; ; i++)
        {
            ResetClients();

            PlayersMoneyFlow = MonopolyDataPrepare.ExecuteTurnsNumber(i, ref Clients, BuyingOrderInLosingMonopolCheck);

            if (PlayersMoneyFlow[1].Income + Consts.Monopoly.StartMoneyAmount < PlayersMoneyFlow[1].Loss)
            {
                break;
            }
        }
        int StayCistWithoutMonopolActual = Clients[0].GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
        Assert.IsTrue(StayCistWithoutMonopolActual == StayCostWithoutMonopolExpected);
    }

    [TestMethod]
    public void TestNationBuildingsBuying()
    {
        List<string> OptionsToBuy = new List<string>();
        OptionsToBuy.Add(Consts.Monopoly.Field);
        OptionsToBuy.Add(Consts.Monopoly.OneHouse);
        OptionsToBuy.Add(Consts.Monopoly.TwoHouses);

        for (int i = 0; i < OptionsToBuy.Count; i++)
        {
            ResetClients();

            Clients[0].ExecutePlayerMove(1);
            Clients[0].ModalResponse(OptionsToBuy[i]);
            MonopolyUpdateMessage FirstClientUpdateData = Clients[0].GetUpdatedData();
            Clients[1].UpdateData(FirstClientUpdateData);


            int FirstClientMoneyAfterBuy = FirstClientUpdateData.PlayersData[0].Money;
            int FirstClientExpectedMoney = Consts.Monopoly.StartMoneyAmount;
            FirstClientExpectedMoney -= Clients[0].GetBoard()[1].GetBuyingBehavior().GetCosts().Buy;

            Clients[1].ExecutePlayerMove(1);
            Clients[1].UpdateData(Clients[1].GetUpdatedData());
            int SecoundClientMoneyAfterStay = Clients[1].GetUpdatedData().PlayersData[1].Money;
            int SecoundClientExpectedMoney = Consts.Monopoly.StartMoneyAmount;
            SecoundClientExpectedMoney -= Clients[0].GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;

            Assert.IsTrue(FirstClientMoneyAfterBuy == FirstClientExpectedMoney);
            Assert.IsTrue(SecoundClientMoneyAfterStay == SecoundClientExpectedMoney);
        }
        
    }

    [TestMethod]
    public void CellsDisplaySynchronizationTest()
    {
        Clients[0].ExecutePlayerMove(2);
        Clients[0].ModalResponse(Consts.Monopoly.Field);

        Clients[1].UpdateData(Clients[0].GetUpdatedData());

        Clients[1].ExecutePlayerMove(1);
        Clients[1].ModalResponse(Consts.Monopoly.OneHouse);

        Clients[0].UpdateData(Clients[1].GetUpdatedData());

        string FirstClientDisplay = Clients[0].GetBoard()[2].OnDisplay();
        string SecoundClientDisplay = Clients[1].GetBoard()[2].OnDisplay();

        Assert.IsTrue(FirstClientDisplay == SecoundClientDisplay);

        FirstClientDisplay = Clients[0].GetBoard()[1].OnDisplay();
        SecoundClientDisplay = Clients[1].GetBoard()[1].OnDisplay();

        Assert.IsTrue(FirstClientDisplay == SecoundClientDisplay);
    }

    [TestMethod]
    public void TestAppearenceOfRepurchancingModal()
    {
        Clients[0].ExecutePlayerMove(1);
        Clients[0].ModalResponse(Consts.Monopoly.Field);

        Clients[1].UpdateData(Clients[0].GetUpdatedData());

        Clients[1].ExecutePlayerMove(1);
        MonopolyModalParameters parameters = Clients[1].GetModalParameters();
        
        Assert.IsTrue(Clients[1].GetBoard()[1].GetBuyingBehavior().GetOwner() == PlayerKey.First);
        Assert.IsTrue(parameters.Parameters.Title == "Do you want to repurchace this cell");
        Assert.IsTrue(parameters.Parameters.ButtonsContent[0] == "Yes" || parameters.Parameters.ButtonsContent[0] == "No");       
        Assert.IsTrue(parameters.Parameters.ButtonsContent[1] == "No" || parameters.Parameters.ButtonsContent[1] == "Yes");       
    }

    [TestMethod]
    public void TestAbilityToRepurchaceCell()
    {
        Clients[0].ExecutePlayerMove(1);
        Clients[0].ModalResponse(Consts.Monopoly.Field);

        Clients[1].UpdateData(Clients[0].GetUpdatedData());

        Clients[1].ExecutePlayerMove(1);

        Assert.IsTrue(Clients[1].GetBoard()[1].GetBuyingBehavior().GetOwner() == PlayerKey.First);

        Clients[1].ModalResponse("Yes");

        Clients[0].UpdateData(Clients[1].GetUpdatedData());
        Assert.IsTrue(Clients[0].GetBoard()[1].GetBuyingBehavior().GetOwner() == PlayerKey.Secound);
    }

    [TestMethod]
    public void TestCellRepurchasing_WithMoney()
    {
        Clients[0].ExecutePlayerMove(1);
        Clients[0].ModalResponse(Consts.Monopoly.Field);

        int BuyCost = Clients[0].GetBoard()[1].GetBuyingBehavior().GetCosts().Buy;
        int StayCost = Clients[0].GetBoard()[1].GetBuyingBehavior().GetCosts().Stay;
        float Multiplyer = Consts.Monopoly.CellRepurchaseMultiplayer;
        int ExpectedMoneyAmount = Consts.Monopoly.StartMoneyAmount - StayCost - (int)(BuyCost * Multiplyer);

        Clients[1].UpdateData(Clients[0].GetUpdatedData());

        Clients[1].ExecutePlayerMove(1);

        Assert.IsTrue(Clients[1].GetBoard()[1].GetBuyingBehavior().GetOwner() == PlayerKey.First);

        Clients[1].ModalResponse("Yes");

        Clients[0].UpdateData(Clients[1].GetUpdatedData());
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Assert.IsTrue(Clients[0].GetBoard()[1].GetBuyingBehavior().GetOwner() == PlayerKey.Secound);

        int ActualMoneyAmount = Clients[1].GetUpdatedData().PlayersData[1].Money;
        Assert.IsTrue(ExpectedMoneyAmount == ActualMoneyAmount);
    }

    [TestMethod]
    public void TestCellRepurchasing_WithoutMoney()
    {

        MonopolyService Client = Clients[1];
        MonopolyDataPrepare.GoTo<MonopolyIslandCell>(ref Client);

        //Make Sure That Client 2 has no more money than paying for escaping from island
        int SecoundClientPos = Clients[1].GetUpdatedData().PlayersData[1].Position;
        Assert.IsTrue(Clients[1].GetBoard()[SecoundClientPos] is MonopolyIslandCell);

        Clients[1].ModalResponse(Consts.Monopoly.PayToEscapeIsland);
        Clients[1].ModalResponse(Consts.Monopoly.PayToEscapeIsland);
        Clients[1].ModalResponse(Consts.Monopoly.PayToEscapeIsland);
        Clients[1].ModalResponse(Consts.Monopoly.PayToEscapeIsland);
        Clients[1].ModalResponse(Consts.Monopoly.PayToEscapeIsland);
        Clients[1].ModalResponse(Consts.Monopoly.PayToEscapeIsland);
        Clients[1].ModalResponse(Consts.Monopoly.PayToEscapeIsland);
        Clients[1].ModalResponse(Consts.Monopoly.PayToEscapeIsland);
        Clients[1].ModalResponse(Consts.Monopoly.PayToEscapeIsland);
        Clients[1].ModalResponse(Consts.Monopoly.PayToEscapeIsland);
        Clients[1].ModalResponse(Consts.Monopoly.PayToEscapeIsland);


        int IslandPos = Clients[1].GetUpdatedData().PlayersData[1].Position;
        int FirstClientPos = 1;
        int BoardSize = Clients[0].GetBoard().Count;
        Clients[1].ExecutePlayerMove(BoardSize - IslandPos + FirstClientPos);

        Clients[0].UpdateData(Clients[1].GetUpdatedData());

        Clients[0].ExecutePlayerMove(1);
        Clients[0].ModalResponse(Consts.Monopoly.TwoHouses);

        Clients[1].UpdateData(Clients[0].GetUpdatedData());

        SecoundClientPos = Clients[0].GetUpdatedData().PlayersData[1].Position;
        Assert.IsTrue(Clients[0].GetBoard()[SecoundClientPos].GetBuyingBehavior().GetOwner() == PlayerKey.First);

        MonopolyModalParameters parameters = Clients[1].GetModalParameters();
        Assert.IsTrue(parameters.WhenShowModal == ModalShow.Never);

        
    }

    [TestMethod]
    public void TestDublet()
    {
        Assert.IsTrue(Clients[0].IsYourTurn());
        Assert.IsTrue(Clients[1].IsYourTurn() == false);

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();


        Assert.IsTrue(Clients[0].IsYourTurn());
        Assert.IsTrue(Clients[1].IsYourTurn() == false);
    }

    [TestMethod]
    public void TestDublet_After6RolledThirdTime()
    {
        Assert.IsTrue(Clients[0].IsYourTurn());
        Assert.IsTrue(Clients[1].IsYourTurn() == false);

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Assert.IsTrue(Clients[0].IsYourTurn() == false);
        Assert.IsTrue(Clients[1].IsYourTurn() == true);
    }

    [TestMethod]
    public void PositionAfterThirdDublet_ThirdThowIsDublet()
    {
        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        int ExpectedPosition = 12;
        int ActualPosition = Clients[0].GetUpdatedData().PlayersData[0].Position;
        Assert.IsTrue(ExpectedPosition == ActualPosition);
    }

    [TestMethod]
    public void PositionAfterThirdDublet_ThirdThowIsntDublet()
    {
        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(1);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        int ExpectedPosition = 13;
        int ActualPosition = Clients[0].GetUpdatedData().PlayersData[0].Position;
        Assert.IsTrue(ExpectedPosition == ActualPosition);
    }

    [TestMethod]
    public void PositionAfterThirdDublet_BreakBetweenDublets()
    {
        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(1);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(1);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        int ExpectedPosition = 20 % Clients[0].GetBoard().Count;
        int ActualPosition = Clients[0].GetUpdatedData().PlayersData[0].Position;
        Assert.IsTrue(ExpectedPosition == ActualPosition);
    }

    [TestMethod]
    public void PositionAfterThirdDublet_TestPayingStayCost()
    {
        

        for (int i = 1; ; i++)
        {
            Clients[1].ExecutePlayerMove(1);

            if (Clients[1].GetBoard()[(i + 6*2) % Clients[1].GetBoard().Count] is MonopolyNationCell)
                break;
        }

        Clients[1].ExecutePlayerMove(12);
        Clients[1].ModalResponse(Consts.Monopoly.Field);
       
        Clients[0].UpdateData(Clients[1].GetUpdatedData());

        int SecoundClientPos = Clients[0].GetUpdatedData().PlayersData[1].Position;
        Assert.IsTrue(Clients[0].GetBoard()[SecoundClientPos] is MonopolyNationCell);


        for (int i = 1; ; i++)
        {
            Clients[0].ExecutePlayerMove(1);

            if (Clients[0].GetBoard()[(i + 6 * 2) % Clients[0].GetBoard().Count] is MonopolyNationCell)
                break;
        }

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[0].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[0].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        Clients[0].ExecutePlayerMove(6);
        Clients[0].NextTurn();
        Clients[1].UpdateData(Clients[0].GetUpdatedData());
        Clients[0].UpdateData(Clients[0].GetUpdatedData());
        Clients[1].NextTurn();

        

        int FirstClientPos = Clients[0].GetUpdatedData().PlayersData[0].Position;

        Assert.IsTrue(FirstClientPos == SecoundClientPos);

        int ExpectedMoney = Consts.Monopoly.StartMoneyAmount;
        ExpectedMoney -= 2 * Clients[0].GetBoard()[FirstClientPos].GetBuyingBehavior().GetCosts().Stay;

        int ActualMoney = Clients[0].GetUpdatedData().PlayersData[0].Money;

        //Assert.IsTrue(ExpectedMoney == ActualMoney);

    }
}
