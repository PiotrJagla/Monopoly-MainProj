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

namespace Services.GamesServices.Monopoly.Board.Cells
{
    public class AirportCell : MonopolyCell
    {
        private CellBuyingBehaviour BuyingBehaviour;
        private MonopolBehaviour monopolBehaviour;
        public AirportCell()
        {
            BuyingBehaviour = new CellNotAbleToBuyBehaviour();
            monopolBehaviour = new NoMonopolBehaviour();
        }

        public int CellBought(MonopolyPlayer MainPlayer, string WhatIsBought,ref List<MonopolyCell> CheckMonopol)
        {
            return 0;
        }

        public void CellSold(ref List<MonopolyCell> MonopolChanges)
        {
            

        }

        public Beach GetBeachName()
        {
            return Beach.NoBeach;
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return BuyingBehaviour;
        }

        public MonopolyModalParameters GetModalParameters(DataToGetModalParameters Data)
        {
            StringModalParameters Parameters = new StringModalParameters();

            Parameters.Title = "Choose a cell where you want to go";
            foreach (var cell in Data.Board)
            {
                if(cell is MonopolyNationCell || cell is MonopolyBeachCell)
                    Parameters.ButtonsContent.Add(cell.OnDisplay());
            }
            return new MonopolyModalParameters(Parameters, ModalShow.BeforeMove);
        }

        public Nation GetNation()
        {
            return Nation.NoNation;
        }

        public ModalResponseUpdate OnModalResponse(ModalResponseData Data)
        {
            ModalResponseUpdate UpdatedData = new ModalResponseUpdate();
            UpdatedData.BoardService = Data.BoardService;
            UpdatedData.PlayersService = Data.PlayersService;

            int MainPlayerPos = UpdatedData.PlayersService.GetMainPlayer().OnCellIndex;
            int CellsToJumpThrough = UpdatedData.BoardService.DistanceToCellFrom(MainPlayerPos, Data.ModalResponse);
            UpdatedData.MoveQuantity = CellsToJumpThrough;

            if (CellsToJumpThrough == 0)
                UpdatedData.MoveQuantity = 0;

            return UpdatedData;
        }

        public string OnDisplay()
        {
            return "Airport";
        }
    }
}
