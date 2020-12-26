using UnityEngine;
using TMPro;

public class OptionPopup : MonoBehaviour
{
    public void Active()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Logout()
    {

    }

    public void EndGame()
    {
        Application.Quit();
    }
}