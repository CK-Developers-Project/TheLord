using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Developers.Structure;
using Developers.Table;
using TMPro;

public class MainLobbyPage : BasePage
{
    public ResourceCanvas ResourceUI { get; set; }
    public TextMeshProUGUI Nickname;
    [SerializeField] Image tierImage = null;
    [SerializeField] List<TierSprite> tierSpriteList = new List<TierSprite> ( );

    // 우선 테스트용
    [SerializeField] BuildingInfoPopup buildingInfoPopup = null;
    [SerializeField] GameObject Prefab_purchasePopup = null;

    public RankingPopup rankingPopup = null;
    //
    public ChatPopup chatPopup = null;


    public void OnRaidButton()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        rankingPopup.gameObject.SetActive ( true );
    }

    public void SetInfoTier(TierType type)
    {
        var tierSprite = tierSpriteList.Find ( x => x.tier == type );
        if(tierSprite == null)
        {
            return;
        }
        tierImage.sprite = tierSprite.sprite;
    }

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
        
        switch(GameManager.Instance.LocalPlayer.playerInfo.Race)
        {
            case Race.Elf:
                SoundManager.Instance.on_music ( LoadManager.Instance.GetMusicData ( MusicType.MainLobby_Elf ).clip );
                break;

            case Race.Human:
                SoundManager.Instance.on_music ( LoadManager.Instance.GetMusicData ( MusicType.MainLobby_Human ).clip );
                break;

            case Race.Undead:
                SoundManager.Instance.on_music ( LoadManager.Instance.GetMusicData ( MusicType.MainLobby_Undead ).clip );
                break;
        }
    }
}
