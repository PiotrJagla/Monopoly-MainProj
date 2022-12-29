using Enums.Monopoly;
using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board
{
    internal class StartCell : MonopolyCell
    {
        public Costs GetCosts()
        {
            return new Costs();
        }

        public Nation GetNation()
        {
            return Nation.NoNation;
        }

        public PlayerKey GetOwner()
        {
            return PlayerKey.NoOne;
        }

        public string OnDisplay()
        {
            return "Start!";
        }

        public void SetCosts(Costs costs)
        {
            
        }

        public void SetOwner(PlayerKey NewOwner)
        {
            
        }
    }
}
