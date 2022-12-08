using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public class PlayerPosition
    {
        public PlayerKey Player{ get; set; }
        public int Position { get; set; }

        public PlayerPosition()
        {
            Player = PlayerKey.LastNumber;
            Position = -1;
        }
    }
}
