using System;
using System.Collections.Generic;
using Developers.Util;
using Developers.Structure;
using UnityEngine;

public class MainBuilding : Building
{


    public override void OnSelect ( )
    {
        // 돈이 들어오도록 하자

        GamePlayer gamePlayer = MonoSingleton<GameManager>.Instance.LocalPlayer;
        gamePlayer.SetGold ( new AdditionGold ( gamePlayer.GetGold ( ) + 10 ) );
        MonoSingleton<GameManager>.Instance.GameMode.CurrentPage.OnUpdate ( );
    }
}
