using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public interface MonopolyCell 
    {
        Nation GetNation();

        PlayerKey GetOwner();
        void SetOwner(PlayerKey NewOwner);

        Costs GetCosts();
        void SetCosts(Costs costs);
        List<MonopolyCell> MonopolChanges(in List<MonopolyCell> Board);

        string OnDisplay();
    }
}
