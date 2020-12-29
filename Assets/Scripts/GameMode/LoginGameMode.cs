using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;
using Developers.Structure;

public class LoginGameMode : BaseGameMode
{
    public LoginPage loginPage = null;
    public RaceSelectPage raceSelectPage = null;
    public CutScenePage cutScenePage = null;

    // 코어 로드
    public override void Load ( )
    {
        base.Load ( );
    }


    public override void OnEnter ( )
    {
        GameManager.Instance.gamePlayers.Clear ( );
        for ( int i = 0; i < GameManager.Instance.transform.childCount; ++i )
        {
            Destroy ( GameManager.Instance.transform.GetChild ( i ).gameObject );
        }

        SetPage ( loginPage );
    }
}
