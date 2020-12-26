using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;

using UnityEngine.InputSystem;

public class RaidGameMode : BaseGameMode
{
    const int RAID_WAVE_COUNT = 3;
    const int RAID_WAVE_TIME = 60;
    const int RAID_BOSS_INDEX = 16;

    [SerializeField] Transform playerSpawnPoint = null;
    [SerializeField] Transform bossSpawnPoint = null;


    GamePlayer enemyPlayer;
    GamePlayer ownerPlayer;

    List<int> characterIndex = new List<int> ( );


    float waveTimer = 0F;
    bool operateWave = false;

    bool gameEnd = false;

    void SpawnCharacter ( int index, GamePlayer player )
    {
        float addHeight = Random.Range ( -0.592F, 0.592F );
        Vector2 position = playerSpawnPoint.position + new Vector3 ( 0F, addHeight, 0F );
        var obj = GameManager.Create<BaseCharacter> ( new ActorRecord ( ActorType.Character, index ), position, player );
        var character = obj.GetComponent<BaseCharacter> ( );
        var ai = obj.AddComponent<CharacterAIForRaid> ( );
        ai.target = enemyPlayer.GetCharacter ( 0 );
        ownerPlayer.CharacterAdd ( character );
    }

    void SpawnBoss ( int index )
    {
        var obj = GameManager.Create<BaseCharacter> ( new ActorRecord ( ActorType.Character, index ), bossSpawnPoint.position, enemyPlayer );
        var character = obj.GetComponent<BaseCharacter> ( );
        obj.AddComponent<TheDevilAI> ( );
        enemyPlayer.CharacterAdd ( character );
    }


    public void PushCharacterIndex(int index, int amount)
    {
        for(int i = 0; i < amount; ++i )
        {
            characterIndex.Insert ( Random.Range ( 0, characterIndex.Count + 1 ), index );
        }
    }


    IEnumerator SpawnPlayerCharacter(int cnt)
    {
        if( operateWave == true)
        {
            yield break;
        }

        operateWave = true;
        waveTimer = 60F;

        while(cnt > 0)
        {
            if(characterIndex.Count == 0)
            {
                break;
            }
            SpawnCharacter ( characterIndex[0], GameManager.Instance.LocalPlayer );
            characterIndex.RemoveAt ( 0 );

            --cnt;
            yield return new WaitForSeconds ( 0.08F );
        }
        operateWave = false;
    }

    public override IEnumerator OnStart ( )
    {
        SpawnBoss ( RAID_BOSS_INDEX );
        //yield return new WaitUntil ( ( ) => GameManager.Instance.IsSynchronized );
        yield return base.OnStart ( );
    }

    public override void OnSynchronize<T> ( T data )
    {

        base.OnSynchronize ( data );
    }

    public override void Load ( )
    {
        base.Load ( );

        ownerPlayer = GameManager.Instance.LocalPlayer;
        ownerPlayer.Initialize ( "Local", Race.Elf );
        PushCharacterIndex ( 1, 30 );
        PushCharacterIndex ( 2, 20 );
        PushCharacterIndex ( 3, 20 );
        PushCharacterIndex ( 4, 10 );
        PushCharacterIndex ( 5, 5 );
        enemyPlayer = GameManager.Instance.Join ( "Raid", Race.Undead );
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
        if( gameEnd || GameManager.Instance.IsGameStart == false)
        {
            return;
        }

        CameraMovement ( );

        if(waveTimer > 0)
            waveTimer -= Time.deltaTime;


        if ( false == operateWave && characterIndex.Count > 0 && 
            ( waveTimer <= 0f || ownerPlayer.GetCharacterAll().Count == 0 ) )
        {
            int cnt = Mathf.RoundToInt(characterIndex.Count / 3f);
            StartCoroutine ( SpawnPlayerCharacter ( cnt ) );
        }
        else if( characterIndex.Count == 0 && ownerPlayer.GetCharacterAll ( ).Count == 0 )
        {
            // 패배
            gameEnd = true;
        }
    }

    public override void OnExit ( )
    {
        ReleaseInput ( );
    }


}
