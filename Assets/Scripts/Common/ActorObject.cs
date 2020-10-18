using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorObject : MonoBehaviour
{
    public Animator Animator { get; protected set; }
    public bool isAnim;

    private void Awake ( )
    {
        Animator = GetComponent<Animator> ( );
    }

    protected void OnEnable ( )
    {
        if ( Animator )
        {
            ActorAnimation.Initialize ( Animator, this );
        }
    }
}
