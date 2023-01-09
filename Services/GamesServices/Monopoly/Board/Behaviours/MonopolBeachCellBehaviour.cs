using Enums.Monopoly;
using Models;
using Services.GamesServices.Monopoly.Board.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Behaviours
{
    public class MonopolBeachCellBehaviour : MonopolBehaviour
    {
        public List<MonopolyCell> UpdateBoardMonopol(in List<MonopolyCell> Board, int OnCell)
        {
            List<MonopolyCell> NewBoard = Board;

            List<MonopolyCell> AllBeaches = NewBoard.FindAll(c => c is MonopolyBeachCell);

            List<PlayerKey> CheckedOwners = new List<PlayerKey>();
            CheckedOwners.Add(PlayerKey.NoOne);
            foreach (var BeachCell in AllBeaches)
            {
                CheckBeachCellMonopol(ref NewBoard, ref CheckedOwners, BeachCell.GetBuyingBehavior().GetOwner());
            }

            return NewBoard;
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
    }
}
