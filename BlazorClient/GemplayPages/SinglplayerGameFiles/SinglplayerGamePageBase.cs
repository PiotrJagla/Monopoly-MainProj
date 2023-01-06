using Enums;
using Microsoft.AspNetCore.Components;

namespace ClientSide.GemplayPages.SinglplayerGameFiles
{
    public class SinglplayerGamePageBase : ComponentBase
    {
        [Parameter]
        public string GameName { get; set; }

        [Parameter]
        public string LoggedUserName { get; set; }

        public SinglplayerGame Game { get; set; }

        protected override void OnInitialized()
        {
            Game = SinglplayerGameMethods.ToEnum(GameName);
        }
    }
}
