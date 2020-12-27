using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;

public class ActorObject : MonoBehaviour
{
    public IActor Owner { get; private set; }
    const string ANIM_ID = "Id";
    const string ANIM_ACTION = "Action";

    public Animator Animator { get; protected set; }
    public bool isAnim;

    List<int> queue = new List<int> ( );


    #region
    public void Anim_Event()
    {
        Owner.Anim_Event = true;
    }

    public void Anim_Sound ( SFXType type )
    {
        Owner.Audio.play ( LoadManager.Instance.GetSFXData ( type ).clip, 1F, 0F, 1F );
    }
    #endregion

    /// <summary>
    /// 대기중인 애니메이션이 없는지?
    /// </summary>
    public bool None {
        get => isAnim && queue.Count == 0;
    }

    public void Add ( int id )
    {
        if ( !Animator )
        {
            return;
        }
        queue.Add ( id );
    }

    public void Set ( int id )
    {
        if ( !Animator )
        {
            return;
        }
        queue.Clear ( );
        isAnim = false;
        Animator.SetInteger ( ANIM_ID, id );
        Animator.SetTrigger ( ANIM_ACTION );
    }

    public bool isPlay ( int id )
    {
        if ( !Animator )
        {
            return true;
        }
        return Animator.GetInteger ( ANIM_ID ) == id;
    }

    public bool isExist ( int id )
    {
        if(!Animator)
        {
            return true;
        }
        return Animator.GetInteger ( ANIM_ID ) == id || queue.Exists ( x => x == id );
    }

    private void Awake ( )
    {
        Owner = GetComponentInParent<IActor> ( );
        Animator = GetComponent<Animator> ( );
    }

    protected void OnEnable ( )
    {
        if ( Animator )
        {
            ActorAnimation.Initialize ( Animator, this );
        }
    }

    private void Update ( )
    {
        if ( queue.Count > 0 )
        {
            if ( isAnim )
            {
                isAnim = false;
                var id = queue[0];
                queue.RemoveAt ( 0 );
                Animator.SetInteger ( ANIM_ID, id );
                Animator.SetTrigger ( ANIM_ACTION );
            }
        }
    }
}
