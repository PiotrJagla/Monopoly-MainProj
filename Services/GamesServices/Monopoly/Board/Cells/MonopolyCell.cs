using Enums.Monopoly;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Behaviours.Buying;
using Services.GamesServices.Monopoly.Board.Behaviours.Monopol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Cells
{
    public interface MonopolyCell
    {
        Nation GetNation();
        Beach GetBeachName();

        CellBuyingBehaviour GetBuyingBehavior();

        void CellBought(MonopolyPlayer MainPlayer, string WhatIsBought,ref List<MonopolyCell> CheckMonopol);
        void CellSold(ref List<MonopolyCell> MonopolChanges, int CellIndex);

        MonopolBehaviour MonopolChanges();
        string OnDisplay();
        MonopolyModalParameters GetModalParameters(in List<MonopolyCell> Board, MonopolyPlayer MainPlayer);
    }
}
