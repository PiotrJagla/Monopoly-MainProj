using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Services.GamesServices.Battleships;
using Services.OnlineConnectionsService;

namespace ASPcoreServer.Hubs
{
    public class BattleshipHub : Hub
    {
        private static GameConnectionService PlayersGameConnection = new PlayersConnection();

        private static BattleshipService BattleshipLogic = new BattleshipGameLogic();

        public void OnUserConnected(string userName, string userConnId)
        {
            PlayersGameConnection.addOnlinePlayer(userName, userConnId);
        }

        public void FindEnemyForUser(string userName)
        {
            bool isEnemyFound = PlayersGameConnection.findEnemy(userName);
            Tuple<Player, Player> twoPlayersRoom = PlayersGameConnection.findGameRoomByOneName(userName);
            if (twoPlayersRoom is not null)
            {
                Clients.Client(twoPlayersRoom.Item1.ConnectionId).SendAsync("IsEnemyFound", isEnemyFound);
                Clients.Client(twoPlayersRoom.Item2.ConnectionId).SendAsync("IsEnemyFound", isEnemyFound);
            }
        }

    }
}
