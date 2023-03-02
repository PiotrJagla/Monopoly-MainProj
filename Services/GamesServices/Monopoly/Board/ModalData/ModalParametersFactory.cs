using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.ModalData
{
    public class ModalParametersFactory
    {
        public  MonopolyModalParameters ChampionshipParameters(DataToGetModalParameters Data,
            string Title, string ButtonsPrefix = "")
        {
            StringModalParameters Parameters = new StringModalParameters();

            foreach (var cell in Data.Board)
            {
                if (CanAddCellToModal(cell, Data.MainPlayer.Key))
                    Parameters.ButtonsContent.Add( $"{ButtonsPrefix}{cell.OnDisplay()}");
            }

            if (Parameters.ButtonsContent.Count == 0)
                return MonopolyModalFactory.NoModalParameters();

            Parameters.Title = Title;
            return new MonopolyModalParameters(Parameters, ModalShow.AfterMove);
        }

        private bool CanAddCellToModal(MonopolyCell cell, PlayerKey MainPlayerKey)
        {
            return cell is MonopolyNationCell && cell.GetBuyingBehavior().GetOwner() == MainPlayerKey;
        }
    }
}
