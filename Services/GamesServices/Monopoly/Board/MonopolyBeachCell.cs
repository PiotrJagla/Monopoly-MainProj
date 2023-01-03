using Enums.Monopoly;
using Models;
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
            List<MonopolyCell> NewBoard = Board;

            List<MonopolyCell> AllBeaches = NewBoard.FindAll(c => c.GetBeachName() != Beach.NoBeach);

            List<PlayerKey> CheckedOwners = new List<PlayerKey>();
            foreach (var BeachCell in AllBeaches)
            {
                CheckBeachCellMonopol(ref NewBoard,ref CheckedOwners, BeachCell.GetOwner());
            }

            return NewBoard;
        }

        private void CheckBeachCellMonopol(ref List<MonopolyCell> NewBoard,ref List<PlayerKey> CheckedOwners, PlayerKey CurrentBeachCellOwner)
        {
            List<MonopolyCell> AllBeaches = NewBoard.FindAll(c => c.GetBeachName() != Beach.NoBeach);
            if (CheckedOwners.IndexOf(CurrentBeachCellOwner) == -1)
            {
                List<MonopolyCell> AllBeachesWithSameOwner = new List<MonopolyCell>();
                AllBeachesWithSameOwner = AllBeaches.FindAll(b => b.GetOwner() == CurrentBeachCellOwner);

                if (AllBeachesWithSameOwner.Count >= 2)
                {
                    ApplyMonopol(ref NewBoard, AllBeachesWithSameOwner);
                }

                CheckedOwners.Add(CurrentBeachCellOwner);
            }
        }

        public void ApplyMonopol(ref List<MonopolyCell> NewBoard,in List<MonopolyCell> AllBeachesWithSameOwner)
        {
            foreach (var BeachCell in AllBeachesWithSameOwner)
            {
                int CellIndexToUpdate = NewBoard.IndexOf(BeachCell);
                NewBoard[CellIndexToUpdate].MultiplyStayCostAmount(
                    Consts.Monopoly.BeachesOwnedMultiplayer[AllBeachesWithSameOwner.Count]
                );
            }
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

        public Beach GetBeachName()
        {
            return BeachName;
        }

        public void MultiplyStayCostAmount(float Multiplayer)
        {
            ActualCosts.Stay = (int)(BaseCosts.Stay * Multiplayer);
        }

        public StringModalParameters GetModalParameters()
        {
            return null;
        }
    }
}
