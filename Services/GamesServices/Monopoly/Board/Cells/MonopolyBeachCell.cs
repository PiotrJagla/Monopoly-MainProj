using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Behaviours;
using Services.GamesServices.Monopoly.Board.BuyingBehaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Cells
{
    public class MonopolyBeachCell : MonopolyCell
    {
        private Beach BeachName { get; set; }

        private CellBuyingBehaviour BuyingBehaviour;

        private MonopolBehaviour monopolBehaviour;

        public MonopolyBeachCell(Costs costs, Beach WhatBeach = Beach.NoBeach)
        {
            BeachName = WhatBeach;

            BuyingBehaviour = new CellAbleToBuyBehaviour(costs);
            monopolBehaviour = new MonopolBeachCellBehaviour();
        }
        public Nation GetNation()
        {
            return Nation.NoNation;
        }
        public List<MonopolyCell> MonopolChanges(in List<MonopolyCell> Board, int OnCell)
        {
            return monopolBehaviour.UpdateBoardMonopol(Board, OnCell);
            //List<MonopolyCell> NewBoard = Board;

            //List<MonopolyCell> AllBeaches = NewBoard.FindAll(c => c is MonopolyBeachCell);

            //List<PlayerKey> CheckedOwners = new List<PlayerKey>();
            //CheckedOwners.Add(PlayerKey.NoOne);
            //foreach (var BeachCell in AllBeaches)
            //{
            //    CheckBeachCellMonopol(ref NewBoard, ref CheckedOwners, BeachCell.GetBuyingBehavior().GetOwner());
            //}

            //return NewBoard;
        }

        private void CheckBeachCellMonopol(ref List<MonopolyCell> NewBoard, ref List<PlayerKey> CheckedOwners, PlayerKey CurrentBeachCellOwner)
        {
            List<MonopolyCell> AllBeaches = NewBoard.FindAll(c => c is MonopolyBeachCell);
            if (CheckedOwners.IndexOf(CurrentBeachCellOwner) == -1)
            {
                List<MonopolyCell> AllBeachesWithSameOwner = new List<MonopolyCell>();
                AllBeachesWithSameOwner = AllBeaches.FindAll(b => b.GetBuyingBehavior().GetOwner() == CurrentBeachCellOwner);

                if (AllBeachesWithSameOwner.Count >= 2)
                {
                    ApplyMonopol(ref NewBoard, AllBeachesWithSameOwner);
                }

                CheckedOwners.Add(CurrentBeachCellOwner);
            }
        }

        public void ApplyMonopol(ref List<MonopolyCell> NewBoard, in List<MonopolyCell> AllBeachesWithSameOwner)
        {
            foreach (var BeachCell in AllBeachesWithSameOwner)
            {
                int CellIndexToUpdate = NewBoard.IndexOf(BeachCell);
                NewBoard[CellIndexToUpdate].GetBuyingBehavior().MultiplyStayCostAmount(
                    Consts.Monopoly.BeachesOwnedMultiplayer[AllBeachesWithSameOwner.Count]
                );
            }
        }

        public string OnDisplay()
        {
            string result = "";
            result += $" Owner: {BuyingBehaviour.GetOwner().ToString()} |";
            result += $" Nation: {BeachName.ToString()} |";
            result += $" Buy For: {BuyingBehaviour.GetCosts().Buy} |";
            result += $" Stay Cost: {BuyingBehaviour.GetCosts().Stay} ";
            return result;
        }
        public Beach GetBeachName()
        {
            return BeachName;
        }

        public MonopolyModalParameters GetModalParameters()
        {
            return null;
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return BuyingBehaviour;
        }
    }
}
