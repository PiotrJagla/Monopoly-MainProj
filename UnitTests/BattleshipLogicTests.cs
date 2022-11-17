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
    }
}
