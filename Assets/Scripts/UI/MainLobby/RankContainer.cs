using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] Sprite[] rankSprites;

    public string nickname;
    public int rank;
    public int score;


    public void SetInfo ( string nickname, int rank, int score )
    {
        this.nickname = nickname;
        this.rank = rank;
        this.score = score;
    }

    public void OnUpdate ( )
    {
        nicknameText.text = nickname;
        scoreText.text = score.ToString ( "n0" );

        switch(rank)
        {
            case 1:
                numberRank.rankImage.gameObject.SetActive ( true );
                numberRank.rankImage.sprite = rankSprites[0];
                numberRank.rankText.gameObject.SetActive ( false );
                break;
            case 2:
                numberRank.rankImage.gameObject.SetActive ( true );
                numberRank.rankImage.sprite = rankSprites[1];
                numberRank.rankText.gameObject.SetActive ( false );
                break;
            case 3:
                numberRank.rankImage.gameObject.SetActive ( true );
                numberRank.rankImage.sprite = rankSprites[2];
                numberRank.rankText.gameObject.SetActive ( false );
                break;
            default:
                numberRank.rankImage.gameObject.SetActive ( false );
                numberRank.rankText.gameObject.SetActive ( true );
                numberRank.rankText.text = rank.ToString();
                break;
        }
    }
}