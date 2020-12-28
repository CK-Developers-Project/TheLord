using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Developers.Structure;
using Developers.Util;
using Developers.Table;
using Developers.Net.Protocol;

public class Building : MonoBehaviour, IActor
{
    protected SpriteRenderer spriteRenderer = null;
    protected WorkTimer workTimer = null;
    protected TooltipBox tooltipBox = null;

    public BuildingInfo info;


    public GamePlayer Owner { get; set; }
    public int Index { get => (int)info.index; }
    public bool Synchronized { get; set; }
    public bool Initialized { get; set; }
    public bool Invincible { get; set; }
    public bool Anim_Event { get; set; }

    public SfxAudio Audio { get; set; }

    Dictionary<string, object> myRecord = null;
    public Dictionary<string, object> MyRecord {
        get
        {
            if(myRecord == null)
            {
                var sheet = TableManager.Instance.BuildingTable.BuildingInfoSheet;
                myRecord = BaseTable.Get ( sheet, "index", (int)info.index );
            }
            return myRecord;
        }
    }

    public virtual void BuildUp ( long targetTime, Action @event = null )
    {
        if(@event == null)
        {
            if ( info.LV == 0 )
            {
                @event = ( ) =>
                {
                    var packet = new BuildingConfirmRequest ( );
                    packet.index = (int)info.index;
                    packet.confirmAction = ConfirmAction.Build;
                    packet.SendPacket ( true, true );
                };
            }
            else
            {
                @event = ( ) =>
                {
                    var packet = new BuildingConfirmRequest ( );
                    packet.index = (int)info.index;
                    packet.confirmAction = ConfirmAction.LevelUp;
                    packet.SendPacket ( true, true );
                };
            }
        }

        info.state = BuildingState.Work;
        int second = ( info.LV + 1 ) * (int)MyRecord["buildTime"];

        workTimer.Run ( new TimeSpan ( targetTime ), new TimeSpan(0, 0, second ), @event );
    }

    public virtual void Initialize ( )
    {
        info.name = (string)MyRecord["name"];
        info.spawnCharacter = (int)MyRecord["spawnCharacter"];
        Initialized = true;
    }

    public virtual void Load ( )
    {
        Synchronized = true;

        var buildingInfo = GameManager.Instance.synchronizeData.GetBuildingInfo ( info.index );
        if ( null == buildingInfo )
        {
            info.state = BuildingState.Empty;
            return;
        }

        info.LV = buildingInfo.LV;
        info.amount = buildingInfo.amount;
        info.workTime = buildingInfo.workTime;

        if ( info.workTime >= 0 )
        {
            Action @event;
            if ( info.LV == 0 )
            {
                @event = ( ) =>
                {
                    var packet = new BuildingConfirmRequest ( );
                    packet.index = (int)info.index;
                    packet.confirmAction = ConfirmAction.Build;
                    packet.SendPacket ( true, true );
                };
            }
            else
            {
                @event = ( ) =>
                {
                    var packet = new BuildingConfirmRequest ( );
                    packet.index = (int)info.index;
                    packet.confirmAction = ConfirmAction.LevelUp;
                    packet.SendPacket ( true, true );
                };
            }

            BuildUp ( info.workTime, @event );
        }
        else if ( info.LV == 0 )
        {
            info.state = BuildingState.Empty;
        }
        else
        {
            info.state = BuildingState.Complete;
        }
    }

    public virtual void OnUpdate ( ) { }

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
        info.state = BuildingState.Complete;
    }


    protected virtual void Awake ( )
    {
        spriteRenderer = GetComponent<SpriteRenderer> ( );
        workTimer = GetComponentInChildren<WorkTimer> ( true );
        tooltipBox = GetComponentInChildren<TooltipBox> ( true );
        Audio = GetComponentInChildren<SfxAudio> ( );

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

    protected virtual void LateUpdate()
    {
        if ( !Initialized )
        {
            return;
        }

        if ( GameManager.Instance.IsSynchronized && !Synchronized )
        {
            Load ( );
        }

        OnUpdate ( );
    }
}