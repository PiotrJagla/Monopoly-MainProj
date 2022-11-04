using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using System.Security.Permissions;

namespace BlazorClient.Components.MultiplayerGameComponents
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

        public bool isEnemyFound { get; set; }

        protected override async Task OnInitializedAsync()
        {
            allPoints = 0;
            isEnemyFound = false;
        }

        protected async Task OnUserButtonClick()
        {
            ++allPoints;
            await multiplayerGameHubConn.SendAsync("SendToEnemyUserButtonPoints", loggedUserName, allPoints);
        }

        protected async Task Connect()
        {
            multiplayerGameHubConn = new HubConnectionBuilder().WithUrl(NavManager.ToAbsoluteUri($"{Constants.ServerURL}/multihub")).WithAutomaticReconnect().Build();
            
            multiplayerGameHubConn.On<bool>("IsEnemyFound", (isEnemyFound) =>
            {
                this.isEnemyFound = isEnemyFound;
                InvokeAsync(StateHasChanged);
            });
            multiplayerGameHubConn.On<int>("RecieveEnemyPoints", (enemyPoints) =>
            {
                this.enemyPoints = enemyPoints;
                InvokeAsync(StateHasChanged);
            });

            await multiplayerGameHubConn.StartAsync();
            await multiplayerGameHubConn.SendAsync("OnPlayerConnected",loggedUserName, multiplayerGameHubConn.ConnectionId);
            
        }

        protected bool IsConnected() { return multiplayerGameHubConn != null && multiplayerGameHubConn.ConnectionId != null; }

        protected async Task FindEnemy()
        {
            await multiplayerGameHubConn.SendAsync("FindEnemyForPlayer", loggedUserName);
        }
    }
}
