using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public class SpecialIndexes
    {
        public int MainPlayer { get; set; }
        public int WhosTurn { get; set; }

        public SpecialIndexes()
        {
            MainPlayer = -1;
            WhosTurn = -1;
        }
    }
}
