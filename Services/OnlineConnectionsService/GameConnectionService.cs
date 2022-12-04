using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.MultiplayerConnection;

namespace Services.OnlineConnectionsService
{
    public interface GameConnectionService
    {
        void addOnlinePlayer(string userName, string connId);
        bool findEnemy(string userName);

        bool JoinToRoom(string userName);

        Tuple<Player, Player> findGameRoomByOneName(string userName);
        Player getUserEnemy(string userName);
    }
}
