using ClientSide.Components.MultiplayerGameComponents.BattleshipComponentFiles;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using System.Security.Permissions;

namespace ClientSide.Components.MultiplayerGameComponents.DemoGameFiles
{
    public class SomeMultiplayerGameBase : ComponentBase
    {

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Parameter]
        public string loggedUserName { get; set; }

        private HubConnection multiplayerGameHubConn;

        public int allPoints { get; set; }

        public int enemyPoints { get; set; }


        public bool IsEnemyFound { get; set; }

        protected override async Task OnInitializedAsync()
        {
            allPoints = 0;
            IsEnemyFound = false;

            multiplayerGameHubConn = new HubConnectionBuilder().WithUrl(NavManager.ToAbsoluteUri($"{Consts.ServerURL}/multihub")).WithAutomaticReconnect().Build();

            multiplayerGameHubConn.On<int>("RecieveEnemyPoints", (enemyPoints) =>
            {
                this.enemyPoints = enemyPoints;
                InvokeAsync(StateHasChanged);
            });
            multiplayerGameHubConn.On<bool>("IsEnemyFound", (isEnemyFound) =>
            {
                IsEnemyFound = isEnemyFound;
                InvokeAsync(StateHasChanged);
            });

            await multiplayerGameHubConn.StartAsync();
            await multiplayerGameHubConn.SendAsync("OnUserConnected", loggedUserName, multiplayerGameHubConn.ConnectionId);
        }

        protected async Task OnUserButtonClick()
        {
            multiplayerGameHubConn.On<bool>("IsEnemyFound", (isEnemyFound) =>
            {
                IsEnemyFound = isEnemyFound;
                InvokeAsync(StateHasChanged);
            });

            if (IsGameStarted() == true)
            {
                ++allPoints;
                await multiplayerGameHubConn.SendAsync("SendToEnemyUserButtonPoints", loggedUserName, allPoints);
            }
        }

        protected bool IsGameStarted() { return IsEnemyFound == true; }


        protected async Task FindEnemy()
        {
            await multiplayerGameHubConn.SendAsync("FindEnemyForUser", loggedUserName);
        }
    }
}
