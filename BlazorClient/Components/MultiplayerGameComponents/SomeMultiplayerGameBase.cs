using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Models;

namespace BlazorClient.Components.MultiplayerGameComponents
{
    public class SomeMultiplayerGameBase : ComponentBase
    {
        public int allPoints { get; private set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        private HubConnection multiplayerGameHubConn;

        protected override async Task OnInitializedAsync()
        {
            allPoints = 0;

            Uri uri = NavManager.ToAbsoluteUri("/bbb");
            

            multiplayerGameHubConn = new HubConnectionBuilder().WithUrl(NavManager.ToAbsoluteUri($"{Constants.ServerURL}/multihub")).WithAutomaticReconnect().Build();
            multiplayerGameHubConn.On<int>("RecieveMessage", (playerPoints) =>
            {
                allPoints = playerPoints;
                InvokeAsync(StateHasChanged);
            });

            await multiplayerGameHubConn.StartAsync();
        }

        protected async Task SendButtonClicked()
        {
            ++allPoints;
            await multiplayerGameHubConn.SendAsync("InformEnemyAboutButtonClick", allPoints);
        }
    }
}
