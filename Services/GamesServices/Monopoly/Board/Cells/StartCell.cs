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
    internal class StartCell : MonopolyCell
    {
        private CellBuyingBehaviour BuyingBehaviour;
        private MonopolBehaviour monopolBehaviour;
        public StartCell()
        {
            BuyingBehaviour = new CellNotAbleToBuyBehaviour();
            monopolBehaviour = new NoMonopolBehaviour();
        }
        public Beach GetBeachName()
        {
            return Beach.NoBeach;
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return BuyingBehaviour;
        }

        public MonopolyModalParameters GetModalParameters()
        {
            return null;
        }

        public List<MonopolyCell> GetMonopolOff(in List<MonopolyCell> Board, int OnCell)
        {
            return monopolBehaviour.GetMonopolOff(Board,OnCell);
        }

        public Nation GetNation()
        {
            return Nation.NoNation;
        }

        public List<MonopolyCell> MonopolChanges(in List<MonopolyCell> Board, int OnCell)
        {
            return monopolBehaviour.UpdateBoardMonopol(Board,OnCell);
        }

        public string OnDisplay()
        {
            return "Start!";
        }
    }
}
