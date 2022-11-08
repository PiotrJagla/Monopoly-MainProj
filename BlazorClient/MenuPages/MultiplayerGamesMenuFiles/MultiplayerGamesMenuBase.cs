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

        public List<MultiplayerGame> AllAddedMultiplayerGames { get; private set; }

        protected override void OnInitialized()
        {
            AllAddedMultiplayerGames = new List<MultiplayerGame>();

            AllAddedMultiplayerGames.Add(MultiplayerGame.Statki);
            AllAddedMultiplayerGames.Add(MultiplayerGame.DemoButtonClickingGame);
        }

        protected void NavigateToMultiplayerGame(MultiplayerGame multiplayerGame)
        {
            NavManager.NavigateTo($"/MultiplayerGame/{MultiplayerGameMethods.ToString(multiplayerGame)}/{loggedUserName}");
        }

    }
}
