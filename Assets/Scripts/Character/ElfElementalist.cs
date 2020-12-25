using UnityEngine;
using Developers.Structure;

public class ElfElementalist : BaseCharacter
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