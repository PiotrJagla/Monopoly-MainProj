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
            throw new NotImplementedException();
        }

        public void CellSold(ref List<MonopolyCell> MonopolChanges)
        {
            throw new NotImplementedException();
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            throw new NotImplementedException();
        }

        public MonopolyModalParameters GetModalParameters(DataToGetModalParameters Data)
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string OnDisplay()
        {
            throw new NotImplementedException();
        }

        public ModalResponseUpdate OnModalResponse(ModalResponseData Data)
        {
            throw new NotImplementedException();
        }
    }
}
