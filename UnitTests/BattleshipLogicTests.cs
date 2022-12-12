using Services.Database_services;
using Services.GamesServices.Battleships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Battleship;
using Enums.Battleship;

namespace UnitTests
{
    [TestClass]
    public class BattleshipLogicTests
    {

        [TestMethod]
        public void OnUserCellClickTest()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

            BattleshipLogic.UserBoardClicked(new Point2D(3, 2));

            List<List<BattleshipCell>> Board;
            Board = BattleshipLogic.GetUserBoard();

            Assert.IsTrue(Board[2][3].state == BattleshipCellState.Ship);
            BattleshipLogic.UserBoardClicked(new Point2D(3, 2));
            Board = BattleshipLogic.GetUserBoard();
            Assert.IsTrue(Board[2][3].state == BattleshipCellState.Empty);
        }

        [TestMethod]
        public void ShipsDistributionValidationTest1()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

            List<string> BadDistributionVisualization = new List<string>
            {
                "-XX-------",
                "----X--X--",
                "-XX---X---",
                "----------",
                "-X------X-",
                "-X-X----X-",
                "-X-X----X-",
                "----------",
                "-XXXX-----",
                "---------X",
            };

            List<Point2D> BadDistribution = new List<Point2D>();

            for (int y = 0; y < Consts.Battleship.BoardSize.y; ++y)
            {
                for (int x = 0; x < Consts.Battleship.BoardSize.x; ++x)
                {
                    if (BadDistributionVisualization[y].ElementAt(x) == 'X')
                        BadDistribution.Add(new Point2D(x, y));
                }
            }

            for (int iii = 0; iii < BadDistribution.Count; ++iii)
                BattleshipLogic.UserBoardClicked(BadDistribution[iii]);

