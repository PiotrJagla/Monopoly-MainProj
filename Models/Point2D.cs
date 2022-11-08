using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Point2D
    {
        public int x { get; set; }
        public int y { get; set; }


        public Point2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
