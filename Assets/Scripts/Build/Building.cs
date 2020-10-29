using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Developers.Structure;
using Developers.Util;

public class Building : MonoBehaviour, IActor
{
    protected SpriteRenderer spriteRenderer = null;
    protected WorkTimer workTimer = null;

    public BuildingInfo info;

    public int Index { get => (int)info.type; }


    
    public void BuildUp(TimeSpan timeSpan, Action callback)
    {
        workTimer.Run ( timeSpan, callback );
    }


    public virtual void Load ( ) { }
    public virtual void OnSelect ( ) 
    { 
        switch ( info.state )
        {
            case BuildingState.Empty:
                OnEmpty ( );
                break;
            case BuildingState.Work:
                OnWork ( );
                break;
            case BuildingState.Complete:
                OnComplete ( );
                break;
        }
    }

    protected virtual void OnEmpty ( ) { }
    protected virtual void OnWork ( ) { }
    protected virtual void OnComplete ( ) { }

    protected virtual void Awake ( )
    {
        spriteRenderer = GetComponent<SpriteRenderer> ( );
        workTimer = GetComponentInChildren<WorkTimer> ( true );
        
        MainLobbyGameMode gameMode = MonoSingleton<GameManager>.Instance.GameMode as MainLobbyGameMode;
        gameMode?.Buildings.Add ( this );
    }

    protected virtual void Start()
    {
        Load();
    }
}