using Models;
using Models.Monopoly;
using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.GamesServices.Monopoly.Board.Cells;
using Services.GamesServices.Monopoly.Update;
using Org.BouncyCastle.Asn1.Cmp;
using Models.MultiplayerConnection;

namespace Services.GamesServices.Monopoly.Board
{
    public class MonopolyBoard
    {
        private List<MonopolyCell> Board;
        private Int MainPlayerTurnsOnIslandRemaining;

        public MonopolyBoard()
        {
            Board = new List<MonopolyCell>();
            MainPlayerTurnsOnIslandRemaining = new Int();
            InitBoard();
        }

        private void InitBoard()
        {
            Board.Add(new StartCell());

            Board.Add(new MonopolyNationCell(new Costs(50, 30), Nation.Poland));
            Board.Add(new MonopolyNationCell(new Costs(80, 40), Nation.Poland));

            Board.Add(new MonopolyBeachCell(new Costs(100, 30), Beach.Dubaj));

            Board.Add(new ChampionshipCell());

            Board.Add(new MonopolyNationCell(new Costs(130, 70), Nation.France));
            Board.Add(new MonopolyNationCell(new Costs(110, 50), Nation.France));
            Board.Add(new MonopolyNationCell(new Costs(150, 100), Nation.France));


            Board.Add(new MonopolyIslandCell(MainPlayerTurnsOnIslandRemaining));

            Board.Add(new MonopolyBeachCell(new Costs(100, 30), Beach.Bali));

            Board.Add(new MonopolyNationCell(new Costs(180, 140), Nation.Argentina));
            Board.Add(new MonopolyNationCell(new Costs(250, 200), Nation.Argentina));
            Board.Add(new MonopolyNationCell(new Costs(210, 150), Nation.Argentina));

            Board.Add(new MonopolyBeachCell(new Costs(100, 30), Beach.Cypr));

        }

        public List<MonopolyCell> GetBoard()
        {
            List<MonopolyCell> BoardCopy = new List<MonopolyCell>();
            BoardCopy = Board;
            return BoardCopy;
        }

        public bool DontHaveMoneyToPay(MonopolyPlayer Debtor)
        {
            return CanAffordStaying(Debtor) == false && DoesCellHaveAnotherOwner(Debtor) == true;
        }

        private bool CanAffordStaying(MonopolyPlayer Visitor)
        {
            return Visitor.MoneyOwned >= Board[Visitor.OnCellIndex].GetBuyingBehavior().GetCosts().Stay;
        }

        public bool DoesCellHaveAnotherOwner(MonopolyPlayer PlayerStepped)
        {
            return IsNoOneCell(PlayerStepped.OnCellIndex) == false && IsPlayerCellOwner(PlayerStepped) == false;
        }

        public bool IsPossibleToBuyCell(MonopolyPlayer buyer)
        {
            return CanAffordBuying(buyer) && IsNoOneCell(buyer.OnCellIndex) && IsBuyableCell(buyer.OnCellIndex);
        }

        private bool CanAffordBuying(MonopolyPlayer buyer)
        {
            return buyer.MoneyOwned >= Board[buyer.OnCellIndex].GetBuyingBehavior().GetCosts().Buy;
        }

        public bool IsNoOneCell(int CellIndex)
        {
            return Board[CellIndex].GetBuyingBehavior().GetOwner() == PlayerKey.NoOne;
        }

        private bool IsBuyableCell(int CellIndex)
        {
            return Board[CellIndex] is MonopolyNationCell ||
                   Board[CellIndex] is MonopolyBeachCell;
        }

        private bool IsPlayerCellOwner(MonopolyPlayer Player)
        {
            return Board[Player.OnCellIndex].GetBuyingBehavior().GetOwner() == Player.Key;
        }

        public MonopolyCell GetCell(int Index)
        {
            return Board[Index];
        }

        public void CheckForMonopolOf(MonopolyPlayer aPlayer)
        {
            Board = Board[aPlayer.OnCellIndex].MonopolChanges().UpdateBoardMonopol(Board, aPlayer.OnCellIndex);
        }

