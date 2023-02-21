using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Behaviours.Buying
{
    public class BuyingTiers
    {
        public static int GetBuyTierNumber(string WhatIsBought)
        {
            if (WhatIsBought == Consts.Monopoly.BeachBuyDeclined)
                return 0;
            else if (WhatIsBought == Consts.Monopoly.BeachBuyAccepted)
                return 1;

            if (WhatIsBought == Consts.Monopoly.NothingBought)
                return 0;
            else if (WhatIsBought == Consts.Monopoly.Field)
                return 1;
            else if (WhatIsBought == Consts.Monopoly.OneHouse)
                return 2;
            else if (WhatIsBought == Consts.Monopoly.TwoHouses)
                return 3;
            else if (WhatIsBought == Consts.Monopoly.ThreeHouses)
                return 4;
            else if (WhatIsBought == Consts.Monopoly.Hotel)
                return 5;

            return -1;
        }
    }
}
