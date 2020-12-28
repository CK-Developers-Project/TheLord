using System;
using System.Collections.Generic;
using Developers.Util;
using Developers.Structure;
using Developers.Net.Protocol;
using UnityEngine;
using Developers.Table;

public class MainBuilding : Building
{
    BigInteger possiveNextLevelGold = new BigInteger();

    public override void Load ( )
    {
        base.Load ( );

        var sheet = TableManager.Instance.BuildingTable.BuildCostSheet;
        float costRate = 0;

        foreach ( var st in sheet )
        {
            if ( (int)st["LV"] > info.LV )
            {
                break;
            }
            costRate = (float)st["mainBuildingRate"];
        }
        possiveNextLevelGold = new BigInteger ( (int)( info.LV * (int)MyRecord["nextLV"] * costRate ) );
    }

    public override void OnUpdate ( )
    {
        if(info.state == BuildingState.Complete && !tooltipBox.gameObject.activeInHierarchy)
        {
            if ( Owner.GetGold ( ResourceType.Gold ) >= possiveNextLevelGold )
            {
                tooltipBox.OnTooltip ( "레벨업 가능!", ( ) => LevelUP ( ) );
            }
        }
        else
        {
            if( tooltipBox.gameObject.activeInHierarchy && Owner.GetGold ( ResourceType.Gold ) < possiveNextLevelGold )
            {
                tooltipBox.End ( );
            }
        }
    }

    public override void OnSelect ( )
    {
        AudioClip clip = LoadManager.Instance.GetSFXData ( SFXType.Coin ).clip;
        Audio.play ( clip, 1F, 0F, 1F );

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

    public override void BuildUp ( long targetTime, Action @event = null )
    {
        base.BuildUp ( targetTime, @event );

        if ( tooltipBox.gameObject.activeInHierarchy )
        {
            tooltipBox.End ( );
        }
    }

    public override void OnBuild ( )
    {
        base.OnBuild ( );

        var sheet = TableManager.Instance.BuildingTable.BuildCostSheet;
        float costRate = 0;

        foreach ( var st in sheet )
        {
            if ( (int)st["LV"] > info.LV )
            {
                break;
            }
            costRate = (float)st["mainBuildingRate"];
        }
        possiveNextLevelGold = new BigInteger ( (int)( info.LV * (int)MyRecord["nextLV"] * costRate ) );
    }


    void LevelUP()
    {
        var packet = new BuildingClickRequest ( );
        packet.index = (int)info.index;
        packet.clickAction = ClickAction.BuildingLevelUp;
        packet.SendPacket ( true, true );
        tooltipBox.End ( );
    }

    protected override void Awake ( )
    {
        base.Awake ( );
        tooltipBox.OnTooltip ( "레벨업 가능!", ( ) => LevelUP ( ) );
    }
}
