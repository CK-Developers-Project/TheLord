using UnityEngine;
using Developers.Structure;
using Developers.Structure.Data;

using UnityEngine.InputSystem;

public class BaseCharacter : MonoBehaviour, IActor
{
    public CharacterData data;
    public Developers.Structure.CharacterInfo info;
    public CharacterAbility ability = new CharacterAbility();
    public DamageCalculator damageCalculator = new DamageCalculator();

    public ActorObject Actor { get; protected set; }
    public ActorPath Path { get; protected set; }


    public int Index { get => data.index; } 

    public void Load ( )
    {
        info = data.GetInfo ( );
        ability.normal.Set ( ActorAbilityType.Speed, 10000 );
    }

    public void OnSelect ( )
    {
        
    }


    public void Move(Vector3 dir)
    {
        float speed = ability.Get ( ActorAbilityType.Speed, true, true, true ) / 10000F;
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
