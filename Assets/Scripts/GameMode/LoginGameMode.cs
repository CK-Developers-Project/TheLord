using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;


public class LoginGameMode : BaseGameMode
{
    public LoginPage loginPage = null;
    public RaceSelectPage raceSelectPage = null;


    // 코어 로드
    public override void Load ( )
    {
        MonoSingleton<GameManager>.Instance.OnStart ( );
    }


    public override void OnEnter ( )
    {
        SetPage ( loginPage );
    }
}
