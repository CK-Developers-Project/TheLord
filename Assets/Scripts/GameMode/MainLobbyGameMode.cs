using UnityEngine;
using System.Collections.Generic;
using System;
using Developers.Util;
using UnityEngine.InputSystem;
using Developers.Net.Protocol;
using System.Collections;

public class MainLobbyGameMode : BaseGameMode
{
    [SerializeField] MainLobbyPage mainLobbyPage = null;

    List<Building> buildings = new List<Building> ( );
    public List<Building> Buildings { get => buildings; }


    public override IEnumerator OnStart ( )
    {
        yield return new WaitUntil ( () => TransitionManager.Instance.WaitSign.IsActive == false );
        yield return StartCoroutine ( base.OnStart ( ) );
    }

    public override void Load ( )
    {
        base.Load ( );
        new LobbyEnterRequest ( ).SendPacket ( );
    }


    void TouchEvent ( IActor target )
    {
        target.OnSelect ( );
    }

    public override void RegisterInput ( )
    { 
        var manager = InputManager.Instance.MainInputActions;
        if(manager.Main.enabled)
        {
            return;
        }
        InputManager.Instance.LayerMask = Physics2D.DefaultRaycastLayers;
        InputManager.Instance.TouchEvent += TouchEvent;
        manager.Main.Enable ( );
    }

    public override void ReleaseInput ( )
    {
        var manager = InputManager.Instance.MainInputActions;
        if ( !manager.Main.enabled )
        {
            return;
        }
        InputManager.Instance.LayerMask = 0;
        InputManager.Instance.TouchEvent -= TouchEvent;
        manager.Main.Disable ( );
    }

    public override void OnEnter ( )
    {
        SetPage ( mainLobbyPage );
        RegisterInput ( );
    }

    public override void OnUpdate ( )
    {

    }

    public override void OnExit ( )
    {
        ReleaseInput ( );
    }
}

