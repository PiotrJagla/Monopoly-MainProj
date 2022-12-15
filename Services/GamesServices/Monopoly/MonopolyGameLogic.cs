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

namespace Services.GamesServices.Monopoly
{
    public class MonopolyGameLogic : MonopolyService
    {
        private static List<MonopolyPlayer> Players = new List<MonopolyPlayer>();
        private static SpecialIndexes PlayersIndexes = new SpecialIndexes();

        private static MonopolyBoard BoardService = new MonopolyBoard();

        public void StartGame(List<Player> PlayersInGame)
        {
            if (GameIsAlreadyStarted())
                return;

            for (int i = 0; i < PlayersInGame.Count; i++)
            {
                AddPlayer((PlayerKey)i);
            }
            PlayersIndexes.WhosTurn = 0;
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

        public MonopolyUpdateMessage GetUpdatedData()
        {
            MonopolyUpdateMessage UpdatedData = new MonopolyUpdateMessage();
            UpdatedData.PlayersData = MakePlayersUpdateData().GetPlayersUpdatedData();
            UpdatedData.CellsOwners = MakeBoardUpdateData().GetCellsOwners();
            UpdatedData.MoneyBond = MakeMoneyBond();
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
            MoneyObligation result = new MoneyObligation();

            if (GameIsAlreadyStarted() == false) return result;

            //if (BoardService.GetBoard()[Players[PlayersIndexes.MainPlayer].OnCellIndex].OwnedBy != PlayerKey.NoOne &&
            //    BoardService.GetBoard()[Players[PlayersIndexes.MainPlayer].OnCellIndex].OwnedBy != Players[PlayersIndexes.MainPlayer].Key)
            //{
            //    result.ObligatedToPay = Players[PlayersIndexes.MainPlayer].Key;
            //    result.PlayerGettingMoney = BoardService.GetBoard()[Players[PlayersIndexes.MainPlayer].OnCellIndex].OwnedBy;
            //    result.ObligationAmount = BoardService.GetBoard()[Players[PlayersIndexes.MainPlayer].OnCellIndex].CellCosts.Stay;
            //}

            result.ObligatedToPay = PlayerKey.NoOne;
            result.PlayerGettingMoney = PlayerKey.NoOne;
            result.ObligationAmount = 0;

            return result;
        }



        public void UpdateData(MonopolyUpdateMessage UpdatedData)
        {
            UpdatePlayersData(UpdatedData.PlayersData);
            UpdateBoardData(UpdatedData.CellsOwners);
            UpdateMoneyObligation(UpdatedData.MoneyBond);
        }

        private void UpdatePlayersData(List<PlayerUpdateData> PlayersUpdatedData)
        {
            for (int i = 0; i < PlayersUpdatedData.Count; i++)
            {
                Players[i].OnCellIndex = PlayersUpdatedData[i].Position;
                Players[i].MoneyOwned = PlayersUpdatedData[i].Money;
                
            }
        }

        private void UpdateBoardData(List<MonopolyCellOwner> BoardUpdatedData)
        {
            for (int i = 0; i < BoardUpdatedData.Count; i++)
            {
                BoardService.GetBoard()[i].OwnedBy = BoardUpdatedData[i].Owner;
            }
        }

        private void UpdateMoneyObligation(MoneyObligation obligation)
        {
            Players.FirstOrDefault(p => p.Key == obligation.PlayerGettingMoney).MoneyOwned += obligation.ObligationAmount;
        }

        public void BuyCellIfPossible()
        {
            int MainPlayerBoardPos = Players[PlayersIndexes.MainPlayer].OnCellIndex;
            int MainPlayerMoney = Players[PlayersIndexes.MainPlayer].MoneyOwned;
            
            if (BoardService.CanAffordBuying(MainPlayerMoney, MainPlayerBoardPos))
            {
                BuyCell(MainPlayerBoardPos);
            }
        }

        private void BuyCell(int MainPlayerBoardPos)
        {
            PlayerKey MainPlayerKey = Players[PlayersIndexes.MainPlayer].Key;
            BoardService.GetBoard()[MainPlayerBoardPos].OwnedBy = MainPlayerKey;
            Players[PlayersIndexes.MainPlayer].MoneyOwned -= BoardService.GetBoard()[MainPlayerBoardPos].CellCosts.Buy;
        }

        public MoveResult Move(int amount)
        {
            OnStartCellCrossed(amount);
            Players[PlayersIndexes.MainPlayer].OnCellIndex = (Players[PlayersIndexes.MainPlayer].OnCellIndex + amount) % BoardService.GetBoard().Count;
            return IsOnSomeonesCell() ? MoveResult.OnSomeonesCell : MoveResult.OnNobodysCell;
        }

        private void OnStartCellCrossed(int MoveAmount)
        {
            if (DidCrossedStartCell(MoveAmount))
            {
                Players[PlayersIndexes.MainPlayer].MoneyOwned += Consts.Monopoly.OnStartCrossedMoneyGiven;
            }
        }

        private bool DidCrossedStartCell(int MoveAmount)
        {
            return (Players[PlayersIndexes.MainPlayer].OnCellIndex + MoveAmount) >= BoardService.GetBoard().Count;
        }

        private bool IsOnSomeonesCell()
        {
            return BoardService.GetBoard()[Players[PlayersIndexes.MainPlayer].OnCellIndex].OwnedBy != PlayerKey.NoOne;
        }

        public bool IsYourTurn()
        {
            return PlayersIndexes.WhosTurn == PlayersIndexes.MainPlayer;
        }

        public void NextTurn()
        {
            PlayersIndexes.WhosTurn = (++PlayersIndexes.WhosTurn) % Players.Count;
        }

        public void SetMainPlayerIndex(int index)
        {
            if (PlayersIndexes.MainPlayer == -1)
                PlayersIndexes.MainPlayer = index;
        }
    }
}
