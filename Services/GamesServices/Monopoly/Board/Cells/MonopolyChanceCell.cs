using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Behaviours.Buying;
using Services.GamesServices.Monopoly.Board.ModalData;
using StringManipulationLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Cells
{
    public class MonopolyChanceCell : MonopolyCell
    {
        public int CellBought(MonopolyPlayer MainPlayer, string WhatIsBought, ref List<MonopolyCell> CheckMonopol)
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
            StringModalParameters Parameters = new StringModalParameters();


            Parameters.Title = "You can roll something";
            int RandomRoll = GetRandom.number.Next(1,2);
            
            if(RandomRoll == 1)
            {
                Parameters.ButtonsContent.Add(Consts.Monopoly.PayTaxRolled);
            }
            else if(RandomRoll == 2)
            {
                ModalParametersFactory Factory = new ModalParametersFactory();
                return Factory.ChampionshipParameters(
                    Data, "You Rolled New Chamionship", Consts.Monopoly.NewChampionshipModalPrefix);
            }
            

            return new MonopolyModalParameters(Parameters, ModalShow.AfterMove);
        }

        private bool CanAddCellToModal(MonopolyCell cell, PlayerKey MainPlayerKey)
        {
            return cell is MonopolyNationCell && cell.GetBuyingBehavior().GetOwner() == MainPlayerKey;
        }

        public ModalResponseUpdate OnModalResponse(ModalResponseData Data)
        {
            ModalResponseUpdate UpdatedData = new ModalResponseUpdate();
            UpdatedData.BoardService = Data.BoardService;
            UpdatedData.PlayersService = Data.PlayersService;
            
            if(Data.ModalResponse == Consts.Monopoly.PayTaxRolled)
            {
                UpdatedData.PlayersService.ChargeMainPlayer(Consts.Monopoly.TaxAmount);
            }
            else if(Data.ModalResponse.Contains(Consts.Monopoly.NewChampionshipModalPrefix))
            {
                int length = Consts.Monopoly.NewChampionshipModalPrefix.Length;
                string CellDisplay = Data.ModalResponse.Remove(0,length);
                UpdatedData.BoardService.SetChampionship(CellDisplay);
            }

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

        public void UpdateData(MonopolyCellUpdate UpdatedData)
        {
            
        }
    }
}
