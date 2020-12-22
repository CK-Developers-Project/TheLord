using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using Developers.Net.Protocol;
using Developers.Structure;
using Developers.Util;

public class WorkTimer : MonoBehaviour
{
    Canvas canvas = null;
    [SerializeField] Image progressBar = null;

    TimeSpan totalTime;
    DateTime realTime;

    bool isWork = false;
    public bool isPause = false;
    public bool isComplete = false;

    public Building Owner { get; private set; }
    public Action timerEvent;

    public void Initialize()
    {
        gameObject.SetActive ( false );
    }

    public void Run(TimeSpan remainTime, TimeSpan totalTime, Action @event)
    {
        timerEvent = @event;
        Retry ( remainTime, totalTime );
    }

    public void Retry( TimeSpan remainTime, TimeSpan totalTime )
    {
        gameObject.SetActive ( true );
        isComplete = false;
        isPause = false;
        this.totalTime = totalTime;
        realTime = GameUtility.Now ( ) + remainTime;
        StartCoroutine ( Runnable ( ) );
    }

    public void Stop ( )
    {
        timerEvent?.Invoke ( );

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

        while ( !isComplete && isWork )
        {
            if( isPause )
            {
                yield return null;
            }

            TimeSpan remainTime = realTime - GameUtility.Now ( );
            TimeSpan current = totalTime - remainTime;
            progressBar.fillAmount = (float)( current.TotalSeconds / totalTime.TotalSeconds );

            if ( progressBar.fillAmount >= 1f)
            {
                isComplete = true;
            }
            yield return null;
        }

        Stop ( );
    }

    void Awake ( )
    {
        canvas = GetComponent<Canvas> ( );
        Owner = GetComponentInParent<Building> ( );
        canvas.worldCamera = GameManager.Instance.MainCamera;
    }

    void Start ( )
    {
        Initialize ( );
    }
}
