using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Enums;
using Models;
using Services.GamesServices.Battleships;
using Services.OnlineConnectionsService;
using Models.Battleship;
using Models.MultiplayerConnection;

namespace ServerSide.Hubs
{
    public class BattleshipHub : Hub
    {
        private static GameConnectionService PlayersGameConnection = new PlayersConnection();

        public void OnUserConnected(string userName)
        {
            Console.WriteLine("USER CONNECTED");
            PlayersGameConnection.addOnlinePlayer(userName,Context.ConnectionId);
        }

        public void FindEnemyForUser(string userName)
        {
            Console.WriteLine("FINDING ENEMY");
            bool isEnemyFound = PlayersGameConnection.findEnemy(userName);
            Tuple<Player, Player> twoPlayersRoom = PlayersGameConnection.findGameRoomByOneName(userName);
            if (twoPlayersRoom is not null)
            {
                bool YourTurn = true;
                Clients.Client(twoPlayersRoom.Item1.ConnectionId).SendAsync("IsEnemyFound", isEnemyFound, YourTurn);
                Clients.Client(twoPlayersRoom.Item2.ConnectionId).SendAsync("IsEnemyFound", isEnemyFound, !YourTurn);
            }
        }

        public async Task UserAttack(Point2D OnPoint, string userName)
        {
            Player enemy = PlayersGameConnection.getUserEnemy(userName);
            await Clients.Client(enemy.ConnectionId).SendAsync("EnemyAttack", OnPoint);
        }

        public async Task SendAttackedCell(BattleshipCell AttackedCell, bool IsShipDestroyed, string userName)
        {
            Player enemy = PlayersGameConnection.getUserEnemy(userName);
            await Clients.Client(enemy.ConnectionId).SendAsync("RecieveAttackedCell", AttackedCell, IsShipDestroyed);
        }



    }
}
