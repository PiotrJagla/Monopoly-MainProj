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
        public char OnCellDisplay { get; set; }

        public BattleshipCell()
        {
            OnCellDisplay = '-';
            state = BattleshipCellState.Empty;
        }

        public void ChangeState(BattleshipCellState state)
        {
            this.state = state;
            OnCellDisplay = GetCorrespondingDisplayToState(state);
        }

        private char GetCorrespondingDisplayToState(BattleshipCellState state)
        {
            switch(state)
            {
                case BattleshipCellState.Empty:
                    return '-';
                case BattleshipCellState.Ship:
                    return 'O';
                default:
                    return '!';
            }
        }
    }
}
