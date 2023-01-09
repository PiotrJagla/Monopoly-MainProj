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
    public class MonopolNationCellBehaviour : MonopolBehaviour
    {
        public List<MonopolyCell> UpdateBoardMonopol(in List<MonopolyCell> Board, int OnCell)
        {
            List<MonopolyCell> UpdatedBoard = new List<MonopolyCell>();
            UpdatedBoard = Board;

            List<MonopolyCell> SingleNationCells = UpdatedBoard.FindAll(
                c => c.GetNation() == UpdatedBoard[OnCell].GetNation()
            );

            PlayerKey SingleNationCellOwner = SingleNationCells[0].GetBuyingBehavior().GetOwner();

            List<MonopolyCell> SingleNationCellsWithSameOwner = SingleNationCells.FindAll(
                c => c.GetBuyingBehavior().GetOwner() == SingleNationCellOwner
            );

            if (SingleNationCells.Count == SingleNationCellsWithSameOwner.Count)
            {
                ApplyMonopol(ref UpdatedBoard, SingleNationCells);
            }
            return UpdatedBoard;
        }

        private void ApplyMonopol(ref List<MonopolyCell> UpdatedBoard, List<MonopolyCell> SingleNationCells)
        {
            foreach (var monopolCell in SingleNationCells)
            {
                UpdatedBoard[UpdatedBoard.IndexOf(monopolCell)].GetBuyingBehavior().MultiplyStayCostAmount(Consts.Monopoly.MonopolMultiplayer);
            }
        }
    }
}
