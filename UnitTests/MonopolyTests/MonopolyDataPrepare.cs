using Enums.Monopoly;
using Models.Monopoly;
using Models.MultiplayerConnection;
using Services.GamesServices.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MonopolyTests
{
    public class MoneyFlow
    {
        public int Income { get; set; }
        public int Loss { get; set; }

        public MoneyFlow()
        {
            Income = 0;
            Loss = 0;
        }
    }

    public class MonopolyDataPrepare
    {
        private static PlayerKey[][] PlayersBuyingOrderInTurns_XClients = new PlayerKey[][]
        {
            new PlayerKey[]{ },
            new PlayerKey[]{ },
            new PlayerKey[]{
                PlayerKey.First, PlayerKey.Secound, PlayerKey.Secound, PlayerKey.First, PlayerKey.First,PlayerKey.First,PlayerKey.First,
                PlayerKey.First,PlayerKey.First,PlayerKey.First,PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne,
            },
            new PlayerKey[]{ 
                PlayerKey.Secound, PlayerKey.Third, PlayerKey.First, PlayerKey.First, PlayerKey.First,PlayerKey.First, PlayerKey.First,
                PlayerKey.First, PlayerKey.First, PlayerKey.NoOne, PlayerKey.NoOne, PlayerKey.NoOne
            }
        };

        private static PlayerKey[] PlayersBuyingOrderOnTurns = new PlayerKey[] { };

        private static List<MoneyFlow> PlayersMoneyFlow;

        public static List<MoneyFlow> ExecuteTurnsNumber(int TurnsAmount, ref List<MonopolyService> Clients)
        {
            PrepareClientsData(ref Clients);
            ExecuteTurns(TurnsAmount, ref Clients);
            return PlayersMoneyFlow;
        }

        public static void PrepareClientsData(ref List<MonopolyService> Clients)
        {
            ChooseBuyingOrder(Clients.Count);
            List<Player> Players = AddPlayers(ref Clients);
            StartClientsGame(ref Clients, Players);
            SetMainPlayersIndex(ref Clients);
        }

        private static void ChooseBuyingOrder(int ClientsNumber)
        {
            PlayersBuyingOrderOnTurns = PlayersBuyingOrderInTurns_XClients[ClientsNumber];
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
            PlayersMoneyFlow = new List<MoneyFlow>();            
            for (int i = 0; i < Clients.Count; i++)
            {
                PlayersMoneyFlow.Add(new MoneyFlow());
                Clients[i].StartGame(Players);
            }
        }

        private static void ExecuteTurns(int TurnsAmount,ref List<MonopolyService> Clients)
        {
            for (int turn = 0; turn < TurnsAmount; turn++)
            {
                ExecuteClientsTurn(ref Clients, turn);
            }
        }

        private static void ExecuteClientsTurn(ref List<MonopolyService> Clients, int turn)
        {
            for (int clientIndex = 0; clientIndex < Clients.Count; clientIndex++)
            {
                MonopolyService CurrentClient = Clients[clientIndex];
                CurrentClient.ExecuteTurn(1);
                BuyCell(turn, clientIndex, ref CurrentClient);
                SellCell(turn, clientIndex, ref CurrentClient);
                UpdateExpectedMoneyFlow(Clients, turn, clientIndex);
                UpdateOthers(ref Clients, ref CurrentClient);
            }
        }

        private static void UpdateExpectedMoneyFlow(List<MonopolyService> Clients, int turn, int clientIndex)
        {
            int CellIndex = (turn + 1) % Clients[0].GetBoard().Count;
            if ((int)(Clients[0].GetBoard()[CellIndex].GetOwner()) < clientIndex)
            {
                PlayersMoneyFlow[clientIndex].Loss += Clients[0].GetBoard()[CellIndex].GetCosts().Stay;
                PlayersMoneyFlow[(int)(Clients[0].GetBoard()[CellIndex].GetOwner())].Income += Clients[0].GetBoard()[CellIndex].GetCosts().Stay;
            }
        }

        public static void UpdateOthers(ref List<MonopolyService> Clients, ref MonopolyService CurrentClient)
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
            while(CurrentClient.DontHaveMoneyToPay() && CurrentClient.GetMainPlayerCells().Count != 0)
            {
                int CellToSellIndex = CurrentClient.GetBoard().IndexOf(CurrentClient.GetMainPlayerCells()[0]);

                PlayersMoneyFlow[clientIndex].Income += CurrentClient.GetBoard()[CellToSellIndex].GetCosts().Buy;

                CurrentClient.SellCell(CurrentClient.GetBoard()[CellToSellIndex].OnDisplay());
            }
        }

        private static void BuyCell(int turn, int clientIndex, ref MonopolyService CurrentClient)
        {
            int CellIndex = (turn + 1) % CurrentClient.GetBoard().Count;
            if (PlayersBuyingOrderOnTurns[turn] == (PlayerKey)clientIndex)
            {
                PlayersMoneyFlow[clientIndex].Loss += CurrentClient.GetBoard()[CellIndex].GetCosts().Buy;
                CurrentClient.BuyCellIfPossible();
            }
        }

        public static List<int> GetExpectedMoney(List<MoneyFlow> MoneyFlows)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < MoneyFlows.Count; i++)
            {
                result.Add(Consts.Monopoly.StartMoneyAmount);
                result[i] += MoneyFlows[i].Income;
                result[i] -= MoneyFlows[i].Loss;
            }
            return result;
        }

        public static bool CompareMoneyAmount(in List<int> Expected,in List<PlayerUpdateData> Actual)
        {
            for (int i = 0; i < Actual.Count && i < Expected.Count; i++)
            {
                if (Expected[i] != Actual[i].Money)
                    return false;
            }
            return true;
        }
    }
}
