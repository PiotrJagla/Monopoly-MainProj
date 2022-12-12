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
                Board.Last().OwnedBy = PlayerKey.LastNumber;
            }
        }


        public List<MonopolyCell> GetBoard()
        {
            return Board;
        }
    }
}
