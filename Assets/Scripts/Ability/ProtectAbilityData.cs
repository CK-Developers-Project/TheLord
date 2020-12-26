using Developers.Structure.Data;
using Developers.Structure;
using UnityEngine;
using System.Collections;

[CreateAssetMenu ( fileName = "ProtectAbilityData", menuName = "ScriptableObjects/Abilitys/ProtectAbilityData" )]
public class ProtectAbilityData : AbilityData
{
    public override bool Add ( AbilityCaster caster )
    {
        if( base.Add ( caster ) == false)
        {
            return false;
        }

        caster.Get ( index ).usableAmount.Add ( 0 );
        caster.Owner.Damaged += Owner_Damaged;
        return true;
    }

    void Owner_Damaged ( BaseCharacter source, BaseCharacter target, DamageCalculator.DamageInfo info )
    {
        if ( info.type != DamageCalculator.DamageType.Normal)
        {
            return;
        }

        var abilityInfo = source.caster.Get ( index );

        if(abilityInfo.isUse)
        {
            return;
        }

        int attackCnt = abilityInfo.amount[0];
        if ( ++abilityInfo.usableAmount[0] < attackCnt )
        {
            abilityInfo.isUse = false;
            return;
        }
        else
        {
            abilityInfo.usableAmount[0] = 0;
        }

        source.StartCoroutine ( Protected ( source, abilityInfo ) );
    }

    IEnumerator Protected(BaseCharacter owner, AbilityInfo info)
    {
        if ( info.isUse )
        {
            yield break;
        }
        info.isUse = true;
        owner.Invincible = true;
        float dur = info.Duration;
        float timer = 0F;
        while(timer < dur)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        owner.Invincible = false;
        info.isUse = false;
    }
}