using Enums;
using Microsoft.AspNetCore.Components;


namespace BlazorClient.MenuPages.MultiplayerGamesMenuFiles
{
    public class MultiplayerGamesMenuBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        [Parameter]
        public string loggedUserName { get; set; }

        public List<MultiplayerGame> AllGames { get; private set; }

        protected override void OnInitialized()
        {
            AllGames = new List<MultiplayerGame>();

            AllGames.Add(MultiplayerGame.Battleship);
            AllGames.Add(MultiplayerGame.DemoButtonClickingGame);
            AllGames.Add(MultiplayerGame.Monopoly);
        }

        protected void NavigateToMultiplayerGame(MultiplayerGame game)
        {
            NavManager.NavigateTo($"/MultiplayerGame/{MultiplayerGameMethods.ToString(game)}/{loggedUserName}");
        }

    }
}
