using UnityEngine;
using Developers.Structure;
using Developers.Structure.Data;

public class BaseCharacter : MonoBehaviour, IActor
{
    public CharacterData data;
    public Developers.Structure.CharacterInfo info;
    public Status<ActorStatus> status = new Status<ActorStatus> ( (int)ActorStatus.End );
    public DamageCalculator damageCalculator = new DamageCalculator();

    public ActorObject Actor { get; protected set; }
    public ActorPath Path { get; protected set; }


    public int Index { get => data.index; }
    public AbilityCaster Caster { get; set; }

    public void Load ( )
    {
        info = data.GetInfo ( );
        Caster = new AbilityCaster(this);
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

    private void Start()
    {
        Load();
    }
}
