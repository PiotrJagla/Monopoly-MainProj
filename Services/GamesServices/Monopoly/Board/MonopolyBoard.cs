using Models;
using Models.Monopoly;
using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board
{
    public class MonopolyBoard
    {
        private List<MonopolyCell> Board = new List<MonopolyCell>();

        public MonopolyBoard()
        {
            Board = new List<MonopolyCell>();
            InitBoard();
        }

        private void InitBoard()
        {
            Board.Add(new StartCell());

            Board.Add(new MonopolyNationCell(new Costs(50,30), Nation.Poland));
            Board.Add(new MonopolyNationCell(new Costs(80, 40), Nation.Poland));

            Board.Add(new MonopolyBeachCell(new Costs(100, 30), Beach.Dubaj));

            Board.Add(new MonopolyNationCell(new Costs(130,70), Nation.France));
            Board.Add(new MonopolyNationCell(new Costs(110, 50), Nation.France));
            Board.Add(new MonopolyNationCell(new Costs(150,100), Nation.France));

            Board.Add(new MonopolyBeachCell(new Costs(100,30), Beach.Bali));

            Board.Add(new MonopolyNationCell(new Costs(180, 140), Nation.Argentina));
            Board.Add(new MonopolyNationCell(new Costs(250,200), Nation.Argentina));
            Board.Add(new MonopolyNationCell(new Costs(210, 150), Nation.Argentina));
        }

        public List<MonopolyCell> GetBoard()
        {
            return Board;
        }

        public bool DontHaveMoneyToPay(MonopolyPlayer Debtor)
        {
            return CanAffordStaying(Debtor) == false && DoesCellHaveAnotherOwner(Debtor) == true;
        }

        private bool CanAffordStaying(MonopolyPlayer Visitor)
        {
            return Visitor.MoneyOwned >= Board[Visitor.OnCellIndex].GetCosts().Stay;
        }

        public bool DoesCellHaveAnotherOwner(MonopolyPlayer PlayerStepped)
        {
            return IsNoOneCell(PlayerStepped.OnCellIndex) == false && IsPlayerCellOwner(PlayerStepped) == false;
        }

        public bool IsPossibleToBuyCell(MonopolyPlayer buyer)
        {
            return CanAffordBuying(buyer) && IsNoOneCell(buyer.OnCellIndex);
        }

        private bool CanAffordBuying(MonopolyPlayer buyer)
        {
            return buyer.MoneyOwned >= Board[buyer.OnCellIndex].GetCosts().Buy;
        }

        public bool IsNoOneCell(int CellIndex)
        {
            return Board[CellIndex].GetOwner() == PlayerKey.NoOne;
        }

        private bool IsPlayerCellOwner(MonopolyPlayer Player)
        {
            return Board[Player.OnCellIndex].GetOwner() == Player.Key;
        }

        public MonopolyCell GetCell(int Index)
        {
            return Board[Index];
        }

        public void CheckForMonopolOf(MonopolyPlayer aPlayer)
        {
            Board = Board[aPlayer.OnCellIndex].MonopolChanges(Board);
        }
    }
}
