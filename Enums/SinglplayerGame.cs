namespace Enums
{
    public enum SinglplayerGame
    {
        BalckJack,
        TicTacToe,
        NoGame
    }

    public static class SinglplayerGameMethods
    {
        private const string BlackJackString = "Black Jack";
        private const string TicTacToeString= "Tic Tac Toe";


        public static string ToString(SinglplayerGame game)
        {
            switch (game)
            {
                case SinglplayerGame.BalckJack:
                    return BlackJackString;
                case SinglplayerGame.TicTacToe:
                    return TicTacToeString;
                default:
                    return "There is no such game";
            }
        }

        public static SinglplayerGame ToEnum(string GameString)
        {
            switch (GameString)
            {
                case BlackJackString:
                    return SinglplayerGame.BalckJack;
                case TicTacToeString:
                    return SinglplayerGame.TicTacToe;
                default:
                    return SinglplayerGame.NoGame;


            }
        }
    }
}