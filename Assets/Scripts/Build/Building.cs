using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Developers.Structure;
using Developers.Util;
using Developers.Table;

public class Building : MonoBehaviour, IActor
{
    protected SpriteRenderer spriteRenderer = null;
    protected WorkTimer workTimer = null;

    public BuildingInfo info;

    public GamePlayer Owner { get; set; }
    public int Index { get => (int)info.index; }
    public bool Synchronized { get; set; }
    public bool Initialized { get; set; }
    public bool Anim_Event { get; set; }

    public void BuildUp ( DateTime targetTime )
    {
        info.state = BuildingState.Work;
        workTimer.Run ( targetTime );
    }

    public virtual void Initialize ( )
    {
        var sheet = TableManager.Instance.BuildingTable.BuildingInfoSheet;
        var record = BaseTable.Get ( sheet, "index", (int)info.index );

        info.name = (string)record["name"];
        Initialized = true;
    }

    public virtual void Load ( ) 
    { 
        Synchronized = true;

        var buildingInfo = GameManager.Instance.synchronizeData.GetBuildingInfo ( info.index );
        if( null == buildingInfo)
        {
            info.state = BuildingState.Empty;
            return;
        }

        info.LV = buildingInfo.LV;
        info.workTime = buildingInfo.workTime;

        TimeSpan buildRemaineTime = info.workTime - DateTime.Now;
        
        if (info.LV == 0)
        {
            info.state = BuildingState.Empty;
        }
        else if( buildRemaineTime.Ticks > 0)
        {
            BuildUp ( info.workTime );
        }
        else
        {
            info.state = BuildingState.Complete;
        }
    }
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

    public virtual void OnBuild()
    {
        info.state = BuildingState.Work;
    }
    

    protected virtual void Awake ( )
    {
        spriteRenderer = GetComponent<SpriteRenderer> ( );
        workTimer = GetComponentInChildren<WorkTimer> ( true );
        workTimer.gameObject.SetActive ( false );

        MainLobbyGameMode gameMode = MonoSingleton<GameManager>.Instance.GameMode as MainLobbyGameMode;
        gameMode?.Buildings.Add ( this );

        Synchronized = false;
        Initialized = false;
        Anim_Event = false;
    }

    IEnumerator Start()
    {
        yield return new WaitUntil ( ( ) => GameManager.Instance.IsGameStart );
        Initialize ( );
    }

    void LateUpdate()
    {
        if ( !Initialized )
        {
            return;
        }

        if ( GameManager.Instance.IsSynchronized && !Synchronized )
        {
            Load ( );
        }
    }
}