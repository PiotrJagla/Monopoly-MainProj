using Enums.Monopoly;
using Models.MultiplayerConnection;
using Services.GamesServices.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MonopolyTests
{
    public class MonopolyDataPrepareForTests
    {
        private static PlayerKey[] IsBuyingCellOnTurn = new PlayerKey[] {
                PlayerKey.First, PlayerKey.Secound, PlayerKey.Secound, PlayerKey.First, PlayerKey.First,PlayerKey.First
            };

        private static Tuple<int, PlayerKey>[] IsSellingCellIndexOnTurn = new Tuple<int, PlayerKey>[] {
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(3,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(2,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne)
            };

        public static void ExecuteTurnsNumber(int TurnsAmount, ref List<MonopolyService> Clients)
        {
            int First = 0;
            int Secound = 1;
            List<Player> Players = new List<Player>();
            for (int i = 0; i < Clients.Count; i++)
            {
                Players.Add(new Player());
            }

            for (int i = 0; i < Clients.Count; i++)
            {
                Clients[i].StartGame(Players);
            }
            
            for (int i = 0; i < Clients.Count; i++)
            {
                Clients[i].SetMainPlayerIndex(i);
            }

            for (int turn = 0; turn < TurnsAmount; turn++)
            {
                for (int clientIndex = 0; clientIndex < Clients.Count; clientIndex++)
                {
                    MonopolyService CurrentClient = Clients[clientIndex];
                    CurrentClient.ExecuteTurn(1);
                    if (IsBuyingCellOnTurn[turn] == (PlayerKey)clientIndex)
                    {
                        CurrentClient.BuyCellIfPossible();
                    }
                    if (IsSellingCellIndexOnTurn[turn].Item2 == (PlayerKey)clientIndex)
                    {
                        CurrentClient.SellCell(CurrentClient.GetBoard()[IsSellingCellIndexOnTurn[turn].Item1].OnDisplay());
                    }
                    foreach (var client in Clients)
                    {
                        if (CurrentClient != client)
                        {
                            client.UpdateData(CurrentClient.GetUpdatedData());
                        }
                    }
                }

            }
        }
    }
}
