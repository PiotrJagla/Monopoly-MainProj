using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.OnlineConnectionsService
{
    public interface GameConnectionService
    {
        void addOnlinePlayer(string userName, string connId);
        bool findEnemy(string userName);

        Tuple<Player, Player> getTwoUsersGameRoomWithGiveName(string userName);
        Player getEnemyWithGivenUserName(string userName);
    }
}
