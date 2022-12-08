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
        public int Position { get; set; }
        public PlayerKey Player{ get; set; }

        public PlayerPosition()
        {
            Position = 0;
            Player = PlayerKey.LastNumber;
        }
    }
}
