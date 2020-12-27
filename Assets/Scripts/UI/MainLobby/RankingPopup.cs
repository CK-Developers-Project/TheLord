using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Developers.Structure;
using Developers.Net.Protocol;
using UnityEngine.UI;
using Developers.Table;

public class RankingPopup : BasePopup
{
    [SerializeField] GameObject Prefab_RankContainer = null;
    [SerializeField] RectTransform content = null;
    [SerializeField] Image HPImage = null;

    [SerializeField] RankContainer myRanking = null;
    [SerializeField] RankContainer lastHitRanking = null;
    public List<RankContainer> rankContainers = new List<RankContainer> ( );
    [SerializeField] List<TierSprite> tierSpriteList = new List<TierSprite> ( );

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
        gameObject.SetActive ( false );
    }

    public void Raid()
    {
        var packet = new EnterContentRequest ( );
        packet.SendPacket ( );
    }

    public void Reward()
    {
        // 여긴 아마 없을듯?
    }

    protected override void OnEnable ( )
    {
        base.OnEnable ( );
        var packet = new RequestRaidRankingRequest ( );
        packet.SendPacket ( );
    }

    public override void OnUpdate()
    {

    }


}
