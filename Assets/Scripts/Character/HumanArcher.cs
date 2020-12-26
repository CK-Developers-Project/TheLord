using UnityEngine;
using Developers.Structure;

public class HumanArcher : BaseCharacter
{
    public override bool LookAtRight {
        get
        {
            return spineMeca.skeleton.ScaleX <= 0f;
        }
        set
        {
            spineMeca.skeleton.ScaleX = value ? -1F : 1F;
        }
    }



    public override void OnSelect ( )
    {
        //caster.OnStart ( AbilityOrder.Move, Position + new Vector3 ( 1F, 0F ) );
    }
}