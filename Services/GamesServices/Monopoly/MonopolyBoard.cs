using Models;
using Models.Monopoly;
using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly
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
            for (int i = 1; i < 10; i++)
            {
                Board.Add(new MonopolyCell());
                Board.Last().Number = i;
                Board.Last().CellCosts.Buy = Consts.Monopoly.BuyCost;
                Board.Last().CellCosts.Stay = Consts.Monopoly.StayCost;
                Board.Last().OwnedBy = PlayerKey.NoOne;
            }
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
            return Board[index].CellCosts.Buy;
        }
    }
}
