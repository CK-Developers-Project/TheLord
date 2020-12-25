using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;

public class CharacterAIForRaid : CharacterAI
{
    public BaseCharacter Target { get; set; }

    public float AttackDistance {
        get
        {
            return pawn.status.Get ( ActorStatus.Distance, true, true, true );
        }
    }




    void Update ( )
    {

    }

    void OnDrawGizmos ( )
    {
        Gizmos.color = new Color ( 1f, 0f, 0, 0.5f );
        Gizmos.DrawSphere ( pawn.Center, AttackDistance );
    }
}
