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
        public PlayerKey PlayerGettingMoney { get; set; }
        public int ObligationAmount { get; set; }

        public MoneyObligation()
        {
            PlayerGettingMoney = PlayerKey.NoOne;
            ObligationAmount = 0;
        }
    }
}
