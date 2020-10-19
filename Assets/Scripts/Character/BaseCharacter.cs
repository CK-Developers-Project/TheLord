﻿using UnityEngine;
using Developers.Structure;
using Developers.Structure.Data;

public class BaseCharacter : MonoBehaviour, IActor
{
    public CharacterData data;

    public DamageCalculator damageCalculator;

    public ActorObject Actor { get; protected set; }
    public ActorPath Path { get; protected set; }


    public int Index { get => data.index; } 

    public void Load ( )
    {
        
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
