using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;
using Developers.Net.Protocol;
using UnityEngine.UI;
using Developers.Util;
using Developers.Table;
using TMPro;
using System;

public class RankingPopup : BasePopup
{
    [SerializeField] GameObject Prefab_RankContainer = null;
    [SerializeField] RectTransform content = null;
    [SerializeField] Image HPImage = null;
    [SerializeField] GameObject TutorialPopup;
    [SerializeField] GameObject RaidTutorialPoup01;
    [SerializeField] GameObject RaidTutorialPoup02;
    [SerializeField] GameObject RewardTutorialPoup01;
    [SerializeField] GameObject RewardTutorialPoup02;

    [SerializeField] TextMeshProUGUI remainTimeText = null;
    [SerializeField] RankContainer myRanking = null;
    [SerializeField] RankContainer lastHitRanking = null;
    public List<RankContainer> rankContainers = new List<RankContainer> ( );
    [SerializeField] List<TierSprite> tierSpriteList = new List<TierSprite> ( );

    DateTime remainTime;

    public void SetRemainTime(long tick)
    {
        remainTime = GameUtility.Now() + new TimeSpan ( tick );
    }

    public void UpdateRemainTime(TimeSpan time)
    {
        string day = time.Days.ToString ( );
        string hour = time.Hours.ToString ( );
        string min = time.Minutes.ToString ( );
        remainTimeText.text = string.Format ( "{0}일 {1}시간 {2}분 남음", day, hour, min );
    }

    public void SetInfoForMyRanking(ProtoData.RaidRankingData.RankingData rankingData)
    {
        myRanking.SetInfo ( rankingData, tierSpriteList.Find(x=>(int)x.tier == rankingData.tier) );
    }

    public void SetInfoForLastHitRnaking( ProtoData.RaidRankingData.RankingData rankingData )
    {
        if( rankingData != null)
            lastHitRanking.SetInfo ( rankingData, tierSpriteList.Find ( x => (int)x.tier == rankingData.tier ) );
        else
            lastHitRanking.SetInfo ( null, null );
    }

    public void SetInfo(List<ProtoData.RaidRankingData.RankingData> rankingDataList)
    {
        for ( int i = 0; i < content.childCount; i++ ) 
        { 
            Destroy ( content.GetChild ( i ).gameObject ); 
        }
        content.DetachChildren ( );

        foreach (var rankingData in rankingDataList)
        {
            GameObject obj = Instantiate ( Prefab_RankContainer, content );
            RankContainer container = obj.GetComponent<RankContainer> ( );
            container.SetInfo ( rankingData, tierSpriteList.Find ( x => (int)x.tier == rankingData.tier ) );
        }
    }

    public void SetHP(int current, int index)
    {
        var sheet = TableManager.Instance.CharacterTable.CharacterInfoSheet;
        var record = BaseTable.Get ( sheet, "index", index );
        float hp = (float)record["hp"];
        HPImage.fillAmount = current / hp;
    }

    public void Cancel()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        gameObject.SetActive ( false );
        switch ( GameManager.Instance.LocalPlayer.playerInfo.Race )
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

    public void Raid()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        var packet = new EnterContentRequest ( );
        packet.SendPacket ( );
    }

    public void Reward()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        // 여긴 아마 없을듯?
    }

    public void RaidTutorial()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        TutorialPopup.SetActive ( true );
        RaidTutorialPoup01.SetActive ( true );
        RaidTutorialPoup02.SetActive ( false );
        RewardTutorialPoup01.SetActive ( false );
        RewardTutorialPoup02.SetActive ( false );
    }

    public void RaidTutorialNotice()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        RaidTutorialPoup01.SetActive ( false );
        RaidTutorialPoup02.SetActive ( true );
    }

    public void RewardTutorial ( )
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        TutorialPopup.SetActive ( true );
        RaidTutorialPoup01.SetActive ( false );
        RaidTutorialPoup02.SetActive ( false );
        RewardTutorialPoup01.SetActive ( true );
        RewardTutorialPoup02.SetActive ( false );
    }

    public void RewardTutorialNotice ( )
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        RewardTutorialPoup01.SetActive ( false );
        RewardTutorialPoup02.SetActive ( true );
    }

    public void TutorialCancle ( )
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        TutorialPopup.SetActive ( false );
    }

    protected override void OnEnable ( )
    {
        base.OnEnable ( );
        var packet = new RequestRaidRankingRequest ( );
        packet.SendPacket ( );

        SoundManager.Instance.on_music ( LoadManager.Instance.GetMusicData ( MusicType.RaidLobby ).clip );
    }

    public override void OnUpdate()
    {
    }

    private void Update ( )
    {
        UpdateRemainTime ( remainTime - GameUtility.Now() );
    }
}
