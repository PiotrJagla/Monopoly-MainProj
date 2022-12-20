using Enums.Monopoly;
using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board
{
    public class MonopolyNationCell : MonopolyCell
    {
        public PlayerKey OwnedBy { get; set; }

        public Costs MoneyNeededFor { get; set; }

        public Nation OfNation { get; set; }


        public MonopolyNationCell(Costs costs = null, PlayerKey owner = PlayerKey.NoOne, Nation nation = Nation.NoNation)
        {
            MoneyNeededFor = costs;
            OfNation = nation;
            OwnedBy = owner;
        }

        public Nation GetNation()
        {
            return OfNation;
        }

        public PlayerKey GetOwner()
        {
            return OwnedBy;
        }

        public Costs GetCosts()
        {
            return MoneyNeededFor;
        }

        public string OnDisplay()
        {
            string result = "";
            result += $" Owner: {OwnedBy.ToString()} |";
            result += $" Nation: {OfNation.ToString()} |";
            result += $" Buy For: {MoneyNeededFor.Buy} |";
            result += $" Stay Cost: {MoneyNeededFor.Stay} ";
            return result;
        }

        public void SetOwner(PlayerKey NewOwner)
        {
            OwnedBy = NewOwner;
        }
    }
}
