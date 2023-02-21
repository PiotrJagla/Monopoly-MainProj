using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Behaviours;
using Services.GamesServices.Monopoly.Board.Behaviours.Buying;
using Services.GamesServices.Monopoly.Board.Behaviours.Monopol;
using Services.GamesServices.Monopoly.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Cells
{
    public class MonopolyIslandCell : MonopolyCell
    {
        private readonly Int TurnsOnIslandRemainingREF;
        private CellBuyingBehaviour BuyingBehaviour;
        private MonopolBehaviour monopolBehaviour;

        public MonopolyIslandCell(Int TurnsOnIslandRemaining)
        {
            TurnsOnIslandRemainingREF = TurnsOnIslandRemaining;
            BuyingBehaviour = new CellNotAbleToBuyBehaviour();
            monopolBehaviour = new NoMonopolBehaviour();
        }

        public Beach GetBeachName()
        {
            return Beach.NoBeach;
        }

        public MonopolyModalParameters GetModalParameters(DataToGetModalParameters Data)
        {
            StringModalParameters Result = new StringModalParameters();
            Result.Title = $"You Are On Desert Island For {TurnsOnIslandRemainingREF.Value} Turns";
            Result.ButtonsContent.Add(Consts.Monopoly.PayToEscapeIslandCellButtonContent);
            Result.ButtonsContent.Add(Consts.Monopoly.ThrowDiceIslandButtonContent);
            return new MonopolyModalParameters(Result, ModalShow.BeforeMove, ModalResponseIdentifier.Island);
        }

        public Nation GetNation()
        {
            return Nation.NoNation;
        }

        public string OnDisplay()
        {
            return Consts.Monopoly.IslandDiaplsy;
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return BuyingBehaviour;
        }


        public int CellBought(MonopolyPlayer MainPlayer, string WhatIsBought,ref List<MonopolyCell> CheckMonopol)
        {
            return 0;
        }

        public void CellSold(ref List<MonopolyCell> MonopolChanges)
        {
            
        }
    }
}
