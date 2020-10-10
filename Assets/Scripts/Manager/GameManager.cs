using UnityEngine;
using Developers.Util;
using Developers.Structure;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoSingleton<GameManager>
{
    #region 기본 오브젝트
    public Camera MainCamera { get; private set; }
    public PixelPerfectCameraHelper PixelPerfectCameraHelper { get; private set; }
    #endregion

    BaseGameMode gameMode;

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

    public bool IsGameStart { get; private set; }


    public static GameObject Create<T>(ActorRecord actorRecord) where T : IActor
    {
        GameObject obj = MonoSingleton<LoadManager>.Instance.GetActor<T> ( actorRecord );
        obj?.GetComponent<T> ( ).Load ( );
        return obj;
    }

    public GamePlayer Join(/* 플레이어 정보 아직 서버가 없으므로 클라이언트에서 처리하겠음 */)
    {
        GameObject obj = new GameObject ( string.Format ( "GamePlayer({0})", gamePlayers.Count + 1 ), typeof(GamePlayer) );
        obj.transform.SetParent ( transform );

        GamePlayer com = obj.GetComponent<GamePlayer> ( );
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

        IsGameStart = false;
    }

    private IEnumerator Start ( )
    {
        #region 기본 오브젝트 초기화
        MainCamera = Camera.main;
        PixelPerfectCameraHelper = MainCamera.GetComponent<PixelPerfectCameraHelper> ( );
        #endregion

        PixelPerfectCameraHelper.UpdateResolution ( );

        // 현재 서버가 없으므로 로컬로 대체
        Join ( );

        // 현재 로딩이 없으므로
        MonoSingleton<LoadManager>.Instance.Initialize ( );

        // FIXME : 우선 로딩 시스템이 구현이 안되어있으므로...
        yield return new WaitUntil ( ( ) => MonoSingleton<LoadManager>.Instance.isInitialize );

        // TODO : 우선 다른 씬이 없으므로 대체
        GameMode = GameObject.FindGameObjectWithTag ( "GameMode" ).GetComponent<BaseGameMode> ( );
        GameMode.Load ( );
        GameMode.CurrentPage.Initialize ( );
    }
}