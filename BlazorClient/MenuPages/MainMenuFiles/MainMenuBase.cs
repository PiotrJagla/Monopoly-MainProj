using Microsoft.AspNetCore.Components;

namespace BlazorClient.MenuPages.MainMenuFiles
{
    public class MainMenuBase : ComponentBase
    {

        [Inject]
        public NavigationManager NavManager{ get; set; }

        protected void NavigateToSingleplayerGamesMenu()
        {
            NavManager.NavigateTo("/SinglplayerGamesMenu");
        }

        protected void NavigateToMultiplayerGamesMenu()
        {
            NavManager.NavigateTo("/MultiplayerGamesMenu");
        }

        protected void NavigateToCommunityMenu()
        {
            NavManager.NavigateTo("/CommunityMenu");
        }
    }
}
