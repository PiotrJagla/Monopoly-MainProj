using Models;
using Models.Monopoly;
using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board
{
    public class MonopolyBoard
    {
        private List<MonopolyCell> Board = new List<MonopolyCell>();

        public MonopolyBoard()
        {
            Board = new List<MonopolyCell>();
            InitBoard();
        }

        private void InitBoard()
        {
            //Board.Add(new MonopolyNationCell(new Costs(50,30), Nation.Poland));
            Board.Add(new StartCell());
            Board.Add(new MonopolyNationCell(new Costs(50,30), Nation.Poland));
            Board.Add(new MonopolyNationCell(new Costs(80, 40), Nation.Poland));
            Board.Add(new MonopolyNationCell(new Costs(130,70), Nation.France));
            Board.Add(new MonopolyNationCell(new Costs(110, 50), Nation.France));
            Board.Add(new MonopolyNationCell(new Costs(150,100), Nation.France));
            Board.Add(new MonopolyNationCell(new Costs(180, 140), Nation.Argentina));
            Board.Add(new MonopolyNationCell(new Costs(250,200), Nation.Argentina));
            Board.Add(new MonopolyNationCell(new Costs(210, 150), Nation.Argentina));
        }

        public List<MonopolyCell> GetBoard()
        {
            return Board;
        }

        public bool DontHaveMoneyToPay(MonopolyPlayer Debtor)
        {
            return CanAffordBuying(Debtor) == false && DoesCellHaveAnotherOwner(Debtor) == true;
        }

        public bool DoesCellHaveAnotherOwner(MonopolyPlayer PlayerStepped)
        {
            return IsNoOneCell(PlayerStepped.OnCellIndex) == false && IsPlayerCellOwner(PlayerStepped) == false;
        }

        public bool IsPossibleToBuyCell(MonopolyPlayer buyer)
        {
            return CanAffordBuying(buyer) && IsNoOneCell(buyer.OnCellIndex);
        }

        private bool CanAffordBuying(MonopolyPlayer buyer)
        {
            return buyer.MoneyOwned >= CellBuyCost(buyer.OnCellIndex);
        }

        private int CellBuyCost(int index)
        {
            return Board[index].GetCosts().Buy;
        }

        public bool IsNoOneCell(int CellIndex)
        {
            return Board[CellIndex].GetOwner() == PlayerKey.NoOne;
        }

        private bool IsPlayerCellOwner(MonopolyPlayer Player)
        {
            return Board[Player.OnCellIndex].GetOwner() == Player.Key;
        }

        public MonopolyCell GetCell(int Index)
        {
            return Board[Index];
        }

        public void CheckForMonopolOf(MonopolyPlayer aPlayer)
        {
            if(DoHaveMonopol(aPlayer))
            {
                ApplyMonopolCostsChanges(Board[aPlayer.OnCellIndex].GetNation());
            }
        }

        private bool DoHaveMonopol(MonopolyPlayer aPlayer)
        {
            foreach (var cell in Board)
            {
                if(cell.GetNation() == Board[aPlayer.OnCellIndex].GetNation() &&
                    cell.GetOwner() != aPlayer.Key)
                {
                    return false;
                }
            }
            return true;
        }

        private void ApplyMonopolCostsChanges(Nation OnNation)
        {
            List<MonopolyCell> MonopolNationCells = Board.FindAll(cell => cell.GetNation() == OnNation);
            foreach (var cell in MonopolNationCells)
            {
                int MonopolCostMultiplayer = 2;
                cell.SetCosts(new Costs( cell.GetCosts().Buy, cell.GetCosts().Stay*MonopolCostMultiplayer ));
            }
        }

        
    }
}
