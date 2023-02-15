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

        public void CellBought(MonopolyPlayer MainPlayer, string WhatIsBought, List<MonopolyCell> CheckMonopol)
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

        public MonopolyModalParameters GetModalParameters(in List<MonopolyCell> Board, MonopolyPlayer MainPlayer)
        {
            StringModalParameters Parameters = new StringModalParameters();

            Parameters.Title = "Choose a cell where you want to go";
            foreach (var cell in Board)
            {
                if(cell is MonopolyNationCell || cell is MonopolyBeachCell)
                    Parameters.ButtonsContent.Add(cell.OnDisplay());
            }
            return new MonopolyModalParameters(Parameters, ModalShow.BeforeMove, ModalResponseIdentifier.Airport);
        }

        public Nation GetNation()
        {
            return Nation.NoNation;
        }

        public MonopolBehaviour MonopolChanges()
        {
            return monopolBehaviour;
        }

        public string OnDisplay()
        {
            return "Airport";
        }
    }
}
