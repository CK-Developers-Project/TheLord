using Developers.Util;
using Developers.Table;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System;

public class TableManager : MonoSingleton<TableManager>
{
    [SerializeField] TextAsset characterTable;
    [SerializeField] TextAsset abilityTable;
    [SerializeField] TextAsset buildingTable;

    public CharacterTable CharacterTable { get; private set; }
    public AbilityTable AbilityTable { get; private set; }
    public BuildingTable BuildingTable { get; private set; }

    public bool isLoad = false; 

    public void Load(Action action)
    {
        var workLoad = AsyncLoad ( ).GetAwaiter ( );

        workLoad.OnCompleted ( ( ) =>
        {
            action?.Invoke ( );
        } );
    }

    async Task AsyncLoad ()
    {
        Func<bool> func = ( ) =>
        {
            CharacterTable = new CharacterTable ( characterTable );
            AbilityTable = new AbilityTable ( abilityTable );
            BuildingTable = new BuildingTable ( buildingTable );
            return true;
        };

        isLoad = await Task.FromResult ( func() );
    }
}
