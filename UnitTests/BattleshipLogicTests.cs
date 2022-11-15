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
        public void ShipsDistributionValidationTest()
        {
            BattleshipService BattleshipLogic = new BattleshipGameLogic();

            List<Point2D> FirstBadDistribution = new List<Point2D> {
                new Point2D(6, 2), new Point2D(1, 2), new Point2D(2, 2), new Point2D(3, 2), new Point2D(6, 4), new Point2D(6, 5), new Point2D(2, 5), new Point2D(3, 6)
            };

            List<Point2D> SecoundBadDistribution = new List<Point2D> {
                new Point2D(1, 0), new Point2D(2, 0), new Point2D(6, 1), new Point2D(1, 2), new Point2D(2, 2), new Point2D(3, 2), new Point2D(8, 2), new Point2D(8, 3),
                new Point2D(8, 4), new Point2D(6, 4), new Point2D(6, 5), new Point2D(2, 5), new Point2D(3, 6), new Point2D(7, 7), new Point2D(8, 7), new Point2D(9, 9),
                new Point2D(1, 8), new Point2D(2, 8), new Point2D(3, 8), new Point2D(4, 8)
            };

            List<Point2D> FirstValidDistribution = new List<Point2D> {
                new Point2D(1, 0), new Point2D(2, 0), new Point2D(6, 1), new Point2D(1, 2), new Point2D(2, 2), new Point2D(3, 2), new Point2D(8, 2), new Point2D(8, 3),
                new Point2D(8, 4), new Point2D(6, 4), new Point2D(6, 5), new Point2D(2, 5), new Point2D(4, 6), new Point2D(7, 7), new Point2D(8, 7), new Point2D(9, 9),
                new Point2D(1, 8), new Point2D(2, 8), new Point2D(3, 8), new Point2D(4, 8)
            };

            for (int iii = 0; iii < FirstBadDistribution.Count; ++iii)
                BattleshipLogic.UserBoardClicked(FirstBadDistribution[iii]);

            Assert.IsFalse(BattleshipLogic.IsUserBoardCorrect());

            BattleshipLogic = new BattleshipGameLogic(); //overwriting object to reset all board
            for (int iii = 0; iii < SecoundBadDistribution.Count; ++iii)
                BattleshipLogic.UserBoardClicked(SecoundBadDistribution[iii]);

            Assert.IsFalse(BattleshipLogic.IsUserBoardCorrect());

            BattleshipLogic = new BattleshipGameLogic(); 
            for (int iii = 0; iii < FirstValidDistribution.Count; ++iii)
                BattleshipLogic.UserBoardClicked(FirstValidDistribution[iii]);

            Assert.IsTrue(BattleshipLogic.IsUserBoardCorrect());
        }
    }
}
