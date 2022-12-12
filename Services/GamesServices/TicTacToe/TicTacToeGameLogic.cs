using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Services.GamesServices.TicTacToe
{
    public class TicTacToeGameLogic : TicTacToeService
    {
        private static List<List<char>> board;
        private static Dictionary<char?, int> EventScore;


        public TicTacToeGameLogic()
        {
            InitBoard();

            EventScore = new Dictionary<char?, int>();
            EventScore.Add(Consts.TicTacToe.Player, -1);
            EventScore.Add(Consts.TicTacToe.Tie, 0);
            EventScore.Add(Consts.TicTacToe.Enemy, 1);
        }

        private void InitBoard()
        {
            board = new List<List<char>>();
            for (int y = 0; y < Consts.TicTacToe.BoardSize.y; y++)
            {
                board.Add(new List<char>());
                for (int x = 0; x < Consts.TicTacToe.BoardSize.x; x++)
                {
                    board[y].Add(Consts.TicTacToe.Empty);
                }
            }
        }

        public void Restart()
        {
            InitBoard();
        }

        

        public void EnemyTurn()
        {
            int bestScore = -999999;
            Point2D bestMove = new Point2D();
            for (int y = 0; y < Consts.TicTacToe.BoardSize.y; y++)
            {
                for (int x = 0; x < Consts.TicTacToe.BoardSize.x; x++)
                {
                    if(IsEmpty(new Point2D(x,y)))
                    {
                        board[y][x] = Consts.TicTacToe.Enemy;
                        int score = minimax(ref board,0,false);
                        board[y][x] = Consts.TicTacToe.Empty;
                        if (score >= bestScore)
                        {
                            bestScore = score;
                            bestMove.x = x; bestMove.y = y;
                        }
                    }
                }
            }
            board[bestMove.y][bestMove.x] = Consts.TicTacToe.Enemy;
        }

        private int minimax(ref List<List<char>> board,int depth, bool isMaximizing)
        {
            char? whoWon = CheckWinner();
            if(whoWon != null)
            {
                return EventScore[whoWon];
            }

            if (isMaximizing)
            {
                int bestScore = -999999;
                for (int y = 0; y < Consts.TicTacToe.BoardSize.y; y++)
                {
                    for (int x = 0; x < Consts.TicTacToe.BoardSize.x; x++)
                    {
                        if(IsEmpty(new Point2D(x,y)))
                        {
                            board[y][x] = Consts.TicTacToe.Enemy;
                            int score = minimax(ref board, depth + 1, false);
                            board[y][x] = Consts.TicTacToe.Empty;
                            bestScore= Math.Max(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = 999999;
                for (int y = 0; y < Consts.TicTacToe.BoardSize.y; y++)
                {
                    for (int x = 0; x < Consts.TicTacToe.BoardSize.x; x++)
                    {
                        if (IsEmpty(new Point2D(x, y)))
                        {
                            board[y][x] = Consts.TicTacToe.Player;
                            int score = minimax(ref board, depth + 1, true);
                            board[y][x] = Consts.TicTacToe.Empty;
                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
        }

        public char? CheckWinner()
        {
            if (IfWin(Consts.TicTacToe.Player))
                return Consts.TicTacToe.Player;

            if (IfWin(Consts.TicTacToe.Enemy))
                return Consts.TicTacToe.Enemy;

            if (IsBoardFull())
                return Consts.TicTacToe.Tie;

            return null;
        }

        bool IfWin(char Player)
        {
            return CheckHorizontal(Player) || CheckVertical(Player) || CheckDiagonal(Player);
        }

        bool CheckHorizontal(char Player)
        {
            for (int y = 0; y < Consts.TicTacToe.BoardSize.y; ++y)
            {
                if (board[y][0] == Player && board[y][1] == Player && board[y][2] == Player)
                    return true;
            }
            return false;
        }

        bool CheckVertical(char Player)
        {
            for (int x = 0; x < Consts.TicTacToe.BoardSize.x; ++x)
            {
                if (board[0][x] == Player && board[1][x] == Player && board[2][x] == Player)
                    return true;
            }
            return false;
        }

        bool CheckDiagonal(char Player)
        {
            if (board[0][0] == Player && board[1][1] == Player && board[2][2] == Player)
                return true;

            if (board[2][0] == Player && board[1][1] == Player && board[0][2] == Player)
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            for (int y = 0; y < Consts.TicTacToe.BoardSize.y; y++)
            {
                for (int x = 0; x < Consts.TicTacToe.BoardSize.x; x++)
                {
                    if (IsEmpty(new Point2D(x, y)))
                        return false;
                }
            }
            return true;
        }

        public bool IsEmpty(Point2D Cell)
        {
            return board[Cell.y][Cell.x] == Consts.TicTacToe.Empty;
        }

        public char GetPoint(Point2D point)
        {
            return board[point.y][point.x];
        }

        

        public void PlayerTurn(Point2D PlayerMove)
        {
            board[PlayerMove.y][PlayerMove.x] = Consts.TicTacToe.Player;
        }
    }
}
