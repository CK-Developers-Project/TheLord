using UnityEngine;

public class ActorAnimation : SceneLinkedSMB<ActorObject>
{
    [SerializeField] private float exit_time = 1f;

    public override void OnSLStateNoTransitionUpdate ( Animator animator, AnimatorStateInfo info, int layer )
    {
        if(component == null)
        {
            Debug.LogErrorFormat ( "{0}에게 {1} 컴포넌트가 없습니다.", animator.gameObject.name, typeof ( ActorObject ).Name );
            return;
        }

        if ( info.normalizedTime >= exit_time )
        {
            component.isAnim = true;
        }
    }
}