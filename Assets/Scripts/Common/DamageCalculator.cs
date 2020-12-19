using System;
using System.Collections.Generic;
using Developers.Structure;
using UnityEngine;

public class DamageCalculator
{
    public enum FilterType
    {
        All,
        Alliance,
        Enemy,
    }

    public class DamageInfo
    {
        public float damage = 0f;
        public float trueDamage = 0f;

        public float armor = 0f;
    }


    public BaseCharacter Owner { get; set; }


    public float Formula(DamageInfo info)
    {
        float amount = info.armor - info.damage;
        if(amount < 1F)
        {
            amount = 1F;
        }
        amount += info.trueDamage;

        return amount;
    }

    public List<BaseCharacter> AttackRange(float range, FilterType filter)
    {
        LayerMask layer = GameLayerHelper.Layer ( GameLayer.Actor );
        var colliders = Physics2D.OverlapCircleAll ( Owner.ACollider.transform.position, range, layer );

        List<BaseCharacter> targets = new List<BaseCharacter> ( );
        foreach(var col in colliders)
        {
            BaseCharacter character = col.transform.GetComponent<BaseCharacter> ( );
            if(character == null || Owner.Equals(character) || targets.Contains(character))
            {
                continue;
            }

            switch ( filter )
            {
                case FilterType.Alliance:
                    if(Owner.Owner.IsEnemy(character.Owner))
                    {
                        continue;
                    }
                    break;
                case FilterType.Enemy:
                    if ( Owner.Owner.IsAlliance ( character.Owner ) )
                    {
                        continue;
                    }
                    break;
            }

            targets.Add ( character );
        }
        return targets;
    }


    public void Damaged ( BaseCharacter target, DamageInfo info )
    {
        Owner.OnDamaged ( Owner, target, info );
        float amount = Formula ( info );
        Owner.Hp -= amount;
    }
}