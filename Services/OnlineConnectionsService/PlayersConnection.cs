using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Services.OnlineConnectionsService
{
    public class PlayersConnection : GameConnectionService
    {
        private static List<Player> ConnectedPlayers = new List<Player>();
        private static List<Tuple<Player,Player>> TwoUsersGameRooms = new List<Tuple<Player, Player>>();
        

        public void addOnlinePlayer(string userName, string connId)
        {
            ConnectedPlayers.Add(new Player());
            ConnectedPlayers.Last().Name = userName;
            ConnectedPlayers.Last().ConnectionId = connId;
            ConnectedPlayers.Last().IsWaitingForGame = true;
        }

        public bool findEnemy(string userName)
        {
            Player PlayerLookingForEnemy = ConnectedPlayers.FirstOrDefault(user => user.Name == userName);
            foreach (Player player in ConnectedPlayers)
            {
                if (player.Name != PlayerLookingForEnemy.Name && player.IsWaitingForGame == true)
                {
                    TwoUsersGameRooms.Add( new Tuple<Player,Player>( player, PlayerLookingForEnemy ) );
                    setEnemiesStatusToInGame(player, PlayerLookingForEnemy);
                    return true;
                }
            }

            return false;
        }


        private void setEnemiesStatusToInGame(Player Enemy1, Player Enemy2)
        {
            ConnectedPlayers[ConnectedPlayers.IndexOf(Enemy1)].IsWaitingForGame = false;
            ConnectedPlayers[ConnectedPlayers.IndexOf(Enemy2)].IsWaitingForGame = false;
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
    }
}
