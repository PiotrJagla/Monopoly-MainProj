using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public class MonopolyCell
    {
        public int Number { get; set; }
        public PlayerKey OwnedBy { get; set; }

        public Costs CellCosts { get; set; }

        public MonopolyCell()
        {
            CellCosts = new Costs();
            Number = 0;
        }
    }
}
