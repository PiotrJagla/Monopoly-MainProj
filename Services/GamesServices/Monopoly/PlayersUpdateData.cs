using Enums.Monopoly;
using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly
{
    public class PlayersUpdateData
    {
        private List<PlayerUpdateData> PlayersData;

        public List<PlayerUpdateData> GetPlayersUpdatedData()
        {
            return PlayersData;
        }

        public PlayerUpdateData GetPlayerUpdatedData(int index)
        {
            return PlayersData[index];
        }

        public void FormatPlayersUpdateData(List<MonopolyPlayer> Players)
        {
            PlayersData = new List<PlayerUpdateData>();
            foreach (var player in Players)
            {
                PlayersData.Add(new PlayerUpdateData());
                PlayersData.Last().Position = player.OnCellIndex;
                PlayersData.Last().Money = player.MoneyOwned;
                PlayersData.Last().Player = player.Key;
            }
        }
    }
}
