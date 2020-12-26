using Developers.Structure.Data;
using Developers.Structure;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu ( fileName = "BossSkillAbilityData", menuName = "ScriptableObjects/Abilitys/BossSkillAbilityData" )]
public class BossSkillAbilityData : AbilityData
{
    const int MAX_COUNT = 30;

    public override bool OnStart ( BaseCharacter owner, AbilityInfo info, Vector3 position )
    {
        owner.StartCoroutine ( Spell ( owner, info, position ) );
        return true;
    }

    public override bool Add ( AbilityCaster caster )
    {
        if ( base.Add ( caster ) == false )
        {
            return false;
        }
        caster.Get ( index ).usableAmount.Add ( 0 );
        caster.Owner.Attack += Owner_Attack;
        return true;
    }

   
    IEnumerator Spell( BaseCharacter owner, AbilityInfo info, Vector3 position )
    {
        owner.caster.abilityCast = true;
        info.isUse = true;

        owner.Order = order;
        owner.SetAnim ( );
        owner.Anim_Event = false;
        while ( owner.Anim_Event == false )
        {
            if ( owner.Order != order || owner.IsDeath )
            {
                break;
            }
            yield return null;
        }

        int cnt = info.amount[0];

        LayerMask layerMask = GameLayerHelper.Layer ( GameLayer.Actor );
        var col = new Collider2D[MAX_COUNT];
        var actors = Physics2D.OverlapCircleNonAlloc ( position, Range, col, layerMask );
        List<BaseCharacter> targets = new List<BaseCharacter> ( );
        for ( int i = 0; i < actors; ++i )
        {
            BaseCharacter character = col[i].GetComponentInParent<BaseCharacter> ( );
            if ( character == null || character.Equals ( owner ) || owner.Owner.IsAlliance ( character.Owner ) )
            {
                continue;
            }
            targets.Insert ( Random.Range ( 0, targets.Count + 1 ), character );
        }

        foreach ( var t in targets )
        {
            DamageCalculator.DamageInfo damageInfo = new DamageCalculator.DamageInfo ( DamageCalculator.DamageType.Magic );
            damageInfo.damage = (int)owner.status.Get ( ActorStatus.Atk, true, true, true );
            owner.damageCalculator.Damaged ( t, damageInfo );
            if ( --cnt == 0 )
            {
                break;
            }
        }

        if ( owner.Order == order || owner.Order == AbilityOrder.Idle )
        {
            owner.Order = AbilityOrder.Idle;
            owner.AddAnim ( );
        }

        info.isUse = false;
        owner.caster.abilityCast = false;
    }


    void Owner_Attack ( BaseCharacter source, BaseCharacter target )
    {
        var abilityInfo = source.caster.Get ( index );

        int attackCnt = abilityInfo.amount[1];
        if ( ++abilityInfo.usableAmount[0] < attackCnt )
        {
            abilityInfo.isUse = false;
            return;
        }
        else
        {
            source.caster.OnStart ( AbilityOrder.BossSkill, target.Position );
            abilityInfo.usableAmount[0] = 0;
        }
    }
}