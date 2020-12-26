using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatData : MonoBehaviour
{
    public TextMeshProUGUI nickText;
    public TextMeshProUGUI chatText;
    public Image userImage;
    public Image chatImage;

    public void Initialize(string nick, string chat, ChatPopup.ChatStyle style, bool isMine = false)
    {
        if (isMine)
        {
            chatImage.color = style.myColor;
        }
        else
        {
            userImage.sprite = style.icon;
            chatImage.color = style.otherColor;
        }
        
        nickText.text = nick;
        chatText.text = chat;
    }
}
