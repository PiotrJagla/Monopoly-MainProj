using Models;
using Models.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.GamesServices.Monopoly.Board.Cells;

public class NationCellCostsFactory
{
    public static Dictionary<string,Costs> GetNationCellCostsOfNumber(int number)
    {
        List<Tuple<Costs,Costs,Costs,Costs,Costs>> NationCellsCosts = new List<Tuple<Costs, Costs, Costs, Costs, Costs>>();
        
        //Poland
        NationCellsCosts.Add(new Tuple<Costs, Costs, Costs, Costs, Costs>(
            new Costs(50,40), new Costs(60,35), new Costs(80,50), new Costs(100, 60), new Costs(130, 90)));
        NationCellsCosts.Add(new Tuple<Costs, Costs, Costs, Costs,Costs>(
            new Costs(60,50), new Costs(75,55), new Costs(80,60), new Costs(110, 65), new Costs(140, 100)));
                                                          
        //France                                          
        NationCellsCosts.Add(new Tuple<Costs, Costs, Costs, Costs, Costs>(
            new Costs(70,65), new Costs(80,60), new Costs(100,80), new Costs(120, 70), new Costs(140, 120)));
        NationCellsCosts.Add(new Tuple<Costs, Costs, Costs, Costs, Costs>(
            new Costs(85,75), new Costs(80,60), new Costs(85,70), new Costs(115, 80), new Costs(145, 110)));
        NationCellsCosts.Add(new Tuple<Costs, Costs, Costs, Costs, Costs>(
            new Costs(90,85), new Costs(100,85), new Costs(120,100), new Costs(130, 110), new Costs(155, 120)));
                                                          
        //Argentina                                       
        NationCellsCosts.Add(new Tuple<Costs, Costs, Costs, Costs, Costs>(
            new Costs(110, 100), new Costs(110, 95), new Costs(150, 130), new Costs(160, 135), new Costs(180, 150)));
        NationCellsCosts.Add(new Tuple<Costs, Costs, Costs, Costs, Costs>(
            new Costs(125, 95), new Costs(120, 95), new Costs(130, 110), new Costs(150, 120), new Costs(170, 130)));
        NationCellsCosts.Add(new Tuple<Costs, Costs, Costs, Costs, Costs>(
            new Costs(140, 130), new Costs(155, 135), new Costs(180, 160), new Costs(200, 170), new Costs(250, 220)));

        Dictionary<string, Costs> BuildingTypeToCostsMap = new Dictionary<string, Costs>();
        BuildingTypeToCostsMap.Add(
            Consts.Monopoly.Field, NationCellsCosts[number].Item1
        );

        BuildingTypeToCostsMap.Add(
            Consts.Monopoly.OneHouse, NationCellsCosts[number].Item2
        );

        BuildingTypeToCostsMap.Add(
            Consts.Monopoly.TwoHouses, NationCellsCosts[number].Item3
        );

        BuildingTypeToCostsMap.Add(
            Consts.Monopoly.ThreeHouses, NationCellsCosts[number].Item4
        );

        BuildingTypeToCostsMap.Add(
            Consts.Monopoly.Hotel, NationCellsCosts[number].Item5
        );

        return BuildingTypeToCostsMap;
    }
}
