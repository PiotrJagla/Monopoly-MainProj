using Microsoft.AspNetCore.Components;
using Services.GamesServices.BlackJack;

namespace BlazorClient.Components.SinglplayerGameComponentFiles.BlackJackFiles
{
    public class BlackJackGameBase : ComponentBase
    {
        [Inject]
        public BlackJackService BlackJackLogic { get; set; }

        public string UserMessage { get; set; }

        protected override void OnInitialized()
        {
            UserMessage = "";
        }

        protected void DrawCard()
        {
            BlackJackLogic.DrawCard();
            InvokeAsync(StateHasChanged);
        }

        protected void StartGame()
        {
            UserMessage = "";
            BlackJackLogic.StartGame();
            InvokeAsync(StateHasChanged);
        }

        protected void DealerTurn()
        {
            BlackJackLogic.DealerTurn();
            WhoWon();
            InvokeAsync(StateHasChanged);
        }

        private void WhoWon()
        {
            if (BlackJackLogic.UserWon())
                UserMessage = "You Won";
            else
                UserMessage = "Dealer Won";
        }


    }
}
