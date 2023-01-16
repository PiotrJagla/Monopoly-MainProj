using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public class MonopolyModalResponse
    {
        public ModalResponseIdentifier Identifier { get; set; }
        public string Message { get; set; }

        public MonopolyModalResponse()
        {
            Identifier = ModalResponseIdentifier.NoResponse;
            Message = "";
        }
    }
}
