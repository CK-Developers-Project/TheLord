using UnityEngine;
using Developers.Structure;
using Developers.Structure.Data;
using System.Collections;
using System;
using Spine.Unity;

public abstract class BaseCharacter : MonoBehaviour, IActor
{

    [HideInInspector] public SkeletonMecanim spineMeca;
    
    public CharacterData data;

    public Status<ActorStatus> status = new Status<ActorStatus> ( (int)ActorStatus.End );
    public DamageCalculator damageCalculator = new DamageCalculator();
    public AbilityCaster caster = new AbilityCaster ( );

    public ActorObject Actor { get; protected set; }
    public ActorPath Path { get; protected set; }
    public ActorCollider ACollider { get; protected set; }

    public GamePlayer Owner { get; set; }
    public int Index { get => data.index; }
    public bool Synchronized { get; set; }
    public bool Initialized { get; set; }
    public bool Anim_Event { get; set; }

    public BaseCharacter OrderTarget { get; set; }
    public Vector3 OrderPosition { get; set; }

    public AbilityOrder Order { get => caster.order; set => caster.order = value; }
    public Vector3 Position { get => Path.transform.position; }
    public float Radius { get => Path.Radius; }

    /// <summary>
    /// 바라보고 있는 방향
    /// </summary>
    public virtual bool LookAtRight { 
        get
        {
            return spineMeca.initialFlipX == false;
        }
        set
        {
            spineMeca.initialFlipX = !value;
        }
    }

    public float Forward {
        get
        {
            return LookAtRight ? 1F : -1F;
        }
    }

    public event FOnDeath @Death = null;
    public event FOnAttack @Attack = null;
    public event FOnHit @Hit = null;
    public event FOnDamaged @Damaged = null;

    protected float hp;
    public float Hp {
        get => hp;
        set
        {
            hp = value;
            float max = status.Get ( ActorStatus.HP, true, true, true );
            if ( hp > max )
            {
                hp = max;
            }
        }
    }

    protected bool isDeath = false;
    public bool IsDeath  {
        get => IsDeath;
        set
        {
            if(!isDeath)
            {
                if ( value )
                {
                    OnDeath ( );
                }
            }
            isDeath = value;
        }
    }


    [Serializable]
    public class AnimationID
    {
        public AbilityOrder order;
        public int id;
    }

    [SerializeField] AnimationID[] animationID;


    #region Animation Function
    public void SetAnim()
    {
        if ( Actor.isPlay ( -1 ) ) return;
        Actor.Set ( PlayAction ( ) );
    }

    public void AddAnim()
    {
        if ( Actor.isPlay ( -1 ) ) return;
        Actor.Add ( PlayAction ( ) );
    }

    public void SetAction ( Action<BaseCharacter> action, bool restart = false )
    {
        if ( Actor.isPlay ( -1 ) ) return;
        action?.Invoke ( this );
        int id = PlayAction ( );
        if ( restart == true || false == Actor.isPlay ( id ) )
        {
            Actor.Set ( id );
        }
    }

    public void SetAction ( Action<BaseCharacter, Vector3> action, Vector3 position, bool restart = false )
    {
        if ( Actor.isPlay ( -1 ) ) return;
        action?.Invoke ( this, position );
        int id = PlayAction ( );
        if ( restart == true || false == Actor.isPlay ( id ) )
        {
            Actor.Set ( id );
        }
    }

    public void SetAction ( Action<BaseCharacter, IActor> action, IActor target, bool restart = false )
    {
        if ( Actor.isPlay ( -1 ) ) return;
        action?.Invoke ( this, target );
        int id = PlayAction ( );
        if ( restart == true || false == Actor.isPlay ( id ) )
        {
            Actor.Set ( id );
        }
    }

    public void AddAction ( Action action )
    {
        if ( Actor.isPlay ( -1 ) ) return;
        action?.Invoke ( );
        Actor.Add ( PlayAction ( ) );
    }

    public void AddAction ( Action<Vector3> action, Vector3 position )
    {
        if ( Actor.isPlay ( -1 ) ) return;
        action?.Invoke ( position );
        Actor.Add ( PlayAction ( ) );
    }

    public void AddAction ( Action<IActor> action, IActor target )
    {
        if ( Actor.isPlay ( -1 ) ) return;
        action?.Invoke ( target );
        Actor.Add ( PlayAction ( ) );
    }

    int PlayAction ( )
    {
        if ( isDeath )
        {
            return -1;
        }

        for ( int i = 0; i < animationID.Length; ++i )
        {
            if ( animationID[i].order == Order )
            {
                return animationID[i].id;
            }
        }
        return 0;
    }
    #endregion

    #region IActor Function
    public virtual void Initialize ( )
    {
        hp = status.Get ( ActorStatus.HP, true, true, true );
        Initialized = true;
    }

    public virtual void Load ( )
    {
        Synchronized = true;
    }


    public virtual void OnUpdate()
    {
    }

    public virtual void OnSelect ( )
    {
    }
    #endregion

    #region Logic Function

    public virtual void OnDeath()
    {
        if(@Death != null)
        {
            @Death ( );
        }

        hp = 0F;
        isDeath = true;
        Actor.Set ( -1 );
    }

    public virtual void OnAttack()
    {
        if(@Attack != null)
        {
            @Attack ( OrderTarget );
        }
    }

    public virtual void OnHit ( BaseCharacter target )
    {
        if(@Hit != null)
        {
            @Hit ( target );
        }
    }

    public virtual void OnDamaged(BaseCharacter source, BaseCharacter target, DamageCalculator.DamageInfo info)
    {
        if( @Damaged  != null)
        {
            @Damaged ( source, target, info );
        }
    }

    #endregion

    protected void Awake ( )
    {
        spineMeca = GetComponentInChildren<SkeletonMecanim> ( );
        Actor = GetComponentInChildren<ActorObject> ( );
        Path = GetComponentInChildren<ActorPath> ( );
        ACollider = GetComponentInChildren<ActorCollider> ( );
        caster.Owner = this;
        damageCalculator.Owner = this;

        Synchronized = false;
        Initialized = false;
        Anim_Event = false;
    }

    IEnumerator Start ( )
    {
        yield return new WaitUntil ( ( ) => GameManager.Instance.IsGameStart );
        yield return new WaitUntil ( ( ) => data.isLoad );
        status.Normal.Set ( ActorStatus.Atk, data.Atk );
        status.Normal.Set ( ActorStatus.Def, data.Def );
        status.Normal.Set ( ActorStatus.HP, data.HP );
        status.Normal.Set ( ActorStatus.Speed, data.Speed );
        status.Normal.Set ( ActorStatus.AtkCoolTime, data.AtkCooltime );
        status.Normal.Set ( ActorStatus.Distance, data.Distance );
        status.Normal.Set ( ActorStatus.CastSpeed, 1F );
        status.Normal.Set ( ActorStatus.AbilityTime, 1F );

        foreach ( int i in data.Ability )
        {
            var data = LoadManager.Instance.GetAbilityData ( i );
            yield return new WaitUntil ( ( ) => data.isLoad );
            data.Add ( caster );
        }

        Initialize ( );
    }

    void LateUpdate ( )
    {
        if(!Initialized )
        {
            return;
        }

        if ( GameManager.Instance.IsSynchronized && !Synchronized )
        {
            Load ( );
        }

        if ( hp <= 0.4999F && !isDeath )
        {
            OnDeath ( );
        }

        OnUpdate ( );
    }
}