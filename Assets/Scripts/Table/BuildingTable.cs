using System.Collections.Generic;
using UnityEngine;

namespace Developers.Table
{
    public class BuildingTable : BaseTable
    {
        public List<Dictionary<string, object>> BuildingInfoSheet { get; private set; }
        public List<Dictionary<string, object>> MainBuildingInfoSheet { get; private set; }
        public List<Dictionary<string, object>> BuildCostSheet { get; private set; }

        public BuildingTable ( TextAsset asset ) : base (asset)
        {
            BuildingInfoSheet = table["BuildingInfo"];
            MainBuildingInfoSheet = table["MainBuildingInfo"];
            BuildCostSheet = table["BuildCost"];
        }
    }
}
