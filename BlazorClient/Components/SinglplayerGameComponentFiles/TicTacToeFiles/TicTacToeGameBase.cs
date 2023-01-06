using Microsoft.AspNetCore.Components;
using Models;
using Services.GamesServices.TicTacToe;

namespace ClientSide.Components.SinglplayerGameComponentFiles.TicTacToeFiles
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
            if (TicTacToeLogic.CheckWinner() == Consts.TicTacToe.Player)
                UserMessage = $"{Consts.TicTacToe.Player} Wins!";
            else if (TicTacToeLogic.CheckWinner() == Consts.TicTacToe.Enemy)
                UserMessage = $"{Consts.TicTacToe.Enemy} Wins!";
            else if(TicTacToeLogic.CheckWinner() == Consts.TicTacToe.Tie)
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
