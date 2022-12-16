using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public class MonopolyCell // Make this class an interface to get the job done right
    {
        public PlayerKey OwnedBy { get; set; }

        public Costs MoneyNeededFor { get; set; }

        public Nation OfNation { get; set; }

        public MonopolyCell(Costs costs = null, PlayerKey owner = PlayerKey.NoOne, Nation nation = Nation.NoNation)
        {
            MoneyNeededFor = costs;
            OfNation = nation;
            OwnedBy = owner;
        }

        public string OnCellDisplay()
        {
            string result = "";
            result += $" Owner: {OwnedBy.ToString()} |";
            result += $" Nation: {OfNation.ToString()} |";
            result += $" Buy For: {MoneyNeededFor.Buy} |";
            result += $" Stay Cost: {MoneyNeededFor.Stay} ";
            return result;
        }

    }
}
