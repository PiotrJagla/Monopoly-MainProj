using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public class PlayerUpdateData
    {
        public PlayerKey Player{ get; set; }
        public int Position { get; set; }
        public int Money { get; set; }

        public PlayerUpdateData()
        {
            Player = PlayerKey.NoOne;
            Position = -1;
            Money = 0;
        }
    }
}
