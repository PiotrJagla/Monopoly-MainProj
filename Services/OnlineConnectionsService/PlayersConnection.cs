using Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach(Player player in ConnectedPlayers)
            {
                if (player.Name != userName && player.IsWaitingForGame == true)
                {
                    TwoUsersGameRooms.Add( new Tuple<Player,Player>( player, ConnectedPlayers.FirstOrDefault(user => user.Name == userName) ) );
                    setFoundEnemiesStatusToInGame(player, ConnectedPlayers.FirstOrDefault(user => user.Name == userName));
                    Console.WriteLine($"{TwoUsersGameRooms.Last().Item1.Name}  :  {TwoUsersGameRooms.Last().Item2.Name}");
                    return true;
                }
            }
            return false;
        }


        private void setFoundEnemiesStatusToInGame(Player Enemy1, Player Enemy2)
        {
            ConnectedPlayers[ConnectedPlayers.IndexOf(Enemy1)].IsWaitingForGame = false;
            ConnectedPlayers[ConnectedPlayers.IndexOf(Enemy2)].IsWaitingForGame = false;
        }
        public Tuple<Player, Player> getTwoUsersGameRoomWithGiveName(string userName)
        {
            return TwoUsersGameRooms.FirstOrDefault(user => (user.Item1.Name == userName || user.Item2.Name == userName));
        }

        public Player getEnemyWithGivenUserName(string userName)
        {
            Tuple<Player, Player> TwoPlayersRoom = getTwoUsersGameRoomWithGiveName(userName);

            if (TwoPlayersRoom.Item1.Name == userName)
                return TwoPlayersRoom.Item2;
            else
                return TwoPlayersRoom.Item1;
        }
    }
}
