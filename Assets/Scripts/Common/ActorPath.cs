using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPath : MonoBehaviour
{
    [HideInInspector] public new Rigidbody2D rigidbody;


    private void Awake ( )
    {
        rigidbody = GetComponentInParent<Rigidbody2D> ( );
    }


    public Vector3 Move(Vector3 dir, float dist)
    {

        return Vector3.zero;
    }

}
