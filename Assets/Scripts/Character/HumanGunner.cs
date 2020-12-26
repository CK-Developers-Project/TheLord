using UnityEngine;
using Developers.Structure;

public class HumanGunner : BaseCharacter
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
}