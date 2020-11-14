using UnityEngine;
using Developers.Structure;
using Developers.Structure.Data;

public abstract class BaseAbility
{
    public Status<AbilityStatus> status = new Status<AbilityStatus> ( (int)AbilityStatus.End );

    public abstract AbilityOrder Order { get; }

    public abstract float Cast { get; }         // 시전 시간
    public abstract float Duration { get; }     // 스킬 시전 후 지속시간
    public abstract float Range { get; }        // 범위
    public abstract float Distance { get; }     // 사정거리
    public abstract float Cooltime { get; }     // 재사용 시간
    public abstract float Amount { get; }

    public abstract bool OnStart ( Vector3 position = default, IActor target = null );
}