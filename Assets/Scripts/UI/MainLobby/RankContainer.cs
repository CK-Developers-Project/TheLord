using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Structure;

public class RankContainer : MonoBehaviour, IGameUI
{
    [System.Serializable]
    public class NumberRank 
    {
        public Image rankImage = null;
        public TextMeshProUGUI rankText = null;
    }
    
    [SerializeField] TextMeshProUGUI nicknameText = null;
    [SerializeField] TextMeshProUGUI scoreText = null;
    [SerializeField] Image rankImage = null;
    [SerializeField] NumberRank numberRank = null;
    [SerializeField] Image iconImage = null;

    [SerializeField] Sprite[] rankSprites;
    [SerializeField] Sprite[] indexSprites;

    public void SetInfo ( ProtoData.RaidRankingData.RankingData rankingData, TierSprite tier )
    {
        if(rankingData == null)
        {
            nicknameText.gameObject.SetActive ( false );
            scoreText.gameObject.SetActive ( false );
            rankImage.gameObject.SetActive ( false );
            numberRank.rankImage.gameObject.SetActive ( false );
            iconImage.gameObject.SetActive ( false );
            return;
        }
        else
        {
            nicknameText.gameObject.SetActive ( true );
            scoreText.gameObject.SetActive ( true );
            rankImage.gameObject.SetActive ( true );
            numberRank.rankImage.gameObject.SetActive ( true );
            iconImage.gameObject.SetActive ( true );
        }

        nicknameText.text = rankingData.nickname;
        scoreText.text = rankingData.score.ToString ( "n0" );
        switch ( rankingData.ranking )
        {
            case 1:
                numberRank.rankImage.enabled = true;
                numberRank.rankImage.sprite = rankSprites[0];
                numberRank.rankText.gameObject.SetActive ( false );
                break;
            case 2:
                numberRank.rankImage.enabled = true;
                numberRank.rankImage.sprite = rankSprites[1];
                numberRank.rankText.gameObject.SetActive ( false );
                break;
            case 3:
                numberRank.rankImage.enabled = true;
                numberRank.rankImage.sprite = rankSprites[2];
                numberRank.rankText.gameObject.SetActive ( false );
                break;
            default:
                numberRank.rankImage.enabled = false;
                numberRank.rankText.gameObject.SetActive ( true );
                numberRank.rankText.text = rankingData.ranking.ToString ( );
                break;
        }

        if(tier != null)
            rankImage.sprite = tier.sprite;

        if( rankingData.index > 0)
            iconImage.sprite = indexSprites[rankingData.index - 1];
    }

    public void OnUpdate ( )
    {
    }
}