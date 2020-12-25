using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;

using UnityEngine.InputSystem;

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

        SummonCharacter ( );
    }

    public override void OnExit ( )
    {
        ReleaseInput ( );
    }


    [SerializeField] int CharacterIndex = 1;
    bool isPress = false;
    void SummonCharacter()
    {
        var keyboard = Keyboard.current;
        if(keyboard == null)
        {
            return;
        }

        if(keyboard.digit1Key.isPressed)
        {
            if(isPress)
            {
                return;
            }
            isPress = true;

            Debug.Log ( "1 press" );
        }
        else
        {
            isPress = false;
        }
    }
}
