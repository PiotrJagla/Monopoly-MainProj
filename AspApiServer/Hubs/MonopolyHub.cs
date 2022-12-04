using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using Services.OnlineConnectionsService;
using Models.MultiplayerConnection;

namespace ASPcoreServer.Hubs
{
    public class MonopolyHub : Hub
    {
        private static GameConnectionService PlayersGameConnection = new PlayersConnection();

        public void OnUserConnected(string userName)
        {
            PlayersGameConnection.addOnlinePlayer(userName, Context.ConnectionId);
        }

        public void JoinRoom(string userName)
        {
            bool DidJoinedToRoom= PlayersGameConnection.JoinToRoom(userName);
            
        }
    }
}
