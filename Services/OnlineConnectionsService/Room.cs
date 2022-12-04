using Models.MultiplayerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OnlineConnectionsService
{
    public class Room
    {
        private int MaxPlayers;
        private List<Player> PlayersInRoom;

        public Room()
        {
            MaxPlayers = 4;
            PlayersInRoom = new List<Player>();
        }

        public void SetMaxPlayers(int MaxPlayers)
        {
            this.MaxPlayers = MaxPlayers;
        }

        public bool AddToRoom(Player player)
        {
            if(PlayersInRoom.Count <= MaxPlayers)
            {
                PlayersInRoom.Add(player);
                return true;
            }
            return false;
        }

        public Player GetPlayer(int index)
        {
            return PlayersInRoom[index];
        }

        public Player GetPlayer(string PlayerName)
        {
            return PlayersInRoom.FirstOrDefault(player => player.Name == PlayerName);
        }

        public bool IsFull()
        {
            return PlayersInRoom.Count == MaxPlayers;
        }



    }
}
