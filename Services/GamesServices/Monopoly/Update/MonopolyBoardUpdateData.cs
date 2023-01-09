using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Update
{
    public interface MonopolyBoardUpdateData
    {
        List<MonopolyCellUpdate> GetCellsUpdateData();

        MonopolyCellUpdate GetOneCellUpdateData(int index);

        void FormatBoardUpdateData(List<MonopolyCell> Board);
    }
}
