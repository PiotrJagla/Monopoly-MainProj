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
        //TODO: Przenieść logike Liczb turn do wyjścia z bezludnej wypspy do BoardService
        private MonopolyBoard BoardService;
        private MonopolyPlayers PlayersService;

        public MonopolyGameLogic()
        {
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
        
        public void BuyCellIfPossible()
        {
            MonopolyPlayer MainPlayer = PlayersService.GetMainPlayer();

            if (BoardService.IsPossibleToBuyCell(MainPlayer))
            {
                BuyCell(MainPlayer.OnCellIndex);
            }
        }

        private void BuyCell(int MainPlayerBoardPos)
        {
            PlayerKey MainPlayerKey = PlayersService.GetMainPlayer().Key;
            BoardService.GetCell(MainPlayerBoardPos).GetBuyingBehavior().SetOwner(MainPlayerKey);
            PlayersService.ChargeMainPlayer(BoardService.GetCell(MainPlayerBoardPos).GetBuyingBehavior().GetCosts().Buy);
            BoardService.CheckForMonopolOf(PlayersService.GetMainPlayer());
        }

        public MonopolyTurnResult ExecutePlayerMove(int MoveAmount)
        {
            Move(MoveAmount);
            CheckEvents();
            return MakeTurnResult();
        }

        

        private void Move(int amount)
        {
            if (IsAbleToMove() == false) return;

            MonopolyPlayer MainPlayer = PlayersService.GetMainPlayer();
            OnStartCellCrossed(amount);
            MainPlayer.OnCellIndex = (MainPlayer.OnCellIndex + amount) % BoardService.GetBoard().Count;
        }

        private bool IsAbleToMove()
        {
            //if (PlayersService.GetMainPlayer().TurnsOnIslandRemaining > 1)
            //{
            //    PlayersService.GetMainPlayer().TurnsOnIslandRemaining--;
            //    return false;
            //}
            //else
            //{
            //    PlayersService.GetMainPlayer().TurnsOnIslandRemaining = 0;
            //}

            //return true;
            return BoardService.IsAbleToMove();
        }

        private void OnStartCellCrossed(int MoveAmount)
        {
            if (DidCrossedStartCell(MoveAmount))
            {
                PlayersService.GiveMainPlayerMoney(Consts.Monopoly.OnStartCrossedMoneyGiven);
            }
        }

        private void CheckEvents()
        {
            CheckIfSteppedOnIsland();
        }

        private void CheckIfSteppedOnIsland()
        {
            //MonopolyPlayer MainPlayer = PlayersService.GetMainPlayer();
            //if (WillStayOnIsland())
            //{
            //    MainPlayer.TurnsOnIslandRemaining = 3;
            //}
            BoardService.CheckIfMainPlayerSteppedOnIsland(PlayersService.GetMainPlayer());
        }

        private bool WillStayOnIsland()
        {
            return BoardService.SteppedOnIsland(PlayersService.GetMainPlayer().OnCellIndex) &&
                   PlayersService.GetMainPlayer().TurnsOnIslandRemaining == 0;
        }

        private MonopolyTurnResult MakeTurnResult()
        {
            if (BoardService.IsPossibleToBuyCell(PlayersService.GetMainPlayer()))
                return MonopolyTurnResult.CanBuyCell;

            return MonopolyTurnResult.CannotBuyCell;
        }

        private bool DidCrossedStartCell(int MoveAmount)
        {
            return (PlayersService.GetMainPlayer().OnCellIndex + MoveAmount) >= BoardService.GetBoard().Count;
        }

        public bool IsYourTurn()
        {
            return PlayersService.IsMainPlayerTurn();
        }

        public void NextTurn()
        {
            PlayersService.NextTurn();
        }

        public void SetMainPlayerIndex(int index)
        {
            PlayersService.SetMainPlayerIndex(index);
        }

        public bool DontHaveMoneyToPay()
        {
            return BoardService.DontHaveMoneyToPay(PlayersService.GetMainPlayer());
        }

        public void SellCell(string CellToSellDisplay)
        {
            MonopolyCell CellToSellRef = BoardService.GetBoard().FirstOrDefault(c => c.OnDisplay() == CellToSellDisplay);

            if (CellToSellRef != null)
            {
                CellToSellRef.GetBuyingBehavior().SetOwner(PlayerKey.NoOne);
                PlayersService.GiveMainPlayerMoney(CellToSellRef.GetBuyingBehavior().GetCosts().Buy);
            }
        }

        public PlayerKey WhoWon()
        {
            return PlayersService.WhoWon();
        }

        public MonopolyModalParameters GetModalParameters()
        {
            int MainPlayerPos = PlayersService.GetMainPlayer().OnCellIndex;
            return BoardService.GetCellModalParameters(MainPlayerPos);
        }

        public void ModalResponse(string StringResponse)
        {
            if(StringResponse == Consts.Monopoly.ThrowDiceIslandButtonContent)
            {
                if(GetRandom.number.Next(1,6) == 1)
                {
                    BoardService.EscapeFromIsland();
                }
            }
            else if(StringResponse == Consts.Monopoly.PayToEscapeIslandCellButtonContent)
            {
                if (IsAbleToPayForEscapingFromIsland())
                {
                    PlayersService.ChargeMainPlayer(Consts.Monopoly.IslandEscapeCost);
                    BoardService.EscapeFromIsland();
                }

            }
        }

        private bool IsAbleToPayForEscapingFromIsland()
        {
            return PlayersService.IsAbleToPayForEscapingFromIsland();
        }
    }
}
