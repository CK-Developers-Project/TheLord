using UnityEngine;
using Developers.Structure;

public class ElfBard : BaseCharacter
{
    public override bool LookAtRight {
        get
        {
            return spineMeca.initialFlipX == false;
        }
        set
        {
            spineMeca.initialFlipX = !value;
        }
    }


}