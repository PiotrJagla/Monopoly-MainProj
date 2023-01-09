﻿using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.BuyingBehaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Cells
{
    internal class StartCell : MonopolyCell
    {
        private CellBuyingBehaviour BuyingBehaviour;
        public StartCell()
        {
            BuyingBehaviour = new CellNotAbleToBuyBehaviour();
        }
        public Beach GetBeachName()
        {
            return Beach.NoBeach;
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return BuyingBehaviour;
        }

        public Costs GetCosts()
        {
            return new Costs();
        }

        public MonopolyModalParameters GetModalParameters()
        {
            return null;
        }

        public Nation GetNation()
        {
            return Nation.NoNation;
        }

        public PlayerKey GetOwner()
        {
            return PlayerKey.NoOne;
        }



        public List<MonopolyCell> MonopolChanges(in List<MonopolyCell> Board)
        {
            return Board;
        }

        public void MultiplyStayCostAmount(float Multiplayer)
        {

        }

        public string OnDisplay()
        {
            return "Start!";
        }

        public void SetCosts(Costs costs)
        {

        }

        public void SetOwner(PlayerKey NewOwner)
        {

        }
    }
}