using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WorkTimer : MonoBehaviour
{
    [SerializeField] TextMeshPro WorkTimeText;

    [HideInInspector] public bool IsComplete { get; set; }

    DateTime targetTime;

    IEnumerator Runnable()
    {
        while (!IsComplete)
        {
            TimeSpan current = targetTime - DateTime.Now;
            WorkTimeText.SetText(string.Format("{0:D2}:{1:D2}", current.Minutes, current.Seconds));
            yield return null;
        }
    }


    private void Start()
    {
        targetTime = DateTime.Now + new TimeSpan(0, 5, 10);
        StartCoroutine(Runnable());
    }

}
