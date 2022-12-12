using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using Services.OnlineConnectionsService;
using Models.MultiplayerConnection;
using Enums.MultiplayerConnection;
using Services.GamesServices.Monopoly;
using Models.Monopoly;
using Models;

namespace ASPcoreServer.Hubs
{
    public class MonopolyHub : Hub
    {
        private static GameConnectionService ConnectionService = new PlayersConnection();

        public void OnUserConnected(string userName)
        {
            ConnectionService.addOnlinePlayer(userName, Context.ConnectionId);
        }

        public async Task JoinToRoom()
        {
            
            ConnectionService.JoinToRoom(Context.ConnectionId);

            string RoomKey = ConnectionService.GetPlayer(Context.ConnectionId).InRoom;
            List<Player> AllPlayersInRoom = ConnectionService.GetPlayersWithCriteria(PlayersSelectCriteria.AllPlayers, Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, RoomKey);
            await Clients.Group(RoomKey).SendAsync("UserJoined", AllPlayersInRoom.Count);
        
        }

        public Task SendMessageToGroup(string message)
        {
            string RoomKey = ConnectionService.GetPlayer(Context.ConnectionId).InRoom;
            return Clients.Group(RoomKey).SendAsync("RecieveMessage", message);
        }

        public async Task UserReady()
        {
            
            ConnectionService.GetPlayer(Context.ConnectionId).NotReady = false;

            string RoomKey = ConnectionService.GetPlayer(Context.ConnectionId).InRoom;
            List<Player> ReadyPlayersInRoom = ConnectionService.GetPlayersWithCriteria(PlayersSelectCriteria.ReadyPlayers, Context.ConnectionId);

            await Clients.Group(RoomKey).SendAsync("ReadyPlayers", ReadyPlayersInRoom);
            
        }

        public Task UpdatePlayersData(List<PlayerUpdateData> NewData)
        {
            string RoomKey = ConnectionService.GetPlayer(Context.ConnectionId).InRoom;
            return Clients.Group(RoomKey).SendAsync("UpdatePlayersData", NewData);
        }

    }
}
