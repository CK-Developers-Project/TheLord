using UnityEngine;
using Developers.Structure;

public class TheDevil : BaseCharacter
{
    SpriteRenderer spriteRenderer = null;

    public override bool LookAtRight { 
        get => spriteRenderer.flipX; 
        set => spriteRenderer.flipX = value;
    }


    protected override void Awake ( )
    {
        base.Awake ( );
        spriteRenderer = Actor.GetComponent<SpriteRenderer> ( );
    }
}