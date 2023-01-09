using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.BuyingBehaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Cells
{
    public class MonopolyIslandCell : MonopolyCell
    {
        private int TurnsRemaining;
        private CellBuyingBehaviour BuyingBehaviour;

        public MonopolyIslandCell()
        {
            TurnsRemaining = 0;
            BuyingBehaviour = new CellNotAbleToBuyBehaviour();
        }

        public Beach GetBeachName()
        {
            return Beach.NoBeach;
        }

        public Costs GetCosts()
        {
            return new Costs();
        }

        public MonopolyModalParameters GetModalParameters()
        {
            StringModalParameters Result = new StringModalParameters();
            Result.Title = $"You Are On Desert Island For {GetTurnsRemaining()} Turns";
            Result.ButtonsContent.Add($"Pay {Consts.Monopoly.IslandEscapeCost} To Leave");
            Result.ButtonsContent.Add("Throw Dice(Excape if 1 is Rolled)");
            return new MonopolyModalParameters(Result, ModalShow.BeforeMove);
        }

        private int GetTurnsRemaining()
        {
            if (TurnsRemaining <= 1)
                TurnsRemaining = 3;
            else
                TurnsRemaining--;

            return TurnsRemaining;
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

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return BuyingBehaviour;
        }
    }
}
