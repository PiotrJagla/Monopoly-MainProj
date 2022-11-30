using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums.BlackJack
{
    public enum Suite
    {
        heart,
        club,
        spade,
        diamond,
        lastSuite
    }

    public class SuiteMethods
    {
        public static string OnSuiteDisplay(Suite suite)
        {
            switch (suite)
            {
                case Suite.club:
                    return "♧";
                case Suite.diamond:
                    return "♢";
                case Suite.heart:
                    return "♡";
                case Suite.spade:
                    return "♤";
                default:
                    return "X";
            }
        }
    }
}
