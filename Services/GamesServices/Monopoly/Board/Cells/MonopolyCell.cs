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

        PlayerKey GetOwner();
        void SetOwner(PlayerKey NewOwner);

        Costs GetCosts();
        void SetCosts(Costs costs);
        List<MonopolyCell> MonopolChanges(in List<MonopolyCell> Board);
        void MultiplyStayCostAmount(float Multiplayer);

        CellBuyingBehaviour GetBuyingBehavior();

        string OnDisplay();

        MonopolyModalParameters GetModalParameters();
    }
}
