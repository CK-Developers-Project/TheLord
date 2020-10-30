using UnityEngine;
using Developers.Structure;
using Developers.Structure.Data;

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
    }

    public void OnSelect ( )
    {
        
    }


    public void Move(Vector3 dir)
    {
        float speed = 0f;// = ability.Get2Float ( ActorAbilityType.Speed, true, true, true );

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
