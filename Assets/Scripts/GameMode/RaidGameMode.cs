using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;

using UnityEngine.InputSystem;

public class RaidGameMode : BaseGameMode
{
    const int RAID_WAVE_COUNT = 3;

    [SerializeField] Transform playerSpawnPoint = null;
    [SerializeField] Transform bossSpawnPoint = null;


    GamePlayer enemyPlayer;
    List<int> characterIndex = new List<int> ( );


    void SpawnCharacter ( int index, GamePlayer player )
    {
        float addHeight = Random.Range ( -0.592F, 0.592F );
        Vector2 position = playerSpawnPoint.position + new Vector3 ( 0F, addHeight, 0F );
        var obj = GameManager.Create<BaseCharacter> ( new ActorRecord ( ActorType.Character, index ), position, player );
        
    }

    void SpawnBoss ( int index )
    {
        var obj = GameManager.Create<BaseCharacter> ( new ActorRecord ( ActorType.Character, index ), playerSpawnPoint.position, enemyPlayer );
    }


    public void PushCharacterIndex(int index, int amount)
    {
        for(int i = 0; i < amount; ++i )
        {
            characterIndex.Add ( index );
        }
    }


    public override void Load ( )
    {
        base.Load ( );
    }

    public override void RegisterInput ( )
    {
        var manager = InputManager.Instance.MainInputActions;
        if ( manager.Main.enabled )
        {
            return;
        }
        InputManager.Instance.layerMask = GameLayerHelper.Layer ( GameLayer.Actor );
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
        Random.InitState ( System.DateTime.Now.Second );
        
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
