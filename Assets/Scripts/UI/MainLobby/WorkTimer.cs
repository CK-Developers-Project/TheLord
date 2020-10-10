using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Developers.Util;


public class WorkTimer : MonoBehaviour
{
    [SerializeField] TextMeshPro WorkTimeText = null;
    SpriteRenderer spriteRenderer = null;
    DateTime targetTime;

    bool isWork = false;

    public bool IsPause { get; set; }
    public bool IsComplete { get; set; }

    Action OnBuilidUpEvent { get; set; }
    Func<TextMeshPro, float> GetTextWidth { get; set; }


    public void Initialize()
    {
        // TODO : 아직은 서버로부터 값을 불러올게 없으므로 무조건 꺼준다.
        gameObject.SetActive ( false );
    }

    public void Run(TimeSpan timeSpan, Action callback)
    {
        gameObject.SetActive ( true );
        IsComplete = false;
        IsPause = false;
        targetTime = DateTime.Now + timeSpan;
        OnBuilidUpEvent += callback;
        StartCoroutine ( Runnable ( ) );
    }

    public void Stop ( )
    {
        isWork = false;
        gameObject.SetActive ( false );
    }


    IEnumerator Runnable()
    {
        if(isWork)
        {
            yield break;
        }
        isWork = true;

        while (!IsComplete && isWork )
        {
            if( IsPause )
            {
                yield return null;
            }

            TimeSpan current = targetTime - DateTime.Now;
            WorkTimeText.SetText(string.Format("{0}:{1:D2}", (int)current.TotalMinutes, current.Seconds));
            spriteRenderer.size = new Vector2 ( GetTextWidth ( WorkTimeText ), spriteRenderer.size.y );
            if(current.TotalSeconds < 0f)
            {
                IsComplete = true;
            }
            yield return null;
        }

        if(IsComplete)
        {
            if ( OnBuilidUpEvent != null )
            {
                OnBuilidUpEvent.Invoke ( );
                OnBuilidUpEvent = null;
            }
        }

        Stop ( );
    }

    private void Awake ( )
    {
        spriteRenderer = GetComponent<SpriteRenderer> ( );
        GetTextWidth = (x) => UILibrary.TextWidthApproximation(x);
    }

    private void Start ( )
    {
        Initialize ( );
    }
}
