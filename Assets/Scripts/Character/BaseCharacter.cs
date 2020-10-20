using UnityEngine;
using Developers.Structure;
using Developers.Structure.Data;

public class BaseCharacter : MonoBehaviour, IActor
{
    public CharacterData data;
    public Developers.Structure.CharacterInfo info = new Developers.Structure.CharacterInfo();
    public CharacterAbility ability = new CharacterAbility();
    public DamageCalculator damageCalculator = new DamageCalculator();

    public ActorObject Actor { get; protected set; }
    public ActorPath Path { get; protected set; }


    public int Index { get => data.index; } 

    public void Load ( )
    {
        /* [FIXME] CharacterData.cs 참고 */
        
        /* */
    }

    public void OnSelect ( )
    {
        
    }


    public void Move(Vector3 dir)
    {

    }


    protected void Awake ( )
    {
        Actor = GetComponentInChildren<ActorObject> ( );
        Path = GetComponentInChildren<ActorPath> ( );
    }
}
