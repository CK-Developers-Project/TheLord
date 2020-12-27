using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Structure;

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
                SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
                callback_Left?.Invoke ( );
                Destroy ( gameObject );
            } );

        cancelButton.onClick.AddListener (
            ( ) =>
            {
                SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
                callback_Right?.Invoke ( );
                Destroy ( gameObject );
            } );
    }

}
