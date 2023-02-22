using Enums.Monopoly;
using Models.Monopoly;
using Models.MultiplayerConnection;
using MySqlX.XDevAPI;
using Services.GamesServices.Monopoly;
using Services.GamesServices.Monopoly.Board.Cells;
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
        private static List<MoneyFlow> PlayersMoneyFlow;

        public static List<MoneyFlow> ExecuteTurnsNumber(int TurnsAmount, ref List<MonopolyService> Clients, PlayerKey[] BuyingOrder)
        {
            PrepareClientsData(ref Clients);
            ExecuteTurns(TurnsAmount, ref Clients, BuyingOrder);
            return PlayersMoneyFlow;
        }

        public static void PrepareClientsData(ref List<MonopolyService> Clients)
        {
            Clients = InitClients(Clients.Count);
        }

        public static List<MonopolyService> InitClients(int HowMany)
        {
            List<MonopolyService> Result = new List<MonopolyService>();
            for (int i = 0; i < HowMany; i++)
            {
                Result.Add(new MonopolyGameLogic());
            }
            List<Player> Players = AddPlayers(ref Result);
            StartClientsGame(ref Result, Players);
            SetMainPlayersIndex(ref Result);
            return Result;
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

        private static void ExecuteTurns(int TurnsAmount,ref List<MonopolyService> Clients, PlayerKey[] BuyingOrder)
        {
            for (int turn = 0; turn < TurnsAmount; turn++)
            {
                ExecuteClientsTurn(ref Clients, turn, BuyingOrder);
            }
        }

        private static void ExecuteClientsTurn(ref List<MonopolyService> Clients, int turn, PlayerKey[] BuyingOrder)
        {
            for (int clientIndex = 0; clientIndex < Clients.Count; clientIndex++)
            {
                MonopolyService CurrentClient = Clients[clientIndex];
                ExecuteClientTestTurn(ref CurrentClient, turn);
                BuyCell(turn, clientIndex, ref CurrentClient, BuyingOrder);
                SellCell(turn, clientIndex, ref CurrentClient);
                UpdateExpectedMoneyFlow(Clients, turn, clientIndex);
                UpdateOthers(ref Clients, ref CurrentClient);
            }
        }
        public static void ExecuteClientTestTurn(ref MonopolyService Client, int turn)
        {
            int CellIndex = (turn + 1) % Client.GetBoard().Count;
            Client.ExecutePlayerMove(1);
            if (Client.GetBoard()[CellIndex] is MonopolyIslandCell)
            {
                Client.ExecutePlayerMove(1);
                Client.ExecutePlayerMove(1);
            }
        }

        public static void ExecuteClientTestTurn(ref List<MonopolyService> Clients,int ClientIndex,  int turn)
        {
            Clients[ClientIndex].ExecutePlayerMove(1);
            if (Clients[ClientIndex].GetBoard()[turn] is MonopolyIslandCell)
            {
                Clients[ClientIndex].ExecutePlayerMove(1);
                Clients[ClientIndex].ExecutePlayerMove(1);
            }
        }

        private static void BuyCell(int turn, int clientIndex, ref MonopolyService CurrentClient, PlayerKey[] BuyingOrder)
        {
            int CellIndex = (turn + 1) % CurrentClient.GetBoard().Count;
            if (BuyingOrder[turn] == (PlayerKey)clientIndex)
            {
                MonopolyModalParameters parameters = CurrentClient.GetModalParameters();
                CurrentClient.ModalResponse(
                    FindStringBuyingCellFrom(parameters.Parameters.ButtonsContent));

                PlayersMoneyFlow[clientIndex].Loss += CurrentClient.GetBoard()[CellIndex].GetBuyingBehavior().GetCosts().Buy;
            }
        }

        private static void SellCell(int turn, int clientIndex, ref MonopolyService CurrentClient)
        {
            while (CurrentClient.DontHaveMoneyToPay() && CurrentClient.GetMainPlayerCells().Count != 0)
            {
                int CellToSellIndex = CurrentClient.GetBoard().IndexOf(CurrentClient.GetMainPlayerCells()[0]);

                PlayersMoneyFlow[clientIndex].Income += CurrentClient.GetBoard()[CellToSellIndex].GetBuyingBehavior().GetCosts().Buy;

                CurrentClient.SellCell(CurrentClient.GetBoard()[CellToSellIndex].OnDisplay());
            }
        }

        private static void UpdateExpectedMoneyFlow(List<MonopolyService> Clients, int turn, int clientIndex)
        {
            int CellIndex = (turn + 1) % Clients[0].GetBoard().Count;
            if ((int)(Clients[0].GetBoard()[CellIndex].GetBuyingBehavior().GetOwner()) < clientIndex)
            {
                PlayersMoneyFlow[clientIndex].Loss += Clients[0].GetBoard()[CellIndex].GetBuyingBehavior().GetCosts().Stay;
                PlayersMoneyFlow[(int)(Clients[0].GetBoard()[CellIndex].GetBuyingBehavior().GetOwner())].Income +=
                    Clients[0].GetBoard()[CellIndex].GetBuyingBehavior().GetCosts().Stay;
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

        public static string FindStringBuyingCellFrom(List<string> Options)
        {
            string? Result = Options.FirstOrDefault(option => option == Consts.Monopoly.BeachBuyAccepted ||
                                                    option == Consts.Monopoly.Field);

            if (Result == null)
                return "";

            return Result;
        }

        
        
    }
}
