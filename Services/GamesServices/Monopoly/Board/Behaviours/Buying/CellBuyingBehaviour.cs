using Enums.Monopoly;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Behaviours.Buying
{
    public interface CellBuyingBehaviour
    {
        PlayerKey GetOwner();
        void SetOwner(PlayerKey NewOwner);
        Costs GetCosts();
        void SetCosts(Costs costs);
        void MultiplyStayCostAmount(float Multiplayer);

        bool IsThereChampionship();
        void SetChampionship();
        void GetChampionshipOff();

    }
}
