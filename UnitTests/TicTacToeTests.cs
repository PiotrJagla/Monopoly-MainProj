using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitTests
{
    [TestClass]
    public class TicTacToeTests
    {
        [TestMethod]
        public void CheckWinnerTest1()
        {
            TicTacToeService TicTacToe = new TicTacToeGameLogic();

            TicTacToe.PlayerTurn(new Point2D(0,0));
            TicTacToe.PlayerTurn(new Point2D(1,1));
            TicTacToe.PlayerTurn(new Point2D(2,2));

            Assert.IsTrue(TicTacToe.CheckWinner() == Constants.TicTacToePlayer);
        }

        [TestMethod]
        public void CheckWinnerTest2()
        {
            TicTacToeService TicTacToe = new TicTacToeGameLogic();

            TicTacToe.PlayerTurn(new Point2D(0, 0));
            TicTacToe.PlayerTurn(new Point2D(1, 1));
            TicTacToe.PlayerTurn(new Point2D(2, 1));
            TicTacToe.PlayerTurn(new Point2D(0, 2));

            Assert.IsTrue(TicTacToe.CheckWinner() == null);
        }

        [TestMethod]
        public void CheckWinnerTest3()
        {
            TicTacToeService TicTacToe = new TicTacToeGameLogic();

            TicTacToe.PlayerTurn(new Point2D(2, 0));
            TicTacToe.PlayerTurn(new Point2D(1, 1));
            TicTacToe.PlayerTurn(new Point2D(0, 2));

            Assert.IsTrue(TicTacToe.CheckWinner() == Constants.TicTacToePlayer);
        }

        [TestMethod]
        public void CheckWinnerTest4()
        {
            TicTacToeService TicTacToe = new TicTacToeGameLogic();

            TicTacToe.PlayerTurn(new Point2D(1, 0));
            TicTacToe.PlayerTurn(new Point2D(1, 1));
            TicTacToe.PlayerTurn(new Point2D(1, 2));

            Assert.IsTrue(TicTacToe.CheckWinner() == Constants.TicTacToePlayer);
        }

        [TestMethod]
        public void CheckWinnerTest5()
        {
            TicTacToeService TicTacToe = new TicTacToeGameLogic();

            TicTacToe.PlayerTurn(new Point2D(0, 2));
            TicTacToe.PlayerTurn(new Point2D(1, 2));
            TicTacToe.PlayerTurn(new Point2D(2, 2));

            Assert.IsTrue(TicTacToe.CheckWinner() == Constants.TicTacToePlayer);
        }

        [TestMethod]
        public void CheckWinnerTest6()
        {
            TicTacToeService TicTacToe = new TicTacToeGameLogic();

            TicTacToe.PlayerTurn(new Point2D(2, 2));
            TicTacToe.PlayerTurn(new Point2D(0, 2));
            TicTacToe.PlayerTurn(new Point2D(1, 1));

            Assert.IsTrue(TicTacToe.CheckWinner() == null);
        }

        [TestMethod]
        public void CheckWinnerTest7()
        {
            TicTacToeService TicTacToe = new TicTacToeGameLogic();

            TicTacToe.PlayerTurn(new Point2D(1, 1));
            TicTacToe.EnemyTurn();
            TicTacToe.PlayerTurn(new Point2D(0, 1));
            TicTacToe.EnemyTurn();
            TicTacToe.PlayerTurn(new Point2D(2, 0));
            TicTacToe.EnemyTurn();
            TicTacToe.PlayerTurn(new Point2D(1, 2));
            TicTacToe.EnemyTurn();
            TicTacToe.PlayerTurn(new Point2D(2, 2));

            Assert.IsTrue(TicTacToe.CheckWinner() == Constants.TicTacToeTie);
        }

        [TestMethod]
        public void EnemyTurnTest1()
        {
            TicTacToeService TicTacToe = new TicTacToeGameLogic();

            TicTacToe.PlayerTurn(new Point2D(0, 2));
            TicTacToe.EnemyTurn();
            TicTacToe.PlayerTurn(new Point2D(1, 2));
            TicTacToe.EnemyTurn();
            

            Assert.IsTrue(TicTacToe.GetPoint(new Point2D(2,2)) == Constants.TicTacToeEnemy);
        }

        [TestMethod]
        public void EnemyTurnTest3()
        {
            TicTacToeService TicTacToe = new TicTacToeGameLogic();

            TicTacToe.PlayerTurn(new Point2D(1, 0));
            TicTacToe.EnemyTurn();
            TicTacToe.PlayerTurn(new Point2D(1, 1));
            TicTacToe.EnemyTurn();


            Assert.IsTrue(TicTacToe.GetPoint(new Point2D(1, 2)) == Constants.TicTacToeEnemy);
        }





    }
}
