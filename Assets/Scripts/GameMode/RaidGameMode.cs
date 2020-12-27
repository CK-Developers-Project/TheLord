using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;
using Developers.Net.Protocol;
using UnityEngine.InputSystem;

public class RaidGameMode : BaseGameMode
{
    const int RAID_WAVE_COUNT = 3;
    const int RAID_WAVE_TIME = 60;
    const int RAID_BOSS_INDEX = 16;

    [SerializeField] Transform playerSpawnPoint = null;
    [SerializeField] Transform bossSpawnPoint = null;
    [SerializeField] RaidBattlePage raidBattlePage = null;

    public GamePlayer enemyPlayer;
    public GamePlayer ownerPlayer;
    public BaseCharacter RaidBoss;

    List<int> characterIndex = new List<int> ( );


    int waveCount = 0;
    float waveTimer = 0F;
    bool operateWave = false;

    bool gameEnd = false;
    bool resultWindow = false;

    public int score;
    public int totalScore;

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

    BaseCharacter SpawnBoss ( int index )
    {
        var obj = GameManager.Create<BaseCharacter> ( new ActorRecord ( ActorType.Character, index ), bossSpawnPoint.position, enemyPlayer );
        var character = obj.GetComponent<BaseCharacter> ( );
        obj.AddComponent<TheDevilAI> ( );
        enemyPlayer.CharacterAdd ( character );
        return character;
    }


    public void RecordScore( BaseCharacter source, BaseCharacter target, DamageCalculator.DamageInfo info )
    {
        score += (int)RaidBoss.damageCalculator.Formula ( info );
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
            yield return new WaitForSeconds ( 0.13F );
        }
        operateWave = false;
    }

    public void BreakBoss()
    {
        gameEnd = true;
    }

    public override IEnumerator OnStart ( )
    {
        RaidBoss = SpawnBoss ( RAID_BOSS_INDEX );
        RaidBoss.Hit += RecordScore;
        new EnterRaidRequest ( ).SendPacket ( );
        GameManager.Instance.IsSynchronized = false;
        yield return new WaitUntil ( ( ) => GameManager.Instance.IsSynchronized );
        yield return base.OnStart ( );
    }

    public override void OnSynchronize<T> ( T data )
    {
        var raidEnterData = data as ProtoData.RaidEnterData;
        if(raidEnterData == null)
        {
            Debug.LogError ( "[RaidGameMode] 싱크로 할 수 없습니다.", this );
            return;
        }

        RaidBoss.InitializeHP = raidEnterData.raidBossData.hp;
        foreach(var charactertData in raidEnterData.characterDataList)
        {
            PushCharacterIndex ( charactertData.index, charactertData.amount );
        }
        waveCount = Mathf.RoundToInt ( characterIndex.Count / 3F );

        score = 0;
        totalScore = raidEnterData.totalScore;

        base.OnSynchronize ( data );
    }


    void ResultWindow()
    {
        var page = CurrentPage as RaidBattlePage;
        page.OnResultPopup ( );
    }


    public override void Load ( )
    {
        base.Load ( );
        ownerPlayer = GameManager.Instance.LocalPlayer;
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
        SetPage ( raidBattlePage );
    }

    public override void OnUpdate ( )
    {
        if( GameManager.Instance.IsGameStart == false)
        {
            return;
        }

        CurrentPage.OnUpdate ( );

        if ( gameEnd )
        {
            if(resultWindow == false)
            {
                ResultWindow ( );
                resultWindow = true;
            }
        }

        CameraMovement ( );

        if(waveTimer > 0)
            waveTimer -= Time.deltaTime;


        if ( false == operateWave && characterIndex.Count > 0 && 
            ( waveTimer <= 0f || ownerPlayer.GetCharacterAll().Count == 0 ) )
        {
            StartCoroutine ( SpawnPlayerCharacter ( waveCount ) );
        }
        else if( (characterIndex.Count == 0 && ownerPlayer.GetCharacterAll ( ).Count == 0) || RaidBoss == null )
        {
            // 패배
            gameEnd = true;
        }
    }

    public override void OnExit ( )
    {
        // 패널티 발생
        ReleaseInput ( );
    }


}
