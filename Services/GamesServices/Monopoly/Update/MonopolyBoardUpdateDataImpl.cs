using Models;
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
                CellsUpdate.Last().Owner = cell.GetBuyingBehavior().GetOwner();
                CellsUpdate.Last().OfCellIndex = Board.IndexOf(cell);
                CellsUpdate.Last().NewCosts = cell.GetBuyingBehavior().GetCosts();
                CellsUpdate.Last().NewBuilding = ExtractBuildingFromDisplay(cell.OnDisplay());
            }
        }

        public string ExtractBuildingFromDisplay(string Display)
        {
            foreach (var building in Consts.Monopoly.PossibleBuildings)
            {
                if (Display.Contains(building))
                    return building;
            }
            return "";
        }
    }
}
