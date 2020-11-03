using UnityEngine;
using Developers.Structure;


public class PassiveAbility : IAbility
{
    public ActorOrder Order => throw new System.NotImplementedException();

    public float Cast => throw new System.NotImplementedException();

    public float Duration => throw new System.NotImplementedException();

    public float Range => throw new System.NotImplementedException();

    public float Distance => throw new System.NotImplementedException();

    public float Amount => throw new System.NotImplementedException();

    public float Cooltime => throw new System.NotImplementedException();

    public bool OnStart(Vector3 position = default, IActor target = null)
    {
        throw new System.NotImplementedException();
    }
}