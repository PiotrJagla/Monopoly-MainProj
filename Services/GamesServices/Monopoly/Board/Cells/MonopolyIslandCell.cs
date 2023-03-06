using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Behaviours;
using Services.GamesServices.Monopoly.Board.Behaviours.Buying;
using Services.GamesServices.Monopoly.Board.Behaviours.Monopol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.GamesServices.Monopoly.Board.ModalData;

namespace Services.GamesServices.Monopoly.Board.Cells
{
    public class MonopolyIslandCell : MonopolyCell
    {
        private readonly Int TurnsOnIslandRemainingREF;
        private CellBuyingBehaviour BuyingBehaviour;


        public MonopolyIslandCell(Int TurnsOnIslandRemaining)
        {
            TurnsOnIslandRemainingREF = TurnsOnIslandRemaining;
            BuyingBehaviour = new CellNotAbleToBuyBehaviour();

        }

        public MonopolyModalParameters GetModalParameters(DataToGetModalParameters Data)
        {
            StringModalParameters Result = new StringModalParameters();
            Result.Title = $"You Are On Desert Island For {TurnsOnIslandRemainingREF.Value} Turns";
            Result.ButtonsContent.Add(Consts.Monopoly.PayToEscapeIsland);
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
            else if (Data.ModalResponse == Consts.Monopoly.PayToEscapeIsland)
            {
                if (UpdatedData.PlayersService.GetMainPlayer().MoneyOwned >= Consts.Monopoly.IslandEscapeCost)
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

        public void UpdateData(MonopolyCellUpdate UpdatedData)
        {
            
        }
    }
}
