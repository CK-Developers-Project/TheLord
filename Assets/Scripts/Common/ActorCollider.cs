using UnityEngine;
using System.Collections.Generic;

public class ActorCollider : MonoBehaviour
{
    public IActor Owner { get; private set; }
    public List<Collider2D> Colliders = new List<Collider2D> ( );


    public void Awake ( )
    {
        Owner = GetComponentInParent<IActor> ( );
        Colliders.AddRange ( GetComponents<Collider2D> ( ) );
    }
}