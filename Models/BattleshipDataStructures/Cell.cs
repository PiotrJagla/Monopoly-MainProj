using Enums.BattleshipEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BattleshipDataStructures
{
    public class Cell
    {
        public CellState state { get; set; }

        public Cell()
        {
            state = CellState.Empty;
        }
    }
}
