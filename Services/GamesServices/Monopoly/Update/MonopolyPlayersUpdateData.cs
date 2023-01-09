using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Update
{
    public interface MonopolyPlayersUpdateData
    {
        List<PlayerUpdateData> GetPlayersUpdatedData();

        PlayerUpdateData GetPlayerUpdatedData(int index);

        void FormatPlayersUpdateData(List<MonopolyPlayer> Players);
    }
}
