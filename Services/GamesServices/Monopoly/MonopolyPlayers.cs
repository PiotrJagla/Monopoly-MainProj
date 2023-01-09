using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Models.MultiplayerConnection;
using Services.GamesServices.Monopoly.Board.Cells;
using Services.GamesServices.Monopoly.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly
{
    public class MonopolyPlayers
    {
        private List<MonopolyPlayer> Players;
        private SpecialIndexes PlayersSpecialIndexes;

        public MonopolyPlayers(List<Player> PlayersInGame)
        {
            Players = new List<MonopolyPlayer>();
            PlayersSpecialIndexes = new SpecialIndexes();
            InitPlayers(PlayersInGame);
        }

        private void InitPlayers(List<Player> PlayersInGame)
        {
            for (int i = 0; i < PlayersInGame.Count; i++)
            {
                AddPlayer((PlayerKey)i);
            }
            PlayersSpecialIndexes.WhosTurn = 0;
        }

        private void AddPlayer(PlayerKey key)
        {
            Players.Add(new MonopolyPlayer());
            Players.Last().Key = key;
            Players.Last().OnCellIndex = 0;
            Players.Last().MoneyOwned = Consts.Monopoly.StartMoneyAmount;
            
        }

        public MonopolyPlayersUpdateData MakePlayersUpdateData()
        {
            MonopolyPlayersUpdateData PlayersUpdatedData = UpdateDataFactory.CreatePlayersUpdateData();
            PlayersUpdatedData.FormatPlayersUpdateData(Players);
            return PlayersUpdatedData;
        }

        public List<MonopolyPlayer> GetPlayers()
        {
            return Players;
        }

        public MonopolyPlayer GetMainPlayer()
        {
            return Players[PlayersSpecialIndexes.MainPlayer];
        }

        public PlayerKey CheckForBankruptPlayer(ref MonopolyUpdateMessage UpdateData)
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

        public void UpdateData(List<PlayerUpdateData> PlayersUpdatedData)
        {
            for (int i = 0; i < PlayersUpdatedData.Count; i++)
            {
                Players[PlayersUpdatedData[i].PlayerIndex].OnCellIndex = PlayersUpdatedData[i].Position;
                Players[PlayersUpdatedData[i].PlayerIndex].MoneyOwned = PlayersUpdatedData[i].Money;
            }
        }

        private void UpdateMoneyObligation(MoneyObligation obligation)
        {
            MonopolyPlayer PlayerGettingMoney = Players.FirstOrDefault(p => p != null && (p.Key == obligation.PlayerGettingMoney));
            MonopolyPlayer PlayerLosingMoney = Players.FirstOrDefault(p => p != null && (p.Key == obligation.PlayerLosingMoney));
            if (PlayerGettingMoney != null && PlayerLosingMoney != null)
            {
                PlayerGettingMoney.MoneyOwned += obligation.ObligationAmount;
                PlayerLosingMoney.MoneyOwned -= obligation.ObligationAmount;
            }
        }

    }
}
