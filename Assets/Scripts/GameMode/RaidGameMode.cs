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


    public GamePlayer enemyPlayer;
    public GamePlayer ownerPlayer;
    public BaseCharacter RaidBoss;

    List<int> characterIndex = new List<int> ( );

    [System.Serializable]
    public class TestUnitPush
    {
        int index;
        [Range(0, 300)]
        public int amount;

        public TestUnitPush ( int index ) => this.index = index;
    }

    [Header ( "테스트용" )]
    public TestUnitPush 엘프궁수 = new TestUnitPush (1 );
    public TestUnitPush 엘프드루이드 = new TestUnitPush (2 );
    public TestUnitPush 엘프바드 = new TestUnitPush (3 );
    public TestUnitPush 엘프정령사 = new TestUnitPush (4 );
    public TestUnitPush 엘프가디언 = new TestUnitPush (5 );

    public TestUnitPush 인간궁수 = new TestUnitPush ( 6);
    public TestUnitPush 인간전사 = new TestUnitPush ( 7);
    public TestUnitPush 인간방패병 = new TestUnitPush (8 );
    public TestUnitPush 인간총잡이 = new TestUnitPush (9 );
    public TestUnitPush 인간수녀 = new TestUnitPush (10 );

    public TestUnitPush 언데드전사 = new TestUnitPush (11 );
    public TestUnitPush 언데드마녀 = new TestUnitPush ( 12);
    public TestUnitPush 언데드리퍼 = new TestUnitPush ( 13);
    public TestUnitPush 언데드네크 = new TestUnitPush ( 14);
    public TestUnitPush 언데드소울나이트 = new TestUnitPush ( 15);

    int waveCount = 0;
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

    BaseCharacter SpawnBoss ( int index )
    {
        var obj = GameManager.Create<BaseCharacter> ( new ActorRecord ( ActorType.Character, index ), bossSpawnPoint.position, enemyPlayer );
        var character = obj.GetComponent<BaseCharacter> ( );
        obj.AddComponent<TheDevilAI> ( );
        enemyPlayer.CharacterAdd ( character );
        return character;
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
        PushCharacterIndex ( 1, 엘프궁수.amount );
        PushCharacterIndex ( 2, 엘프드루이드.amount );
        PushCharacterIndex ( 3, 엘프바드.amount );
        PushCharacterIndex ( 4, 엘프정령사.amount );
        PushCharacterIndex ( 5, 엘프가디언.amount );

        PushCharacterIndex ( 6, 인간궁수.amount );
        PushCharacterIndex ( 7, 인간전사.amount );
        PushCharacterIndex ( 8, 인간방패병.amount );
        PushCharacterIndex ( 9, 인간총잡이.amount );
        PushCharacterIndex ( 10, 인간수녀.amount );

        PushCharacterIndex ( 11, 언데드전사.amount );
        PushCharacterIndex ( 12, 언데드마녀.amount );
        PushCharacterIndex ( 13, 언데드리퍼.amount );
        PushCharacterIndex ( 14, 언데드네크.amount );
        PushCharacterIndex ( 15, 언데드소울나이트.amount );

        waveCount = Mathf.RoundToInt ( characterIndex.Count / 3F );
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
        ownerPlayer.Initialize ( "local", Race.Elf );
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
            StartCoroutine ( SpawnPlayerCharacter ( waveCount ) );
        }
        else if( (characterIndex.Count == 0 && ownerPlayer.GetCharacterAll ( ).Count == 0) )
        {
            gameEnd = true;
        }
    }

    public override void OnExit ( )
    {
        // 패널티 발생
        ReleaseInput ( );
    }


}
