using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Monopoly;
using Models.MultiplayerConnection;

namespace Services.GamesServices.Monopoly
{
    public interface MonopolyService
    {
        void StartGame(List<Player> PlayersInGame);

        List<MonopolyCell> GetBoard();

        PlayersUpdateData GetPlayersUpdatedData();

        void UpdatePlayersData(List<PlayerUpdateData> UpdatedData);

        void Move(int amount);

        void SetMainPlayerIndex(int index);

        bool IsYourTurn();

        void NextTurn();

    }
}
