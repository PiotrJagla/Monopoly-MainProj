using Models.MultiplayerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums.Monopoly;
using System.Threading.Tasks;
using Models.Monopoly;
using Models;

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

        public void Move(int amount)
        {
            OnStartCellCrossed(amount);
            Players[PlayersIndexes.MainPlayer].OnCellIndex = (Players[PlayersIndexes.MainPlayer].OnCellIndex + amount) % BoardService.GetBoard().Count;
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

        public void SetMainPlayerIndex(int index)
        {
            if(PlayersIndexes.MainPlayer == -1)
                PlayersIndexes.MainPlayer = index;
        }


        public PlayersUpdateData GetPlayersUpdatedData()
        {
            PlayersUpdateData PlayersData = new PlayersUpdateData();
            PlayersData.FormatPlayersUpdateData(Players);
            return PlayersData;
        }

        public void UpdatePlayersData(List<PlayerUpdateData> UpdatedData)
        {
            for (int i = 0; i < UpdatedData.Count; i++)
            {
                Players[i].OnCellIndex = UpdatedData[i].Position;
                Players[i].MoneyOwned = UpdatedData[i].Money;
            }
        }

        public bool IsYourTurn()
        {
            return PlayersIndexes.WhosTurn == PlayersIndexes.MainPlayer;
        }

        public void NextTurn()
        {
            PlayersIndexes.WhosTurn = (++PlayersIndexes.WhosTurn) % Players.Count;
        }
    }
}
