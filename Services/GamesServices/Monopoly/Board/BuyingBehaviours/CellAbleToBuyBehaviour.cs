using Enums.Monopoly;
using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.BuyingBehaviours
{
    public class CellAbleToBuyBehaviour : CellBuyingBehaviour
    {
        public PlayerKey OwnedBy { get; set; }

        public Costs ActualCosts { get; set; }
        public Costs BaseCosts { get; set; }

        public CellAbleToBuyBehaviour(Costs costs)
        {
            BaseCosts = costs;
            ActualCosts = costs;
        }

        public Costs GetCosts()
        {
            return ActualCosts;
        }

        public PlayerKey GetOwner()
        {
            return OwnedBy;
        }

        public void MultiplyStayCostAmount(float Multiplayer)
        {
            ActualCosts.Stay = (int)(BaseCosts.Stay * Multiplayer);
        }

        public void SetCosts(Costs costs)
        {
            ActualCosts.Stay = costs.Stay;
            ActualCosts.Buy = costs.Buy;
        }

        public void SetOwner(PlayerKey NewOwner)
        {
            OwnedBy = NewOwner;
        }
    }
}
