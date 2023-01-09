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
        private List<MonopolyPlayer> Players;
        private SpecialIndexes PlayersSpecialIndexes;
        private MonopolyBoard BoardService;
        private MonopolyPlayers PlayersService;

        public MonopolyGameLogic()
        {
            Players = new List<MonopolyPlayer>();
            PlayersSpecialIndexes = new SpecialIndexes();
            BoardService = new MonopolyBoard();
            PlayersService = null;
        }

        public void StartGame(List<Player> PlayersInGame)
        {
            if (GameIsAlreadyStarted())
                return;

            PlayersService = new MonopolyPlayers(PlayersInGame);

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
            return BoardService.GetMainPlayerCells(Players[PlayersSpecialIndexes.MainPlayer].Key);
        }

        public int GetDebtAmount() 
        {
            return BoardService.GetDebtAmount(Players[PlayersSpecialIndexes.MainPlayer].OnCellIndex);
        }

        public MonopolyUpdateMessage GetUpdatedData()
        {
            MonopolyUpdateMessage UpdatedData = new MonopolyUpdateMessage();
            if (GameIsAlreadyStarted() == true)
            {
                UpdatedData.PlayersData = MakePlayersUpdateData().GetPlayersUpdatedData();
                //UpdatedData.CellsOwners = MakeBoardUpdateData().GetCellsUpdateData();
                UpdatedData.CellsOwners = BoardService.MakeBoardUpdateData().GetCellsUpdateData();
                UpdatedData.MoneyBond = MakeMoneyBond();
                UpdatedData.BankruptPlayer = CheckForBankruptPlayer(ref UpdatedData);
            }
            return UpdatedData;
        }

        private MonopolyPlayersUpdateData MakePlayersUpdateData() 
        {
            MonopolyPlayersUpdateData PlayersUpdatedData = UpdateDataFactory.CreatePlayersUpdateData();
            PlayersUpdatedData.FormatPlayersUpdateData(Players);
            return PlayersUpdatedData;
        }

        private MonopolyBoardUpdateData MakeBoardUpdateData()
        {
            MonopolyBoardUpdateData BoardUpdatedData = UpdateDataFactory.CreateBoardUpdateData();
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

        private MoneyObligation CalculateBond() //TA METODA MOGLABY BYC DO MonopolyPlayers class
        {
            MoneyObligation result = new MoneyObligation();
            if (BoardService.DoesCellHaveAnotherOwner(Players[PlayersSpecialIndexes.MainPlayer]))
            {
                MonopolyCell CellMainPlayerSteppedOn = BoardService.GetCell(Players[PlayersSpecialIndexes.MainPlayer].OnCellIndex);
                result.PlayerGettingMoney = CellMainPlayerSteppedOn.GetBuyingBehavior().GetOwner();
                result.PlayerLosingMoney = Players[PlayersSpecialIndexes.MainPlayer].Key;
                result.ObligationAmount = CellMainPlayerSteppedOn.GetBuyingBehavior().GetCosts().Stay;
            }
            return result;
        }

        private PlayerKey CheckForBankruptPlayer(ref MonopolyUpdateMessage UpdateData)
        {
            //There is copy of MoneyObligation Because lambda doesnt accept references
            MoneyObligation BondCopy = new MoneyObligation();
            BondCopy.PlayerLosingMoney = UpdateData.MoneyBond.PlayerLosingMoney;
            int PlayerObligatedToPayMoneyOwned = GetPlayerObligatedToPayMoneyOwned(BondCopy);

            int ObligationAmount = UpdateData.MoneyBond.ObligationAmount;

            ChangeMoneyBondIfBankrupt(ref UpdateData, PlayerObligatedToPayMoneyOwned);
            return GetBankruptPlayer(UpdateData, PlayerObligatedToPayMoneyOwned, ObligationAmount);
        }
        private int GetPlayerObligatedToPayMoneyOwned(MoneyObligation BondCopy)
        {
            int PlayerObligatedToPayMoneyOwned = 0;
            MonopolyPlayer PlayerObligatedToPay = Players.FirstOrDefault(p => p != null && (p.Key == BondCopy.PlayerLosingMoney));
            if (PlayerObligatedToPay != null)
            {
                PlayerObligatedToPayMoneyOwned = PlayerObligatedToPay.MoneyOwned;
            }

            return PlayerObligatedToPayMoneyOwned;
        }

        private void ChangeMoneyBondIfBankrupt(ref MonopolyUpdateMessage UpdateData, int PlayerObligatedToPayMoneyOwned)
        {
            if (PlayerObligatedToPayMoneyOwned < UpdateData.MoneyBond.ObligationAmount)
            {
                UpdateData.MoneyBond.ObligationAmount = PlayerObligatedToPayMoneyOwned;
            }
        }
        private PlayerKey GetBankruptPlayer(MonopolyUpdateMessage UpdateData, int PlayerObligatedToPayMoneyOwned, int ObligationAmount)
        {
            if (PlayerObligatedToPayMoneyOwned < ObligationAmount)
            {
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
                Players[PlayersUpdatedData[i].PlayerIndex].OnCellIndex = PlayersUpdatedData[i].Position;
                Players[PlayersUpdatedData[i].PlayerIndex].MoneyOwned = PlayersUpdatedData[i].Money;
            }
        }

        private void UpdateBoardData(List<MonopolyCellUpdate> BoardUpdatedData)
        {
            for (int i = 0; i < BoardUpdatedData.Count; i++)
            {
                BoardService.GetCell(i).GetBuyingBehavior().SetOwner(BoardUpdatedData[i].Owner);
                BoardService.GetCell(i).GetBuyingBehavior().SetCosts(BoardUpdatedData[i].NewCosts);
            }
        }

        private void UpdateMoneyObligation(MoneyObligation obligation)
        {
            MonopolyPlayer PlayerGettingMoney = Players.FirstOrDefault(p => p!=null &&( p.Key == obligation.PlayerGettingMoney));
            MonopolyPlayer PlayerLosingMoney = Players.FirstOrDefault(p => p!=null &&( p.Key == obligation.PlayerLosingMoney));
            if (PlayerGettingMoney != null && PlayerLosingMoney != null)
            {
                PlayerGettingMoney.MoneyOwned += obligation.ObligationAmount;
                PlayerLosingMoney.MoneyOwned -= obligation.ObligationAmount;
            }
        }

        private void UpdateBankruptPlayer(PlayerKey BankruptPlayerKey)
        {
            CheckIfMainPlayerWentBankrupt(BankruptPlayerKey);

            MonopolyPlayer BankruptPlayer = Players.FirstOrDefault(p => p != null && ( p.Key == BankruptPlayerKey));
            int BankruptPlayerIndex = Players.IndexOf(BankruptPlayer);
            if(BankruptPlayerIndex != -1)
                Players[BankruptPlayerIndex] = null;
        }

        private void CheckIfMainPlayerWentBankrupt(PlayerKey BankruptPlayer)
        {
            if (PlayersSpecialIndexes.MainPlayer == -1) return;

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
            BoardService.GetCell(MainPlayerBoardPos).GetBuyingBehavior().SetOwner(MainPlayerKey);
            Players[PlayersSpecialIndexes.MainPlayer].MoneyOwned -= BoardService.GetCell(MainPlayerBoardPos).GetBuyingBehavior().GetCosts().Buy;
            BoardService.CheckForMonopolOf(Players[PlayersSpecialIndexes.MainPlayer]);
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

            MonopolyPlayer MainPlayer = Players[PlayersSpecialIndexes.MainPlayer];
            OnStartCellCrossed(amount);
            MainPlayer.OnCellIndex = (MainPlayer.OnCellIndex + amount) % BoardService.GetBoard().Count;
        }

        private bool IsAbleToMove()
        {
            if (Players[PlayersSpecialIndexes.MainPlayer].TurnsOnIslandRemaining > 1)
            {
                Players[PlayersSpecialIndexes.MainPlayer].TurnsOnIslandRemaining--;
                return false;
            }
            else
            {
                Players[PlayersSpecialIndexes.MainPlayer].TurnsOnIslandRemaining = 0;
            }

            return true;
        }

        private void OnStartCellCrossed(int MoveAmount)
        {
            if (DidCrossedStartCell(MoveAmount))
            {
                Players[PlayersSpecialIndexes.MainPlayer].MoneyOwned += Consts.Monopoly.OnStartCrossedMoneyGiven;
            }
        }

        private void CheckEvents()
        {
            CheckIfSteppedOnIsland();
        }

        private void CheckIfSteppedOnIsland()
        {
            MonopolyPlayer MainPlayer = Players[PlayersSpecialIndexes.MainPlayer];
            if (WillStayOnIsland())
            {
                MainPlayer.TurnsOnIslandRemaining = 3;
            }
        }

        private bool WillStayOnIsland()
        {
            return BoardService.SteppedOnIsland(Players[PlayersSpecialIndexes.MainPlayer].OnCellIndex) &&
                   Players[PlayersSpecialIndexes.MainPlayer].TurnsOnIslandRemaining == 0;
        }

        private MonopolyTurnResult MakeTurnResult()
        {
            if (BoardService.IsPossibleToBuyCell(Players[PlayersSpecialIndexes.MainPlayer]))
                return MonopolyTurnResult.CanBuyCell;

            return MonopolyTurnResult.CannotBuyCell;
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

            while (Players[PlayersSpecialIndexes.WhosTurn] == null)
                PlayersSpecialIndexes.WhosTurn = (++PlayersSpecialIndexes.WhosTurn) % Players.Count;
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
                CellToSellRef.GetBuyingBehavior().SetOwner(PlayerKey.NoOne);
                MainPlayer.MoneyOwned += CellToSellRef.GetBuyingBehavior().GetCosts().Buy;
            }
        }

        public PlayerKey WhoWon()
        {
            if (Players.FindAll(p => p != null).Count == 1)
                return Players.FirstOrDefault(p => p != null).Key;

            return PlayerKey.NoOne;
        }

        public MonopolyModalParameters GetModalParameters()
        {
            int MainPlayerPos = Players[PlayersSpecialIndexes.MainPlayer].OnCellIndex;
            return BoardService.GetCellModalParameters(MainPlayerPos);
        }

        public void ModalResponse(string StringResponse)
        {
            if(StringResponse == "Throw Dice(Excape if 1 is Rolled)")
            {
                if(GetRandom.number.Next(1,6) == 1)
                {
                    Players[PlayersSpecialIndexes.MainPlayer].TurnsOnIslandRemaining = 0;
                }
            }
            else if(StringResponse == $"Pay {Consts.Monopoly.IslandEscapeCost} To Leave")
            {
                if (IsAbleToPayForEscapingFromIsland())
                {
                    Players[PlayersSpecialIndexes.MainPlayer].MoneyOwned -= Consts.Monopoly.IslandEscapeCost;
                    Players[PlayersSpecialIndexes.MainPlayer].TurnsOnIslandRemaining = 0;
                }

            }
        }

        private bool IsAbleToPayForEscapingFromIsland()
        {
            return Players[PlayersSpecialIndexes.MainPlayer].MoneyOwned >= Consts.Monopoly.IslandEscapeCost;
        }
    }
}
