using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class Constants
    {
        public static string MySQLConnectionString => "server=localhost;port=3306;database=bazaDanych;user=root;password=1234";
        public static string ServerURL => "https://localhost:7268";
        public static string MonopolyHubURL => "/monopolyhub";
        public static string BattleshipHubURL => "/battleshiphub";
        public static string DemoButtonsHubURL => "/multihub";


        public static Point2D BattleshipBoardSize = new Point2D(10, 10);

        public static int BlackJackMaxPoints = 31;
        public static int BlackJackMinPoints = 27;

        public static Point2D TicTacToeBoardSize = new Point2D(3, 3);
        public static char TicTacToePlayer = 'X';
        public static char TicTacToeEnemy = 'O';
        public static char TicTacToeEmpty = ' ';
        public static char TicTacToeTie = '#';
    }
}
