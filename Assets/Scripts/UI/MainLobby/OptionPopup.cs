using UnityEngine;
using TMPro;
using Developers.Structure;

public class OptionPopup : BasePopup
{
    public void Active()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Logout()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        Developers.Net.PhotonEngine.Instance.Disconnect();
    }

    public void MainLobby()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        string sceneName = SceneName.GetMainLobby ( GameManager.Instance.LocalPlayer.playerInfo.Race );
        TransitionManager.Instance.OnSceneTransition ( sceneName, TransitionType.Loading01_Blank, null );
    }

    public void EndGame()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        Application.Quit();
    }
}