﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Models.Battleship;
using Models;
using Validation;
using Enums.Battleship;

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
            for (int iii = 0; iii < Consts.Battleship.BoardSize.y; ++iii)
            {
                board.Add(new List<BattleshipCell>());
                for (int kkk = 0; kkk < Consts.Battleship.BoardSize.x; ++kkk)
                {
                    board[iii].Add(new BattleshipCell());
                    board[iii][kkk].CellPoint.x = kkk;
                    board[iii][kkk].CellPoint.y = iii;
                }
            }
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
            else if (IsThereFloatingShip(UserBoard, OnPoint))
                ShipHit(OnPoint);
        }

        private void ShipHit(Point2D OnPoint)
        {
            UserBoard[OnPoint.y][OnPoint.x].ChangeState(BattleshipCellState.DestroyedShip);
            IsShipDestroyed(OnPoint,ref UserBoard);
        }

        public bool IsShipDestroyed(Point2D ShipPosition, ref List<List<BattleshipCell>> Board)
        {
            List<Point2D> ShipPoints = new List<Point2D>();

            Point2D NextTileDirection = GetNextShipTileDirection(ShipPosition,ref Board);
            if (IsFloatingShipFound(ShipPosition, NextTileDirection, ref ShipPoints, Board))
                return false;

            Point2D NextTileOppositeDirection = new Point2D(NextTileDirection.x * (-1), NextTileDirection.y * (-1));
            if (IsFloatingShipFound(ShipPosition, NextTileOppositeDirection, ref ShipPoints, Board))
                return false;

            RevealAdjancedPointsToShip(ref ShipPoints, ref Board);
            return true;
        }

        private bool IsFloatingShipFound(in Point2D StartingPoint, in Point2D MovingDirection, ref List<Point2D> ShipPoints, in List<List<BattleshipCell>> Board)
        {
            Point2D NextShipTilePosition = new Point2D(StartingPoint.x, StartingPoint.y);

            while ( !(ValidateIndex.IsWithin2DArray(NextShipTilePosition, Consts.Battleship.BoardSize) == false || IsEmpty(Board, NextShipTilePosition)) )
            {
                if (IsThereFloatingShip(Board, NextShipTilePosition))
                    return true;
                else if(IsThereDestroyedShip(Board, NextShipTilePosition))
                    ShipPoints.Add(new Point2D(NextShipTilePosition.x, NextShipTilePosition.y));

                NextShipTilePosition.x += MovingDirection.x;
                NextShipTilePosition.y += MovingDirection.y;
            }

            return false;
        }

        private void RevealAdjancedPointsToShip(ref List<Point2D> Ship, ref List<List<BattleshipCell>> Board)
        {
            if (Ship.Count == 0) return;

            Ship = Ship.GroupBy(point => new { point.x, point.y }).Select(g => g.First()).ToList(); //Removing Duplicates
            Ship.Sort((point1, point2) => point1.x.CompareTo(point2.x));
            Ship.Sort((point1, point2) => point1.y.CompareTo(point2.y));

            for(int y = Ship[0].y - 1; y <= Ship.Last().y + 1; ++y)
            {
                for(int x = Ship[0].x - 1; x <= Ship.Last().x + 1; ++x)
                {
                    if(Ship.FirstOrDefault(point => point.x == x && point.y == y) == null &&
                        ValidateIndex.IsWithin2DArray(new Point2D(x,y), Consts.Battleship.BoardSize))
                    {
                        Board[y][x].ChangeState(BattleshipCellState.Checked);
                    }
                }
            }
        }

        public void AttackOnEnemyBoard(BattleshipCell AttackPoint, bool IsThisShipDestroyed)
        {
            EnemyBoard[AttackPoint.CellPoint.y][AttackPoint.CellPoint.x].ChangeState(AttackPoint.state);
            if(IsThisShipDestroyed)
            {
                EnemyShipDestroyed(AttackPoint.CellPoint,ref EnemyBoard);
            }
        }

        private void EnemyShipDestroyed(Point2D ShipPosition, ref List<List<BattleshipCell>> Board)
        {
            IsShipDestroyed(ShipPosition,ref Board);
        }
        

        private void DefaultShipsPositions()
        {
            List<string> ValidDistributionVisualization = new List<string>
            {
                "--XXXX----",
                "-------XXX",
                "X---X-----",
                "X---X---X-",
                "----------",
                "---X-X----",
                "-----X----",
                "---X------",
                "--------X-",
                "XXX-------",
            };

            List<Point2D> ValidDsitribution = new List<Point2D>();

            for (int y = 0; y < Consts.Battleship.BoardSize.y; ++y)
            {
                for (int x = 0; x < Consts.Battleship.BoardSize.x; ++x)
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
            List<Point2D> AllShipsPositions = GetShipsPositions();
            return IsShipsDistributionCorrect(UserBoard, AllShipsPositions) && IsShipsNumberCorrect(UserBoard, AllShipsPositions);
        }

        private List<Point2D> GetShipsPositions()
        {
            List<Point2D> Result = new List<Point2D>();

            for(int y = 0; y < Consts.Battleship.BoardSize.y; ++y)
            {
                for(int x = 0; x < Consts.Battleship.BoardSize.x; ++x)
                {
                    Point2D CurrentPoint = new Point2D(x, y);
                    if (IsThereFloatingShip(UserBoard, CurrentPoint))
                        Result.Add(CurrentPoint);
                }
            }

            return Result;
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
                if(ValidateIndex.IsWithin2DArray(IncorrectPosition, Consts.Battleship.BoardSize) &&
                    IsThereFloatingShip(board, IncorrectPosition))
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
                    GetNextShipTileDirection(FirstShipPosition, ref UserBoard),
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

        private Point2D GetNextShipTileDirection(Point2D shipPosition, ref List<List<BattleshipCell>> board)
        {
            List<Point2D> PossibleDirections = new List<Point2D>
            {
                new Point2D(1,0), new Point2D(-1,0), new Point2D(0,1), new Point2D(0,-1)
            };

            foreach(Point2D Direction in PossibleDirections)
            {
                Point2D NextShipTile = new Point2D(shipPosition.x + Direction.x, shipPosition.y + Direction.y);
                
                if (ValidateIndex.IsWithin2DArray(NextShipTile, Consts.Battleship.BoardSize) && IsThereAShip(board,NextShipTile))
                    return Direction;
            }
            Point2D OneTileShip = new Point2D(1,1);
            return OneTileShip;
        }

        public bool IsGameOver()
        {
            return CheckIfUserLost() || CheckIfEnemyLost();
        }

        private bool CheckIfUserLost()
        {
            for (int y = 0; y < Consts.Battleship.BoardSize.y; y++)
            {
                for (int x = 0; x < Consts.Battleship.BoardSize.x; x++)
                {
                    if (IsThereFloatingShip(UserBoard, new Point2D(x, y)))
                        return false;
                }
            }
            return true;
        }

        private bool CheckIfEnemyLost()
        {
            int DestroyedShipsNumber = 0;
            for (int y = 0; y < Consts.Battleship.BoardSize.y; y++)
            {
                for (int x = 0; x < Consts.Battleship.BoardSize.x; x++)
                {
                    if (IsThereDestroyedShip(EnemyBoard, new Point2D(x, y)))
                        DestroyedShipsNumber++;
                }
            }
            int ExpectedShipTilesNumber = 20;
            return ExpectedShipTilesNumber == DestroyedShipsNumber;
        }

        public bool IsShipHit(Point2D OnPoint)
        {
            return IsThereAShip(UserBoard, OnPoint);
        }

        public bool DoesEnemyDestroyedShip(Point2D ShipPosition)
        {
            return IsThereAShip(UserBoard, ShipPosition) && IsShipDestroyed(ShipPosition, ref UserBoard);
        }

        private bool IsThereAShip(in List<List<BattleshipCell>> board, Point2D point)
        {
            return IsThereDestroyedShip(board, point) || IsThereFloatingShip(board, point);
        }

        private bool IsThereFloatingShip(in List<List<BattleshipCell>> board, Point2D point)
        {
            return board[point.y][point.x].state == BattleshipCellState.Ship;
        }

        private bool IsThereDestroyedShip(in List<List<BattleshipCell>> board, Point2D point)
        {
            return board[point.y][point.x].state == BattleshipCellState.DestroyedShip;
        }

        private bool IsEmpty(List<List<BattleshipCell>> board, Point2D point)
        {
            return board[point.y][point.x].state == BattleshipCellState.Empty ||
                   board[point.y][point.x].state == BattleshipCellState.Checked;
        }

        public BattleshipCell GetUserBoardCell(Point2D OnPosition)
        {
            return UserBoard[OnPosition.y][OnPosition.x];
        }

        public BattleshipCell GetEnemyBoardCell(Point2D OnPosition)
        {
            return EnemyBoard[OnPosition.y][OnPosition.x];
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
