using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;
using Developers.Structure;
using Developers.Table;

public class BarrackBuilding : Building
{
    [SerializeField] GameObject hologram = null;

    public override void Load ( )
    {
        base.Load ( );

        switch ( info.state )
        {
            case BuildingState.Empty:
            {
                spriteRenderer.enabled = false;
                hologram.SetActive ( true );
            }
            break;
        }
    }

    protected override void OnEmpty ( )
    {
        MainLobbyPage page = GameManager.Instance.GameMode.CurrentPage as MainLobbyPage;
        if ( page == null )
        {
            return;
        }

        var sheet = TableManager.Instance.BuildingTable.BuildingInfoSheet;
        var record = BaseTable.Get ( sheet, "index", (int)info.index );

        string noticeMsg = string.Format ( "{0}를 건설하시겠습니까?", info.name );
        BigInteger price = new BigInteger ( (int)record["cost"] );

        page.OnPurchaseInfo ( noticeMsg, Utility.Ordinal ( price ), BuildOK, null );
        page.OnUpdate ( );
    }

    protected override void OnWork ( )
    {
        string noticeMsg = string.Format ( "아직 {0}가 지어지지않았습니다.", info.name );
        BasePage.OnMessageBox ( noticeMsg, true, null, "확인" );
    }

    protected override void OnComplete ( )
    {
        MainLobbyPage page = GameManager.Instance.GameMode.CurrentPage as MainLobbyPage;
        if( page == null)
        {
            return;
        }

        page.OnBarrackInfo ( info );
        page.OnUpdate ( );
    }

    void BuildOK()
    {
        info.state = BuildingState.Complete;
        spriteRenderer.enabled = true;
        hologram.SetActive ( false );

        /*
        if(MonoSingleton<GameManager>.Instance.LocalPlayer.GetGold(ResourceType.Gold) >= price)
        {
            GamePlayer gamePlayer = MonoSingleton<GameManager>.Instance.LocalPlayer;
            gamePlayer.AddGold ( ResourceType.Gold, new BigInteger ( -price ) );
            MonoSingleton<GameManager>.Instance.GameMode.CurrentPage.OnUpdate ( );
        }
        else
        {
            return;
        }

        info.state = BuildingState.Work;
        BuildUp ( new TimeSpan ( 0, 0, 10 ), Build );
        spriteRenderer.enabled = true;
        hologram.SetActive ( false );
        info.LV = 1;
        */
    }

    protected override void OnBuild()
    {
        base.OnBuild ( );

    }
}