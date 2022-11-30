namespace Enums
{
    public enum SinglplayerGame
    {
        BalckJack,
        NoGame
    }

    public static class SinglplayerGameMethods
    {
        private const string BlackJackString = "Black Jack";


        public static string ToString(SinglplayerGame game)
        {
            switch (game)
            {
                case SinglplayerGame.BalckJack:
                    return BlackJackString;
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
                default:
                    return SinglplayerGame.NoGame;


            }
        }
    }
}