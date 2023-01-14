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
    public class ChampionshipCell : MonopolyCell
    {
        private CellBuyingBehaviour BuyingBehaviour;
        private MonopolBehaviour monopolBehaviour;

        public ChampionshipCell()
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

        public MonopolyModalParameters GetModalParameters(in List<MonopolyCell> Board, PlayerKey MainPlayerKey)
        {
            StringModalParameters Result = new StringModalParameters();


            List<MonopolyCell> MainPlayerNationCells = Board.FindAll(
                c => c is MonopolyNationCell && c.GetBuyingBehavior().GetOwner() == MainPlayerKey
            );

            foreach (var cell in MainPlayerNationCells)
            {
                Result.ButtonsContent.Add(cell.OnDisplay());
            }

            Result.Title = "Choose Cell To Set World Championship";
            return new MonopolyModalParameters(Result, ModalShow.AfterMove);
        }

        public Nation GetNation()
        {
            return Nation.NoNation;
        }

        public MonopolBehaviour MonopolCHanges_NEW()
        {
            return monopolBehaviour;
        }

        public string OnDisplay()
        {
            return "World Championship";
        }
    }
}
