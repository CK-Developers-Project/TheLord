using UnityEngine;
using Developers.Structure;
using Developers.Structure.Data;

public class BaseCharacter : MonoBehaviour, IActor
{
    public CharacterData data;
    public Status<ActorStatus> status = new Status<ActorStatus> ( (int)ActorStatus.End );
    public DamageCalculator damageCalculator = new DamageCalculator();

    public ActorObject Actor { get; protected set; }
    public ActorPath Path { get; protected set; }


    public int Index { get => data.index; }
    public AbilityCaster Caster { get; set; }

    public bool Synchronized { get => throw new System.NotImplementedException ( ); set => throw new System.NotImplementedException ( ); }

    public void Initialize ( )
    {

    }

    public void Load ( )
    {
        Caster = new AbilityCaster ( this );

        status.Normal.Set ( ActorStatus.Atk, data.Atk );
        status.Normal.Set ( ActorStatus.Def, data.Def );
        status.Normal.Set ( ActorStatus.HP, data.HP );
        status.Normal.Set ( ActorStatus.Speed, data.Speed );
        status.Normal.Set ( ActorStatus.AtkCoolTime, data.AtkCooltime );
        status.Normal.Set ( ActorStatus.Distance, data.Distance );
        status.Normal.Set ( ActorStatus.CastSpeed, 1F );
        status.Normal.Set ( ActorStatus.AbilityTime, 1F );

        
    }

    public void OnSelect ( )
    {
        
    }


    public void Move(Vector3 dir)
    {
        float speed = status.Get ( ActorStatus.Speed, true, true, true ) / 10000F;
        Path.Move ( dir, speed );
    }


    protected void Awake ( )
    {
        Actor = GetComponentInChildren<ActorObject> ( );
        Path = GetComponentInChildren<ActorPath> ( );
    }
}
