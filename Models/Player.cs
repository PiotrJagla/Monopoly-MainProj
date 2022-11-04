using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Player
    {
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public bool IsWaitingForGame{ get; set; }

        public Player()
        {
            Name = "";
            ConnectionId = "";
            IsWaitingForGame = true;
        }
    }
}
