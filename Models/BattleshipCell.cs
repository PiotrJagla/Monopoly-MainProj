using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Models
{
    public class BattleshipCell
    {
        public BattleshipCellState state { get; set; }

        public BattleshipCell()
        {
            state = BattleshipCellState.Empty;
        }
    }
}
