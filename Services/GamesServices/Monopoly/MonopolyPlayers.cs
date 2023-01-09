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
    }
}
