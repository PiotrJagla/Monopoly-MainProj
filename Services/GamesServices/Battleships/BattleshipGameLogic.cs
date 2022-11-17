using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Enums;
using Models;
using Validation;

namespace Services.GamesServices.Battleships
{
    public class BattleshipGameLogic : BattleshipService
    {
        private static List<List<BattleshipCell>> UserBoard;
        private static List<List<BattleshipCell>> EnemyBoard;

        public BattleshipGameLogic()
        {
            InitBoard(ref UserBoard);
            InitBoard(ref EnemyBoard);
        }

        private void InitBoard(ref List<List<BattleshipCell>> board)
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
            if (IsEmpty(UserBoard, ClickPoint))
                UserBoard[ClickPoint.y][ClickPoint.x].ChangeState(BattleshipCellState.Ship);
            else
                UserBoard[ClickPoint.y][ClickPoint.x].ChangeState(BattleshipCellState.Empty);
        }

        
        private bool IsEmpty(List<List<BattleshipCell>> board, Point2D point)
        {
            return board[point.y][point.x].state == BattleshipCellState.Empty;
        }

        public bool IsUserBoardCorrect()
        {
            //GiveBattleshipShips();
            List<Point2D> AllShipsPositions = GetShipsPositionsFrom(UserBoard);
            return IsShipsDistributionCorrect(UserBoard, AllShipsPositions) && IsThereCorrectNumberOfShips(UserBoard, AllShipsPositions);
        }

        private void GiveBattleshipShips()
        {
            List<string> ValidDistributionVisualization = new List<string>
            {
                "-XX-------",
                "----X--X--",
                "-XX-------",
                "-----X----",
                "-X------X-",
                "-X-X----X-",
                "-X-X----X-",
                "----------",
                "-XXXX-----",
                "---------X",
            };

            List<Point2D> ValidDsitribution = new List<Point2D>();

            for (int y = 0; y < Constants.BattleshipBoardSize.y; ++y)
            {
                for (int x = 0; x < Constants.BattleshipBoardSize.x; ++x)
                {
                    if (ValidDistributionVisualization[y].ElementAt(x) == 'X')
                        ValidDsitribution.Add(new Point2D(x, y));
                }
            }

            for (int iii = 0; iii < ValidDsitribution.Count; ++iii)
                UserBoardClicked(ValidDsitribution[iii]);
        }

        private List<Point2D> GetShipsPositionsFrom(in List<List<BattleshipCell>> board)
        {
            List<Point2D> Result = new List<Point2D>();

            for(int y = 0; y < Constants.BattleshipBoardSize.y; ++y)
            {
                for(int x = 0; x < Constants.BattleshipBoardSize.x; ++x)
                {
                    Point2D CurrentPoint = new Point2D(x, y);
                    if (IsThereAShip(board, CurrentPoint))
                        Result.Add(CurrentPoint);
                }
            }

            return Result;
        }

        private bool IsThereAShip(in List<List<BattleshipCell>> board, Point2D point)
        {
            return board[point.y][point.x].state == BattleshipCellState.Ship;
        }

        private bool IsShipsDistributionCorrect(in List<List<BattleshipCell>> board, in List<Point2D> ShipsPositions)
        {
            foreach(Point2D ShipPos in ShipsPositions)
            {
                if(IsShipAtIncorrectPosition(board, ShipPos) == true)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsShipAtIncorrectPosition(in List<List<BattleshipCell>> board, Point2D ShipPos)
        {
            List<Point2D> PointsToCheck = new List<Point2D> {
                new Point2D(ShipPos.x-1,ShipPos.y-1),new Point2D(ShipPos.x+1,ShipPos.y+1),new Point2D(ShipPos.x+1,ShipPos.y-1),new Point2D(ShipPos.x-1,ShipPos.y+1)
            };

            foreach(Point2D PointToCheck in PointsToCheck)
            {
                if(ValidateIndex.IsWithin2DArray(PointToCheck, Constants.BattleshipBoardSize) &&
                    IsThereAShip(board, PointToCheck))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsThereCorrectNumberOfShips(in List<List<BattleshipCell>> board, in List<Point2D> ShipsPositions)
        {
            return IsCorrectShipsNumberOfSize(1, ShipsPositions) &&
                   IsCorrectShipsNumberOfSize(2, ShipsPositions) &&
                   IsCorrectShipsNumberOfSize(3, ShipsPositions) &&
                   IsCorrectShipsNumberOfSize(4, ShipsPositions);
        }

        private bool IsCorrectShipsNumberOfSize(int ShipSize, in List<Point2D> AllShipsPositions)
        {
            Console.WriteLine($"Ship Size: {ShipSize} | ");
            int ShipsFound= 0;
            int ShipsExpectedNumber = 5 - ShipSize;
            List<Point2D> CheckedShipsPositions = new List<Point2D>();

            foreach (Point2D ShipPos in AllShipsPositions)
            {
                if (CheckedShipsPositions.FirstOrDefault(Pos => Pos.x == ShipPos.x && Pos.y == ShipPos.y) == null)
                {
                    Point2D NextShipTileDir = GetNextShipTileDirection(ShipPos, AllShipsPositions);
                    int ShipTiles = 0;

                    Point2D NextShipPos = new Point2D(ShipPos.x, ShipPos.y);
                    while(AllShipsPositions.FirstOrDefault(Pos => Pos.x == NextShipPos.x && Pos.y == NextShipPos.y) != null)
                    {
                        CheckedShipsPositions.Add(new Point2D(NextShipPos.x, NextShipPos.y));
                        NextShipPos.x += NextShipTileDir.x;
                        NextShipPos.y += NextShipTileDir.y;
                        ++ShipTiles;
                        if (NextShipPos.x == ShipPos.x && NextShipPos.y == ShipPos.y) break;
                    }

                    if (ShipTiles == ShipSize)
                        ++ShipsFound;
                }
            }

            return ShipsFound == ShipsExpectedNumber;
        }

        

        private Point2D GetNextShipTileDirection(Point2D shipPosition, in List<Point2D> AllShipsPositions)
        {
            Point2D NoDirection = new Point2D(0,0);

            List<Point2D> PossibleDirections = new List<Point2D>
            {
                new Point2D(1,0), new Point2D(-1,0), new Point2D(0,1), new Point2D(0,-1)
            };

            foreach(Point2D Direction in PossibleDirections)
            {
                Point2D NextPosition = new Point2D(shipPosition.x + Direction.x, shipPosition.y + Direction.y);

                if (AllShipsPositions.FirstOrDefault(Point => Point.x == NextPosition.x && Point.y == NextPosition.y) != null)
                    return Direction;
            }
            return NoDirection;
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
