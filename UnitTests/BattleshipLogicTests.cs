using Models;
using Services.Database_services;
using Services.GamesServices.Battleships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

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

            for (int y = 0; y < Constants.BattleshipBoardSize.y; ++y)
            {
                for (int x = 0; x < Constants.BattleshipBoardSize.x; ++x)
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

            for (int y = 0; y < Constants.BattleshipBoardSize.y; ++y)
            {
                for (int x = 0; x < Constants.BattleshipBoardSize.x; ++x)
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

            for (int y = 0; y < Constants.BattleshipBoardSize.y; ++y)
            {
                for (int x = 0; x < Constants.BattleshipBoardSize.x; ++x)
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

            for (int y = 0; y < Constants.BattleshipBoardSize.y; ++y)
            {
                for (int x = 0; x < Constants.BattleshipBoardSize.x; ++x)
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

            for (int y = 0; y < Constants.BattleshipBoardSize.y; ++y)
            {
                for (int x = 0; x < Constants.BattleshipBoardSize.x; ++x)
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

            for (int y = 0; y < Constants.BattleshipBoardSize.y; ++y)
            {
                for (int x = 0; x < Constants.BattleshipBoardSize.x; ++x)
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

            for (int y = 0; y < Constants.BattleshipBoardSize.y; ++y)
            {
                for (int x = 0; x < Constants.BattleshipBoardSize.x; ++x)
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
    }
}
