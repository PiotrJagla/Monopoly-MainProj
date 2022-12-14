using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManipulationLib
{
    public class StringConvert
    {
        public static bool StringToBool(string Convert)
        {
            if (Convert.ToLower() == "false")
            {
                return false;
            }
            else if (Convert.ToLower() == "true")
            {
                return true;
            }
            else
            {
                throw new Exception("Cannot Convert String To Bool");
            }
        }
    }
}
