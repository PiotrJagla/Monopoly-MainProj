using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Services.OnlineConnectionsService;
using Models.MultiplayerConnection;

namespace ASPcoreServer.Hubs
{
    public class SomeMultiplayerGameHub : Hub
    {
        private static GameConnectionService PlayersGameConnection = new PlayersConnection();


        public void SendToEnemyUserButtonPoints(string userName, int userPoints)
        {
            Player enemy = PlayersGameConnection.getUserEnemy(userName);
            Clients.Client(enemy.ConnectionId).SendAsync("RecieveEnemyPoints", userPoints);
        }

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
