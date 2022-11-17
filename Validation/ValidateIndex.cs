using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation
{
    public class ValidateIndex
    {
        public static bool IsWithin2DArray(in Point2D index,in Point2D size)
        {
            return index.x >= 0 && index.x < size.x && index.y >= 0 && index.y < size.y;
        }
    }
}
