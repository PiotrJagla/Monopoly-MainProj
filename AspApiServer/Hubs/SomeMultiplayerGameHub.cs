using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Services.OnlineConnectionsService;
using Models;

namespace ASPcoreServer.Hubs
{
    public class SomeMultiplayerGameHub : Hub
    {
        private static GameConnectionService PlayersGameConnection = new PlayersConnection();


        public void SendToEnemyUserButtonPoints(string userName, int userPoints)
        {
            Player enemy = PlayersGameConnection.getEnemyWithGivenUserName(userName);
            Clients.Client(enemy.ConnectionId).SendAsync("RecieveEnemyPoints", userPoints);
        }

        public void OnPlayerConnected(string userName, string userConnId)
        {
            PlayersGameConnection.addOnlinePlayer(userName, userConnId);
        }

        public void FindEnemyForPlayer(string userName)
        {
            bool isEnemyFound = PlayersGameConnection.findEnemy(userName);
            Tuple<Player, Player> twoPlayersRoom = PlayersGameConnection.getTwoUsersGameRoomWithGiveName(userName);
            if (twoPlayersRoom is not null)
            {
                Clients.Client(twoPlayersRoom.Item1.ConnectionId).SendAsync("IsEnemyFound", isEnemyFound);
                Clients.Client(twoPlayersRoom.Item2.ConnectionId).SendAsync("IsEnemyFound", isEnemyFound);
            }
        }

    }
}
