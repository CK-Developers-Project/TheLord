using UnityEngine;
using Developers.Structure;

public class ElfArcher : BaseCharacter
{
    public override bool LookAtRight {
        get
        {
            return spineMeca.initialFlipX;
        }
        set
        {
            spineMeca.initialFlipX = value;
        }
    }
}