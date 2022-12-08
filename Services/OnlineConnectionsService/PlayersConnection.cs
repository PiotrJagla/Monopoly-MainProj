using Enums.MultiplayerConnection;
using Models.MultiplayerConnection;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Services.OnlineConnectionsService
{
    public class PlayersConnection : GameConnectionService
    {
        private static List<Player> ConnectedPlayers = new List<Player>();
        private static List<Tuple<Player,Player>> TwoUsersGameRooms = new List<Tuple<Player, Player>>();
        private static List<Room> GameRooms = new List<Room>();
        

        public void JoinToRoom(string UserConnId)
        {
            if (IsAlreadyInRoom(UserConnId))
                return;

            if(IsRoomToJoin(UserConnId) == false)
            {
                GameRooms.Add(new Room(UserConnId));
                GameRooms.Last().AddToRoom(ConnectedPlayers.FirstOrDefault(user => user.ConnectionId == UserConnId));
                ConnectedPlayers.FirstOrDefault(user => user.ConnectionId == UserConnId).InRoom = UserConnId;
            }
        }

        private bool IsAlreadyInRoom(string UserConnId)
        {
            return GameRooms.FirstOrDefault(room => room.GetPlayer(UserConnId) != null) != null;
        }

        private bool IsRoomToJoin(string UserConnId)
        {
            foreach (Room room in GameRooms)
            {
                if (room.IsFull() == false)
                {
                    room.AddToRoom(ConnectedPlayers.FirstOrDefault(user => user.ConnectionId == UserConnId));
                    ConnectedPlayers.FirstOrDefault(user => user.ConnectionId == UserConnId).InRoom = room.GetName();
                    return true;
                }
            }
            return false;
        }


        public List<Player> GetPlayersWithCriteria(PlayersSelectCriteria criteria, string ConnId)
        {
            Room room = GameRooms.FirstOrDefault(room => room.GetPlayer(ConnId) != null);
            return room.GetPlayersWith(criteria);
        }

        

        public bool findEnemy(string userName)
        {
            Player PlayerLookingForEnemy = ConnectedPlayers.FirstOrDefault(user => user.Name == userName);
            foreach (Player player in ConnectedPlayers)
            {
                if (player.Name != PlayerLookingForEnemy.Name && player.NotReady == true)
                {
                    TwoUsersGameRooms.Add(new Tuple<Player, Player>(player, PlayerLookingForEnemy));
                    setEnemiesStatusToInGame(player, PlayerLookingForEnemy);
                    return true;
                }
            }

            return false;
        }

        private void setEnemiesStatusToInGame(Player Enemy1, Player Enemy2)
        {
            ConnectedPlayers[ConnectedPlayers.IndexOf(Enemy1)].NotReady = false;
            ConnectedPlayers[ConnectedPlayers.IndexOf(Enemy2)].NotReady = false;
        }


        public Player getUserEnemy(string userName)
        {
            Tuple<Player, Player> TwoPlayersRoom = findGameRoomByOneName(userName);

            if (TwoPlayersRoom == null) return null;

            if (TwoPlayersRoom.Item1.Name == userName)
                return TwoPlayersRoom.Item2;
            else
                return TwoPlayersRoom.Item1;
        }

        public Tuple<Player, Player> findGameRoomByOneName(string userName)
        {
            return TwoUsersGameRooms.FirstOrDefault(user => (user.Item1.Name == userName || user.Item2.Name == userName));
        }

        public void addOnlinePlayer(string userName, string connId)
        {
            if (ConnectedPlayers.FirstOrDefault(user => user.ConnectionId == connId) != null)
                return;

            ConnectedPlayers.Add(new Player());
            ConnectedPlayers.Last().Name = userName;
            ConnectedPlayers.Last().ConnectionId = connId;
            ConnectedPlayers.Last().NotReady = true;
        }

        public Player GetPlayer(string ConnId)
        {
            return ConnectedPlayers.FirstOrDefault(user => user.ConnectionId == ConnId);
        }

    }
}
