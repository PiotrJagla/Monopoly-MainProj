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
    public class ChampionshipCell : MonopolyCell
    {
        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return new CellNotAbleToBuyBehaviour();
        }

        public MonopolyModalParameters GetModalParameters(DataToGetModalParameters Data)
        {
            ModalParametersFactory Factory = new ModalParametersFactory();
            return Factory.ChampionshipParameters(Data, "Choose Cell To Set World Championship");

            StringModalParameters Parameters = new StringModalParameters();

            foreach (var cell in Data.Board)
            {
                if(CanAddCellToModal(cell,Data.MainPlayer.Key))
                    Parameters.ButtonsContent.Add(cell.OnDisplay());
            }

            if(Parameters.ButtonsContent.Count == 0)
                return MonopolyModalFactory.NoModalParameters();

            Parameters.Title = "Choose Cell To Set World Championship";
            return new MonopolyModalParameters(Parameters, ModalShow.AfterMove);
        }
        
        private bool CanAddCellToModal(MonopolyCell cell, PlayerKey MainPlayerKey)
        {
            return cell is MonopolyNationCell && cell.GetBuyingBehavior().GetOwner() == MainPlayerKey;
        }

        public string OnDisplay()
        {
            return "World Championship";
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

            UpdatedData.BoardService.SetChampionship(Data.ModalResponse);

            return UpdatedData;
        }

        public string GetName()
        {
            return "Championship";
        }

        public void UpdateData(MonopolyCellUpdate UpdatedData)
        {
            
        }
    }
}
