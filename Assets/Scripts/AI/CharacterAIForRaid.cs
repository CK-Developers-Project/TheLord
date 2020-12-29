using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;

public class CharacterAIForRaid : CharacterAI
{
    const float FIND_ENEMY_TICK = 0.135F;
    const int FIND_ENEMY_MAX = 100;

    [SerializeField] List<BaseCharacter> holdCharacters = new List<BaseCharacter> ( );

    public BaseCharacter target;
    public float Target2Dist {
        get
        {
            if(target == null)
            {
                return 0F;
            }
            return Vector2.Distance ( pawn.Position, target.Position );
        }
    }

    public Vector2 Target2Dir { 
        get
        {
            if(target == null)
            {
                return Vector2.zero;
            }

            return target.Position - pawn.Position;
        }
    }

    IEnumerator FindEnemy()
    {
        while(pawn.IsDeath == false)
        {
            var col = new Collider2D[FIND_ENEMY_MAX];
            LayerMask layerMask = GameLayerHelper.Layer ( GameLayer.Actor );
            var actors = Physics2D.OverlapCircleNonAlloc ( pawn.Center, AttackDistance, col, layerMask );
            holdCharacters.Clear ( );
            for (int i = 0; i < actors; ++i )
            {
                BaseCharacter character = col[i].transform.GetComponentInParent<BaseCharacter> ( );
                if(character == null || character.Equals(pawn) || character.IsDeath)
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
        if ( target == null || pawn == null || pawn.IsDeath )
        {
            return;
        }

        BaseCharacter obstacle = null;
        foreach(var character in holdCharacters)
        {
            if(pawn.Owner.IsEnemy(character.Owner) &&  character.IsDeath == false )
            {
                obstacle = character;
                break;
            }
        }

        if( obstacle != null)
        {
            Vector2 dir = obstacle.Position - pawn.Position;
            pawn.LookAtRight = dir.x >= 0 ? true : false;
            SetOrder ( AbilityOrder.Attack, obstacle );
        }
        else if(AttackDistance >= Vector2.Distance(target.Position, pawn.Position))
        {
            Vector2 dir = target.Position - pawn.Position;
            pawn.LookAtRight = dir.x >= 0 ? true : false;
            SetOrder ( AbilityOrder.Attack, target );
        }
        else
        {
            Vector2 point = new Vector2 ( target.Position.x, pawn.Position.y );
            SetOrder ( AbilityOrder.Move, point );
        }
    }
}
