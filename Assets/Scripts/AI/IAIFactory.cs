using Developers.Structure;
using UnityEngine;

public interface IAIFactory
{
    IActor Actor { get; }

    bool SetOrder ( AbilityOrder order );
    bool SetOrder ( AbilityOrder order, IActor target );
    bool SetOrder ( AbilityOrder order, Vector3 position );
}