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
        private static List<MonopolyCell> Board = new List<MonopolyCell>();

        public MonopolyBoard()
        {
            InitBoard();
        }

        private void InitBoard()
        {
            Board.Add(new MonopolyNationCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.Poland));
            Board.Add(new MonopolyNationCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.Poland));
            Board.Add(new MonopolyNationCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.France));
            Board.Add(new MonopolyNationCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.France));
            Board.Add(new MonopolyNationCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.France));
            Board.Add(new MonopolyNationCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.Argentina));
            Board.Add(new MonopolyNationCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.Argentina));
            Board.Add(new MonopolyNationCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.Argentina));
        }

        public List<MonopolyCell> GetBoard()
        {
            return Board;
        }

        public bool CanAffordBuying(int MoneyAmount, int CellIndex)
        {
            return MoneyAmount >= CellBuyCost(CellIndex);
        }
        private int CellBuyCost(int index)
        {
            return Board[index].GetCosts().Buy;
        }

        public bool DoesCellHaveAnotherOwner(MonopolyPlayer PlayerStepped)
        {
            return IsNoOneCell(PlayerStepped.OnCellIndex) == false && IsPlayerCellOwner(PlayerStepped) == false;
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
    }
}
