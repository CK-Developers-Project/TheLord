using UnityEngine;
using System.Collections.Generic;
using System;
using Developers.Util;
using UnityEngine.InputSystem;
using Developers.Net.Protocol;
using System.Collections;
using Cinemachine;

public class MainLobbyGameMode : BaseGameMode
{
    [SerializeField] MainLobbyPage mainLobbyPage = null;
    [SerializeField] CinemachineVirtualCamera mainLoobyCV = null;

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
        InputManager.Instance.layerMask = Physics2D.DefaultRaycastLayers;
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
        InputManager.Instance.layerMask = 0;
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
        InputManager manager = InputManager.Instance;

        if ( manager.isPressed )
        {
            Vector3 start = GameManager.Instance.MainCamera.ScreenToWorldPoint ( manager.Position );
            Vector3 end = GameManager.Instance.MainCamera.ScreenToWorldPoint ( manager.StartPoint );
            Vector3 drag = start - end;
            Vector3 target = GameManager.Instance.MainCamera.transform.position + new Vector3 ( drag.x, 0F, 0F );
            mainLoobyCV.ForceCameraPosition ( target, Quaternion.identity );
            manager.StartPoint = manager.Position;
        }
    }

    public override void OnExit ( )
    {
        ReleaseInput ( );
    }
}

