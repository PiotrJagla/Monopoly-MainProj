using Enums.Monopoly;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.BuyingBehaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Cells
{
    public interface MonopolyCell
    {
        Nation GetNation();
        Beach GetBeachName();

        List<MonopolyCell> MonopolChanges(in List<MonopolyCell> Board, int OnCell);
        CellBuyingBehaviour GetBuyingBehavior();
        string OnDisplay();
        MonopolyModalParameters GetModalParameters();
    }
}
