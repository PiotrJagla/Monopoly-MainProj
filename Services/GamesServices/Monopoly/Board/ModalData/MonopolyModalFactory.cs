using Enums.Monopoly;
using Models;
using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.ModalData
{
    public class MonopolyModalFactory
    {
        public static MonopolyModalParameters NoModalParameters()
        {
            return new MonopolyModalParameters(new StringModalParameters(), ModalShow.Never);
        }
    }
}
