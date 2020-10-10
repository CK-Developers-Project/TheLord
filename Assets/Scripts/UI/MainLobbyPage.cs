using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;

public class MainLobbyPage : BasePage
{
    public ResourceCanvas ResourceUI { get; set; }


    public override void Initialize ( )
    {
        base.Initialize ( );

        ResourceUI = gameUIList.Find ( _ => _ is ResourceCanvas ) as ResourceCanvas;
        if ( ResourceUI == null )
        {
            // TODO : UI 생성
        }
    }

    public override void OnUpdate()
    {
        ResourceUI.Gold = MonoSingleton<GameManager>.Instance.LocalPlayer.GetGold ( );
        base.OnUpdate ( );
    }

    protected override void Construct ( )
    {
        OnUpdate ( );
    }

    void Update ( )
    {
        
    }
}
