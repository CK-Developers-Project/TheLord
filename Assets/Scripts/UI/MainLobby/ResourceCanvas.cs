using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceCanvas : MonoBehaviour, IGameUI
{
    [SerializeField] TextMeshProUGUI DisplayText = null;

    // FIXME : 한국어 밖에 없으므로 자원 고정
    string ResourceText = "자원";
    // FIXME  : 서버로부터 골드를 가져와야 함으로 우선은 어디서든 접근 가능하도록 설정한다.
    public int Gold { get; set; }

    public void OnUpdate()
    {
        DisplayText.SetText ( string.Format ( "{0}: {1}", ResourceText, Gold ) );
    }
}