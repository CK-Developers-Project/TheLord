using System.Collections.Generic;
using UnityEngine;

namespace Developers.Table
{
    public class CharacterTable : BaseTable
    {
        public List<Dictionary<string, object>> CharacterInfoSheet { get; private set; }

        public CharacterTable ( TextAsset asset ) : base ( asset )
        {
            CharacterInfoSheet = table["CharacterInfo"];
        }
    }
}