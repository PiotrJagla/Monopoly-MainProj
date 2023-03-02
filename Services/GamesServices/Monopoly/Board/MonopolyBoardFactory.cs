using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board
{
    public class MonopolyBoardFactory
    {
        public static List<MonopolyCell> MakeBoard(ref Int RefToMainPlayerTurnsOnIslandRemainig)
        {
            List<MonopolyCell> Board = new List<MonopolyCell>();
            Board.Add(new MonopolyStartCell());

            int NationCellNumber = 0;
            Board.Add(new MonopolyNationCell(
                NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.Poland, City.Warszawa));
            Board.Add(new MonopolyNationCell(
                NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.Poland, City.Krakow));

            Board.Add(new MonopolyChanceCell());

            Board.Add(new AirportCell());

            Board.Add(new MonopolyBeachCell(Consts.Monopoly.BeachCellCosts, Beach.Dubaj));

            Board.Add(new ChampionshipCell());
            
            Board.Add(new MonopolyNationCell(
                NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.France, City.Paris));
            Board.Add(new MonopolyNationCell(
                NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.France,City.Lyon));
            Board.Add(new MonopolyNationCell(
                NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.France,City.Marsylia));

            Board.Add(new MonopolyTaxCell());

            Board.Add(new MonopolyIslandCell(RefToMainPlayerTurnsOnIslandRemainig));

            Board.Add(new MonopolyBeachCell(Consts.Monopoly.BeachCellCosts, Beach.Bali));

            Board.Add(new MonopolyNationCell(
                NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.Argentina,City.Quilmes));
            Board.Add(new MonopolyNationCell(
                NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.Argentina, City.Cordoba));
            Board.Add(new MonopolyNationCell(
                NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.Argentina, City.Rosario));

            

            Board.Add(new MonopolyBeachCell(Consts.Monopoly.BeachCellCosts, Beach.Cypr));
            return Board;
        }
    }
}
