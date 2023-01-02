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
        public Beach GetBeachName()
        {
            return Beach.NoBeach;
        }

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

        

        public List<MonopolyCell> MonopolChanges(in List<MonopolyCell> Board)
        {
            return Board;
        }

        public void MultiplyStayCostAmount(float Multiplayer)
        {
            
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
