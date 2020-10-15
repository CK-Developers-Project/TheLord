using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;

public class MainLobbyPage : BasePage
{
    public ResourceCanvas ResourceUI { get; set; }

    // 우선 테스트용
    [SerializeField] GameObject Prefab_BarrackPopup = null;

    public void OnBarrackInfo(BarrackBuilding barrack)
    {
        GameObject obj = Instantiate ( Prefab_BarrackPopup, GameManager.Instance.GameMode.CurrentPage.transform );
        BarrackPopup popup = obj.GetComponent<BarrackPopup> ( );
        popup.barrack = barrack;
        gameUIList.Add ( popup );
    }

    public override void Initialize ( )
    {
        base.Initialize ( );

        ResourceUI = gameUIList.Find ( _ => _ is ResourceCanvas ) as ResourceCanvas;
    }

    public override void OnUpdate()
    {
        ResourceUI.Gold = GameManager.Instance.LocalPlayer.GetGold ( );
        base.OnUpdate ( );
    }

    protected override void Construct ( )
    {
        OnUpdate ( );
    }
}
