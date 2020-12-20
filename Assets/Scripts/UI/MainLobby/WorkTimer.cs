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

    public void Initialize()
    {
        canvas.worldCamera = GameManager.Instance.MainCamera;
        gameObject.SetActive ( false );
    }

    public void Run(DateTime completeTime)
    {
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
        var packet = new BuildingConfirmRequest ( );
        packet.index = (int)Owner.info.index;
        packet.confirmAction = ConfirmAction.Build;
        packet.SendPacket ( true );

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

        TimeSpan remainTime = targetTime - DateTime.Now;

        if( remainTime.TotalSeconds <= 0F )
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

            TimeSpan current = targetTime - DateTime.Now;
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
    }

    void Start ( )
    {
        Initialize ( );
    }
}
