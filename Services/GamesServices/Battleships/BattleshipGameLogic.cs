using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums.BattleshipEnums;
using Models;
using Models.BattleshipDataStructures;

namespace Services.GamesServices.Battleships
{
    public class BattleshipGameLogic : BattleshipService
    {
        private static List<List<Cell>> UserBoard;
        private static List<List<Cell>> EnemyBoard;

        public BattleshipGameLogic()
        {
            InitBoard(UserBoard);
            InitBoard(EnemyBoard);
        }

        private void InitBoard(List<List<Cell>> board)
        {
            board = new List<List<Cell>>();
            for (int iii = 0; iii < 10; ++iii)
            {
                board.Add(new List<Cell>());
                for (int kkk = 0; kkk < 10; ++kkk)
                {
                    board[iii].Add(new Cell());
                }
            }
        }

        public List<List<Cell>> GetUserBoard()
        {
            return UserBoard;
        }

        public List<List<Cell>> GetUserEnemy()
        {
            return EnemyBoard;
        }

        public void UserBoardClicked(Point2D ClickPoint)
        {
            if(IsEmpty(UserBoard, ClickPoint))
                UserBoard[ClickPoint.x][ClickPoint.y].state = CellState.Ship;
            else
                UserBoard[ClickPoint.x][ClickPoint.y].state = CellState.Empty;
        }

        private bool IsThereAShip(List<List<Cell>> board, Point2D point)
        {
            return board[point.y][point.x].state == CellState.Ship;
        }
        private bool IsEmpty(List<List<Cell>> board, Point2D point)
        {
            return board[point.y][point.x].state == CellState.Empty;
        }

    }
}
