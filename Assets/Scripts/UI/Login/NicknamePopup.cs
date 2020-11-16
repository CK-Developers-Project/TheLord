using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Util;

public class NicknamePopup : BasePopup
{
    [SerializeField] TMP_InputField IDinputField = null;
    [SerializeField] Button loginButton = null;
    public string Nickname { get => IDinputField.text; }

    public void Confirm()
    {
        if(Nickname.Length < 2)
        {
            BasePage.OnMessageBox ( "닉네임은 3글자 이상이여야 합니다.", true, null, "확인" );
            return;
        }

        GameManager.Instance.LocalPlayer.playerInfo.Nickname = Nickname;
        LoginGameMode gameMode = GameManager.Instance.GameMode as LoginGameMode;
        gameMode.SetPage ( gameMode.raceSelectPage );
    }

    private void Awake ( )
    {
        loginButton.onClick.AddListener ( Confirm );
    }
}
