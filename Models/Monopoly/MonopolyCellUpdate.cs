using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public class MonopolyCellUpdate
    {
        public PlayerKey Owner{ get; set; }
        public int OfCellIndex { get; set; }

        public string NewBuilding { get; set; }

        public Costs NewCosts { get; set; }

        public MonopolyCellUpdate()
        {
            NewCosts = new Costs();
            Owner = PlayerKey.NoOne;
            OfCellIndex = -1;
            NewBuilding = "";
        }
    }
}
