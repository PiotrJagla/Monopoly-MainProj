using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Models;
using System.Threading.Tasks;

namespace Services.GamesServices.TicTacToe
{
    public interface TicTacToeService
    {
        bool IsEmpty(Point2D Cell);

        void PlayerTurn(Point2D PlayerMove);

        void EnemyTurn();

        char? CheckWinner();

        char GetPoint(Point2D point);

        void Restart();
    }
}
