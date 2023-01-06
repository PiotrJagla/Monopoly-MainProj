using Enums;
using Microsoft.AspNetCore.Components;

namespace BlazorClient.MenuPages.SingleplayerGamesMenuFiles
{
    public class SingleplayerGamesMenuBase : ComponentBase
    {

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Parameter]
        public string loggedUserName { get; set; }

        public List<SinglplayerGame> AllGames { get; private set; }

        protected override void OnInitialized()
        {
            AllGames = new List<SinglplayerGame>();

            AllGames.Add(SinglplayerGame.BalckJack);
            AllGames.Add(SinglplayerGame.TicTacToe);
        }

        protected void NavigateToSinglplayerGame(SinglplayerGame game)
        {
            NavManager.NavigateTo($"/SinglplayerGame/{SinglplayerGameMethods.ToString(game)}/{loggedUserName}");
        }
    }
}
