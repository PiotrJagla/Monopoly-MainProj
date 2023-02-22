using Enums.Monopoly;
using Models;
using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Behaviours.Buying
{
    public class CellAbleToBuyBehaviour : CellBuyingBehaviour
    {
        private PlayerKey OwnedBy;
        private Costs ActualCosts;
        private Costs BaseCosts;

        private bool IsChampionshiSet;

        public CellAbleToBuyBehaviour(Costs costs)
        {
            OwnedBy = PlayerKey.NoOne;
            BaseCosts = new Costs(costs.Buy, costs.Stay);
            ActualCosts = new Costs(costs.Buy, costs.Stay);
            
            IsChampionshiSet = false;
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
            if (Multiplayer < 1.0f)
            {
                if (BaseCosts.Stay < ActualCosts.Stay)
                {
                    ActualCosts.Stay = (int)(ActualCosts.Stay * Multiplayer);
                }
            }
            else
            {
                ActualCosts.Stay = (int)(BaseCosts.Stay * Multiplayer);
            }
        }

        public void UpdateCosts(Costs costs)
        {
            ActualCosts.Stay = costs.Stay;
            ActualCosts.Buy = costs.Buy;
        }

        public void SetOwner(PlayerKey NewOwner)
        {
            OwnedBy = NewOwner;
        }

        public bool IsThereChampionship()
        {
            return IsChampionshiSet;
        }

        public void SetChampionship()
        {
            ActualCosts.Stay = (int)(ActualCosts.Stay * Consts.Monopoly.ChampionshipMultiplayer);
            IsChampionshiSet = true;
        }

        public void GetChampionshipOff()
        {
            ActualCosts.Stay = (int)(ActualCosts.Stay * (1.0f / Consts.Monopoly.ChampionshipMultiplayer));
            IsChampionshiSet = false;
        }

        public void SetBaseCosts(Costs costs)
        {
            BaseCosts.Stay = costs.Stay;
            BaseCosts.Buy = costs.Buy;
            ActualCosts.Stay = costs.Stay;
            ActualCosts.Buy = costs.Buy;
        }
    }
}
