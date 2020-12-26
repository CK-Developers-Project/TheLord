using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;


public class TheDevilAI : CharacterAI
{
    const float FIND_ENEMY_TICK = 0.1F;
    const int FIND_ENEMY_MAX = 50;

    List<BaseCharacter> holdCharacters = new List<BaseCharacter> ( );


    IEnumerator FindEnemy ( )
    {
        while ( pawn.IsDeath == false )
        {
            var col = new Collider2D[FIND_ENEMY_MAX];
            LayerMask layerMask = GameLayerHelper.Layer ( GameLayer.Actor );
            var actors = Physics2D.OverlapCircleNonAlloc ( pawn.Center, AttackDistance, col, layerMask );
            holdCharacters.Clear ( );
            for ( int i = 0; i < actors; ++i )
            {
                BaseCharacter character = col[i].transform.GetComponentInParent<BaseCharacter> ( );
                if ( character == null || character.Equals ( pawn ) )
                {
                    continue;
                }

                holdCharacters.Add ( character );
            }
            yield return new WaitForSeconds ( FIND_ENEMY_TICK );
        }
    }


    protected override IEnumerator Construct ( )
    {
        yield return base.Construct ( );
        StartCoroutine ( FindEnemy ( ) );
    }


    void Update ( )
    {
        if ( pawn == null || pawn.IsDeath )
        {
            return;
        }

        BaseCharacter obstacle = null;
        foreach ( var character in holdCharacters )
        {
            if ( pawn.Owner.IsEnemy ( character.Owner ) )
            {
                obstacle = character;
                break;
            }
        }

        if ( obstacle != null )
        {
            Vector2 dir = obstacle.Position - pawn.Position;
            pawn.LookAtRight = dir.x >= 0 ? true : false;
            SetOrder ( AbilityOrder.Attack, obstacle );
        }
    }
}