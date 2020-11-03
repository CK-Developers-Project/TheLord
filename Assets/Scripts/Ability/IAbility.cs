using UnityEngine;
using Developers.Structure;

public interface IAbility
{
    ActorOrder Order { get; }

    float Cast { get; }         // 시전 시간
    float Duration { get; }     // 스킬 시전 후 지속시간
    float Range { get; }        // 범위
    float Distance { get; }     // 사정거리
    float Amount { get; }
    float Cooltime { get; }

    bool OnStart ( Vector3 position = default, IActor target = null );
}