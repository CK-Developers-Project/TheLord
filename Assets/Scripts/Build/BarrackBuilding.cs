using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;
using Developers.Structure;
using Developers.Table;
using Developers.Net.Protocol;

public class BarrackBuilding : Building
{
    BuildingBalloon balloon = null;

    public override void Load ( )
    {
        base.Load ( );
        
        if(info.state == BuildingState.Empty)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }

    }


    public override void OnUpdate ( )
    {
        if ( info.state == BuildingState.Empty && !balloon.isActive )
        {
            // 현재 티어가 가능한 티어라면 조건 추가 필요함
            balloon.isActive = true;
        }
        else if( info.state != BuildingState.Empty && balloon.isActive )
        {
            balloon.isActive = false;
        }
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

    public override void BuildUp ( long targetTime, Action @event )
    {
        base.BuildUp ( targetTime, @event );
        spriteRenderer.enabled = true;
    }

    public override void OnBuild()
    {
        base.OnBuild ( );
        spriteRenderer.enabled = true;
    }

    protected override void Awake ( )
    {
        base.Awake ( );
        balloon = GetComponentInChildren<BuildingBalloon> ( true );
    }
}