using Developers.Util;
using Developers.Table;
using UnityEngine;
using System.Threading.Tasks;

public class TableManager : MonoSingleton<TableManager>
{
    [SerializeField] TextAsset characterTable;
    [SerializeField] TextAsset abilityTable;
    [SerializeField] TextAsset buildingTable;

    public CharacterTable CharacterTable { get; private set; }
    public AbilityTable AbilityTable { get; private set; }
    public BuildingTable BuildingTable { get; private set; }





    public void Load()
    {

    }



    protected override void Awake ( )
    {
        base.Awake ( );

        if ( instance == this )
        {
            CharacterTable = new CharacterTable ( characterTable );
            AbilityTable = new AbilityTable ( abilityTable );
            BuildingTable = new BuildingTable ( buildingTable );
        }
    }

}
