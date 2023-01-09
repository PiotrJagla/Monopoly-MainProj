using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Update
{
    public class UpdateDataFactory
    {
        public static MonopolyBoardUpdateData CreateBoardUpdateData()
        {
            return new MonopolyBoardUpdateDataImpl();
        }

        public static MonopolyPlayersUpdateData CreatePlayersUpdateData()
        {
            return new MonopolyPlayersUpdateDataImpl();
        }

        
    }
}
