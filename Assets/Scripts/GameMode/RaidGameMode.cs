using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;

using UnityEngine.InputSystem;

public class RaidGameMode : BaseGameMode
{
    public GamePlayer LocalPlayer;

    public GamePlayer EnemyPlayer;

    public override void Load ( )
    {
        LocalPlayer.Initialize ( "Local", Race.Elf );
        EnemyPlayer.Initialize ( "Enemy", Race.Undead );

        GameManager.Instance.gamePlayers.Add ( LocalPlayer );
        GameManager.Instance.gamePlayers.Add ( EnemyPlayer );

        foreach(var character in LocalPlayer.GetCharacterAll ( ))
        {
            character.gameObject.AddComponent<CharacterAIForRaid> ( );
        }

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
    bool isTestPress = false;
    void SummonCharacter()
    {
        var keyboard = Keyboard.current;
        if(keyboard == null)
        {
            return;
        }

        if(keyboard.digit1Key.isPressed)
        {
            if(isTestPress)
            {
                return;
            }
            isTestPress = true;

            Debug.Log ( "1 press" );
        }
        else
        {
            isTestPress = false;
        }
    }
}
