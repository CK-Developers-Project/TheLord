using UnityEngine;

public class ActorAnimation : SceneLinkedSMB<ActorObject>
{
    [SerializeField] private float exit_time = 1f;

    public override void OnSLStateNoTransitionUpdate ( Animator animator, AnimatorStateInfo info, int layer )
    {
        if ( info.normalizedTime >= exit_time )
        {
            component.isAnim = true;
        }
    }
}