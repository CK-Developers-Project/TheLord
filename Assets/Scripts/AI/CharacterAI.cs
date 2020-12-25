using UnityEngine;
using Developers.Structure;

public class CharacterAI : MonoBehaviour, IAIFactory
{
    protected BaseCharacter pawn;


    public IActor Actor { get => pawn; }

    public bool SetOrder ( AbilityOrder order )
    {
        return pawn.caster.OnStart ( order );
    }

    public bool SetOrder ( AbilityOrder order, IActor target )
    {
        return pawn.caster.OnStart ( order, target );
    }

    public bool SetOrder ( AbilityOrder order, Vector3 position )
    {
        return pawn.caster.OnStart ( order, position );
    }

    void Awake ( )
    {
        pawn = GetComponentInParent<BaseCharacter> ( );
    }
}
