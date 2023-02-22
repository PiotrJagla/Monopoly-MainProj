using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Models.MultiplayerConnection;
using Services.GamesServices.Monopoly.Board.Cells;
using Services.GamesServices.Monopoly.Update;

namespace Services.GamesServices.Monopoly
{
    public interface MonopolyService
    {
        void StartGame(List<Player> PlayersInGame);
        List<MonopolyCell> GetBoard();
        List<MonopolyCell> GetMainPlayerCells();
        MonopolyUpdateMessage GetUpdatedData();
        int GetDebtAmount();
        void UpdateData(MonopolyUpdateMessage UpdatedData);
        void ExecutePlayerMove(int MoveAmount);
        void SetMainPlayerIndex(int index);
        bool IsYourTurn();
        void NextTurn();
        bool DontHaveMoneyToPay();
        void SellCell(string CellToSellDisplay);
        PlayerKey WhoWon();
        MonopolyModalParameters GetModalParameters();
        void ModalResponse(string StringResponse);

    }
}
