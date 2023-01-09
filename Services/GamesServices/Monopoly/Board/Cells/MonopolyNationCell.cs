using Enums.Monopoly;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using Models.Monopoly;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pkcs;
using Services.GamesServices.Monopoly.Board.Behaviours;
using Services.GamesServices.Monopoly.Board.BuyingBehaviours;
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
            return result;
        }
        public List<MonopolyCell> MonopolChanges(in List<MonopolyCell> Board, int OnCell)
        {
            return monopolBehaviour.UpdateBoardMonopol(Board, OnCell);
            //List<MonopolyCell> UpdatedBoard = new List<MonopolyCell>();
            //UpdatedBoard = Board;

            //List<MonopolyCell> SingleNationCells = UpdatedBoard.FindAll(
            //    c => c.GetNation() == OfNation
            //);

            //PlayerKey SingleNationCellOwner = SingleNationCells[0].GetBuyingBehavior().GetOwner();

            //List<MonopolyCell> SingleNationCellsWithSameOwner = SingleNationCells.FindAll(
            //    c => c.GetBuyingBehavior().GetOwner() == SingleNationCellOwner
            //);

            //if (SingleNationCells.Count == SingleNationCellsWithSameOwner.Count)
            //{
            //    ApplyMonopol(ref UpdatedBoard, SingleNationCells);
            //}
            //return UpdatedBoard;
        }

        private void ApplyMonopol(ref List<MonopolyCell> UpdatedBoard, List<MonopolyCell> SingleNationCells)
        {
            foreach (var monopolCell in SingleNationCells)
            {
                UpdatedBoard[UpdatedBoard.IndexOf(monopolCell)].GetBuyingBehavior().MultiplyStayCostAmount(Consts.Monopoly.MonopolMultiplayer);
            }
        }

        public Beach GetBeachName()
        {
            return Beach.NoBeach;
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
