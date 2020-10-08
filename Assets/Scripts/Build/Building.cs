using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;
using Developers.Util;

public class Building : MonoBehaviour, IActor
{
    protected SpriteRenderer spriteRenderer = null;
    protected WorkTimer workTimer = null;

    public BuildingInfo info;

    public int Index { get => (int)info.type; }


    public virtual void Load ( ) { }
    public virtual void OnSelect ( ) { }


    protected virtual void Awake ( )
    {
        spriteRenderer = GetComponent<SpriteRenderer> ( );
        workTimer = GetComponentInChildren<WorkTimer> ( true );
        
        MainLobbyGameMode gameMode = MonoSingleton<GameManager>.Instance.GameMode as MainLobbyGameMode;
        gameMode?.Buildings.Add ( this );
    }
}