            Assert.IsFalse(BattleshipLogic.IsUserBoardCorrect());
        }

        [TestMethod]
        public void ShipsDistributionValidationTest2()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

            List<string> BadDistributionVisualization = new List<string>
            {
                "-XX-------",
                "----X--X--",
                "-XX-------",
                "----------",
                "-X------X-",
                "-X-X----X-",
                "-X-X----X-",
                "----------",
                "-XXXX-----",
                "---X-----X",
            };

            List<Point2D> BadDsitribution = new List<Point2D>();

            for (int y = 0; y < Consts.Battleship.BoardSize.y; ++y)
            {
                for (int x = 0; x < Consts.Battleship.BoardSize.x; ++x)
                {
                    if (BadDistributionVisualization[y].ElementAt(x) == 'X')
                        BadDsitribution.Add(new Point2D(x, y));
                }
            }

            for (int iii = 0; iii < BadDsitribution.Count; ++iii)
                BattleshipLogic.UserBoardClicked(BadDsitribution[iii]);

            Assert.IsFalse(BattleshipLogic.IsUserBoardCorrect());
        }

        [TestMethod]
        public void ShipsDistributionValidationTest3()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

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

            for (int y = 0; y < Consts.Battleship.BoardSize.y; ++y)
            {
                for (int x = 0; x < Consts.Battleship.BoardSize.x; ++x)
                {
                    if (ValidDistributionVisualization[y].ElementAt(x) == 'X')
                        ValidDsitribution.Add(new Point2D(x, y));
                }
            }

            for (int iii = 0; iii < ValidDsitribution.Count; ++iii)
                BattleshipLogic.UserBoardClicked(ValidDsitribution[iii]);

            Assert.IsTrue(BattleshipLogic.IsUserBoardCorrect());
        }
        [TestMethod]
        public void ShipsDistributionValidationTest4()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

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

            for (int y = 0; y < Consts.Battleship.BoardSize.y; ++y)
            {
                for (int x = 0; x < Consts.Battleship.BoardSize.x; ++x)
                {
                    if (ValidDistributionVisualization[y].ElementAt(x) == 'X')
                        ValidDsitribution.Add(new Point2D(x, y));
                }
            }

            for (int iii = 0; iii < ValidDsitribution.Count; ++iii)
                BattleshipLogic.UserBoardClicked(ValidDsitribution[iii]);

            Assert.IsTrue(BattleshipLogic.IsUserBoardCorrect());
        }

        [TestMethod]
        public void ShipDestroyedTest1()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

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

            for (int y = 0; y < Consts.Battleship.BoardSize.y; ++y)
            {
                for (int x = 0; x < Consts.Battleship.BoardSize.x; ++x)
                {
                    if (ValidDistributionVisualization[y].ElementAt(x) == 'X')
                        ValidDsitribution.Add(new Point2D(x, y));
                }
            }

            for (int iii = 0; iii < ValidDsitribution.Count; ++iii)
                BattleshipLogic.UserBoardClicked(ValidDsitribution[iii]);

            BattleshipLogic.EnemyAttack(new Point2D(8, 8));

            for (int y = 7; y < 9; y++)
            {
                for (int x = 7; x < 9; x++)
                {
                    if(x!=y)
                        Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(x, y)).state == BattleshipCellState.Checked);
                }
            }

        }

        

        [TestMethod]
        public void ShipDestroyedTest2()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

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

            for (int y = 0; y < Consts.Battleship.BoardSize.y; ++y)
            {
                for (int x = 0; x < Consts.Battleship.BoardSize.x; ++x)
                {
                    if (ValidDistributionVisualization[y].ElementAt(x) == 'X')
                        ValidDsitribution.Add(new Point2D(x, y));
                }
            }

            for (int iii = 0; iii < ValidDsitribution.Count; ++iii)
                BattleshipLogic.UserBoardClicked(ValidDsitribution[iii]);

            BattleshipLogic.EnemyAttack(new Point2D(0, 9));
            BattleshipLogic.EnemyAttack(new Point2D(1, 9));
            BattleshipLogic.EnemyAttack(new Point2D(2, 9));

            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(0, 8)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(1, 8)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(2, 8)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(3, 8)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(3, 9)).state == BattleshipCellState.Checked);
            

        }

        [TestMethod]
        public void ShipDestroyedTest3()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

            List<string> ValidDistributionVisualization = new List<string>
            {
                "XX--------",
                "-------XXX",
                "----X-----",
                "----X---X-",
                "----------",
                "X--X-X----",
                "X----X----",
                "---X-X----",
                "-----X--X-",
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
                BattleshipLogic.UserBoardClicked(ValidDsitribution[iii]);



            BattleshipLogic.EnemyAttack(new Point2D(5, 5));
            BattleshipLogic.EnemyAttack(new Point2D(5, 6));
            BattleshipLogic.EnemyAttack(new Point2D(5, 7));
            BattleshipLogic.EnemyAttack(new Point2D(5, 8));

            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(5, 4)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(5, 9)).state == BattleshipCellState.Checked);

            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(4, 4)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(4, 5)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(4, 6)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(4, 7)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(4, 8)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(4, 9)).state == BattleshipCellState.Checked);

            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(6, 4)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(6, 5)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(6, 6)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(6, 7)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(6, 8)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetUserBoardCell(new Point2D(6, 9)).state == BattleshipCellState.Checked);


        }

        [TestMethod]
        public void ShipDestroyedTest4()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

            List<string> EnemyShips = new List<string>
            {
                "----X-X--X",
                "-X-X-X--X",
                "-X--------",
                "-X---X----",
                "-X--------",
                "----XXX---",
                "----------",
                "-X--XXX---",
                "----------",
                "X--X------",
            };



            BattleshipCell AttackCell = new BattleshipCell();
            AttackCell.CellPoint = new Point2D(1, 1);
            AttackCell.ChangeState(BattleshipCellState.DestroyedShip);
            BattleshipLogic.AttackOnEnemyBoard(AttackCell, false);

            AttackCell.CellPoint = new Point2D(1, 2);
            AttackCell.ChangeState(BattleshipCellState.DestroyedShip);
            BattleshipLogic.AttackOnEnemyBoard(AttackCell, false);

            AttackCell.CellPoint = new Point2D(1, 3);
            AttackCell.ChangeState(BattleshipCellState.DestroyedShip);
            BattleshipLogic.AttackOnEnemyBoard(AttackCell, false);

            AttackCell.CellPoint = new Point2D(1, 4);
            AttackCell.ChangeState(BattleshipCellState.DestroyedShip);
            BattleshipLogic.AttackOnEnemyBoard(AttackCell, true);



            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (x!=1)
                        Assert.IsTrue(BattleshipLogic.GetEnemyBoardCell(new Point2D(x, y)).state == BattleshipCellState.Checked);
                }
            }

            Assert.IsTrue(BattleshipLogic.GetEnemyBoardCell(new Point2D(1, 0)).state == BattleshipCellState.Checked);
            Assert.IsTrue(BattleshipLogic.GetEnemyBoardCell(new Point2D(1, 5)).state == BattleshipCellState.Checked);
        }

        [TestMethod]
        public void UserGameOverTest()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

            List<string> ValidDistributionVisualization = new List<string>
            {
                "XX--------",
                "-------XXX",
                "----X-----",
                "----X---X-",
                "----------",
                "X--X-X----",
                "X----X----",
                "---X-X----",
                "-----X--X-",
                "XXX-------",
            
            };

            Point2D[] AttackedCells= {
                new Point2D(5, 5), new Point2D(5, 6),new Point2D(5, 7),new Point2D(5, 8),new Point2D(0, 9),new Point2D(1, 9),new Point2D(2, 9),new Point2D(7, 1),
                new Point2D(8, 1),new Point2D(9, 1),new Point2D(0, 0),new Point2D(1, 0),new Point2D(0, 5),new Point2D(0, 6),new Point2D(4, 2),new Point2D(4, 3),
                new Point2D(8, 3),new Point2D(3, 5),new Point2D(3, 7),new Point2D(8, 8) };

            int AttackedCellsNumber = 20;
            for (int i = 0; i < AttackedCellsNumber; i++)
            {
                BattleshipLogic.EnemyAttack(AttackedCells[i]);
            }

            Assert.IsTrue(BattleshipLogic.IsGameOver());
        }
        [TestMethod]
        public void EnemyGameOverTest()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

            List<string> ValidDistributionVisualization = new List<string>
            {
                "XX--------",
                "-------XXX",
                "----X-----",
                "----X---X-",
                "----------",
                "X--X-X----",
                "X----X----",
                "---X-X----",
                "-----X--X-",
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
                BattleshipLogic.UserBoardClicked(ValidDsitribution[iii]);

            BattleshipCell cell = new BattleshipCell();
            Point2D[] CellPositionsToAttack = { 
                new Point2D(5, 5), new Point2D(5, 6),new Point2D(5, 7),new Point2D(5, 8),new Point2D(0, 9),new Point2D(1, 9),new Point2D(2, 9),new Point2D(7, 1),
                new Point2D(8, 1),new Point2D(9, 1),new Point2D(0, 0),new Point2D(1, 0),new Point2D(0, 5),new Point2D(0, 6),new Point2D(4, 2),new Point2D(4, 3),
                new Point2D(8, 3),new Point2D(3, 5),new Point2D(3, 7),new Point2D(8, 8) };

            int AttackedCellsNumber = 20;
            for(int iii = 0; iii < AttackedCellsNumber; ++iii)
            {
                BattleshipCell Cell = new BattleshipCell();
                Cell.CellPoint = CellPositionsToAttack[iii];
                Cell.ChangeState(BattleshipCellState.DestroyedShip);
                BattleshipLogic.AttackOnEnemyBoard(Cell, false);
            }

            Assert.IsTrue(BattleshipLogic.IsGameOver());
        }

    }
}
