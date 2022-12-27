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
        private static PlayerKey[] PlayersBuyingOrder_TwoClients = new PlayerKey[] {
                PlayerKey.First, PlayerKey.Secound, PlayerKey.Secound, PlayerKey.First, PlayerKey.First,PlayerKey.First
            };

        private static Tuple<int, PlayerKey>[] PlayersSellingOrder_TwoClients = new Tuple<int, PlayerKey>[] {
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(3,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(2,PlayerKey.Secound),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne)
            };

        private static PlayerKey[] PlayersBuyingOrder_ThreeClient = new PlayerKey[] {
                PlayerKey.Secound, PlayerKey.Third, PlayerKey.First, PlayerKey.First, PlayerKey.First,PlayerKey.First
            };

        private static Tuple<int, PlayerKey>[] PlayersSellingOrder_ThreeClient = new Tuple<int, PlayerKey>[] {
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(-1,PlayerKey.NoOne),
                new Tuple<int,PlayerKey>(2,PlayerKey.Third),
                new Tuple<int,PlayerKey>(1,PlayerKey.Secound)
            };

        private static PlayerKey[] PlayersBuyingOrderOnTurns = new PlayerKey[] { };
        private static Tuple<int, PlayerKey>[] PlayersSellingOrderOnTurns = new Tuple<int, PlayerKey>[] { };

        public static void ExecuteTurnsNumber(int TurnsAmount, ref List<MonopolyService> Clients)
        {
            PrepareClientsData(ref Clients);
            ExecuteTurns(TurnsAmount, ref Clients);
        }

        private static void PrepareClientsData(ref List<MonopolyService> Clients)
        {
            ChooseByingAndSellingOrder(Clients.Count);
            List<Player> Players = AddPlayers(ref Clients);
            StartClientsGame(ref Clients, Players);
            SetMainPlayersIndex(ref Clients);
        }

        private static void ChooseByingAndSellingOrder(int ClientsNumber)
        {
            switch (ClientsNumber)
            {
                case 2:
                    PlayersBuyingOrderOnTurns = PlayersBuyingOrder_TwoClients;
                    PlayersSellingOrderOnTurns = PlayersSellingOrder_TwoClients;
                    break;
                case 3:
                    PlayersBuyingOrderOnTurns = PlayersBuyingOrder_ThreeClient;
                    PlayersSellingOrderOnTurns = PlayersSellingOrder_ThreeClient;
                    break;
            }
        }


        private static void SetMainPlayersIndex(ref List<MonopolyService> Clients)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                Clients[i].SetMainPlayerIndex(i);
            }
        }

        private static List<Player> AddPlayers(ref List<MonopolyService> Clients)
        {
            List<Player> Players = new List<Player>();
            for (int i = 0; i < Clients.Count; i++)
            {
                Players.Add(new Player());
            }

            return Players;
        }

        private static void StartClientsGame(ref List<MonopolyService> Clients, List<Player> Players)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                Clients[i].StartGame(Players);
            }
        }

        private static void ExecuteTurns(int TurnsAmount,ref List<MonopolyService> Clients)
        {
            for (int turn = 0; turn < TurnsAmount; turn++)
            {
                for (int clientIndex = 0; clientIndex < Clients.Count; clientIndex++)
                {
                    MonopolyService CurrentClient = Clients[clientIndex];
                    CurrentClient.ExecuteTurn(1);
                    BuyCell(turn, clientIndex,ref CurrentClient);
                    SellCell(turn, clientIndex,ref CurrentClient);
                    UpdateOthers(ref Clients,ref CurrentClient);
                }

            }
        }

        private static void UpdateOthers(ref List<MonopolyService> Clients, ref MonopolyService CurrentClient)
        {
            foreach (var client in Clients)
            {
                if (CurrentClient != client)
                {
                    client.UpdateData(CurrentClient.GetUpdatedData());
                }
                client.NextTurn();
            }
        }

        private static void SellCell(int turn, int clientIndex, ref MonopolyService CurrentClient)
        {
            if (PlayersSellingOrderOnTurns[turn].Item2 == (PlayerKey)clientIndex)
            {
                CurrentClient.SellCell(CurrentClient.GetBoard()[PlayersSellingOrderOnTurns[turn].Item1].OnDisplay());
            }
        }

        private static void BuyCell(int turn, int clientIndex, ref MonopolyService CurrentClient)
        {
            if (PlayersBuyingOrderOnTurns[turn] == (PlayerKey)clientIndex)
            {
                CurrentClient.BuyCellIfPossible();
            }
        }
    }
}
