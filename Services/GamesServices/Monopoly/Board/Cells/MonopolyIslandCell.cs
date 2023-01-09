using Enums.Monopoly;
using Models;
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
    public class MonopolyIslandCell : MonopolyCell
    {
        private int TurnsRemaining;
        private CellBuyingBehaviour BuyingBehaviour;
        private MonopolBehaviour monopolBehaviour;

        public MonopolyIslandCell()
        {
            TurnsRemaining = 0;
            BuyingBehaviour = new CellNotAbleToBuyBehaviour();
            monopolBehaviour = new NoMonopolBehaviour();
        }

        public Beach GetBeachName()
        {
            return Beach.NoBeach;
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

        public List<MonopolyCell> MonopolChanges(in List<MonopolyCell> Board, int OnCell)
        {
            return monopolBehaviour.UpdateBoardMonopol(Board,OnCell);
        }

        public string OnDisplay()
        {
            return Consts.Monopoly.IslandDiaplsy;
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return BuyingBehaviour;
        }
    }
}
