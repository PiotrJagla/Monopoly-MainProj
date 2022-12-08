using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums.MultiplayerConnection;
using Models.MultiplayerConnection;
using Models.UsersManagment;

namespace Services.OnlineConnectionsService
{
    public interface GameConnectionService
    {
        void addOnlinePlayer(string userName, string connId);
        bool findEnemy(string userName);

        void JoinToRoom(string UserConnId);

        Tuple<Player, Player> findGameRoomByOneName(string userName);
        Player getUserEnemy(string userName);

        List<Player> GetPlayersWithCriteria(PlayersSelectCriteria criteria, string ConnId);

        Player GetPlayer(string ConnId);

    }
}
