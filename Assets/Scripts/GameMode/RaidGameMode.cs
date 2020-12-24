using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;

public class RaidGameMode : BaseGameMode
{
    public Race race;

    public override void Load ( )
    {
        GameManager.Instance.Join ( "Local", race );
        GameManager.Instance.Join ( "Enemy", race );

        base.Load ( );
    }

    public override void RegisterInput ( )
    {
        var manager = InputManager.Instance.MainInputActions;
        if ( manager.Main.enabled )
        {
            return;
        }
        //InputManager.Instance.layerMask = GameLayerHelper.Layer ( GameLayer.Actor );
        manager.Main.Enable ( );
    }

    public override void ReleaseInput ( )
    {
        var manager = InputManager.Instance.MainInputActions;
        if ( !manager.Main.enabled )
        {
            return;
        }
        InputManager.Instance.layerMask = Physics2D.DefaultRaycastLayers;
        manager.Main.Disable ( );
    }

    public override void OnEnter ( )
    {
        RegisterInput ( );
    }

    public override void OnUpdate ( )
    {
        CameraMovement ( );
    }

    public override void OnExit ( )
    {
        ReleaseInput ( );
    }
}
