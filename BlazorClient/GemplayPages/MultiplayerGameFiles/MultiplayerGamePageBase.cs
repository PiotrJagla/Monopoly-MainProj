using Enums;
using Microsoft.AspNetCore.Components;

namespace ClientSide.GemplayPages.MultiplayerGameFiles
{
    public class MultiplayerGamePageBase : ComponentBase
    {
        [Parameter]
        public string multiplayerGameName { get; set; }

        [Parameter]
        public string loggedUserName { get; set; }

        public MultiplayerGame multiplayerGame { get; set; }

        protected override void OnInitialized()
        {
            multiplayerGame = MultiplayerGameMethods.ToEnum(multiplayerGameName);
        }
    }
}
