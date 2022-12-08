using Models.MultiplayerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
using Enums.MultiplayerConnection;
using System.ComponentModel;

namespace Services.OnlineConnectionsService
{
    public class Room
    {
        private int MaxPlayers;
        private List<Player> PlayersInRoom;
        private string Name;

        public Room(string name)
        {
            this.Name = name;
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

        public Player GetPlayer(string PlayerConnId)
        {
            return PlayersInRoom.FirstOrDefault(player => player.ConnectionId == PlayerConnId);
        }

        public string GetName()
        {
            return Name;
        }

        public bool IsFull()
        {
            return PlayersInRoom.Count == MaxPlayers;
        }

        public void IsEveryoneReady(PlayersCalculator calculator)
        {
            if (AreReadyEqualAllPlayers(calculator))
                MaxPlayers = PlayersInRoom.Count;
        }

        private bool AreReadyEqualAllPlayers(PlayersCalculator calculator)
        {
            return calculator is ReadyPlayersCalculator && calculator.GetPlayers(PlayersInRoom).Count == PlayersInRoom.Count;
        }

        public List<Player> GetPlayersWith(PlayersSelectCriteria criteria)
        {
            PlayersCalculator calculator = CreatePlayersCalculator(criteria);
            IsEveryoneReady(calculator);
            return calculator.GetPlayers(PlayersInRoom);
        }

        private PlayersCalculator CreatePlayersCalculator(PlayersSelectCriteria criteria)
        {
            switch(criteria)
            {
                case PlayersSelectCriteria.AllPlayers:
                    return new AllPlayersCalculator();
                case PlayersSelectCriteria.ReadyPlayers:
                    return new ReadyPlayersCalculator();
                default:
                    return null;
            }
        }



    }

    public interface PlayersCalculator
    {
        List<Player> GetPlayers(List<Player> players);
    }

    public class AllPlayersCalculator : PlayersCalculator
    {
        public List<Player> GetPlayers(List<Player> players)
        {
            return players;
        }
    }

    public class ReadyPlayersCalculator : PlayersCalculator
    {
        public List<Player> GetPlayers(List<Player> players)
        {
            List<Player> result = new List<Player>();
            foreach (Player player in players)
            {
                if (player.NotReady == false)
                    result.Add(player);
            }
            return result;
        }
    }
}
