using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Update
{
    public class MonopolyBoardUpdateDataImpl : MonopolyBoardUpdateData
    {
        private List<MonopolyCellUpdate> CellsUpdate;

        public List<MonopolyCellUpdate> GetCellsUpdateData()
        {
            return CellsUpdate;
        }

        public MonopolyCellUpdate GetOneCellUpdateData(int index)
        {
            return CellsUpdate[index];
        }

        public void FormatBoardUpdateData(List<MonopolyCell> Board)
        {
            CellsUpdate = new List<MonopolyCellUpdate>();
            foreach (var cell in Board)
            {
                CellsUpdate.Add(new MonopolyCellUpdate());
                CellsUpdate.Last().Owner = cell.GetOwner();
                CellsUpdate.Last().OfCellIndex = Board.IndexOf(cell);
                CellsUpdate.Last().NewCosts = cell.GetCosts();
            }
        }
    }
}
