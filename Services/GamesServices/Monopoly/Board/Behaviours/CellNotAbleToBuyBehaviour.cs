using Enums.Monopoly;
using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.BuyingBehaviours
{
    public class CellNotAbleToBuyBehaviour : CellBuyingBehaviour
    {
        public void GetChampionshipOff()
        {
            
        }

        public Costs GetCosts()
        {
            return new Costs();
        }

        public PlayerKey GetOwner()
        {
            return PlayerKey.NoOne;
        }

        public bool IsThereChampionship()
        {
            return false;
        }

        public void MultiplyStayCostAmount(float Multiplayer)
        {
            
        }

        public void SetChampionship()
        {
            
        }

        public void SetCosts(Costs costs)
        {
            
        }

        public void SetOwner(PlayerKey NewOwner)
        {
            
        }
    }
}
