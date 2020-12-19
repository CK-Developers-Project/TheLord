using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchasePopup : BasePopup
{
    [SerializeField] TextMeshProUGUI NoticeText = null;
    [SerializeField] Button purchaseButton = null;
    [SerializeField] TextMeshProUGUI purchaseGoldText = null;
    [SerializeField] Button cancelButton = null;

    public void OnMeesageBox(string msg, string price, Action callback_Left, Action callback_Right)
    {
        NoticeText.text = msg;
        purchaseGoldText.text = price;

        purchaseButton.onClick.AddListener (
            ( ) =>
            {
                callback_Left?.Invoke ( );
                Destroy ( gameObject );
            } );

        cancelButton.onClick.AddListener (
            ( ) =>
            {
                callback_Right?.Invoke ( );
                Destroy ( gameObject );
            } );
    }

}
