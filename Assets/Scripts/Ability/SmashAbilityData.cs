using Developers.Structure.Data;
using Developers.Structure;
using UnityEngine;

[CreateAssetMenu ( fileName = "SmashAbilityData", menuName = "ScriptableObjects/Abilitys/SmashAbilityData" )]
public class SmashAbilityData : AbilityData
{
    public override bool Add ( AbilityCaster caster )
    {
        if ( base.Add ( caster ) == false )
        {
            return false;
        }

        caster.Get ( index ).usableAmount.Add ( 0 );
        caster.Owner.Damaged += Owner_Damaged;
        return true;
    }

    void Owner_Damaged ( BaseCharacter source, BaseCharacter target, DamageCalculator.DamageInfo info )
    {
        if ( info.type != DamageCalculator.DamageType.Normal )
        {
            return;
        }

        var abilityInfo = source.caster.Get ( index );
        if ( abilityInfo.isUse )
        {
            return;
        }
        abilityInfo.isUse = true;

        int attackCnt = abilityInfo.amount[1];
        if ( ++abilityInfo.usableAmount[0] < attackCnt )
        {
            abilityInfo.isUse = false;
            return;
        }
        else
        {
            abilityInfo.usableAmount[0] = 0;
        }

        float atk = info.damage * (abilityInfo.amount[0] * 0.01f);
        info.trueDamage += atk;

        source.Audio.play ( LoadManager.Instance.GetSFXData ( SFXType.UndeadSkill ).clip, 1F, 0F, 1F );
        // TODO 이펙트 추가

        abilityInfo.isUse = false;
    }
}