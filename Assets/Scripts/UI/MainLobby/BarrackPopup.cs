using UnityEngine;
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
        GamePlayer gamePlayer = GameManager.Instance.LocalPlayer;
        /*
        int characterPrice = (int)( barrack.info.characterData.Info.price + ( barrack.info.current * ( barrack.info.characterData.Info.price * 0.15f ) ) );
        if(barrack.info.current >= barrack.info.max || gamePlayer.GetGold ( ResourceType.Gold ) < characterPrice )
        {
            return;
        }
        gamePlayer.AddGold ( ResourceType.Gold, -characterPrice );
        barrack.info.current++;
        */
        GameManager.Instance.GameMode.CurrentPage.OnUpdate ( );
    }

    void LevelUp()
    {
        GamePlayer gamePlayer = GameManager.Instance.LocalPlayer;
        /*
        if ( gamePlayer.GetGold ( ResourceType.Gold ) < barrack.info.price )
        {
            return;
        }
        gamePlayer.AddGold ( ResourceType.Gold, -barrack.info.price );
        barrack.info.price += (int)( barrack.info.price * 0.75f );
        barrack.info.LV++;
        */
        GameManager.Instance.GameMode.CurrentPage.OnUpdate ( );
    }

    void Exit()
    {
        GameManager.Instance.GameMode.CurrentPage.RemovePopup ( this );
    }


    public override void OnUpdate ( )
    {
        /*
        string nameText = string.Format ( "Lv.{0} {1}", barrack.info.LV, barrack.info.name );
        string barrackText = string.Format ( "{0} {1}/{2}명 훈련중", barrack.info.characterData.Info.name, barrack.info.current, barrack.info.max );
        int characterPrice = (int)( barrack.info.characterData.Info.price + ( barrack.info.current * ( barrack.info.characterData.Info.price * 0.15f ) ) );
        string purchaseText = string.Format ( "구매\n{0}원", characterPrice );
        string levelUpText = string.Format ( "레벨업\n{0}원", barrack.info.price );

        NameText.SetText ( nameText );
        BarrackText.SetText ( barrackText );
        PurchaseButton.text.SetText ( purchaseText );
        LevelUpButton.text.SetText ( levelUpText );
        */
    }

    public void Awake ( )
    {
        PurchaseButton.button.onClick.AddListener ( Purchase );
        LevelUpButton.button.onClick.AddListener ( LevelUp );
        ExitButton.onClick.AddListener ( Exit );
    }
}
