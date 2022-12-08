using Enums.Monopoly;
using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly
{
    public class PlayersPositionsData
    {
        private List<PlayerPosition> PlayersKeyPositions;

        public PlayersPositionsData()
        {

        }

        public List<PlayerPosition> GetPlayersPositions()
        {
            return PlayersKeyPositions;
        }

        public PlayerPosition GetPlayerPosition(int index)
        {
            return PlayersKeyPositions[index];
        }

        public void FormatPlayersPositionsData(List<MonopolyPlayer> Players)
        {
            PlayersKeyPositions = new List<PlayerPosition>();
            foreach (var player in Players)
            {
                PlayersKeyPositions.Add(new PlayerPosition());
                PlayersKeyPositions.Last().Position = player.OnCellIndex;
                PlayersKeyPositions.Last().Player = player.Key;
            }
        }
    }
}
