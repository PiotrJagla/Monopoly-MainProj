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
            List<Point2D> AllShipsPositions = GetShipsPositionsFrom(UserBoard);
            return IsShipsDistributionCorrect(UserBoard, AllShipsPositions) && IsShipsNumberCorrect(UserBoard, AllShipsPositions);
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

        private bool IsShipsDistributionCorrect(in List<List<BattleshipCell>> board, in List<Point2D> AllShipsPositions)
        {
            foreach(Point2D ShipPosition in AllShipsPositions)
            {
                if(IsShipOnIncorrectPosition(board, ShipPosition) == true)
                    return false;
            }
            return true;
        }

        private bool IsShipOnIncorrectPosition(in List<List<BattleshipCell>> board, Point2D ShipPosition)
        {
            List<Point2D> PositionsToCheck = new List<Point2D> {
                new Point2D(ShipPosition.x - 1,ShipPosition.y - 1),
                new Point2D(ShipPosition.x + 1,ShipPosition.y + 1),
                new Point2D(ShipPosition.x + 1,ShipPosition.y - 1),
                new Point2D(ShipPosition.x - 1,ShipPosition.y + 1)
            };

            foreach(Point2D IncorrectPosition in PositionsToCheck)
            {
                if(ValidateIndex.IsWithin2DArray(IncorrectPosition, Constants.BattleshipBoardSize) &&
                    IsThereAShip(board, IncorrectPosition))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsShipsNumberCorrect(in List<List<BattleshipCell>> board, in List<Point2D> AllShipsPositions)
        {
            return IsCorrectShipsNumberOfSize(1, AllShipsPositions) &&
                   IsCorrectShipsNumberOfSize(2, AllShipsPositions) &&
                   IsCorrectShipsNumberOfSize(3, AllShipsPositions) &&
                   IsCorrectShipsNumberOfSize(4, AllShipsPositions);
        }

        private bool IsCorrectShipsNumberOfSize(int ShipSize, in List<Point2D> AllShipsPositions)
        {
            int ShipsFound= 0;
            List<Point2D> CheckedShipsPositions = new List<Point2D>();

            foreach (Point2D ShipPos in AllShipsPositions)
            {
                //if (CheckedShipsPositions.FirstOrDefault(Pos => Pos.x == ShipPos.x && Pos.y == ShipPos.y) == null)
                //{
                //    Point2D NextShipTileDir = GetNextShipTileDirection(ShipPos, AllShipsPositions);

                //    int ShipTiles = 0;
                //    Point2D NextShipPos = new Point2D(ShipPos.x, ShipPos.y);
                //    while (AllShipsPositions.FirstOrDefault(Pos => Pos.x == NextShipPos.x && Pos.y == NextShipPos.y) != null)
                //    {
                //        CheckedShipsPositions.Add(new Point2D(NextShipPos.x, NextShipPos.y));
                //        NextShipPos.x += NextShipTileDir.x;
                //        NextShipPos.y += NextShipTileDir.y;
                //        ++ShipTiles;
                //        //if (NextShipPos.x == ShipPos.x && NextShipPos.y == ShipPos.y) break;
                //    }

                //    if (ShipTiles == ShipSize)
                //        ++ShipsFound;
                //}

                if (GetShipSize(ShipPos, AllShipsPositions, ref CheckedShipsPositions) == ShipSize)
                    ++ShipsFound;
            }            

            int ShipsExpectedNumber = 5 - ShipSize;
            return ShipsFound == ShipsExpectedNumber;
        }

        private int GetShipSize(Point2D FirstShipPosition, in List<Point2D> AllShipsPositions, ref List<Point2D> CheckedPositions)
        {
            if (CheckedPositions.FirstOrDefault(Pos => Pos.x == FirstShipPosition.x && Pos.y == FirstShipPosition.y) == null)
            {
                Point2D NextShipTileDir = GetNextShipTileDirection(FirstShipPosition, AllShipsPositions);

                int ShipTiles = 0;
                Point2D NextShipPos = new Point2D(FirstShipPosition.x, FirstShipPosition.y);
                while (AllShipsPositions.FirstOrDefault(Pos => Pos.x == NextShipPos.x && Pos.y == NextShipPos.y) != null)
                {
                    CheckedPositions.Add(new Point2D(NextShipPos.x, NextShipPos.y));
                    NextShipPos.x += NextShipTileDir.x;
                    NextShipPos.y += NextShipTileDir.y;
                    ++ShipTiles;
                }

                return ShipTiles;
            }
            else
                return 0;
        }

        private Point2D GetNextShipTileDirection(Point2D shipPosition, in List<Point2D> AllShipsPositions)
        {
            List<Point2D> PossibleDirections = new List<Point2D>
            {
                new Point2D(1,0), new Point2D(-1,0), new Point2D(0,1), new Point2D(0,-1)
            };

            foreach(Point2D Direction in PossibleDirections)
            {
                Point2D NextShipTile = new Point2D(shipPosition.x + Direction.x, shipPosition.y + Direction.y);

                if (AllShipsPositions.FirstOrDefault(Point => Point.x == NextShipTile.x && Point.y == NextShipTile.y) != null)
                    return Direction;
            }

            Point2D OneTileShip = new Point2D(Constants.BattleshipBoardSize.x + 1, Constants.BattleshipBoardSize.y + 1);
            return OneTileShip;
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