        public void GetMonopolOff(MonopolyCell aCell)
        {
            int aCellIndex = Board.IndexOf(aCell);
            Board = Board[aCellIndex].MonopolChanges().GetMonopolOff(Board, aCellIndex);
        }

        public void SetChampionship(string OnCellDisplay)
        {
            MonopolyCell? CellWithChampionship = Board.FirstOrDefault(
                c => c.GetBuyingBehavior().IsThereChampionship() == true
            );
            if (CellWithChampionship != null)
                CellWithChampionship.GetBuyingBehavior().GetChampionshipOff();


            MonopolyCell? CellToSetChampionship = Board.FirstOrDefault(
                c => c.OnDisplay() == OnCellDisplay
            );
            CellToSetChampionship.GetBuyingBehavior().SetChampionship();
        }

        public MonopolyModalParameters GetCellModalParameters(MonopolyPlayer MainPlayer)
        {
            return Board[MainPlayer.OnCellIndex].GetModalParameters(Board, MainPlayer.Key);
        }

        public MonopolyBoardUpdateData MakeBoardUpdateData()
        {
            MonopolyBoardUpdateData BoardUpdatedData = UpdateDataFactory.CreateBoardUpdateData();
            BoardUpdatedData.FormatBoardUpdateData(Board);
            return BoardUpdatedData;
        }

        public int GetDebtAmount(int MainPlayerCellPos)
        {
            return Board[MainPlayerCellPos].GetBuyingBehavior().GetCosts().Stay;
        }

        public List<MonopolyCell> GetMainPlayerCells(PlayerKey MainPlayerKey)
        {
            return Board.FindAll(c => c.GetBuyingBehavior().GetOwner() == MainPlayerKey);
        }

        public MoneyObligation MakeMoneyBond(in MonopolyPlayer MainPlayer)
        {
            try
            {
                return CalculateBond(MainPlayer);
            }
            catch
            {
                return new MoneyObligation();
            }
        }

        private MoneyObligation CalculateBond(in MonopolyPlayer MainPlayer) 
        {
            MoneyObligation result = new MoneyObligation();
            if (DoesCellHaveAnotherOwner(MainPlayer))
            {
                result.PlayerGettingMoney = Board[MainPlayer.OnCellIndex].GetBuyingBehavior().GetOwner();
                result.PlayerLosingMoney = MainPlayer.Key;
                result.ObligationAmount = Board[MainPlayer.OnCellIndex].GetBuyingBehavior().GetCosts().Stay;
            }
            return result;
        }

        public void UpdateData(List<MonopolyCellUpdate> BoardUpdatedData)
        {
            for (int i = 0; i < BoardUpdatedData.Count; i++)
            {
                Board[i].GetBuyingBehavior().SetOwner(BoardUpdatedData[i].Owner);
                Board[i].GetBuyingBehavior().SetCosts(BoardUpdatedData[i].NewCosts);
            }
        }

        public void EscapeFromIsland()
        {
            MainPlayerTurnsOnIslandRemaining.Value = 0;
        }

        public bool IsAbleToMove()
        {
            if (MainPlayerTurnsOnIslandRemaining.Value > 1)
            {
                MainPlayerTurnsOnIslandRemaining.Value--;
                return false;
            }
            else
            {
                MainPlayerTurnsOnIslandRemaining.Value = 0;
            }

            return true;
        }

        public void CheckIfMainPlayerSteppedOnIsland(MonopolyPlayer MainPlayer)
        {
            if (WillStayOnIsland(MainPlayer))
            {
                MainPlayerTurnsOnIslandRemaining.Value = 3;
            }
        }

        private bool WillStayOnIsland(MonopolyPlayer MainPlayer)
        {
            return Board[MainPlayer.OnCellIndex] is MonopolyIslandCell &&
                   MainPlayerTurnsOnIslandRemaining.Value == 0;
        }

    }
}
