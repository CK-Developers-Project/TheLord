using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatData : MonoBehaviour
{
    public TextMeshProUGUI nickText;
    public TextMeshProUGUI chatText;
    public Image userImage;

    public void Initialize(string nick, string chat, Sprite icon = null)
    {
        if (icon != null)
            userImage.sprite = icon;

        nickText.text = nick;
        chatText.text = chat;
    }
}
