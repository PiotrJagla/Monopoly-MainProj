using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //Intent of this class is to store a reference to an int
    public class Int
    {
        public int Value { get; set; }
        public Int(int value = 0)
        {
            Value = value;
        }
    }
}
