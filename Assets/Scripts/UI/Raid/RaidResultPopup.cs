using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Structure;
using Developers.Net.Protocol;

public class RaidResultPopup : BasePopup
{
    [SerializeField] AnimationCurve tween = null;

    [SerializeField] TextMeshProUGUI currentScoreText = null;
    [SerializeField] TextMeshProUGUI totalScoreText = null;

    RaidGameMode gameMode;

    IEnumerator UpdateScore(TextMeshProUGUI widget, float amount, float dur)
    {
        float time = 0F;
        while(time < dur)
        {
            time += Time.deltaTime;
            float value = time / dur;
            widget.text = ( (int)Mathf.Lerp ( 0F, amount, tween.Evaluate ( value ) ) ).ToString ( "n0" );
            yield return null;
        }
    }

    public IEnumerator ShowUI(float current, float total)
    {
        currentScoreText.text = "0";
        totalScoreText.text = "0";
        // 총 데미지 양
        yield return StartCoroutine ( UpdateScore ( currentScoreText, current, 2F ) );
        yield return StartCoroutine ( UpdateScore ( totalScoreText, current + total, 2F ) );
    }


    public void Confirm()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        var packet = new ResultRaidRankingRequest ( );
        packet.score = gameMode.score;
        packet.SendPacket ( );
        string sceneName = SceneName.GetMainLobby ( GameManager.Instance.LocalPlayer.playerInfo.Race );
        TransitionManager.Instance.OnSceneTransition ( sceneName, TransitionType.Loading01_Slide );
    }

    protected override void OnEnable ( )
    {
        base.OnEnable ( );
        gameMode = GameManager.Instance.GameMode as RaidGameMode;
        StartCoroutine ( ShowUI ( gameMode.score, gameMode.totalScore ) );
    }
}
