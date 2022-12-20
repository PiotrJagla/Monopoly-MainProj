using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Update
{
    public class MonopolyBoardUpdateData
    {
        private List<MonopolyCellUpdate> CellsUpdate;

        public List<MonopolyCellUpdate> GetCellsUpdate()
        {
            return CellsUpdate;
        }

        public MonopolyCellUpdate GetCellUpdate(int index)
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
