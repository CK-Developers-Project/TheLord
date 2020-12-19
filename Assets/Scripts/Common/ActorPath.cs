using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPath : MonoBehaviour
{
    [HideInInspector] public new Rigidbody2D rigidbody = null;
    CapsuleCollider2D path;

    Vector3 preVelocity = Vector3.zero;

    public float Radius { get => path.size.x / 2F; }

    void Awake ( )
    {
        rigidbody = GetComponentInParent<Rigidbody2D> ( );
        path = GetComponent<CapsuleCollider2D> ( );
    }


    /// <returns>다음 적용될 속도</returns>
    public Vector3 Move(Vector3 dir, float speed, bool back = false)
    {
        preVelocity = dir.normalized * speed;
        return preVelocity;
    }

    void FixedUpdate ( )
    {
        // [TODO] 이동식
        rigidbody.velocity = preVelocity;
        preVelocity = Vector3.zero;
    }
}
