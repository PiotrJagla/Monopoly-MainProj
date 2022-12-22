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
using Services.GamesServices.Monopoly.Board;

namespace Services.GamesServices.Monopoly
{
    public class MonopolyGameLogic : MonopolyService
    {
        private List<MonopolyPlayer> Players;
        private SpecialIndexes PlayersSpecialIndexes;
        private MonopolyBoard BoardService;

        public MonopolyGameLogic()
        {
            Players = new List<MonopolyPlayer>();
            PlayersSpecialIndexes = new SpecialIndexes();
            BoardService = new MonopolyBoard();
        }

        public void StartGame(List<Player> PlayersInGame)
        {
            if (GameIsAlreadyStarted())
                return;

            for (int i = 0; i < PlayersInGame.Count; i++)
            {
                AddPlayer((PlayerKey)i);
            }
            PlayersSpecialIndexes.WhosTurn = 0;
        }

        private bool GameIsAlreadyStarted()
        {
            return Players.Count != 0;
        }

        private void AddPlayer(PlayerKey playerKey)
        {
            Players.Add(new MonopolyPlayer());
            Players.Last().Key = playerKey;
            Players.Last().OnCellIndex = 0;
            Players.Last().MoneyOwned = Consts.Monopoly.StartMoneyAmount;
        }

        public List<MonopolyCell> GetBoard()
        {
            return BoardService.GetBoard();   
        }

        public List<MonopolyCell> GetMainPlayerCells()
        {
            List<MonopolyCell> cells = BoardService.GetBoard().FindAll(cell => cell.GetOwner() == Players[PlayersSpecialIndexes.MainPlayer].Key);
            
            return cells;
        }

        public int GetDebtAmount()
        {
            int MainPlayerPos = Players[PlayersSpecialIndexes.MainPlayer].OnCellIndex;
            return BoardService.GetCell(MainPlayerPos).GetCosts().Stay;
        }

        public MonopolyUpdateMessage GetUpdatedData()
        {
            MonopolyUpdateMessage UpdatedData = new MonopolyUpdateMessage();
            if (GameIsAlreadyStarted() == true)
            {
                UpdatedData.PlayersData = MakePlayersUpdateData().GetPlayersUpdatedData();
                UpdatedData.CellsOwners = MakeBoardUpdateData().GetCellsUpdate();
                UpdatedData.MoneyBond = MakeMoneyBond();
                UpdatedData.BankruptPlayer = CheckForBankruptPlayer(ref UpdatedData);
            }
            return UpdatedData;
        }

        private PlayersUpdateData MakePlayersUpdateData()
        {
            PlayersUpdateData PlayersUpdatedData = new PlayersUpdateData();
            PlayersUpdatedData.FormatPlayersUpdateData(Players);
            return PlayersUpdatedData;
        }

        private MonopolyBoardUpdateData MakeBoardUpdateData()
        {
            MonopolyBoardUpdateData BoardUpdatedData = new MonopolyBoardUpdateData();
            BoardUpdatedData.FormatBoardUpdateData(BoardService.GetBoard());
            return BoardUpdatedData;
        }

        private MoneyObligation MakeMoneyBond()
        {
            try
            {
                return CalculateBond();
            }
            catch
            {
                return new MoneyObligation();
            }
        }

        private MoneyObligation CalculateBond()
        {
            MoneyObligation result = new MoneyObligation();
            if (BoardService.DoesCellHaveAnotherOwner(Players[PlayersSpecialIndexes.MainPlayer]))
            {
                MonopolyCell CellMainPlayerSteppedOn = BoardService.GetCell(Players[PlayersSpecialIndexes.MainPlayer].OnCellIndex);
                result.PlayerGettingMoney = CellMainPlayerSteppedOn.GetOwner();
                result.PlayerLosingMoney = Players[PlayersSpecialIndexes.MainPlayer].Key;
                result.ObligationAmount = CellMainPlayerSteppedOn.GetCosts().Stay;
            }
            return result;
        }

        private PlayerKey CheckForBankruptPlayer(ref MonopolyUpdateMessage UpdateData)
        {
            //There is copy of MoneyObligation Because lambda doesnt accept references
            MoneyObligation BondCopy = new MoneyObligation();
            BondCopy.PlayerLosingMoney = UpdateData.MoneyBond.PlayerLosingMoney;

            MonopolyPlayer PlayerObligatedToPay = Players.FirstOrDefault(p => p.Key == BondCopy.PlayerLosingMoney);
            int PlayerObligatedToPayMoney = 0;
            if (PlayerObligatedToPay != null)
            {
                PlayerObligatedToPayMoney = PlayerObligatedToPay.MoneyOwned;
            }
            

            if (PlayerObligatedToPayMoney < UpdateData.MoneyBond.ObligationAmount)
            {
                UpdateData.MoneyBond.ObligationAmount = PlayerObligatedToPayMoney;
                return UpdateData.MoneyBond.PlayerLosingMoney;
            }
            return PlayerKey.NoOne;
        }

        public void UpdateData(MonopolyUpdateMessage UpdatedData)
        {
            UpdatePlayersData(UpdatedData.PlayersData);
            UpdateBoardData(UpdatedData.CellsOwners);
            UpdateMoneyObligation(UpdatedData.MoneyBond);
            UpdateBankruptPlayer(UpdatedData.BankruptPlayer);
        }

