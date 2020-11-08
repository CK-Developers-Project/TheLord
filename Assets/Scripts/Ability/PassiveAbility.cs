using UnityEngine;
using Developers.Structure;

public class PassiveAbility : BaseAbility
{
    

    public override AbilityOrder Order { get => default; }

    public override float Cast { get => default; }

    public override float Duration { get => default; }

    public override float Range { get => default; }

    public override float Distance { get => default; }

    public override float Amount { get => default; }

    public override float Cooltime { get => default; }

    public override bool OnStart(Vector3 position = default, IActor target = null)
    {
        return false;
    }
}