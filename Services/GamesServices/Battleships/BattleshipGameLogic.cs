using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
using Models;

namespace Services.GamesServices.Battleships
{
    public class BattleshipGameLogic : BattleshipService
    {
        private static List<List<BattleshipCell>> UserBoard;
        private static List<List<BattleshipCell>> EnemyBoard;

        public BattleshipGameLogic()
        {
            InitBoard(UserBoard);
            InitBoard(EnemyBoard);
        }

        private void InitBoard(List<List<BattleshipCell>> board)
        {
            board = new List<List<BattleshipCell>>();
            for (int iii = 0; iii < Constants.BattleshipBoardSize.y; ++iii)
            {
                board.Add(new List<BattleshipCell>());
                for (int kkk = 0; kkk < Constants.BattleshipBoardSize.x; ++kkk)
                {
                    board[iii].Add(new BattleshipCell());
                }
            }
        }

        public BattleshipCell GetUserBoardCell(Point2D OnPosition)
        {
            return UserBoard[OnPosition.y][OnPosition.x];
        }

        public BattleshipCell GetEnemyBoardCell(Point2D OnPosition)
        {
            return EnemyBoard[OnPosition.y][OnPosition.x];
        }

        public void UserBoardClicked(Point2D ClickPoint)
        {
            if(IsEmpty(UserBoard, ClickPoint))
                UserBoard[ClickPoint.y][ClickPoint.x].state = BattleshipCellState.Ship;
            else
                UserBoard[ClickPoint.y][ClickPoint.x].state = BattleshipCellState.Empty;
        }

        private bool IsThereAShip(List<List<BattleshipCell>> board, Point2D point)
        {
            return board[point.y][point.x].state == BattleshipCellState.Ship;
        }
        private bool IsEmpty(List<List<BattleshipCell>> board, Point2D point)
        {
            return board[point.y][point.x].state == BattleshipCellState.Empty;
        }

        public bool IsUserBoardCorrect()
        {
            return false;
        }

        public List<List<BattleshipCell>> GetUserBoard()
        {
            return UserBoard;
        }

        public List<List<BattleshipCell>> GetEnemyBoard()
        {
            return EnemyBoard;
        }
    }
}
