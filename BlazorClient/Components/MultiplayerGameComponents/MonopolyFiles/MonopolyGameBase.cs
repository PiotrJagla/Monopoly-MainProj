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

            MonopolyHubConn.On<MonopolyUpdateMessage>("UpdateData", (NewData) =>
            {
                MonopolyLogic.UpdateData(NewData);
                MonopolyLogic.NextTurn();
                InvokeAsync(StateHasChanged);
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
            await PlayersMove();
            await BrodcastUpdatedInformations();
        }
        private async Task PlayersMove()
        {
            if(MonopolyLogic.Move(GetRandom.number.Next(1, 3)) == MoveResult.OnNobodysCell)
            {
                await CellBuyingProcess();    
            }
            else
            {
                //TODO: Owes money to someone
            }
        }

        private async Task CellBuyingProcess()
        {
            if (await IsCellBought())
            {
                MonopolyLogic.BuyCellIfPossible();
            }
        }

        private async Task<bool> IsCellBought()
        {
            ModalParameters parameters = new ModalParameters();
            parameters.Add(nameof(YesOrNoModal.Title), "Do you want to buy this?");

            var ModalResponse = ModalService.Show<YesOrNoModal>("Passing Data", parameters);
            var Response = await ModalResponse.Result;

            if(Response.Confirmed)
            {
                return StringConvert.StringToBool(Response.Data.ToString());
            }

            return false;
        }

        private async Task BrodcastUpdatedInformations()
        {
            MonopolyUpdateMessage UpdatedData = MonopolyLogic.GetUpdatedData();
            await MonopolyHubConn.SendAsync("UpdateData", UpdatedData);
        }
    }
}
