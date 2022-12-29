using Enums.Monopoly;
using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board
{
    public class MonopolyBeachCell : MonopolyCell
    {
        public PlayerKey OwnedBy { get; set; }
        public Beach BeachName { get; set; }

        public Costs ActualCosts { get; set; }
        public Costs BaseCosts { get; set; }

        public MonopolyBeachCell(Costs costs = null, Beach WhatBeach = Beach.NoBeach)
        {
            BeachName = WhatBeach;
            ActualCosts = new Costs(costs.Buy, costs.Stay);
            BaseCosts = new Costs(costs.Buy, costs.Stay);
            OwnedBy = PlayerKey.NoOne;
        }

        public Costs GetCosts()
        {
            return ActualCosts;
        }

        public Nation GetNation()
        {
            return Nation.NoNation;
        }

        public PlayerKey GetOwner()
        {
            return OwnedBy;
        }

        public List<MonopolyCell> MonopolChanges(in List<MonopolyCell> Board)
        {
            List<MonopolyCell> UpdatedBoard = Board;

            return UpdatedBoard;
        }

        public string OnDisplay()
        {
            string result = "";
            result += $" Owner: {OwnedBy.ToString()} |";
            result += $" Beach Name: {BeachName.ToString()} |";
            result += $" Buy For: {ActualCosts.Buy} |";
            result += $" Stay Cost: {ActualCosts.Stay} ";
            return result;
        }

        public void SetCosts(Costs costs)
        {
            ActualCosts = costs;
        }

        public void SetOwner(PlayerKey NewOwner)
        {
            OwnedBy = NewOwner;
        }
    }
}
