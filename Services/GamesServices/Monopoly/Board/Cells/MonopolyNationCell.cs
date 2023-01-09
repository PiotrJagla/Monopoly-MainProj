using Enums.Monopoly;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using Models.Monopoly;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pkcs;
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
        private PlayerKey OwnedBy;
        private Costs ActualCosts { get; set; }
        private Costs BaseCosts { get; set; }
        private Nation OfNation { get; set; }

        private CellBuyingBehaviour BuyingBehaviour;


        public MonopolyNationCell(Costs costs, Nation nation = Nation.NoNation)
        {
            ActualCosts = new Costs(costs.Buy, costs.Stay);
            BaseCosts = new Costs(costs.Buy, costs.Stay);
            OfNation = nation;
            OwnedBy = PlayerKey.NoOne;

            BuyingBehaviour = new CellAbleToBuyBehaviour(costs);
        }

        public Nation GetNation()
        {
            return OfNation;
        }

        public PlayerKey GetOwner()
        {
            return OwnedBy;
        }

        public Costs GetCosts()
        {
            return ActualCosts;
        }

        public void SetCosts(Costs costs)
        {
            ActualCosts.Stay = costs.Stay;
            ActualCosts.Buy = costs.Buy;
        }

        public string OnDisplay()
        {
            string result = "";
            result += $" Owner: {OwnedBy.ToString()} |";
            result += $" Nation: {OfNation.ToString()} |";
            result += $" Buy For: {ActualCosts.Buy} |";
            result += $" Stay Cost: {ActualCosts.Stay} ";
            return result;
        }

        public void SetOwner(PlayerKey NewOwner)
        {
            OwnedBy = NewOwner;
        }

        public List<MonopolyCell> MonopolChanges(in List<MonopolyCell> Board)
        {
            List<MonopolyCell> UpdatedBoard = new List<MonopolyCell>();
            UpdatedBoard = Board;

            List<MonopolyCell> SingleNationCells = UpdatedBoard.FindAll(
                c => c.GetNation() == OfNation
            );

            PlayerKey SingleNationCellOwner = SingleNationCells[0].GetOwner();

            List<MonopolyCell> SingleNationCellsWithSameOwner = SingleNationCells.FindAll(
                c => c.GetOwner() == SingleNationCellOwner
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
                UpdatedBoard[UpdatedBoard.IndexOf(monopolCell)].MultiplyStayCostAmount(Consts.Monopoly.MonopolMultiplayer);
            }
        }

        public Beach GetBeachName()
        {
            return Beach.NoBeach;
        }

        public void MultiplyStayCostAmount(float Multiplayer)
        {
            ActualCosts.Stay = (int)(BaseCosts.Stay * Multiplayer);
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
