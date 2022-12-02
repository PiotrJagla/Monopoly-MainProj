using Microsoft.AspNetCore.Components;
using Models;
using Services.GamesServices.TicTacToe;

namespace BlazorClient.Components.SinglplayerGameComponentFiles.TicTacToeFiles
{
    public class TicTacToeGameBase : ComponentBase
    {
        [Inject]
        public TicTacToeService TicTacToeLogic { get; set; }

        public string UserMessage { get; set; }


        protected override void OnInitialized()
        {
            UserMessage = "";
        }

        protected void OnUserBoardClick(Point2D ClickPoint)
        {
            if (TicTacToeLogic.IsEmpty(ClickPoint))
            {
                TicTacToeLogic.PlayerTurn(ClickPoint);
                if(IsGameOver() == false)
                {
                    TicTacToeLogic.EnemyTurn();
                    IsGameOver();
                }
                InvokeAsync(StateHasChanged);
            }
        }

        private bool IsGameOver()
        {
            if (TicTacToeLogic.CheckWinner() == Constants.TicTacToePlayer)
                UserMessage = $"{Constants.TicTacToePlayer} Wins!";
            else if (TicTacToeLogic.CheckWinner() == Constants.TicTacToeEnemy)
                UserMessage = $"{Constants.TicTacToeEnemy} Wins!";
            else if(TicTacToeLogic.CheckWinner() == Constants.TicTacToeTie)
                UserMessage = "Tie";
            else
                return false;

            return true;
        }

        public void PlayAgain()
        {
            TicTacToeLogic.Restart();
            UserMessage = "";
            InvokeAsync(StateHasChanged);
        }
    }
}
