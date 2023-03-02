using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Behaviours;
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
    internal class MonopolyStartCell : MonopolyCell
    {
        public int CellBought(MonopolyPlayer MainPlayer, string WhatIsBought,ref List<MonopolyCell> CheckMonopol)
        {
            return 0;
        }

        public void CellSold(ref List<MonopolyCell> MonopolChanges)
        {
            
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return new CellNotAbleToBuyBehaviour();
        }

        public MonopolyModalParameters GetModalParameters(DataToGetModalParameters Data)
        {
            return MonopolyModalFactory.NoModalParameters();
        }

        public ModalResponseUpdate OnModalResponse(ModalResponseData Data)
        {
            ModalResponseUpdate UpdatedData = new ModalResponseUpdate();
            UpdatedData.BoardService = Data.BoardService;
            UpdatedData.PlayersService = Data.PlayersService;
            return UpdatedData;
        }

        public string OnDisplay()
        {
            return "Start!";
        }

        public string GetName()
        {
            return "StartCell";
        }

        public void UpdateData(MonopolyCellUpdate UpdatedData)
        {
            
        }
    }
}
