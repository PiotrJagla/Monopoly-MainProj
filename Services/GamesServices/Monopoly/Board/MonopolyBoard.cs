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
            //for (int i = 1; i < 10; i++)
            //{
            //    Board.Add(new MonopolyCell());
            //    Board.Last().MoneyNeededFor.Buy =  Consts.Monopoly.BuyCost;
            //    Board.Last().MoneyNeededFor.Stay = Consts.Monopoly.StayCost;
            //    Board.Last().OwnedBy = PlayerKey.NoOne;
            //}
            Board.Add(new MonopolyCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.Poland));
            Board.Add(new MonopolyCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.Poland));
            Board.Add(new MonopolyCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.France));
            Board.Add(new MonopolyCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.France));
            Board.Add(new MonopolyCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.France));
            Board.Add(new MonopolyCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.Argentina));
            Board.Add(new MonopolyCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.Argentina));
            Board.Add(new MonopolyCell(new Costs(Consts.Monopoly.BuyCost, Consts.Monopoly.StayCost), PlayerKey.NoOne, Nation.Argentina));
        }

        private MonopolyCell AddCell(int Number, int MoneyForBuy, int MoneyForStay, PlayerKey Owner, Nation nation)
        {
            //This is not in constructor of monopolycell because then
            MonopolyCell result = new MonopolyCell();
            //result.Number = Number;
            result.MoneyNeededFor.Buy = MoneyForBuy;
            result.MoneyNeededFor.Stay = MoneyForStay;
            result.OwnedBy = Owner;
            result.OfNation = nation;
            return result;
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
            return Board[index].MoneyNeededFor.Buy;
        }

        public bool DidStepOnSomeonesCell(MonopolyPlayer MainPlayer)
        {
            return Board[MainPlayer.OnCellIndex].OwnedBy != PlayerKey.NoOne && Board[MainPlayer.OnCellIndex].OwnedBy != MainPlayer.Key;
        }

        public MonopolyCell GetCell(int Index)
        {
            return Board[Index];
        }
    }
}
