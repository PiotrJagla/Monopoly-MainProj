using Enums.Monopoly;
using Models.Monopoly;
using Models.MultiplayerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Update
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
            
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i] != null)
                {
                    PlayersData.Add(new PlayerUpdateData());
                    PlayersData.Last().Position = Players[i].OnCellIndex;
                    PlayersData.Last().Money = Players[i].MoneyOwned;
                    PlayersData.Last().Player = Players[i].Key;
                    PlayersData.Last().PlayerIndex = i;
                }
            }
        }
    }
}
