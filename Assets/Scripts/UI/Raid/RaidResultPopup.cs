using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Structure;

public class RaidResultPopup : BasePopup
{
    [SerializeField] AnimationCurve tween = null;

    [SerializeField] TextMeshProUGUI currentScoreText = null;
    [SerializeField] TextMeshProUGUI totalScoreText = null;


    IEnumerator UpdateScore(TextMeshProUGUI widget, float amount, float dur)
    {
        float time = 0F;
        while(time > dur)
        {
            time += Time.deltaTime;
            float value = time / dur;
            widget.text = ( (int)Mathf.Lerp ( 0F, amount, tween.Evaluate ( value ) ) ).ToString ( "n0" );
            yield return null;
        }
    }

    IEnumerator ShowUI(float current, float total)
    {
        // 총 데미지 양
        yield return StartCoroutine ( UpdateScore ( currentScoreText, current, 2F ) );
        yield return StartCoroutine ( UpdateScore ( totalScoreText, total, 2F ) );
    }


    public void Confirm()
    {
        // TODO : 서버한테 보냄
        // 메인메뉴로
        string sceneName = SceneName.GetMainLobby ( GameManager.Instance.LocalPlayer.playerInfo.Race );
        TransitionManager.Instance.OnSceneTransition ( sceneName, TransitionType.Loading01_Slide );
    }
}
