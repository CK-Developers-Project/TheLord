using Developers.Structure.Data;
using Developers.Structure;
using UnityEngine;
using System.Collections;

[CreateAssetMenu ( fileName = "MoveAbilityData", menuName = "ScriptableObjects/Abilitys/MoveAbilityData" )]
public class MoveAbilityData : AbilityData
{

    public override bool OnStart ( BaseCharacter owner, AbilityInfo info, Vector3 position )
    {
        owner.StartCoroutine ( Move2Position ( owner, info, position ) );
        return true;
    }


    IEnumerator Move2Position( BaseCharacter owner, AbilityInfo info, Vector3 position)
    {
        owner.OrderPosition = position;
        if (owner.Order == order)
        {
            yield break;
        }

        owner.Order = order;
        float distance = Vector2.Distance(owner.Position, owner.OrderPosition );
        while ( owner.Radius < distance)
        {
            if(owner.Order != order)
            {
                break;
            }
            Vector3 dir = owner.OrderPosition - owner.Position;
            owner.SetAction ( Move, dir );
            distance = Vector2.Distance ( owner.Position, owner.OrderPosition );
            yield return new WaitForFixedUpdate();
        }

        info.isUse = false;
        owner.Order = AbilityOrder.Idle;
        owner.SetAnim ( );
    }

    void Move(BaseCharacter owner, Vector3 dir)
    {
        float speed = owner.status.Get ( ActorStatus.Speed, true, true, true );
        owner.LookAtRight = dir.x >= 0 ? true : false;
        owner.Path.Move ( dir, speed );
    }
}