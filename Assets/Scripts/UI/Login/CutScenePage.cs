using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Developers.Structure;

public class CutScenePage : BasePage
{
    [SerializeField] TextMeshProUGUI storyText;
    [SerializeField] Image front_Image, back_Image, white_Image;
    [SerializeField] List<Sprite> cutSprite = new List<Sprite>();
    [SerializeField] List<string> textList = new List<string>();

    private string story { get => storyText.text; set => storyText.text = value; }
    private Vector2 startPos, endPos;
    private Vector2 vel = Vector2.zero;
    private float endTime = 3f;
    private int spriteCnt = 0;
    private int textCnt = 0;
    private bool isSkip = false;

    public override void Active()
    {
        base.Active();

        startPos = front_Image.transform.localPosition;
        endPos = startPos * -Vector2.one;

        StartCoroutine ( ActiveCutScene ( ) );
    }

    public void Skip()
    {
        if (isSkip)
            return;

        isSkip = true;
        StopAllCoroutines();

        Action action = () =>
        {
            LoginGameMode gameMode = GameManager.Instance.GameMode as LoginGameMode;
            GameManager.Instance.GameMode.CurrentPage = gameMode.raceSelectPage;
        };
        TransitionManager.Instance.OnTransition(TransitionType.Loading01_Slide, TransitionType.Loading01_Blank, action, null);
    }

    private IEnumerator ActiveCutScene()
    {
        for (int i = 0; i <= cutSprite.Count; i++)
        {
            float _endTime = endTime;

            if (textCnt < textList.Count)
            {
                story = textList[textCnt++];
                storyText.CrossFadeAlpha(1f, 0.3f, true);
            }

            if (textCnt == 1 || textCnt == 5)
            {
                StartCoroutine(StoryTextCor(3f));
            }

            yield return new WaitForSeconds(0.8f);

            while (front_Image.transform.localPosition.x > endPos.x + 0.1f)
            {
                _endTime -= 0.5f * Time.smoothDeltaTime;
                front_Image.transform.localPosition = Vector2.SmoothDamp(front_Image.transform.localPosition, endPos, ref vel, _endTime);

                yield return null;
            }

            yield return new WaitForSeconds(0.8f);

            if (textCnt < textList.Count)
                storyText.CrossFadeAlpha(0f, 0.1f, true);

            if (i == cutSprite.Count)
            {
                white_Image.gameObject.SetActive(true);
            }

            front_Image.CrossFadeAlpha(0f, 0.5f, true);
            yield return new WaitForSeconds(0.5f);

            if (spriteCnt < cutSprite.Count)
            {
                front_Image.transform.localPosition = startPos;
                front_Image.sprite = back_Image.sprite;
                front_Image.CrossFadeAlpha(1f, 0f, true);
                back_Image.sprite = cutSprite[spriteCnt++];
            }
        }

        float tempX = back_Image.rectTransform.sizeDelta.x;
        back_Image.rectTransform.sizeDelta = new Vector2(back_Image.rectTransform.sizeDelta.x + 500, back_Image.rectTransform.sizeDelta.y);

        yield return new WaitForSeconds(0.5f);
        white_Image.CrossFadeAlpha(0f, 1f, true);

        story = textList[textCnt++];
        storyText.CrossFadeAlpha(1f, 0.3f, true);

        while (back_Image.rectTransform.sizeDelta.x >= tempX)
        {
            back_Image.rectTransform.sizeDelta = new Vector2(back_Image.rectTransform.sizeDelta.x - (700 * Time.smoothDeltaTime), back_Image.rectTransform.sizeDelta.y);
            yield return null;
        }

        yield return new WaitForSeconds(4f);
        
        Action action = ( ) =>
        {
            LoginGameMode gameMode = GameManager.Instance.GameMode as LoginGameMode;
            GameManager.Instance.GameMode.CurrentPage = gameMode.raceSelectPage;
        };
        TransitionManager.Instance.OnTransition ( TransitionType.Loading01_Slide, TransitionType.Loading01_Blank, action, null );
    }

    private IEnumerator StoryTextCor(float _time)
    {
        yield return new WaitForSeconds(_time);

        storyText.CrossFadeAlpha(0f, 0.1f, true);

        yield return new WaitForSeconds(0.1f);

        story = textList[textCnt++];
        storyText.CrossFadeAlpha(1f, 0.3f, true);
    }
}
