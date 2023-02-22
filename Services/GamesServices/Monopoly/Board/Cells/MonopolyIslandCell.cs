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

        public MonopolyModalParameters GetModalParameters(DataToGetModalParameters Data)
        {
            StringModalParameters Result = new StringModalParameters();
            Result.Title = $"You Are On Desert Island For {TurnsOnIslandRemainingREF.Value} Turns";
            Result.ButtonsContent.Add(Consts.Monopoly.PayToEscapeIslandCellButtonContent);
            Result.ButtonsContent.Add(Consts.Monopoly.ThrowDiceIslandButtonContent);
            return new MonopolyModalParameters(Result, ModalShow.BeforeMove);
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

        public ModalResponseUpdate OnModalResponse(ModalResponseData Data)
        {
            ModalResponseUpdate UpdatedData = new ModalResponseUpdate();
            UpdatedData.BoardService = Data.BoardService;
            UpdatedData.PlayersService = Data.PlayersService;

            if (Data.ModalResponse == Consts.Monopoly.ThrowDiceIslandButtonContent)
            {
                if (GetRandom.number.Next(1, 6) == 1)
                {
                    UpdatedData.BoardService.EscapeFromIsland();
                }
            }
            else if (Data.ModalResponse == Consts.Monopoly.PayToEscapeIslandCellButtonContent)
            {
                if (UpdatedData.PlayersService.IsAbleToPayForEscapingFromIsland())
                {
                    UpdatedData.PlayersService.ChargeMainPlayer(Consts.Monopoly.IslandEscapeCost);
                    UpdatedData.BoardService.EscapeFromIsland();
                }
            }

            return UpdatedData;
        }

        public string GetName()
        {
            return "Island";
        }
    }
}
