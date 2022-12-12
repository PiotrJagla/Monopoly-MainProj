using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public class Costs
    {
        public int Buy { get; set; }
        public int Stay { get; set; }

        public Costs()
        {
            Buy = 0;
            Stay = 0;
        }
    }
}
