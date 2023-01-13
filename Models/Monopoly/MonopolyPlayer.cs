using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public class MonopolyPlayer
    {
        public PlayerKey Key { get; set; }
        public int OnCellIndex { get; set; }
        public int MoneyOwned { get; set; }

        public MonopolyPlayer()
        {
            Key = PlayerKey.First;
            OnCellIndex = 0;
            MoneyOwned = 0;
        }
    }
}
