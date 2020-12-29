using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Structure;
using Developers.Net.Protocol;

public class RaceSelectPage : BasePage
{
    const int Nickname_Min_Lenth = 3;
    const int Nickname_Max_Lenth = 6;

    [SerializeField] AnimationButton ElfButton = null;
    [SerializeField] AnimationButton HumanButton = null;
    [SerializeField] AnimationButton UndeadButton = null;
    [SerializeField] Button ConfirmButton = null;
    [SerializeField] TMP_InputField NicknameInputField = null;

    string Nickname { get => NicknameInputField.text; }
    Race Race { get; set; }
    
    AnimationButton CurrentButton { get; set; }

    public void Select( AnimationButton button, Race race )
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        CurrentButton?.SetAnimation ( AnimationButton.State.Default );
        CurrentButton = button;
        CurrentButton.SetAnimation ( AnimationButton.State.Select );
        Race = race;
    }


    public void Confirm()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        int lenth = Nickname.Length;
        if ( lenth > Nickname_Max_Lenth  || lenth < Nickname_Min_Lenth )
        {
            string msg = string.Format ( "닉네임이 잘못되었습니다.\n({0} ~ {1}글자)", Nickname_Min_Lenth, Nickname_Max_Lenth );
            OnMessageBox ( msg, true, null, "확인" );
            return;
        }
        else if( Race == 0 || Race >= Race.End )
        {
            OnMessageBox ( "종족을 선택하세요.", true, null, "확인" );
            return;
        }
        UserResistrationRequest request = new UserResistrationRequest ( Nickname, (int)Race );
        request.SendPacket ( );
    }


    protected override void Construct ( )
    {
        SoundManager.Instance.on_music ( LoadManager.Instance.GetMusicData ( MusicType.Login ).clip );
    }


    void Awake()
    {
        ElfButton.button.onClick.AddListener ( ( ) => Select ( ElfButton, Race.Elf ) );
        HumanButton.button.onClick.AddListener ( ( ) => Select ( HumanButton, Race.Human ) );
        UndeadButton.button.onClick.AddListener ( ( ) => Select ( UndeadButton, Race.Undead ) );

        ConfirmButton.onClick.AddListener ( ( ) => Confirm ( ) );
    }

    private void OnEnable ( )
    {
        HumanButton.button.onClick.Invoke ( );
    }
}