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

        void EnemyAttack(Point2D OnPoint);

        bool IsUserBoardCorrect();

        bool IsShipHit(Point2D OnPoint);

        void AttackOnEnemyBoard(BattleshipCell AttackPoint, bool IsThisShipDestroyed);

        bool DoesEnemyDestroyedShip(Point2D ShipPosition);

        bool IsGameOver();

    }
}
