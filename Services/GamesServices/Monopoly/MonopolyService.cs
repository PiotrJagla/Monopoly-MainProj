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

        PlayersPositionsData GetPlayersPositions();

        void UpdatePlayersPositions(PlayersPositionsData UpdatedPositions);

        void Move(int amount);

        void SetMainPlayerIndex(int index);

        Point2D GetPoint();
    }
}
