using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums.BlackJack
{
    public enum CardType
    {
        two,
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine,
        ten,
        jack,
        queen,
        king,
        ace,
        AllCards
    }

    public class CardTypeMethods
    {
        public static string OnCardTypeDisplay(CardType type)
        {
            switch(type)
            {
                case CardType.two:
                    return "2";
                case CardType.three:
                    return "3";
                case CardType.four:
                    return "4";
                case CardType.five:
                    return "5";
                case CardType.six:
                    return "6";
                case CardType.seven:
                    return "7";
                case CardType.eight:
                    return "8";
                case CardType.nine:
                    return "9";
                case CardType.ten:
                    return "10";
                case CardType.jack:
                    return "jack";
                case CardType.queen:
                    return "queen";
                case CardType.king:
                    return "king";
                case CardType.ace:
                    return "A";
                default:
                    return "X";
            }
        }
    }
}
