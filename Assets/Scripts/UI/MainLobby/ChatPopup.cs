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
    }

    public void SendChat()
    {
        string chat = msg.text;

        if (isTime || string.IsNullOrEmpty(chat))
        {
            return;
        }

        var packet = new SendChat();
        packet.index = 0;
        packet.nick = GameManager.Instance.LocalPlayer.playerInfo.Nickname;
        packet.msg = chat;
        packet.SendPacket(true);
        
        isTime = true;
        msg.text = "";
        Instantiate(myChat, chatGrid).GetComponent<ChatData>().Initialize(GameManager.Instance.LocalPlayer.playerInfo.Nickname, chat);
        
        StartCoroutine(Timer(0.5f));
        StartCoroutine(FocusChat());
    }

    public void AddChat(int index, string nick, string msg)
    {
        Instantiate(myChat, chatGrid).GetComponent<ChatData>().Initialize(nick, msg);
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