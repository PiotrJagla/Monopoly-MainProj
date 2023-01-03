using Enums.Monopoly;
using Models;
using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board
{
    public class MonopolyIslandCell : MonopolyCell
    {
        public Beach GetBeachName()
        {
            return Beach.NoBeach;
        }

        public Costs GetCosts()
        {
            return new Costs();
        }

        public StringModalParameters GetModalParameters()
        {
            StringModalParameters Result = new StringModalParameters();
            Result.Title = "You Are On Desert Island For 3 Turns";
            Result.ButtonsContent.Add($"Pay {Consts.Monopoly.IslandEscapeCost} To Leave");
            Result.ButtonsContent.Add("Throw Dice(Excape if 1 is Rolled)");
            Result.ButtonsContent.Add("Wait");
            return Result;
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
            return Consts.Monopoly.IslandDiaplsy;
        }

        public void SetCosts(Costs costs)
        {
            
        }

        public void SetOwner(PlayerKey NewOwner)
        {
            
        }
    }
}
