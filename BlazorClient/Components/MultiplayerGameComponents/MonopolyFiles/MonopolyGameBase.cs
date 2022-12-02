using Microsoft.AspNetCore.Components;

namespace BlazorClient.Components.MultiplayerGameComponents.MonopolyFiles
{
    public class MonopolyGameBase : ComponentBase
    {
        [Parameter]
        public string loggerUserName { get; set; }
    }
}
