using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Services.GamesServices.Battleships;

namespace BlazorClient.Components.MultiplayerGameComponents.BattleshipComponentFiles
{
    public class BattleshipGameBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public BattleshipService BattleshipLogic { get; set; }

        [Parameter]
        public string loggedUserName { get; set; }

        private HubConnection multiplayerGameHubConn;

        public string UserMessage { get; private set; }

        public bool IsEnemyFound { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UserMessage = "";
            multiplayerGameHubConn = new HubConnectionBuilder().WithUrl(NavManager.ToAbsoluteUri($"{Constants.ServerURL}/battleshiphub")).WithAutomaticReconnect().Build();

            multiplayerGameHubConn.On<bool>("IsEnemyFound", (isEnemyFound) =>
            {
                IsEnemyFound = isEnemyFound;
                InvokeAsync(StateHasChanged);
            });

            await multiplayerGameHubConn.StartAsync();
            await multiplayerGameHubConn.SendAsync("OnUserConnected", loggedUserName, multiplayerGameHubConn.ConnectionId);
        }

        protected bool IsGameStarted() { return IsEnemyFound == true; }


        protected async Task FindEnemy()
        {
            if (BattleshipLogic.IsUserBoardCorrect())
                await multiplayerGameHubConn.SendAsync("FindEnemyForUser", loggedUserName);
            else
                UserMessage = "Ships distribution is not correct";
        }

        protected void UserBoardClicked(Point2D OnPoint)
        {
            Console.WriteLine($"X: {OnPoint.x} : Y: {OnPoint.y}");
        }
    }
}
