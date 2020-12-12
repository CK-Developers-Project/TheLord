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
        GamePlayer gamePlayer = GameManager.Instance.LocalPlayer;
        gamePlayer.AddGold ( ResourceType.Gold, new BigInteger ( 10 ) );
        GameManager.Instance.GameMode.CurrentPage.OnUpdate ( );
    }
}
