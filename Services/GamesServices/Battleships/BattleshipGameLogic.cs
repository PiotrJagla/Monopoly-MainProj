using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums.BattleshipEnums;
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

        public List<List<Cell>> GetUserBoard()
        {
            return UserBoard;
        }

        public List<List<Cell>> GetUserEnemy()
        {
            return EnemyBoard;
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
    }
}
