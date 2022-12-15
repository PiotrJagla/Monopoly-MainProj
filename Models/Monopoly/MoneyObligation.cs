using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public class MoneyObligation
    {
        public PlayerKey ObligatedToPay { get; set; }
        public PlayerKey PlayerGettingMoney { get; set; }
        public int ObligationAmount { get; set; }

        public MoneyObligation()
        {
            ObligatedToPay = PlayerKey.NoOne;
            PlayerGettingMoney = PlayerKey.NoOne;
            ObligationAmount = 0;
        }
    }
}
