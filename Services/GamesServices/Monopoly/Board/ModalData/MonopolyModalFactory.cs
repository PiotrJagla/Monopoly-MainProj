using Enums.Monopoly;
using Models;
using Models.Monopoly;
using Services.GamesServices.Monopoly.Board.Cells;
using StringManipulationLib;
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

        public static MonopolyModalParameters ChooseCellToSell(DataToGetModalParameters Data, int StayCost)
        {
            StringModalParameters Parameterss = new StringModalParameters();
            int Moneyhh = StayCost - Data.MainPlayer.MoneyOwned;
            Parameterss.Title = $"What Cell Do You Wanna sell |You dont have {Moneyhh}";

            foreach (var cell in Data.Board)
            {
                if (cell.GetBuyingBehavior().GetOwner() == Data.MainPlayer.Key)
                {
                    Parameterss.ButtonsContent.Add(
                        GetCellSellingString(cell)
                    );
                }
            }

            if (Parameterss.ButtonsContent.Count == 0)
                return NoModalParameters();

            return new MonopolyModalParameters(Parameterss, ModalShow.AfterMove);
        }

        public static string GetCellSellingString(MonopolyCell cell)
        {
            return $"{Consts.Monopoly.SellCellPrefix}/" +
                   $"{cell.OnDisplay()}/" +
                   $" SellFor:{cell.GetBuyingBehavior().GetCosts().Buy}";
        }

        public static bool DoHaveToSellCell(MonopolyPlayer MainPlayer, int StayCost, PlayerKey CellOwner)
        {
            return MainPlayer.MoneyOwned < StayCost &&
                CellOwner != PlayerKey.NoOne &&
                CellOwner != MainPlayer.Key;
        }


        public static ModalResponseUpdate OnModalBuyableCellResponse(ModalResponseData Data)
        {
            if (Data.ModalResponse.Contains(Consts.Monopoly.SellCellPrefix))
                return OnModalCellSoldResponse(Data);
            else
                return OnModalCellBoughtResponse(Data);
        }

        private static ModalResponseUpdate OnModalCellBoughtResponse(ModalResponseData Data)
        {
            ModalResponseUpdate UpdatedData = new ModalResponseUpdate();
            UpdatedData.BoardService = Data.BoardService;
            UpdatedData.PlayersService = Data.PlayersService;

            MonopolyPlayer MainPlayer = UpdatedData.PlayersService.GetMainPlayer();
            int BuyCost = UpdatedData.BoardService.BuyCell(MainPlayer, Data.ModalResponse);
            UpdatedData.PlayersService.ChargeMainPlayer(BuyCost);
            return UpdatedData;

        }

        private static ModalResponseUpdate OnModalCellSoldResponse(ModalResponseData Data)
        {
            ModalResponseUpdate UpdatedData = new ModalResponseUpdate();
            UpdatedData.BoardService = Data.BoardService;
            UpdatedData.PlayersService = Data.PlayersService;

            char Separator = Data.ModalResponse.ElementAt(Consts.Monopoly.SellCellPrefix.Length);
            
            string CellDisplay = StringLib.GetStringsSeparatedBy(Separator,Data.ModalResponse)[1];
            
            int ReturnAmount = UpdatedData.BoardService.SellCell(CellDisplay);
            UpdatedData.PlayersService.GiveMainPlayerMoney(ReturnAmount);
            return UpdatedData;
        }

        public static List<string> GetPossibleNationCellEnhancments()
        {
            List<string> PossibleEnhanceBuildings = new List<string>();
            PossibleEnhanceBuildings.Add(Consts.Monopoly.OneHouse);
            PossibleEnhanceBuildings.Add(Consts.Monopoly.TwoHouses);
            PossibleEnhanceBuildings.Add(Consts.Monopoly.ThreeHouses);
            PossibleEnhanceBuildings.Add(Consts.Monopoly.Hotel);
            return PossibleEnhanceBuildings;
        }
    }
}
