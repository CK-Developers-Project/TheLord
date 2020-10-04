using UnityEngine;
using System.Collections.Generic;
using System;
using Developers.Util;
using UnityEngine.InputSystem;

public class MainLobbyGameMode : BaseGameMode
{
    [SerializeField] MainLobbyPage mainLobbyPage = null;

    List<Building> buildings = new List<Building> ( );
    public List<Building> Buildings { get => buildings; }


    void TouchEvent ( IActor target )
    {
        target.OnSelect ( );
    }

    public override void RegisterInput ( )
    { 
        var manager = MonoSingleton<InputManager>.Instance.MainInputActions;
        if(manager.Main.enabled)
        {
            return;
        }
        MonoSingleton<InputManager>.Instance.LayerMask = Physics2D.DefaultRaycastLayers;
        MonoSingleton<InputManager>.Instance.TouchEvent += TouchEvent;
        manager.Main.Enable ( );
    }

    public override void ReleaseInput ( )
    {
        var manager = MonoSingleton<InputManager>.Instance.MainInputActions;
        if ( !manager.Main.enabled )
        {
            return;
        }
        MonoSingleton<InputManager>.Instance.LayerMask = 0;
        MonoSingleton<InputManager>.Instance.TouchEvent -= TouchEvent;
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

