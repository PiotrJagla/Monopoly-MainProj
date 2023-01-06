using Models.Monopoly;
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
        private SpecialIndexes PlayersSpecjalIndexes;

        public MonopolyPlayers()
        {
            Players = new List<MonopolyPlayer>();
            PlayersSpecjalIndexes = new SpecialIndexes();
        }

        public List<MonopolyPlayer> GetPlayers()
        {
            return Players;
        }

        public MonopolyPlayer GetMainPlayer()
        {
            return Players[PlayersSpecjalIndexes.MainPlayer];
        }
    }
}
