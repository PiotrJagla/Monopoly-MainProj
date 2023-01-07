using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using Services.OnlineConnectionsService;
using Models.MultiplayerConnection;
using Enums.MultiplayerConnection;
using Models.Monopoly;
using Models;
using Services.GamesServices.Monopoly.Update;

namespace ServerSide.Hubs
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

        public async Task UserReady()
        {
            
            ConnectionService.GetPlayer(Context.ConnectionId).NotReady = false;

            string RoomKey = ConnectionService.GetPlayer(Context.ConnectionId).InRoom;
            List<Player> ReadyPlayersInRoom = ConnectionService.GetPlayersWithCriteria(PlayersSelectCriteria.ReadyPlayers, Context.ConnectionId);

            await Clients.Group(RoomKey).SendAsync("ReadyPlayers", ReadyPlayersInRoom);
            
        }

        public Task UpdateData(MonopolyUpdateMessage NewData)
        {
            string RoomKey = ConnectionService.GetPlayer(Context.ConnectionId).InRoom;
            return Clients.Group(RoomKey).SendAsync("UpdateData", NewData);
        }

    }
}
