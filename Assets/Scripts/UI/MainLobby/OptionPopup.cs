using UnityEngine;
using TMPro;
using Developers.Structure;

public class OptionPopup : MonoBehaviour
{
    public void Active()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Logout()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );

    }

    public void EndGame()
    {
        SoundManager.Instance.play ( LoadManager.Instance.GetSFXData ( SFXType.Tabsound ).clip, AudioSettings.dspTime + Time.deltaTime, 0F, 1F );
        Application.Quit();
    }
}