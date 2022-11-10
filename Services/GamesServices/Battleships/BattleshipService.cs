using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.BattleshipDataStructures;

namespace Services.GamesServices.Battleships
{
    public interface BattleshipService
    {
        List<List<Cell>> GetUserBoard();
        List<List<Cell>> GetUserEnemy();

        void UserBoardClicked(Point2D ClickPoint);
    }
}
