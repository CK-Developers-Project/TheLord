using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Structure;

public class NoticePopup : BasePopup
{

    [SerializeField] private TextMeshProUGUI NoticeText = null;
    [SerializeField] private DevButton LeftButton = null;
    [SerializeField] private DevButton RightButton = null;


    public void OnMessageBox ( string msg, bool action_Left, Action callback_Left, string leftMsg, bool action_Right, Action callback_Right = null, string rightMsg = "" )
    {
        NoticeText.text = msg;

        LeftButton.button.gameObject.SetActive ( action_Left );
        LeftButton.button.onClick.AddListener (
            ( ) => {
                callback_Left?.Invoke ( );
                Destroy ( gameObject );
            }
            );
        LeftButton.text.text = leftMsg;

        RightButton.button.gameObject.SetActive ( action_Right );
        RightButton.button.onClick.AddListener (
            ( ) => {
                callback_Right?.Invoke ( );
                Destroy ( gameObject );
            }
            );
        RightButton.text.text = rightMsg;
    }

    private void Start ( )
    {
        
    }
}