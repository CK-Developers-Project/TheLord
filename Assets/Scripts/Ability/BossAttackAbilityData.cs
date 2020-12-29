using Developers.Structure.Data;
using Developers.Structure;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu ( fileName = "BossAttackAbilityData", menuName = "ScriptableObjects/Abilitys/BossAttackAbilityData" )]
public class BossAttackAbilityData : AbilityData
{
    const int MAX_COUNT = 30;

    public override bool Add ( AbilityCaster caster )
    {
        if( base.Add ( caster ) == false)
        {
            return false;
        }

        caster.Owner.Damaged += Owner_Damaged;
        return true;
    }

    private void Owner_Damaged ( BaseCharacter source, BaseCharacter target, DamageCalculator.DamageInfo info )
    {
        if(info.type != DamageCalculator.DamageType.Normal)
        {
            return;
        }

        var abilityInfo = source.caster.Get ( index );
        if(abilityInfo.isUse)
        {
            return;
        }
        abilityInfo.isUse = true;

        int cnt = abilityInfo.amount[0];

        LayerMask layerMask = GameLayerHelper.Layer ( GameLayer.Actor );
        var col = new Collider2D[MAX_COUNT];
        var actors = Physics2D.OverlapCircleNonAlloc ( target.Position, Range, col, layerMask );
        List<BaseCharacter> targets = new List<BaseCharacter> ( );
        for(int i = 0; i < actors; ++i )
        {
            BaseCharacter character = col[i].GetComponentInParent<BaseCharacter> ( );
            if(character == null || character.Equals(source) || character.Equals(target) || source.Owner.IsAlliance(character.Owner) || character.IsDeath )
            {
                continue;
            }
            targets.Insert ( Random.Range(0, targets.Count + 1), character );
        }

        GameObject vfx = LoadManager.Instance.GetVFX ( VFXType.BossSkill );

        foreach ( var t in targets)
        {
            Instantiate ( vfx, t.Position, Quaternion.identity );
            source.damageCalculator.Damaged ( t, info );
            if(--cnt == 0 )
            {
                break;
            }
        }
        abilityInfo.isUse = false;
    }
}