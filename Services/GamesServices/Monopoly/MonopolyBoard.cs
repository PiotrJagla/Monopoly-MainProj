using Models.Monopoly;
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
            }
        }


        public List<MonopolyCell> GetBoard()
        {
            return Board;
        }
    }
}
