using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Behaviours;
using Services.GamesServices.Monopoly.Board.Behaviours.Buying;
using Services.GamesServices.Monopoly.Board.Behaviours.Monopol;
using Services.GamesServices.Monopoly.Board.ModalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Cells
{
    public class MonopolyBeachCell : MonopolyCell
    {
        private Beach BeachName { get; set; }

        private CellBuyingBehaviour BuyingBehaviour;

        private MonopolBehaviour monopolBehaviour;

        public MonopolyBeachCell(Costs costs, Beach WhatBeach = Beach.NoBeach)
        {
            BeachName = WhatBeach;

            BuyingBehaviour = new CellAbleToBuyBehaviour(costs);
            monopolBehaviour = new MonopolBeachCellBehaviour();
        }

        public MonopolyBeachCell()
        {
        }
        
        public string OnDisplay()
        {
            string result = "";
            result += $" Owner: {BuyingBehaviour.GetOwner().ToString()} |";
            result += $" Beach: {BeachName.ToString()} |";
            result += $" Buy For: {BuyingBehaviour.GetCosts().Buy} |";
            result += $" Stay Cost: {BuyingBehaviour.GetCosts().Stay} ";
            return result;
        }

        public MonopolyModalParameters GetModalParameters(DataToGetModalParameters Data)
        {
            if (Data.MainPlayer.MoneyOwned < BuyingBehaviour.GetCosts().Stay &&
                BuyingBehaviour.GetOwner() != PlayerKey.NoOne && BuyingBehaviour.GetOwner() != Data.MainPlayer.Key)
            {
                StringModalParameters Parameterss = new StringModalParameters();
                int Moneyhh = BuyingBehaviour.GetCosts().Stay - Data.MainPlayer.MoneyOwned;
                Parameterss.Title = $"What Cell Do You Wanna sell |You dont have {Moneyhh}";

                foreach (var cell in Data.Board)
                {
                    if (cell.GetBuyingBehavior().GetOwner() == Data.MainPlayer.Key)
                    {
                        Parameterss.ButtonsContent.Add($"{Consts.Monopoly.SellCellPrefix}{cell.OnDisplay()}");
                    }
                }

                if (Parameterss.ButtonsContent.Count == 0)
                    return MonopolyModalFactory.NoModalParameters();

                return new MonopolyModalParameters(Parameterss, ModalShow.AfterMove);
            }

            if (Data.Board[Data.MainPlayer.OnCellIndex].GetBuyingBehavior().GetOwner() != PlayerKey.NoOne)
                return MonopolyModalFactory.NoModalParameters();

            if (Data.MainPlayer.MoneyOwned < BuyingBehaviour.GetCosts().Buy)
                return MonopolyModalFactory.NoModalParameters();

            StringModalParameters Parameters = new StringModalParameters();

            Parameters.Title = "Do you wany to buy this cell";
            Parameters.ButtonsContent.Add(Consts.Monopoly.BeachBuyDeclined);
            Parameters.ButtonsContent.Add(Consts.Monopoly.BeachBuyAccepted);

            return new MonopolyModalParameters(Parameters, ModalShow.AfterMove);
        }

        public CellBuyingBehaviour GetBuyingBehavior()
        {
            return BuyingBehaviour;
        }



        public int CellBought(MonopolyPlayer MainPlayer, string WhatIsBought,ref List<MonopolyCell> CheckMonopol)
        {
            if(WhatIsBought == Consts.Monopoly.BeachBuyAccepted)
            {
                BuyingBehaviour.SetOwner(MainPlayer.Key);
                CheckMonopol = monopolBehaviour.UpdateBoardMonopol(CheckMonopol, MainPlayer.OnCellIndex);
                return BuyingBehaviour.GetCosts().Buy;
            }

            return 0;
        }

        public void CellSold(ref List<MonopolyCell> MonopolChanges)
        {
            BuyingBehaviour.SetOwner(PlayerKey.NoOne);

            int CellIndex = MonopolChanges.IndexOf(this);

            MonopolChanges = monopolBehaviour.GetMonopolOff(MonopolChanges,CellIndex);
        }

        public ModalResponseUpdate OnModalResponse(ModalResponseData Data)
        {
            ModalResponseUpdate UpdatedData = new ModalResponseUpdate();
            UpdatedData.BoardService = Data.BoardService;
            UpdatedData.PlayersService = Data.PlayersService;

            if (Data.ModalResponse.Contains(Consts.Monopoly.SellCellPrefix))
            {
                string CellDisplay = Data.ModalResponse.Remove(0, Consts.Monopoly.SellCellPrefix.Length);
                int ReturnAmount = UpdatedData.BoardService.SellCell(CellDisplay);
                UpdatedData.PlayersService.GiveMainPlayerMoney(ReturnAmount);
                return UpdatedData;
            }

            MonopolyPlayer MainPlayer = UpdatedData.PlayersService.GetMainPlayer();
            int BuyCost = UpdatedData.BoardService.BuyCell(MainPlayer, Data.ModalResponse);
            UpdatedData.PlayersService.ChargeMainPlayer(BuyCost);

            return UpdatedData;
        }

        public string GetName()
        {
            return BeachName.ToString();
        }

        public void UpdateData(MonopolyCellUpdate UpdatedData)
        {
            BuyingBehaviour.SetOwner(UpdatedData.Owner);
            BuyingBehaviour.UpdateCosts(UpdatedData.NewCosts);
        }
    }
}
