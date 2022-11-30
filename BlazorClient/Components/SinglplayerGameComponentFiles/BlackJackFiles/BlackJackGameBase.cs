using Microsoft.AspNetCore.Components;

namespace BlazorClient.Components.SinglplayerGameComponentFiles.BlackJackFiles
{
    public class BlackJackGameBase : ComponentBase
    {

        [Parameter]
        public string LoggedUserName { get; set; }

    }
}
