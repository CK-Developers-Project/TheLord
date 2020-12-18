using UnityEngine;
using Developers.Structure;
using Developers.Structure.Data;

public abstract class BaseAbility
{
    public Status<AbilityStatus> status = new Status<AbilityStatus> ( (int)AbilityStatus.End );

    public abstract AbilityOrder Order { get; }

    public virtual float Cast { get; }         // 시전 시간
    public virtual float Duration { get; }     // 스킬 시전 후 지속시간
    public virtual float Range { get; }        // 범위
    public virtual float Distance { get; }     // 사정거리
    public virtual float Cooltime { get; }     // 재사용 시간
    public virtual float Amount { get; }

    public abstract bool OnStart ( Vector3 position = default, IActor target = null );
}