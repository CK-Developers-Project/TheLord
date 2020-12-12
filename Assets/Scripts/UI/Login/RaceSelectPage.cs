using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Developers.Structure;
using Developers.Net.Protocol;

public class RaceSelectPage : BasePage
{
    const int Nickname_Min_Lenth = 3;
    const int Nickname_Max_Lenth = 6;

    [SerializeField] Button ElfButton = null;
    [SerializeField] Button HumanButton = null;
    [SerializeField] Button UndeadButton = null;
    [SerializeField] Button ConfirmButton = null;
    [SerializeField] TMP_InputField NicknameInputField = null;

    string Nickname { get => NicknameInputField.text; }
    Race Race { get; set; }

    public void Select(Race race)
    {
        Race = race;
    }


    public void Confirm()
    {
        UserResistrationRequest request = new UserResistrationRequest ( Nickname, (int)Race );
        int lenth = Nickname.Length;
        if ( lenth > Nickname_Max_Lenth  || lenth < Nickname_Min_Lenth )
        {
            string msg = string.Format ( "닉네임이 잘못되었습니다.\n({0} ~ {1}글자)", Nickname_Min_Lenth, Nickname_Max_Lenth );
            OnMessageBox ( msg, true, null, "확인" );
        }
        else if( Race == 0 || Race >= Race.End )
        {
            OnMessageBox ( "종족을 선택하세요.", true, null, "확인" );
        }
        request.SendPacket ( );
    }


    void Awake()
    {
        ElfButton.onClick.AddListener(() => Select(Race.Elf));
        HumanButton.onClick.AddListener(() => Select(Race.Human));
        UndeadButton.onClick.AddListener(() => Select(Race.Undead));

        ConfirmButton.onClick.AddListener ( ( ) => Confirm ( ) );
    }

}