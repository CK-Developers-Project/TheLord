using System.Collections.Generic;
using UnityEngine;

namespace Developers.Table
{
    public class AbilityTable : BaseTable
    {
        public List<Dictionary<string, object>> AbilityInfoSheet { get; private set; }

        public AbilityTable ( TextAsset asset ) : base ( asset )
        {
            AbilityInfoSheet = table["AbilityInfo"];
        }
    }
}
