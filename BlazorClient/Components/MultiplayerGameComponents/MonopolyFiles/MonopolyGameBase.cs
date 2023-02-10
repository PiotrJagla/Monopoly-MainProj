using BlazorClient.Components.UIComponents;
using Blazored.Modal;
using Blazored.Modal.Services;
using Enums.Monopoly;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using Models.Monopoly;
using Models.MultiplayerConnection;
using Org.BouncyCastle.Asn1.X509;
using Services.GamesServices.Monopoly;
using Services.GamesServices.Monopoly.Update;
using StringManipulationLib;
using System.Net.NetworkInformation;

namespace BlazorClient.Components.MultiplayerGameComponents.MonopolyFiles
{
    public class MonopolyGameBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public MonopolyService MonopolyLogic { get; set; }

        [Inject]
        public IModalService ModalService { get; set; }

        [Parameter]
        public string loggerUserName { get; set; }

        private HubConnection MonopolyHubConn;

        public List<string> Messages { get; set; }

        public int RoomPlayersNumber { get; set; }

        protected override async Task OnInitializedAsync()
        {
            RoomPlayersNumber = 0;
            Messages = new List<string>();
            MonopolyHubConn = new HubConnectionBuilder().WithUrl(NavManager.ToAbsoluteUri($"{Consts.ServerURL}{Consts.HubUrl.Monopoly}")).WithAutomaticReconnect().Build();
            await MonopolyHubConn.StartAsync();

            MonopolyHubConn.On<int>("UserJoined", (AllPlayersInRoom) =>
            {
                RoomPlayersNumber = AllPlayersInRoom;
                MonopolyLogic.SetMainPlayerIndex(RoomPlayersNumber - 1);
                Messages.Add($"Players in Room: {RoomPlayersNumber}");
                InvokeAsync(StateHasChanged);
            });

            MonopolyHubConn.On<List<Player>>("ReadyPlayers", (ReadyPlayers) =>
            {
                Messages.Add($"Ready Players: {ReadyPlayers.Count}/{RoomPlayersNumber}");
                IsEveryoneReady(ReadyPlayers);
                InvokeAsync(StateHasChanged);
            });

            MonopolyHubConn.On<MonopolyUpdateMessage>("UpdateData", async (NewData) =>
            {
                Update(NewData);
                MonopolyLogic.NextTurn();
                await ExecuteBeforeMoveActions();
                await InvokeAsync(StateHasChanged);
            });
        }

        private void IsEveryoneReady(List<Player> ReadyPlayers)
        {
            if (ReadyPlayers.Count == RoomPlayersNumber)
            {
                MonopolyLogic.StartGame(ReadyPlayers);
                Messages.Add("Everyone is Ready");
            }
        }

        private void Update(MonopolyUpdateMessage NewData)
        {
            MonopolyLogic.UpdateData(NewData);
            PlayerBankruptMessage(NewData.BankruptPlayer);
            if (MonopolyLogic.WhoWon() != PlayerKey.NoOne)
            {
                Messages.Add($"Player: {MonopolyLogic.WhoWon().ToString()} Has Won!!");
            }
        }

        private void PlayerBankruptMessage(PlayerKey BankruptPlayer)
        {
            if (BankruptPlayer != PlayerKey.NoOne)
            {
                Messages.Add($"Player: {BankruptPlayer.ToString()} went bankrupt");
            }
        }
        protected async Task ExecuteBeforeMoveActions()
        {
            await ExecuteModal(ModalShow.BeforeMove);
            await InvokeAsync(StateHasChanged);
        }

        protected async Task ExecuteModal(ModalShow When)
        {

            if (MonopolyLogic.IsYourTurn() == true)
            {
                MonopolyModalParameters ModalParameters = MonopolyLogic.GetModalParameters();
                if (ModalParameters != null && ModalParameters.WhenShowModal == When)
                {
                    ModalParameters parameters = new ModalParameters();
                    parameters.Add(nameof(SelectButtonModal.StringParameters), ModalParameters.Parameters);
                    var ModalResponse = ModalService.Show<SelectButtonModal>("Passing Data", parameters);
                    var Response = await ModalResponse.Result;
                    if (Response.Confirmed)
                    {
                        MonopolyLogic.ModalResponse(Response.Data.ToString(), ModalParameters.Identifier);
                    }
                }
            }
        }

        protected async Task EnterRoom()
        {
            await MonopolyHubConn.SendAsync("OnUserConnected", loggerUserName);
            await MonopolyHubConn.SendAsync("JoinToRoom");
            Messages.Add("Joined To Room");
            InvokeAsync(StateHasChanged);
        }

        protected async Task Ready()
        {
            await MonopolyHubConn.SendAsync("UserReady");
        }

        protected async Task Move()
        {
            try
            {
                await PlayersMove();
                await CheckForBanckrupcy();
                await BrodcastUpdatedInformations();
            }
            catch
            {
                Messages.Add("Game has not started yet");
            }
        }
        private async Task PlayersMove()
        {
            MonopolyLogic.ExecutePlayerMove(1);
            await ExecuteModal(ModalShow.AfterMove);
        }

        private async Task CheckForBanckrupcy()
        {
            while (MonopolyLogic.DontHaveMoneyToPay() == true &&
                MonopolyLogic.GetMainPlayerCells().Count != 0)
            {
                MonopolyLogic.SellCell(await ChooseCellToSell());
            }
        }

        private async Task<string> ChooseCellToSell()
        {
            ModalParameters parameters = new ModalParameters();
            parameters.Add(
                nameof(PayOffDebtModal.DebtAmount),
                MonopolyLogic.GetDebtAmount()
            );
            parameters.Add(
                nameof(PayOffDebtModal.PossibleCellsToSell),
                MonopolyLogic.GetMainPlayerCells()
            );

            var ModalResponse = ModalService.Show<PayOffDebtModal>("Passing Data", parameters);
            var Response = await ModalResponse.Result;

            if (Response.Confirmed)
            {
                return Response.Data.ToString();
            }

            return "No More cells to sell";
        }

        private async Task BrodcastUpdatedInformations()
        {
            MonopolyUpdateMessage UpdatedData = MonopolyLogic.GetUpdatedData();
            await MonopolyHubConn.SendAsync("UpdateData", UpdatedData);
        }

        protected bool CanMove()
        {
            return MonopolyLogic.IsYourTurn() == true &&
                MonopolyLogic.WhoWon() == PlayerKey.NoOne;
        }
    }
}
