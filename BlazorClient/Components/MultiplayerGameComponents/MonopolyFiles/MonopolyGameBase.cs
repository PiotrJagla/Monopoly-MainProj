using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
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

        public string? UserMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UserMessage = null;
            MonopolyHubConn = new HubConnectionBuilder().WithUrl(NavManager.ToAbsoluteUri($"{Constants.ServerURL}{Constants.MonopolyHubURL}")).WithAutomaticReconnect().Build();
            await MonopolyHubConn.StartAsync();
        }

        protected async Task EnterRoom()
        {
            await MonopolyHubConn.SendAsync("OnUserConnected", loggerUserName);
        }
    }
}
