﻿using System;
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

    const int OVERLAP_MAX_ACTOR = 24;

    public BaseCharacter Owner { get; set; }


    public float Formula(DamageInfo info)
    {
        float amount = info.damage - info.armor;
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
        var col = new Collider2D[OVERLAP_MAX_ACTOR];
        var actors = Physics2D.OverlapCircleNonAlloc ( Owner.Center, range, col, layer );

        List<BaseCharacter> targets = new List<BaseCharacter> ( );
        for ( int i = 0; i < actors; ++i )
        {
            BaseCharacter character = col[i].transform.GetComponentInParent<BaseCharacter> ( );
            if ( character == null || character.Equals ( Owner ) )
            {
                continue;
            }

            switch ( filter )
            {
                case FilterType.Alliance:
                    if ( Owner.Owner.IsEnemy ( character.Owner ) )
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
        target.OnDamaged ( Owner, target, info );
        float amount = Formula ( info );
        target.Hp -= amount;
    }
}