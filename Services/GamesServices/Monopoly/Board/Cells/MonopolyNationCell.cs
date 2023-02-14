using Enums.Monopoly;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using Models.Monopoly;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pkcs;
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
    public class MonopolyNationCell : MonopolyCell
    {
        private Nation OfNation { get; set; }

        private CellBuyingBehaviour BuyingBehaviour;
        private MonopolBehaviour monopolBehaviour;

        public MonopolyNationCell(Costs costs, Nation nation = Nation.NoNation)
        {
            OfNation = nation;
            BuyingBehaviour = new CellAbleToBuyBehaviour(costs);
            monopolBehaviour = new MonopolNationCellBehaviour();
        }

        public Nation GetNation()
        {
            return OfNation;
        }
        public string OnDisplay()
        {
            string result = "";
            result += $" Owner: {BuyingBehaviour.GetOwner().ToString()} |";
            result += $" Nation: {OfNation.ToString()} |";
            result += $" Buy For: {BuyingBehaviour.GetCosts().Buy} |";
            result += $" Stay Cost: {BuyingBehaviour.GetCosts().Stay} ";
            if (BuyingBehaviour.IsThereChampionship() == true)
                result += Consts.Monopoly.ChampionshipInfo;
            return result;
        }
        public Beach GetBeachName()
        {
            return Beach.NoBeach;
        }
        public MonopolyModalParameters GetModalParameters(in List<MonopolyCell> Board, MonopolyPlayer MainPlayer)
        {
            if (Board[MainPlayer.OnCellIndex].GetBuyingBehavior().GetOwner() != PlayerKey.NoOne)
                return null;

            StringModalParameters Parameters = new StringModalParameters();

            Parameters.Title = "Do you wany to buy this cell";
            Parameters.ButtonsContent.Add("Yes");
            Parameters.ButtonsContent.Add("No");
            return new MonopolyModalParameters(Parameters, ModalShow.AfterMove, ModalResponseIdentifier.Nation);
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return BuyingBehaviour;
        }

        public MonopolBehaviour MonopolChanges()
        {
            return monopolBehaviour;
        }

        public void CellBought(MonopolyPlayer MainPlayer, string WhatIsBought)
        {
            BuyingBehaviour.SetOwner(MainPlayer.Key);
        }
    }
}
