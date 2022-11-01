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
        NoGame
    }

    public static class MultiplayerGameMethods
    {
        private const string StatkiGameString = "Statki";

        public static string ToString(MultiplayerGame multiplayerGame)
        {
            switch(multiplayerGame)
            {
                case MultiplayerGame.Statki:
                    return StatkiGameString;
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
                default:
                    return MultiplayerGame.NoGame;
                        

            }
        }
    }
}
