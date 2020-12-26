using System;
using UnityEngine;
using UnityEngine.UI;
using Developers.Structure;
using Developers.Table;
using TMPro;

public class RaidBattlePage : BasePage
{
    [SerializeField] Image hpImage = null;

    RaidGameMode gameMode;

    void UpdateHP()
    {

    }


    public override void Initialize ( )
    {
        base.Initialize ( );
        gameMode = GameManager.Instance.GameMode as RaidGameMode;
    }


    public override void OnUpdate ( )
    {
        base.OnUpdate ( );
        UpdateHP ( );
    }

}
