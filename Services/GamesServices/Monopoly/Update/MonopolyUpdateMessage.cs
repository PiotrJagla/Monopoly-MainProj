using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Update
{
    public class MonopolyUpdateMessage
    {
        public List<PlayerUpdateData> PlayersData { get; set; }
        public List<MonopolyCellOwner> CellsOwners { get; set; }
        public MoneyObligation MoneyBond { get; set; }

        public MonopolyUpdateMessage()
        {
            PlayersData = new List<PlayerUpdateData>();
            CellsOwners = new List<MonopolyCellOwner>();
            MoneyBond = new MoneyObligation();
        }
    }
}
