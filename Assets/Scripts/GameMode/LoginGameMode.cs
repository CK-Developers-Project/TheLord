using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;


public class LoginGameMode : BaseGameMode
{
    public LoginPage loginPage = null;
    public RaceSelectPage raceSelectPage = null;
    public CutScenePage cutScenePage = null;


    // 코어 로드
    public override void Load ( )
    {
        GameManager.Instance.OnStart ( );
    }


    public override void OnEnter ( )
    {
        SetPage ( loginPage );
    }
}
