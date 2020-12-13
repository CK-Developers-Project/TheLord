using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;
using Developers.Structure;

public class BarrackBuilding : Building
{
    [SerializeField] GameObject hologram = null;

    protected override void OnEmpty ( )
    {
        //string noticeMsg = string.Format ( "{0}를 건설하시겠습니까?", msg );
        //string leftMsg = string.Format("네\n-{0}", price);
        //BasePage.OnMessageBox ( noticeMsg, true, BuildOK, leftMsg, true, null, "아니요" );
    }

    protected override void OnWork ( )
    {
        //string noticeMsg = string.Format ( "아직 {0}가 지어지지않았습니다.", msg );
        //BasePage.OnMessageBox ( noticeMsg, true, null, "확인" );
    }

    protected override void OnComplete ( )
    {
        MainLobbyPage page = GameManager.Instance.GameMode.CurrentPage as MainLobbyPage;
        if( page == null)
        {
            return;
        }
        page.OnBarrackInfo ( this );
        page.OnUpdate ( );
    }

    void BuildOK()
    {
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

    void Build()
    {
        info.state = BuildingState.Complete;
    }


    protected override void Start ( )
    {
        spriteRenderer.enabled = false;
        hologram.SetActive ( true );
    }
}