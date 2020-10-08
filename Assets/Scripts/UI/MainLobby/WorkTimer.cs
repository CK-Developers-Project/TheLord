using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Developers.Util;


public class WorkTimer : MonoBehaviour
{
    [SerializeField] TextMeshPro WorkTimeText;
    SpriteRenderer spriteRenderer;
    DateTime targetTime;


    [HideInInspector] public bool IsComplete { get; set; }


    Func<TextMeshPro, float> GetTextWidth { get; set; }


    IEnumerator Runnable()
    {
        while (!IsComplete)
        {
            TimeSpan current = targetTime - DateTime.Now;
            WorkTimeText.SetText(string.Format("{0}:{1:D2}", (int)current.TotalMinutes, current.Seconds));
            spriteRenderer.size = new Vector2 ( GetTextWidth ( WorkTimeText ), spriteRenderer.size.y );
            yield return null;
        }
    }

    private void Awake ( )
    {
        spriteRenderer = GetComponent<SpriteRenderer> ( );
        GetTextWidth = (x) => UILibrary.TextWidthApproximation(x);
    }

    private void Start()
    {
        //targetTime = DateTime.Now + new TimeSpan(2,0, 10);
        //StartCoroutine(Runnable());
    }
}
