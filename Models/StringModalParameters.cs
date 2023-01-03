using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Models
{
    public class StringModalParameters
    {
        public string Title { get; set; }
        public List<string> ButtonsContent { get; set; }

        public StringModalParameters()
        {
            Title = "";
            ButtonsContent = new List<string>();
        }

    }
}
