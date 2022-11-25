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
        public Point2D CellPoint { get; set; }
        public char OnCellDisplay { get; set; }

        public BattleshipCell()
        {
            CellPoint = new Point2D();
            OnCellDisplay = '-';
            state = BattleshipCellState.Empty;
        }

        public void ChangeState(BattleshipCellState state)
        {
            this.state = state;
            OnCellDisplay = GetStateDisplay(state);
        }

        private char GetStateDisplay(BattleshipCellState state)
        {
            switch(state)
            {
                case BattleshipCellState.Empty:
                    return '-';
                case BattleshipCellState.Ship:
                    return 'O';
                case BattleshipCellState.Checked:
                    return '~';
                case BattleshipCellState.DestroyedShip:
                    return 'X';
                default:
                    return '!';
            }
        }
    }
}
