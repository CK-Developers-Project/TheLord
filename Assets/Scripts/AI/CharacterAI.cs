using System.Collections;
using UnityEngine;
using Developers.Structure;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent]
public class CharacterAI : MonoBehaviour, IAIFactory
{
    protected BaseCharacter pawn;

    public IActor Actor { get => pawn; }

    public float AttackDistance {
        get
        {
            return pawn.status.Get ( ActorStatus.Distance, true, true, true );
        }
    }

#if UNITY_EDITOR
    [Header ( "Display HP" )]
    public bool displayHP = false;
    [Header ( "Display Range" )]
    public bool displayDistance = false;
#endif


    public bool SetOrder ( AbilityOrder order )
    {
        return pawn.caster.OnStart ( order );
    }

    public bool SetOrder ( AbilityOrder order, IActor target )
    {
        return pawn.caster.OnStart ( order, target );
    }

    public bool SetOrder ( AbilityOrder order, Vector3 position )
    {
        return pawn.caster.OnStart ( order, position );
    }

    protected virtual IEnumerator Construct()
    {
        yield return new WaitUntil ( ( ) => pawn.Initialized );
    }

    protected virtual void Awake ( )
    {
        pawn = GetComponentInParent<BaseCharacter> ( );
        pawn.AI = this;
    }

    void OnEnable ( )
    {
        StartCoroutine ( Construct ( ) );
    }


#if UNITY_EDITOR
    void OnDrawGizmos ( )
    {
        /*
        //draw the cone of view
            Vector3 forward = spriteFaceLeft ? Vector2.left : Vector2.right;
            forward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * forward;

            if (GetComponent<SpriteRenderer>().flipX) forward.x = -forward.x;

            Vector3 endpoint = transform.position + (Quaternion.Euler(0, 0, viewFov * 0.5f) * forward);

            Handles.color = new Color(0, 1.0f, 0, 0.2f);
            Handles.DrawSolidArc(transform.position, -Vector3.forward, (endpoint - transform.position).normalized, viewFov, viewDistance);

            //Draw attack range
            Handles.color = new Color(1.0f, 0,0, 0.1f);
            Handles.DrawSolidDisc(transform.position, Vector3.back, meleeRange); 
        */

        //Vector3 forward = new Vector3 ( pawn.Forward, 0F, 0F );

        if(pawn == null)
        {
            return;
        }

        if(displayHP)
        {
            float height = pawn.ACollider.transform.localPosition.y;
            Vector2 pos = pawn.Center + new Vector3 ( 0F, height );
            var hp = string.Format ( "{0}/{1}", (int)pawn.Hp, (int)pawn.status.Get ( ActorStatus.HP, true, true, true ) );
            GUIStyle style = new GUIStyle ( );
            style.fontSize = 18;
            style.normal.textColor = Color.red;
            Handles.Label ( pos, hp, style );
        }

        if( displayDistance )
        {
            Handles.color = new Color ( 0.0F, 1.0F, 0.0F, 0.2F );
            Handles.DrawSolidDisc ( pawn.Center, Vector3.back, AttackDistance );
        }
    }
#endif

}
