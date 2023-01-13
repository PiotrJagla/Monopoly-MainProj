using Services.GamesServices.Monopoly.Board.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Behaviours
{
    public class NoMonopolBehaviour : MonopolBehaviour
    {
        public List<MonopolyCell> GetMonopolOff(in List<MonopolyCell> Board, int OnCell)
        {
            return Board;
        }

        public List<MonopolyCell> UpdateBoardMonopol(in List<MonopolyCell> Board, int OnCell)
        {
            return Board;
        }
    }
}
