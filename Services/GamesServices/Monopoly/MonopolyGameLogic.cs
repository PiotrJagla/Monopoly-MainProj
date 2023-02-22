using Models.MultiplayerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums.Monopoly;
using System.Threading.Tasks;
using Models.Monopoly;
using Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Services.GamesServices.Monopoly.Update;
using MySqlX.XDevAPI.Common;
using Services.GamesServices.Monopoly.Board.Cells;
using Services.GamesServices.Monopoly.Board;


namespace Services.GamesServices.Monopoly
{
    public class MonopolyGameLogic : MonopolyService
    {
        private MonopolyBoard BoardService;
        private MonopolyPlayers PlayersService;
        private int MoveQuantity;

        public MonopolyGameLogic()
        {
            MoveQuantity = 0;
            BoardService = new MonopolyBoard();
            PlayersService = new MonopolyPlayers(); 
        }

        public void StartGame(List<Player> PlayersInGame)
        {
            if (PlayersService.DidGameStart())
                return;

            PlayersService.InitPlayers(PlayersInGame);
        }

        public List<MonopolyCell> GetBoard()
        {
            return BoardService.GetBoard();   
        }

        public List<MonopolyCell> GetMainPlayerCells() 
        {
            return BoardService.GetMainPlayerCells(PlayersService.GetMainPlayer().Key);
        }

        public int GetDebtAmount() 
        {
            return BoardService.GetDebtAmount(PlayersService.GetMainPlayer().OnCellIndex);
        }

        public MonopolyUpdateMessage GetUpdatedData()
        {
            MonopolyUpdateMessage UpdatedData = new MonopolyUpdateMessage();
            if (PlayersService.DidGameStart() == true)
            {
                UpdatedData.PlayersData = PlayersService.MakePlayersUpdateData().GetPlayersUpdatedData();
                UpdatedData.CellsOwners = BoardService.MakeBoardUpdateData().GetCellsUpdateData();
                UpdatedData.MoneyBond = BoardService.MakeMoneyBond(PlayersService.GetMainPlayer());
                UpdatedData.BankruptPlayer = PlayersService.CheckForBankruptPlayer(ref UpdatedData);
            }
            return UpdatedData;
        }

        public void UpdateData(MonopolyUpdateMessage UpdatedData)
        {
            PlayersService.UpdateData(UpdatedData);
            BoardService.UpdateData(UpdatedData.CellsOwners);
        }

        public void SellCell(string CellToSellDisplay)
        {
            int BuyCost = BoardService.SellCell(CellToSellDisplay);
            PlayersService.GiveMainPlayerMoney(BuyCost);
        }

        public bool IsYourTurn()
        {
            return PlayersService.IsMainPlayerTurn();
        }

        public void NextTurn()
        {
            PlayersService.NextTurn();
        }

        public void ExecutePlayerMove(int MoveAmount)
        {
            if (MoveQuantity > 0)
                MoveAmount = MoveQuantity;

            Move(MoveAmount);
            CheckEvents();
            MoveQuantity = 0;
        }

        private void Move(int amount)
        {
            if (BoardService.IsAbleToMove() == false) return;

            MonopolyPlayer MainPlayer = PlayersService.GetMainPlayer();
            OnStartCellCrossed(amount);
            MainPlayer.OnCellIndex = (MainPlayer.OnCellIndex + amount) % BoardService.GetBoard().Count;
        }

        private void OnStartCellCrossed(int MoveAmount)
        {
            if (BoardService.DidCrossedStartCell(MoveAmount, PlayersService.GetMainPlayer().OnCellIndex))
            {
                PlayersService.GiveMainPlayerMoney(Consts.Monopoly.OnStartCrossedMoneyGiven);
            }
        }

        private void CheckEvents()
        {
            BoardService.CheckIfSteppedOnIsland(PlayersService.GetMainPlayer());
        }

        public void SetMainPlayerIndex(int index)
        {
            PlayersService.SetMainPlayerIndex(index);
        }

        public bool DontHaveMoneyToPay()
        {
            return BoardService.DontHaveMoneyToPay(PlayersService.GetMainPlayer());
        }

        public PlayerKey WhoWon()
        {
            return PlayersService.WhoWon();
        }

        public MonopolyModalParameters GetModalParameters()
        {
            return BoardService.GetCellModalParameters(PlayersService.GetMainPlayer());
        }

        public void ModalResponse(string StringResponse)
        {
            ModalResponseData Data = new ModalResponseData();
            Data.BoardService = BoardService;
            Data.PlayersService = PlayersService;
            Data.ModalResponse = StringResponse;

            ModalResponseUpdate UpdatedData = BoardService.OnCellModalResponse(Data);

            BoardService = UpdatedData.BoardService;
            PlayersService = UpdatedData.PlayersService;
            MoveQuantity = UpdatedData.MoveQuantity;
        }
    }
}
