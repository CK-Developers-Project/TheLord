using Developers.Structure.Data;
using Developers.Structure;
using UnityEngine;
using System.Collections;

[CreateAssetMenu ( fileName = "BlessingAbilityData", menuName = "ScriptableObjects/Abilitys/BlessingAbilityData" )]
public class BlessingAbilityData : AbilityData
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

        int attackCnt = abilityInfo.amount[2];
        if ( ++abilityInfo.usableAmount[0] < attackCnt )
        {
            abilityInfo.isUse = false;
            return;
        }
        else
        {
            abilityInfo.usableAmount[0] = 0;
        }

        source.StartCoroutine ( Blessing ( source, abilityInfo ) );
    }

    IEnumerator Blessing ( BaseCharacter owner, AbilityInfo info )
    {
        if ( info.isUse )
        {
            yield break;
        }
        owner.Audio.play ( LoadManager.Instance.GetSFXData ( SFXType.HumanSkill ).clip, 1F, 0F, 1F );
        GameObject vfx = LoadManager.Instance.GetVFX ( VFXType.HumanSkill );
        Instantiate ( vfx, owner.Actor.transform ).transform.localPosition = Vector3.zero;

        float atk = owner.status.Get ( ActorStatus.Atk, true, true );
        float rateAtk = info.amount[0] * 0.01F;
        float resultAtk = atk * rateAtk;

        float def = owner.status.Get ( ActorStatus.Def, true, true );
        float rateDef = info.amount[1] * 0.01F;
        float resultDef = def * rateDef;

        // TODO : 버프 클래스 만들어야 할듯?
        owner.status.Multiplicative.Set ( ActorStatus.Atk, resultAtk );
        owner.status.Multiplicative.Set ( ActorStatus.Def, resultDef );

        float dur = info.Duration;
        float timer = 0F;
        while ( timer < dur )
        {
            timer += Time.deltaTime;
            yield return null;
        }

        owner.status.Multiplicative.Set ( ActorStatus.Atk, -resultAtk );
        owner.status.Multiplicative.Set ( ActorStatus.Def, -resultDef );
    }
}