using UnityEngine;
using Developers.Structure;

public class VFXObject : MonoBehaviour
{
    public Animator animator;
    public VFXType type;

    public void Start ( )
    {
        animator.SetTrigger ( "Action" );
        animator.SetInteger ( "Id", (int)type );
    }

    public void Destroy()
    {
        Destroy ( gameObject );
    }
}
