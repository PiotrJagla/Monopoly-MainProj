using Enums.Monopoly;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Behaviours.Buying;
using Services.GamesServices.Monopoly.Board.Behaviours.Monopol;
using Services.GamesServices.Monopoly.Board.ModalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Cells
{
    public interface MonopolyCell
    {
        string GetName();
        string OnDisplay();


        CellBuyingBehaviour GetBuyingBehavior();

        int CellBought(MonopolyPlayer MainPlayer, string WhatIsBought,ref List<MonopolyCell> CheckMonopol);
        void CellSold(ref List<MonopolyCell> MonopolChanges);

        MonopolyModalParameters GetModalParameters(DataToGetModalParameters Data);
        ModalResponseUpdate OnModalResponse(ModalResponseData Data);
    }
}
