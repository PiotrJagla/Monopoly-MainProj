using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Behaviours.Buying;
using Services.GamesServices.Monopoly.Board.ModalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Cells
{
    public class MonopolyChanceCell : MonopolyCell
    {
        private CellBuyingBehaviour BuyingBehaviour;

        public MonopolyChanceCell()
        {
            BuyingBehaviour = new CellNotAbleToBuyBehaviour();
        }

        public int CellBought(MonopolyPlayer MainPlayer, string WhatIsBought, ref List<MonopolyCell> CheckMonopol)
        {
            return 0;
        }

        public void CellSold(ref List<MonopolyCell> MonopolChanges)
        {
            
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return BuyingBehaviour;
        }

        public MonopolyModalParameters GetModalParameters(DataToGetModalParameters Data)
        {
            StringModalParameters Parameters = new StringModalParameters();

            Parameters.Title = "You can roll something";
            Parameters.ButtonsContent.Add("Take a chance!");

            return new MonopolyModalParameters(Parameters, ModalShow.AfterMove);
        }
        public ModalResponseUpdate OnModalResponse(ModalResponseData Data)
        {
            ModalResponseUpdate UpdatedData = new ModalResponseUpdate();
            UpdatedData.BoardService = Data.BoardService;
            UpdatedData.PlayersService = Data.PlayersService;



            return UpdatedData;
        }

        public string GetName()
        {
            return "Chance Cell";
        }

        public string OnDisplay()
        {
            return "CHANCE!";
        }

    }
}
