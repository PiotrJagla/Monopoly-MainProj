using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums
{
    public enum MultiplayerGame
    {
        Statki,
        DemoButtonClickingGame,
        NoGame
    }

    public static class MultiplayerGameMethods
    {
        private const string StatkiGameString = "Statki";
        private const string DemoButtonCLickingGameString= "Demo button clicking game";


        public static string ToString(MultiplayerGame multiplayerGame)
        {
            switch(multiplayerGame)
            {
                case MultiplayerGame.Statki:
                    return StatkiGameString;
                case MultiplayerGame.DemoButtonClickingGame:
                    return DemoButtonCLickingGameString;
                default:
                    return "There is no such game";
            }
        }

        public static MultiplayerGame ToEnum(string multiplayerGame)
        {
            switch(multiplayerGame)
            {
                case StatkiGameString:
                    return MultiplayerGame.Statki;
                case DemoButtonCLickingGameString:
                    return MultiplayerGame.DemoButtonClickingGame;
                default:
                    return MultiplayerGame.NoGame;
                        

            }
        }
    }
}
