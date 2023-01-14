using Enums.Monopoly;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Behaviours;
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

        CellBuyingBehaviour GetBuyingBehavior();

        MonopolBehaviour MonopolChanges();
        string OnDisplay();
        MonopolyModalParameters GetModalParameters(in List<MonopolyCell> Board, PlayerKey MainPlayerKey);
    }
}
