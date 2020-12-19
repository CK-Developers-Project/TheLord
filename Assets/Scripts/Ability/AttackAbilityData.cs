using Developers.Structure;
using Developers.Structure.Data;
using UnityEngine;
using System.Collections;

[CreateAssetMenu ( fileName = "AttackAbilityData", menuName = "ScriptableObjects/Abilitys/AttackAbilityData" )]
public class AttackAbilityData : AbilityData
{
    public override bool OnStart ( BaseCharacter owner, AbilityInfo info )
    {
        owner.StartCoroutine ( Attack ( owner, info ) );
        return true;
    }

    IEnumerator Attack( BaseCharacter owner, AbilityInfo info )
    {
        info.cooltime = owner.status.Get ( ActorStatus.AtkCoolTime, true, true, true );

        owner.Order = order;
        owner.SetAnim ( );

        owner.Anim_Event = false;
        while ( owner.Anim_Event == false)
        {
            if(owner.Order != order || owner.IsDeath)
            {
                break;
            }
            yield return null;
        }

        float range = owner.status.Get ( ActorStatus.Distance, true, true, true );
        var targets = owner.damageCalculator.AttackRange ( range, DamageCalculator.FilterType.Enemy );
        Vector3 sPos = owner.ACollider.transform.position;
        Vector3 foward = new Vector3 ( owner.Forward, 0F );
        float delta = Mathf.Cos ( 90F * Mathf.Deg2Rad );
        
        foreach (var target in targets)
        {
            Vector3 tPos = target.ACollider.transform.position;
            Vector3 dir = tPos - sPos;
            float dot = Vector3.Dot ( foward, dir.normalized );

            if(dot > delta )
            {
                DamageCalculator.DamageInfo damageInfo = new DamageCalculator.DamageInfo ( );
                damageInfo.damage = owner.status.Get ( ActorStatus.Atk, true, true, true );
                owner.damageCalculator.Damaged ( target, damageInfo );
                break;
            }
        }

        owner.Order = AbilityOrder.Idle;
        owner.AddAnim ( );

        info.isUse = false;
        owner.StartCoroutine ( info.UpdateCoolTime ( ) );
    }
}