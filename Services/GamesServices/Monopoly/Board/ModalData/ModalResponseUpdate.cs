using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.ModalData
{
    public class ModalResponseUpdate
    {
        public MonopolyBoard BoardService;
        public MonopolyPlayers PlayersService;
        public int MoveQuantity;

        public ModalResponseUpdate()
        {
            BoardService = new MonopolyBoard();
            PlayersService = new MonopolyPlayers(BoardService.GetBoard().Count);
            MoveQuantity = 0;
        }
    }
}
