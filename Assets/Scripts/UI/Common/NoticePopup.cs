using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Structure;

public class NoticePopup : BasePopup
{

    [SerializeField] private TextMeshProUGUI NoticeText = null;
    [SerializeField] private DevButton OKButton = null;
    [SerializeField] private DevButton CancelButton = null;


    public void OnMessage ( string msg )
    {

    }


}