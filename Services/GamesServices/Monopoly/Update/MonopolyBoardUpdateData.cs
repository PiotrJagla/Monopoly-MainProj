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
        private List<MonopolyCellOwner> CellsOwners;

        public List<MonopolyCellOwner> GetCellsOwners()
        {
            return CellsOwners;
        }

        public MonopolyCellOwner GetCellOwner(int index)
        {
            return CellsOwners[index];
        }

        public void FormatBoardUpdateData(List<MonopolyCell> Board)
        {
            CellsOwners = new List<MonopolyCellOwner>();
            foreach (var cell in Board)
            {
                CellsOwners.Add(new MonopolyCellOwner());
                CellsOwners.Last().Owner = cell.OwnedBy;
                CellsOwners.Last().OfCellIndex = cell.Number;
            }
        }
    }
}
