using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Models.MultiplayerConnection;
using Services.GamesServices.Monopoly.Update;

namespace Services.GamesServices.Monopoly
{
    public interface MonopolyService
    {
        void StartGame(List<Player> PlayersInGame);

        List<MonopolyCell> GetBoard();

        MonopolyUpdateMessage GetUpdatedData();

        void UpdateData(MonopolyUpdateMessage UpdatedData);

        MoveResult Move(int amount);

        void SetMainPlayerIndex(int index);

        bool IsYourTurn();

        void NextTurn();

        void BuyCellIfPossible();

    }
}
