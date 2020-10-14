using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;
using Developers.Structure;

public class BarrackBuilding : Building
{
    [SerializeField] GameObject hologram = null;
    public string msg;
    public int price;

    protected override void OnEmpty ( )
    {
        string noticeMsg = string.Format ( "{0}를 건설하시겠습니까?", msg );
        string leftMsg = string.Format("네\n-{0}", price);
        BasePage.OnMessageBox ( noticeMsg, true, BuildOK, leftMsg, true, null, "아니요" );
    }

    protected override void OnWork ( )
    {
        string noticeMsg = string.Format ( "아직 {0}가 지어지지않았습니다.", msg );
        BasePage.OnMessageBox ( noticeMsg, true, null, "확인" );
    }

    void BuildOK()
    {
        if(MonoSingleton<GameManager>.Instance.LocalPlayer.GetGold() >= price)
        {
            GamePlayer gamePlayer = MonoSingleton<GameManager>.Instance.LocalPlayer;
            gamePlayer.SetGold ( new AdditionGold ( gamePlayer.GetGold ( ) - price ) );
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
    }

    void Build()
    {
        info.state = BuildingState.Complete;
    }


    private void Start ( )
    {
        spriteRenderer.enabled = false;
        hologram.SetActive ( true );
    }
}