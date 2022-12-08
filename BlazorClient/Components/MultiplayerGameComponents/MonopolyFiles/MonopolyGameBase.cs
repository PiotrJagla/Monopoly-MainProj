using Enums.Monopoly;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.Monopoly;
using Models.MultiplayerConnection;
using Services.GamesServices.Monopoly;

namespace BlazorClient.Components.MultiplayerGameComponents.MonopolyFiles
{
    public class MonopolyGameBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public MonopolyService MonopolyLogic{ get; set; }

        [Parameter]
        public string loggerUserName { get; set; }

        private HubConnection MonopolyHubConn;

        public List<string> Messages{ get; set; }

        public int RoomPlayersNumber { get; set; }

        protected override async Task OnInitializedAsync()
        {
            RoomPlayersNumber = 0;
            Messages = new List<string>();
            MonopolyHubConn = new HubConnectionBuilder().WithUrl(NavManager.ToAbsoluteUri($"{Constants.ServerURL}{Constants.MonopolyHubURL}")).WithAutomaticReconnect().Build();
            await MonopolyHubConn.StartAsync();

            MonopolyHubConn.On<string>("RecieveMessage", (message) =>
            {
                Messages.Add(message);
                InvokeAsync(StateHasChanged);
            });

            MonopolyHubConn.On<int>("UserJoined", (AllPlayersInRoom) =>
            {
                RoomPlayersNumber = AllPlayersInRoom;
                MonopolyLogic.SetMainPlayerIndex(RoomPlayersNumber - 1);
                Messages.Add($"Players in Room: {RoomPlayersNumber}");
                InvokeAsync(StateHasChanged);
            });

            MonopolyHubConn.On<List<Player>>("ReadyPlayers", (ReadyPlayers) =>
            {
                Messages.Add($"Ready Players: {ReadyPlayers.Count}/{RoomPlayersNumber}");
                IsEveryoneReady(ReadyPlayers);
                InvokeAsync(StateHasChanged);
            });

            MonopolyHubConn.On<List<PlayerPosition>>("UpdatePositions", (NewPositions) =>
            {
                MonopolyLogic.UpdatePlayersPositions(NewPositions);
                MonopolyLogic.NextTurn();
                InvokeAsync(StateHasChanged);
            });
        }

        private void IsEveryoneReady(List<Player> ReadyPlayers)
        {
            if(ReadyPlayers.Count == RoomPlayersNumber)
            {
                MonopolyLogic.StartGame(ReadyPlayers);
                Messages.Add("Everyone is Ready");
            }
        }

        protected async Task EnterRoom()
        {
            await MonopolyHubConn.SendAsync("OnUserConnected", loggerUserName);
            await MonopolyHubConn.SendAsync("JoinToRoom");
            Messages.Add("Joined To Room");
            InvokeAsync(StateHasChanged);
        }

        protected async Task SendMessage()
        {
            await MonopolyHubConn.SendAsync("SendMessageToGroup", "Message from user");
        }

        protected async Task Ready()
        {
            await MonopolyHubConn.SendAsync("UserReady");
        }

        protected async Task Move()
        {
            MonopolyLogic.Move(GetRandom.number.Next(1, 3));
            PlayersPositionsData UpdatedPositions = MonopolyLogic.GetPlayersPositions();
            await MonopolyHubConn.SendAsync("UpdatePlayersPositions", UpdatedPositions.GetPlayersPositions());
        }
    }
}
