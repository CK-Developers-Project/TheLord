﻿using System;
using UnityEngine;
using UnityEngine.UI;
using Developers.Structure;
using Developers.Table;
using TMPro;

public class RaidBattlePage : BasePage
{
    [SerializeField] Image hpImage = null;

    RaidGameMode gameMode;

    [SerializeField] RaidResultPopup raidResultPopup;

    void UpdateHP()
    {
        if(gameMode == null)
        {
            gameMode = GameManager.Instance.GameMode as RaidGameMode;
        }

        if(gameMode.RaidBoss == null)
        {
            hpImage.fillAmount = 0F;
            return;
        }
        hpImage.fillAmount = gameMode.RaidBoss.Hp / gameMode.RaidBoss.status.Get ( ActorStatus.HP, true, true, true );
    }

    public void OnResultPopup()
    {
        raidResultPopup.gameObject.SetActive ( true );
    }


    public override void Initialize ( )
    {
        base.Initialize ( );
        gameMode = GameManager.Instance.GameMode as RaidGameMode;
    }

    protected override void Construct ( )
    {
        SoundManager.Instance.on_music ( LoadManager.Instance.GetMusicData ( MusicType.RaidBattle ).clip );
    }


    public override void OnUpdate ( )
    {
        base.OnUpdate ( );
        UpdateHP ( );
    }

}
