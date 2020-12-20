using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using Developers.Net.Protocol;
using Developers.Structure;

public class WorkTimer : MonoBehaviour
{
    Canvas canvas = null;
    [SerializeField] Image progressBar = null;

    DateTime targetTime;

    bool isWork = false;
    public bool isPause = false;
    public bool isComplete = false;

    public Building Owner { get; private set; }
    public Action timerEvent;

    public void Initialize()
    {
        gameObject.SetActive ( false );
    }

    public void Run(DateTime completeTime, Action @event)
    {
        timerEvent = @event;
        Retry ( completeTime );
    }

    public void Retry( DateTime completeTime )
    {
        gameObject.SetActive ( true );
        isComplete = false;
        isPause = false;
        targetTime = completeTime;
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

        DateTime now = DateTime.UtcNow.ToUniversalTime();
        TimeSpan remainTime = targetTime - now;
        if ( remainTime.TotalSeconds <= 0F )
        {
            isComplete = true;
            progressBar.fillAmount = 1F;
        }

        while ( !isComplete && isWork )
        {
            if( isPause )
            {
                yield return null;
            }

            TimeSpan current = targetTime - DateTime.UtcNow.ToUniversalTime ( );
            progressBar.fillAmount = 1F - Mathf.Max ( 0F, (float)( current.TotalSeconds / remainTime.TotalSeconds ) );

            if (current.TotalSeconds < 0f)
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
