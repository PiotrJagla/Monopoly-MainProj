using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.GamesServices.Battleships
{
    public interface BattleshipService
    {
        BattleshipCell GetUserBoardCell(Point2D OnPosition);
        BattleshipCell GetEnemyBoardCell(Point2D OnPosition);

        List<List<BattleshipCell>> GetUserBoard(); 
        List<List<BattleshipCell>> GetEnemyBoard(); 

        void UserBoardClicked(Point2D ClickPoint);

        bool IsUserBoardCorrect();
    }
}
