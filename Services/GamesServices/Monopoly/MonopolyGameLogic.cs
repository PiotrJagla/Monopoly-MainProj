using Models.MultiplayerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums.Monopoly;
using System.Threading.Tasks;
using Models.Monopoly;
using Models;

namespace Services.GamesServices.Monopoly
{
    public class MonopolyGameLogic : MonopolyService
    {
        private static List<MonopolyPlayer> Players = new List<MonopolyPlayer>();
        private static int MainPlayerIndex = -1;
        private static int WhosTurnIndex = -1;

        
        private static MonopolyBoard BoardService = new MonopolyBoard();

        public void StartGame(List<Player> PlayersInGame)
        {
            if (Players.Count != 0)
                return;

            for (int i = 0; i < PlayersInGame.Count; i++)
            {
                Players.Add(new MonopolyPlayer());
                Players.Last().Key = (PlayerKey)(i);
                Players.Last().OnCellIndex = 0;
            }

            WhosTurnIndex = 0;
            
        }

        public List<MonopolyCell> GetBoard()
        {
            return BoardService.GetBoard();   
        }

        public void Move(int amount)
        {
            Players[MainPlayerIndex].OnCellIndex = (Players[MainPlayerIndex].OnCellIndex + amount) % BoardService.GetBoard().Count;
        }

        public void SetMainPlayerIndex(int index)
        {
            if(MainPlayerIndex == -1)
                MainPlayerIndex = index;
        }


        public PlayersPositionsData GetPlayersPositions()
        {
            PlayersPositionsData PlayersPositions = new PlayersPositionsData();
            PlayersPositions.FormatPlayersPositionsData(Players);
            return PlayersPositions;
        }

        public void UpdatePlayersPositions(List<PlayerPosition> UpdatedPositions)
        {
            for (int i = 0; i < UpdatedPositions.Count; i++)
            {
                Players[i].OnCellIndex = UpdatedPositions[i].Position;
            }
        }

        public bool IsYourTurn()
        {
            return WhosTurnIndex == MainPlayerIndex;
        }

        public void NextTurn()
        {
            WhosTurnIndex = (++WhosTurnIndex) % Players.Count;
        }
    }
}
