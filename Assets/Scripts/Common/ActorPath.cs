using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPath : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigidbody2D;

    Vector3 preVelocity = Vector3.zero;

    void Awake ( )
    {
        rigidbody2D = GetComponentInParent<Rigidbody2D> ( );
    }


    /// <returns>다음 적용될 속도</returns>
    public Vector3 Move(Vector3 dir, float speed, bool back = false)
    {
        preVelocity = dir * speed;
        return preVelocity;
    }

    void FixedUpdate ( )
    {
        // [TODO] 이동식
        rigidbody2D.velocity = preVelocity;
        preVelocity = Vector3.zero;
    }
}
