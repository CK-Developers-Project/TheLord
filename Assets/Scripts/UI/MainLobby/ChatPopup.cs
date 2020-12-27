using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Developers.Net.Protocol;
using TMPro;

public class ChatPopup : MonoBehaviour
{
    [Serializable]
    public class ChatStyle
    {
        public Sprite icon;
        public Color otherColor;
        public Color myColor;
    }

    public List<ChatStyle> chatStyles = new List<ChatStyle>();
    public Transform chatGrid;
    public GameObject otherChat;
    public GameObject myChat;
    public GameObject chatBG;
    public TMP_InputField msg;
    public ScrollRect scroll;

    private bool isTime = false;

    public void Active()
    {
        chatBG.SetActive(!chatBG.activeSelf);
        StartCoroutine(FocusChat());
    }

    public void SendChat()
    {
        string chat = msg.text;

        if (isTime || string.IsNullOrEmpty(chat))
        {
            return;
        }

        if( chat.IndexOf ( "/slomo" ) != 0 )
        {
            Time.timeScale = int.Parse ( chat.Substring ( 7 ) );
            return;
        }

            var info = GameManager.Instance.LocalPlayer.playerInfo;
        var packet = new SendChat();
        packet.index = (int)info.Race - 1;
        packet.nick = info.Nickname;
        packet.msg = chat;
        packet.SendPacket(true);

        isTime = true;
        msg.text = "";

        if (chat.IndexOf("/gold") != 0)
        {
            Instantiate(myChat, chatGrid).GetComponent<ChatData>().Initialize(info.Nickname, chat, chatStyles[(int)info.Race - 1], true);
        }
        
        StartCoroutine(Timer(0.5f));
        StartCoroutine(FocusChat());
    }

    public void AddChat(int index, string nick, string msg)
    {
        Instantiate(otherChat, chatGrid).GetComponent<ChatData>().Initialize(nick, msg, chatStyles[index]);
        StartCoroutine(FocusChat());
    }

    private IEnumerator FocusChat()
    {
        yield return new WaitForEndOfFrame();
        scroll.verticalNormalizedPosition = 0f; // 스크롤 아래로 내려줌
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        isTime = false;
    }
}