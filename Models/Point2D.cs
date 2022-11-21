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


        public Point2D(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        //public Point2D(Point2D NewPoint)
        //{
        //    x = NewPoint.x;
        //    y = NewPoint.y;
        //}
    }
}
