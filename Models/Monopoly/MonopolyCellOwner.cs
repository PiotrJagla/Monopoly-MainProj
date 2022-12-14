using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public class MonopolyCellOwner
    {
        public PlayerKey Owner{ get; set; }
        public int OfCellIndex { get; set; }

        public MonopolyCellOwner()
        {
            Owner = PlayerKey.NoOne;
            OfCellIndex = -1;
        }
    }
}
