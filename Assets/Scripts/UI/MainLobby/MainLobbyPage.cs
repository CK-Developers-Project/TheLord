using System;
using UnityEngine;
using Developers.Structure;
using Developers.Structure.Data;
using Developers.Util;
using Developers.Table;
using TMPro;

public class MainLobbyPage : BasePage
{
    public ResourceCanvas ResourceUI { get; set; }
    public TextMeshProUGUI Nickname;


    // 우선 테스트용
    [SerializeField] BuildingInfoPopup buildingInfoPopup = null;
    [SerializeField] GameObject Prefab_purchasePopup = null;
    //
    public ChatPopup chatPopup = null;

    public void OnBarrackInfo(BuildingInfo info)
    {
        buildingInfoPopup.gameObject.SetActive ( true );
        var buildingInfoSheet = TableManager.Instance.BuildingTable.BuildingInfoSheet;
        var buildingInfoRecord = BaseTable.Get ( buildingInfoSheet, "index", (int)info.index );

        var characterData = LoadManager.Instance.GetCharacterData ( (int)buildingInfoRecord["spawnCharacter"] );
        var abilityData = LoadManager.Instance.GetAbilityData ( (int)buildingInfoRecord["showAbility"] );

        buildingInfoPopup.SetTarget ( characterData, abilityData, info );
    }

    public void OnPurchaseInfo( string msg, string price, Action callback_Left, Action callback_Right )
    {
        GameObject obj = Instantiate ( Prefab_purchasePopup, transform );
        PurchasePopup popup = obj.GetComponent<PurchasePopup> ( );
        popup.OnMeesageBox ( msg, price, callback_Left, callback_Right );
        gameUIList.Add ( popup );
    }

    public override void Initialize ( )
    {
        base.Initialize ( );

        ResourceUI = gameUIList.Find ( _ => _ is ResourceCanvas ) as ResourceCanvas;

        new Developers.Net.Protocol.ResourceRequest ( ).SendPacket ( );
    }

    public override void OnUpdate()
    {
        base.OnUpdate ( );
        Nickname.text = GameManager.Instance.LocalPlayer.playerInfo.Nickname;
    }

    protected override void Construct ( )
    {
        OnUpdate ( );
    }
}
