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
            Board.Add(new MonopolyNationCell(NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.Poland));
            Board.Add(new MonopolyNationCell(NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.Poland));

            Board.Add(new AirportCell());

            Board.Add(new MonopolyBeachCell(new Costs(100, 30), Beach.Dubaj));

            Board.Add(new ChampionshipCell());

            Board.Add(new MonopolyNationCell(NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.France));
            Board.Add(new MonopolyNationCell(NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.France));
            Board.Add(new MonopolyNationCell(NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.France));

            Board.Add(new MonopolyTaxCell());

            Board.Add(new MonopolyIslandCell(RefToMainPlayerTurnsOnIslandRemainig));

            Board.Add(new MonopolyBeachCell(new Costs(100, 30), Beach.Bali));

            Board.Add(new MonopolyNationCell(NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.Argentina));
            Board.Add(new MonopolyNationCell(NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.Argentina));
            Board.Add(new MonopolyNationCell(NationCellCostsFactory.GetNationCellCostsOfNumber(NationCellNumber++), Nation.Argentina));

            Board.Add(new MonopolyBeachCell(new Costs(100, 30), Beach.Cypr));
            return Board;
        }
    }
}
