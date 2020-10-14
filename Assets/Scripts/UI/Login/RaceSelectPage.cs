using Developers.Structure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceSelectPage : BasePage
{
    [SerializeField] Button ElfButton;
    [SerializeField] Button HumanButton;
    [SerializeField] Button UndeadButton;


    public void Select(Race race)
    {
        GameManager.Instance.LocalPlayer.playerInfo.Race = race;
        TransitionManager.Instance.OnSceneTransition(SceneName.GetMainLobby(race), TransitionType.Slide, null);
    }



    void Awake()
    {
        ElfButton.onClick.AddListener(() => Select(Race.Elf));
        HumanButton.onClick.AddListener(() => Select(Race.Human));
        UndeadButton.onClick.AddListener(() => Select(Race.Undead));
    }

}