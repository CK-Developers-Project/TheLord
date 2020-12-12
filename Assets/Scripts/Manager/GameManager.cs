#define Develop

using UnityEngine;
using Developers.Util;
using Developers.Structure;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoSingleton<GameManager>
{
    #region 기본 오브젝트
    Camera mainCamera;
    PixelPerfectCameraHelper pixelPerfectCameraHelper;

    public Camera MainCamera {
        get
        {
            if( mainCamera == null)
            {
                mainCamera = Camera.main;
            }
            return mainCamera;
        }
    }
    public PixelPerfectCameraHelper PixelPerfectCameraHelper {
        get
        {
            if( pixelPerfectCameraHelper  == null)
            {
                pixelPerfectCameraHelper = MainCamera.GetComponent<PixelPerfectCameraHelper> ( );
            }
            return pixelPerfectCameraHelper;
        }
    }
    #endregion

    [SerializeField] BaseGameMode gameMode;

    public List<GamePlayer> gamePlayers = new List<GamePlayer>();

    public GamePlayer LocalPlayer 
    {  
        get
        {
            return gamePlayers.Count > 0 ? gamePlayers[0] : null;
        }
    }

    public BaseGameMode GameMode 
    {   
        get => gameMode; 
        set
        {
            gameMode?.OnExit ( );
            gameMode = value;
            gameMode?.OnEnter ( );
        }
    }


    public IEnumerator Initialize ()
    {
        if (!LoadManager.Instance.IsInitialize)
        {
            LoadManager.Instance.Initialize();
            yield return new WaitUntil(() => LoadManager.Instance.IsInitialize);
        }
        
        if ( GameMode == null )
        {
            BaseGameMode com = GameObject.FindGameObjectWithTag ( "GameMode" ).GetComponent<BaseGameMode> ( );
            gameMode = com;
        }

        GameMode.Load ( );
        yield return new WaitUntil ( ( ) => IsGameStart );
        GameMode.OnEnter ( );
    }

    public IEnumerator Dispose()
    {
        if ( GameMode != null )
        {
            GameMode.OnExit ( );
            GameMode = null;
        }
        IsGameStart = false;
        yield break;
    }

    public bool IsGameStart { get; private set; }


    public static GameObject Create<T>(ActorRecord actorRecord) where T : IActor
    {
        GameObject obj = MonoSingleton<LoadManager>.Instance.GetActor<T> ( actorRecord );
        return obj;
    }

    public GamePlayer Join ( string nickname, Race race )
    {
        GameObject obj = new GameObject ( string.Format ( "Player - {0}({1})", nickname, gamePlayers.Count + 1 ), typeof ( GamePlayer ) );
        obj.transform.SetParent ( transform );

        GamePlayer com = obj.GetComponent<GamePlayer> ( );
        com.playerInfo.Nickname = nickname;
        com.playerInfo.Race = race;
        gamePlayers.Add ( com );
        return com;
    }

    public void OnStart()
    {
        IsGameStart = true;
    }


    protected override void Awake ( )
    {
        base.Awake ( );

        if(instance != this)
        {
            instance.gameMode = gameMode;
        }

        IsGameStart = false;

#if Develop
        Join("Tester", Race.Human);
#endif
    }

    private IEnumerator Start ( )
    {

        if ( instance == this )
        {
            // TODO : 따로 로딩페이지를 만들자. . .
            if ( !MonoSingleton<LoadManager>.Instance.IsInitialize )
            {
                MonoSingleton<LoadManager>.Instance.Initialize ( );
                yield return new WaitUntil ( ( ) => MonoSingleton<LoadManager>.Instance.IsInitialize );
            }
            GameMode.Load ( );
            yield return new WaitUntil ( ( ) => IsGameStart );
            GameMode.OnEnter ( );
        }       
    }


    private void Update ( )
    {
#if Develop
        if( TransitionManager.instance.WaitSign.IsActive )
        {
            TransitionManager.instance.OnWaitSigh ( false );
        }
#endif
    }
}