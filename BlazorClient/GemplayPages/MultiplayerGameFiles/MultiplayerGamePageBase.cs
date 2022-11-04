using Enums;
using Microsoft.AspNetCore.Components;

namespace BlazorClient.GemplayPages.MultiplayerGameFiles
{
    public class MultiplayerGamePageBase : ComponentBase
    {
        [Parameter]
        public string multiplayerGameType { get; set; }

        [Parameter]
        public string loggedUserName { get; set; }

        public MultiplayerGame multiplayerGame { get; set; }

        protected override void OnInitialized()
        {
            multiplayerGame = MultiplayerGameMethods.ToEnum(multiplayerGameType);
        }
    }
}
