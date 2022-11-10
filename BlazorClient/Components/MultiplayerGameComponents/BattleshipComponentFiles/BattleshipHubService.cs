using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Models;

namespace BlazorClient.Components.MultiplayerGameComponents.BattleshipComponentFiles
{
    public class BattleshipHubService : ComponentBase
    {
        private HubConnection multiplayerGameHubConn;
        public bool IsEnemyFound { get; set; }
        public string UserName { get; set; }

        public async Task InitializeHub(string userName, Uri hubConnectionURI)
        {
            IsEnemyFound = false;
            this.UserName = userName;

            multiplayerGameHubConn = new HubConnectionBuilder().WithUrl(hubConnectionURI).WithAutomaticReconnect().Build();
            
            multiplayerGameHubConn.On<bool>("IsEnemyFound", (isEnemyFound) =>
            {
                IsEnemyFound = isEnemyFound;
                InvokeAsync(StateHasChanged);
            });

            await multiplayerGameHubConn.StartAsync();
            await multiplayerGameHubConn.SendAsync("OnUserConnected", userName, multiplayerGameHubConn.ConnectionId);
        }

        protected bool IsGameStarted() { return IsEnemyFound == true; }


        protected async Task FindEnemy()
        {
            await multiplayerGameHubConn.SendAsync("FindEnemyForUser", UserName);
        }
    }
}
