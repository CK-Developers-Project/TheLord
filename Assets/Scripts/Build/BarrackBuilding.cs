using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;
using Developers.Structure;
using Developers.Table;
using Developers.Net.Protocol;

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
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        MainLobbyPage page = GameManager.Instance.GameMode.CurrentPage as MainLobbyPage;
        if ( page == null )
        {
            return;
        }

        var sheet = TableManager.Instance.BuildingTable.BuildingInfoSheet;
        var record = BaseTable.Get ( sheet, "index", (int)info.index );

        string noticeMsg = string.Format ( "{0}를 건설하시겠습니까?", info.name );
        BigInteger price = new BigInteger ( (int)record["cost"] );

        page.OnPurchaseInfo ( noticeMsg, GameUtility.Ordinal ( price ), BuildOK, null );
        page.OnUpdate ( );
    }

    protected override void OnWork ( )
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        string noticeMsg = string.Format ( "아직 {0}가 지어지지않았습니다.", info.name );
        BasePage.OnMessageBox ( noticeMsg, true, null, "확인" );
    }

    protected override void OnComplete ( )
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
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
        BuildingClickRequest packet = new BuildingClickRequest ( );
        packet.index = (int)info.index;
        packet.clickAction = ClickAction.BuildingBuild;
        packet.SendPacket ( true, true );
    }

    public override void BuildUp ( long targetTime, Action @event )
    {
        base.BuildUp ( targetTime, @event );
        spriteRenderer.enabled = true;
        hologram.SetActive ( false );
    }

    public override void OnBuild()
    {
        base.OnBuild ( );
        spriteRenderer.enabled = true;
        hologram.SetActive ( false );
    }
}