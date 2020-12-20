using System;
using System.Collections.Generic;
using Developers.Util;
using Developers.Structure;
using Developers.Net.Protocol;
using UnityEngine;
using Developers.Table;

public class MainBuilding : Building
{


    public override void OnSelect ( )
    {
        BuildingClickRequest request = new BuildingClickRequest ( );
        request.index = (int)info.index;
        request.clickAction = ClickAction.MainBuildingTakeGold;
        request.value = 1;
        request.SendPacket ( false );

        var sheet = TableManager.Instance.BuildingTable.MainBuildingInfoSheet;
        var record = BaseTable.Get ( sheet, "index", (int)info.index );
        int value = info.LV * (int)record["nextLV"];

        GamePlayer gamePlayer = GameManager.Instance.LocalPlayer;
        gamePlayer.AddGold ( ResourceType.Gold, new BigInteger ( value ) );
        GameManager.Instance.GameMode.CurrentPage.OnUpdate ( );
    }
}
