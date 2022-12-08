using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.MultiplayerConnection
{
    public class Player
    {
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public bool NotReady { get; set; }

        public string InRoom { get; set; }

        public Player()
        {
            InRoom = "";
            Name = "";
            ConnectionId = "";
            NotReady = true;
        }
    }
}
