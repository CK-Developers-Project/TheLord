using Developers.Structure;
using Developers.Structure.Data;
using UnityEngine;
using System.Collections;

[CreateAssetMenu ( fileName = "AttackAbilityData", menuName = "ScriptableObjects/Abilitys/AttackAbilityData" )]
public class AttackAbilityData : AbilityData
{
    public override bool OnStart ( BaseCharacter owner, AbilityInfo info, IActor target )
    {
        owner.StartCoroutine ( Attack ( owner, info, target ) );
        return true;
    }

    IEnumerator Attack ( BaseCharacter owner, AbilityInfo info, IActor target )
    {
        BaseCharacter targetCharacter = target as BaseCharacter;
        info.cooltime = owner.status.Get ( ActorStatus.AtkCoolTime, true, true, true );

        owner.Order = order;
        owner.SetAnim ( );
        owner.OnAttack ( targetCharacter );
        owner.Anim_Event = false;
        while ( owner.Anim_Event == false )
        {
            if ( owner.Order != order || owner.IsDeath )
            {
                break;
            }
            yield return null;
        }

        if ( targetCharacter == null || targetCharacter.IsDeath )
        {
            owner.StartCoroutine ( info.UpdateCoolTime ( ) );
            yield break;
        }

        Vector2 forward = new Vector3 ( owner.Forward, 0F );
        float delta = Mathf.Cos ( 90F * Mathf.Deg2Rad );
        Vector2 dir = targetCharacter.Center - owner.Center;
        float dot = Vector2.Dot ( forward, dir.normalized );
        if ( dot > delta )
        {
            DamageCalculator.DamageInfo damageInfo = new DamageCalculator.DamageInfo ( DamageCalculator.DamageType.Normal );
            damageInfo.damage = (int)owner.status.Get ( ActorStatus.Atk, true, true, true );
            owner.damageCalculator.Damaged ( targetCharacter, damageInfo );
        }

        if ( owner.Order == order || owner.Order == AbilityOrder.Idle )
        {
            owner.Order = AbilityOrder.Idle;
            owner.AddAnim ( );
        }

        owner.StartCoroutine ( info.UpdateCoolTime ( ) );
    }
}