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

        public Costs(int Buy = 0, int Stay = 0)
        {
            this.Buy = Buy;
            this.Stay = Stay;
        }
    }
}
