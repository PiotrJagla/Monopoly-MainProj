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
                    board[iii][kkk].CellPoint.x = kkk;
                    board[iii][kkk].CellPoint.y = iii;
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

        public void EnemyAttack(Point2D OnPoint)
        {
            if (IsEmpty(UserBoard, OnPoint))
                UserBoard[OnPoint.y][OnPoint.x].ChangeState(BattleshipCellState.Checked);
            else if (IsThereAShip(UserBoard, OnPoint))
                ShipHit(OnPoint);
        }

        private void ShipHit(Point2D OnPoint)
        {
            UserBoard[OnPoint.y][OnPoint.x].ChangeState(BattleshipCellState.DestroyedShip);
            IsShipDestroyed(OnPoint);
        }

        private bool IsShipDestroyed(Point2D ShipPoint)
        {
            List<Point2D> ShipPoints = new List<Point2D>();
            Point2D NextTileDirection = GetNextShipTileDirection(ShipPoint);
            Point2D NextTileOppositeDirection = GetNextShipTileDirection(ShipPoint);
            NextTileOppositeDirection.x *= -1;
            NextTileOppositeDirection.y *= -1;

            if(NextTileDirection.isSameAs(new Point2D(1,1)))
            {//This is 1-Tile ship if Direction is (1,1)
                ShipPoints.Add(new Point2D(ShipPoint.x, ShipPoint.y));
                return true;
            }

            Point2D NextShipPosition = ShipPoint;

            while(true)
            {
                if (IsThereAShip(UserBoard, NextShipPosition))
                    return false;
                else
                    ShipPoints.Add(new Point2D(NextShipPosition.x, NextShipPosition.y));

                NextShipPosition.x += NextTileDirection.x;
                NextShipPosition.y += NextTileDirection.y;

                if (IsEmpty(UserBoard, NextShipPosition))
                    break;
            }

            NextTileDirection = ShipPoint;

            while (true)
            {
                NextShipPosition.x += NextTileOppositeDirection.x;
                NextShipPosition.y += NextTileOppositeDirection.y;

                if (IsThereAShip(UserBoard, NextShipPosition))
                    return false;
                else
                    ShipPoints.Add(new Point2D(NextShipPosition.x, NextShipPosition.y));

                if (IsEmpty(UserBoard, NextShipPosition))
                    break;
            }

            RevealAdjancedPointsToShip(ShipPoints);
            return true;
        }

        private void RevealAdjancedPointsToShip(in List<Point2D> Ship)
        {
            Ship.Sort(
                (point1, point2) => point1.x);
        }

        public void AttackOnEnemyBoard(Point2D AttackPoint)
        {

        }


        private bool IsEmpty(List<List<BattleshipCell>> board, Point2D point)
        {
            return board[point.y][point.x].state == BattleshipCellState.Empty ||
                   board[point.y][point.x].state == BattleshipCellState.Checked;
        }

        private void DefaultShipsPositions()
        {
            List<string> ValidDistributionVisualization = new List<string>
            {
                "XX--------",
                "-------XXX",
                "----X-----",
                "----X---X-",
                "X---------",
                "X--X-X----",
                "X----X----",
                "X--X------",
                "--------X-",
                "XXX-------",
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

        public bool IsUserBoardCorrect()
        {
            //DefaultShipsPositions();
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

            foreach (Point2D ShipPosition in AllShipsPositions)
            {
                if (GetShipSize(ShipPosition, AllShipsPositions, ref CheckedShipsPositions) == ShipSize)
                    ++ShipsFound;
            }
            
            int ShipsExpectedNumber = 5 - ShipSize;
            return ShipsFound == ShipsExpectedNumber;
        }

        private int GetShipSize(Point2D FirstShipPosition, in List<Point2D> AllShipsPositions, ref List<Point2D> CheckedPositions)
        {
            if (CheckedPositions.FirstOrDefault(Pos => Pos.x == FirstShipPosition.x && Pos.y == FirstShipPosition.y) == null)
            {
                return CountShipTiles(
                    new Point2D(FirstShipPosition.x, FirstShipPosition.y),
                    GetNextShipTileDirection(FirstShipPosition),
                    AllShipsPositions, ref CheckedPositions
                );
            }
            else
                return 0;
        }

        private int CountShipTiles(Point2D NextShipPosition,in Point2D NextPositionDirection, in List<Point2D> AllShipsPositions, ref List<Point2D> CheckedPositions)
        {
            int ShipTiles = 0;
            while (AllShipsPositions.FirstOrDefault(Pos => Pos.x == NextShipPosition.x && Pos.y == NextShipPosition.y) != null)
            {
                CheckedPositions.Add(new Point2D(NextShipPosition.x, NextShipPosition.y));
                NextShipPosition.x += NextPositionDirection.x;
                NextShipPosition.y += NextPositionDirection.y;
                ++ShipTiles;
            }
            return ShipTiles;
        }

        private Point2D GetNextShipTileDirection(Point2D shipPosition)
        {
            List<Point2D> PossibleDirections = new List<Point2D>
            {
                new Point2D(1,0), new Point2D(-1,0), new Point2D(0,1), new Point2D(0,-1)
            };

            foreach(Point2D Direction in PossibleDirections)
            {
                Point2D NextShipTile = new Point2D(shipPosition.x + Direction.x, shipPosition.y + Direction.y);
                
                if (ValidateIndex.IsWithin2DArray(NextShipTile, Constants.BattleshipBoardSize) && IsThereAShip(UserBoard, NextShipTile))
                    return Direction;
            }
            Point2D OneTileShip = new Point2D(1,1);
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
