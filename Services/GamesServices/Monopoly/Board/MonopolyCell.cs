using Enums.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Monopoly
{
    public interface MonopolyCell // Make this class an interface to get the job done right
    {
        Nation GetNation();


        PlayerKey GetOwner();
        void SetOwner(PlayerKey NewOwner);

        Costs GetCosts();
        void SetCosts(Costs costs);

        string OnDisplay();
    }
}
