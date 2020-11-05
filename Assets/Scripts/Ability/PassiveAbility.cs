using UnityEngine;
using Developers.Structure;


public class PassiveAbility : IAbility
{
    public virtual ActorOrder Order { get => default; }

    public virtual float Cast { get => default; }

    public virtual float Duration { get => default; }

    public virtual float Range { get => default; }

    public virtual float Distance { get => default; }

    public virtual float Amount { get => default; }

    public virtual float Cooltime { get => default; }

    public virtual bool OnStart(Vector3 position = default, IActor target = null)
    {
        return false;
    }
}