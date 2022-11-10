using Microsoft.AspNetCore.Components;
using Models;

namespace BlazorClient.Components.MultiplayerGameComponents.BattleshipComponentFiles
{
    public class BattleshipGameBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        [Parameter]
        public string loggedUserName { get; set; }

        private BattleshipHubService BattleshipMultiplayer;

        protected override async Task OnInitializedAsync()
        {
            BattleshipMultiplayer = new BattleshipHubService();
            BattleshipMultiplayer.InitializeHub(loggedUserName, NavManager.ToAbsoluteUri($"{Constants.ServerURL}/battleshiphub"));
        }
    }
}