        private void UpdatePlayersData(List<PlayerUpdateData> PlayersUpdatedData)
        {
            for (int i = 0; i < PlayersUpdatedData.Count; i++)
            {
                Players[i].OnCellIndex = PlayersUpdatedData[i].Position;
                Players[i].MoneyOwned = PlayersUpdatedData[i].Money;
            }
        }

        private void UpdateBoardData(List<MonopolyCellUpdate> BoardUpdatedData)
        {
            for (int i = 0; i < BoardUpdatedData.Count; i++)
            {
                BoardService.GetCell(i).SetOwner(BoardUpdatedData[i].Owner);
                BoardService.GetCell(i).SetCosts(BoardUpdatedData[i].NewCosts);
            }
        }

        private void UpdateMoneyObligation(MoneyObligation obligation)
        {
            MonopolyPlayer PlayerGettingMoney = Players.FirstOrDefault(p => p.Key == obligation.PlayerGettingMoney);
            MonopolyPlayer PlayerLosingMoney = Players.FirstOrDefault(p => p.Key == obligation.PlayerLosingMoney);
            if (PlayerGettingMoney != null && PlayerLosingMoney != null)
            {
                PlayerGettingMoney.MoneyOwned += obligation.ObligationAmount;
                PlayerLosingMoney.MoneyOwned -= obligation.ObligationAmount;
            }
        }

        private void UpdateBankruptPlayer(PlayerKey BankruptPlayer)
        {
            CheckIfMainPlayerWentBankrupt(BankruptPlayer);
            Players.Remove( Players.FirstOrDefault(p => p.Key == BankruptPlayer) );
        }

        private void CheckIfMainPlayerWentBankrupt(PlayerKey BankruptPlayer)
        {
            if (BankruptPlayer == Players[PlayersSpecialIndexes.MainPlayer].Key)
                PlayersSpecialIndexes.MainPlayer = -1;
        }

        public void BuyCellIfPossible()
        {
            MonopolyPlayer MainPlayer = Players[PlayersSpecialIndexes.MainPlayer];

            if (BoardService.IsPossibleToBuyCell(MainPlayer))
            {
                BuyCell(MainPlayer.OnCellIndex);
            }
        }

        private void BuyCell(int MainPlayerBoardPos)
        {
            PlayerKey MainPlayerKey = Players[ PlayersSpecialIndexes.MainPlayer ].Key;
            BoardService.GetCell(MainPlayerBoardPos).SetOwner(MainPlayerKey);
            Players[PlayersSpecialIndexes.MainPlayer].MoneyOwned -= BoardService.GetCell(MainPlayerBoardPos).GetCosts().Buy;
            BoardService.CheckForMonopolOf(Players[PlayersSpecialIndexes.MainPlayer]);
        }

        public MonopolyTurnResult ExecuteTurn(int MoveAmount)
        {
            Move(MoveAmount);
            return MakeTurnResult();
        }

        private void Move(int amount)
        {
            OnStartCellCrossed(amount);
            MonopolyPlayer MainPlayer = Players[PlayersSpecialIndexes.MainPlayer];
            MainPlayer.OnCellIndex = (MainPlayer.OnCellIndex + amount) % BoardService.GetBoard().Count;
        }

        private MonopolyTurnResult MakeTurnResult()
        {
            return BoardService.IsPossibleToBuyCell(Players[PlayersSpecialIndexes.MainPlayer]) ?
                MonopolyTurnResult.CanBuyCell :
                MonopolyTurnResult.CannotBuyCell;
        }

        private void OnStartCellCrossed(int MoveAmount)
        {
            if (DidCrossedStartCell(MoveAmount))
            {
                Players[PlayersSpecialIndexes.MainPlayer].MoneyOwned += Consts.Monopoly.OnStartCrossedMoneyGiven;
            }
        }

        private MonopolyTurnResult PlayerTurnResult()
        {
            MonopolyTurnResult result = MonopolyTurnResult.NoResult;



            return result;
        }

        private bool DidCrossedStartCell(int MoveAmount)
        {
            return (Players[PlayersSpecialIndexes.MainPlayer].OnCellIndex + MoveAmount) >= BoardService.GetBoard().Count;
        }

        public bool IsYourTurn()
        {
            return PlayersSpecialIndexes.WhosTurn == PlayersSpecialIndexes.MainPlayer;
        }

        public void NextTurn()
        {
            PlayersSpecialIndexes.WhosTurn = (++PlayersSpecialIndexes.WhosTurn) % Players.Count;
        }

        private bool IsBankrupt()
        {
            return PlayersSpecialIndexes.MainPlayer == -1;
        }

        public void SetMainPlayerIndex(int index)
        {
            if (PlayersSpecialIndexes.MainPlayer == -1)
                PlayersSpecialIndexes.MainPlayer = index;
        }

        public bool DontHaveMoneyToPay()
        {
            return BoardService.DontHaveMoneyToPay(Players[PlayersSpecialIndexes.MainPlayer]);
        }

        public void SellCell(string CellToSellDisplay)
        {
            MonopolyCell CellToSellRef = BoardService.GetBoard().FirstOrDefault(c => c.OnDisplay() == CellToSellDisplay);
            MonopolyPlayer MainPlayer = Players[PlayersSpecialIndexes.MainPlayer];

            if (CellToSellRef != null)
            {
                CellToSellRef.SetOwner(PlayerKey.NoOne);
                MainPlayer.MoneyOwned += CellToSellRef.GetCosts().Buy;
            }
        }

        public PlayerKey WhoWon()
        {
            if (Players.Count == 1)
                return Players[0].Key;

            return PlayerKey.NoOne;
        }
    }
}
