﻿using Enums.Monopoly;
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
    public class MonopolyTaxCell : MonopolyCell
    {
        public int CellBought(MonopolyPlayer MainPlayer, string WhatIsBought, ref List<MonopolyCell> CheckMonopol)
        {
            return 0;
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return new CellNotAbleToBuyBehaviour();
        }

        public MonopolyModalParameters GetModalParameters(DataToGetModalParameters Data)
        {
            StringModalParameters parameters = new StringModalParameters();

            parameters.Title = $"You have to pay {Consts.Monopoly.TaxAmount} tax";

            parameters.ButtonsContent.Add("Pay");

            return new MonopolyModalParameters(parameters, ModalShow.AfterMove);
        }

        public string GetName()
        {
            return "Tax";
        }

        public string OnDisplay()
        {
            return "Tax";
        }

        public ModalResponseUpdate OnModalResponse(ModalResponseData Data)
        {
            ModalResponseUpdate UpdatedData = new ModalResponseUpdate();
            UpdatedData.BoardService = Data.BoardService;
            UpdatedData.PlayersService = Data.PlayersService;

            UpdatedData.PlayersService.ChargeMainPlayer(Consts.Monopoly.TaxAmount);

            return UpdatedData;
        }

        public void UpdateData(MonopolyCellUpdate UpdatedData){}
        public void CellSold(ref List<MonopolyCell> MonopolChanges){}
        

        
    }
}
