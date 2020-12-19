using Developers.Structure;
using Developers.Structure.Data;
using UnityEngine;

[CreateAssetMenu ( fileName = "StopAbilityData", menuName = "ScriptableObjects/Abilitys/StopAbilityData" )]
public class StopAbilityData : AbilityData
{
    public override bool OnStart ( BaseCharacter owner, AbilityInfo info )
    {
        owner.Order = AbilityOrder.Stop;
        owner.SetAnim ( );
        return false;
    }
}