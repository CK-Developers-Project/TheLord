using UnityEngine;
using Developers.Util;
using Developers.Structure;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoSingleton<GameManager>
{
    #region 기본 오브젝트
    public Camera MainCamera { get; private set; }
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

    public void OnStart()
    {
        IsGameStart = true;
    }


    protected override void Awake ( )
    {
        base.Awake ( );

        IsGameStart = false;
    }

    private void Start ( )
    {
        #region 기본 오브젝트 초기화
        MainCamera = Camera.main;
        #endregion

        // TODO : 로그인 씬이 없으므로 여기에서 로컬 플레이어 넣어줌
        GamePlayer.Initialize ( );

        // TODO : 우선 다른 씬이 없으므로 대체
        GameMode = GameObject.FindGameObjectWithTag ( "GameMode" ).GetComponent<BaseGameMode> ( );
        GameMode.Load ( );
    }
}