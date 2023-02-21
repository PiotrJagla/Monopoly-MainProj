using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board
{
    public class DataToGetModalParameters
    {
        public List<MonopolyCell> Board;
        public MonopolyPlayer MainPlayer;
        public bool IsThisFirstLap;
    }
}
