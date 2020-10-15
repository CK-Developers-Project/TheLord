﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Structure;
using Developers.Util;

public class BarrackPopup : BasePopup
{
    public BarrackBuilding barrack { get; set; }
    
    [SerializeField] TextMeshProUGUI NameText = null;
    [SerializeField] TextMeshProUGUI BarrackText = null;
    [SerializeField] DevButton PurchaseButton = null;
    [SerializeField] DevButton LevelUpButton = null;
    [SerializeField] Button ExitButton = null;


    void Purchase()
    {
        int characterPrice = (int)( barrack.info.characterData.Info.price + ( barrack.info.current * ( barrack.info.characterData.Info.price * 0.15f ) ) );
        if(barrack.info.current >= barrack.info.max || GameManager.Instance.LocalPlayer.GetGold ( ) < characterPrice )
        {
            return;
        }
        GameManager.Instance.LocalPlayer.SetGold ( new AdditionGold ( GameManager.Instance.LocalPlayer.GetGold() - characterPrice ) );
        barrack.info.current++;
        GameManager.Instance.GameMode.CurrentPage.OnUpdate ( );
    }

    void LevelUp()
    {
        if( GameManager.Instance.LocalPlayer.GetGold ( ) < barrack.info.price )
        {
            return;
        }
        GameManager.Instance.LocalPlayer.SetGold ( new AdditionGold ( GameManager.Instance.LocalPlayer.GetGold() - barrack.info.price ) );
        barrack.info.price += (int)( barrack.info.price * 0.75f );
        barrack.info.level++;
        GameManager.Instance.GameMode.CurrentPage.OnUpdate ( );
    }

    void Exit()
    {
        GameManager.Instance.GameMode.CurrentPage.RemovePopup ( this );
    }


    public override void OnUpdate ( )
    {
        string nameText = string.Format ( "Lv.{0} {1}", barrack.info.level, barrack.info.name );
        string barrackText = string.Format ( "{0} {1}/{2}명 훈련중", barrack.info.characterData.rename, barrack.info.current, barrack.info.max );
        int characterPrice = (int)(barrack.info.characterData.Info.price + (barrack.info.current * ( barrack.info.characterData.Info.price * 0.15f)));
        string purchaseText = string.Format ( "구매\n{0}원", characterPrice );
        string levelUpText = string.Format ( "레벨업\n{0}원", barrack.info.price );

        NameText.SetText ( nameText );
        BarrackText.SetText ( barrackText );
        PurchaseButton.text.SetText ( purchaseText );
        LevelUpButton.text.SetText ( levelUpText );
    }

    public void Awake ( )
    {
        PurchaseButton.button.onClick.AddListener ( Purchase );
        LevelUpButton.button.onClick.AddListener ( LevelUp );
        ExitButton.onClick.AddListener ( Exit );
    }
}