using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Developers.Util;

public class LoginPage : BasePage
{
    delegate void ConfirmEvent(string id, string pwd);

    #region Platform Class
    interface IPlatform
    {
        void AddEvent(ConfirmEvent @event);
    }

    [Serializable]
    class Tester : IPlatform
    {
        public TMP_InputField IDinputField = null;
        public Button loginButton = null;
        public string ID {get => IDinputField.text;}
        event ConfirmEvent confirmEvent;

        public void AddEvent(ConfirmEvent @event)
        {
            loginButton.onClick.AddListener(Confirm);
            confirmEvent += @event;
        }
        
        void Confirm()
        {
            // TODO : Tester 전용 서버 연결
            confirmEvent.Invoke(ID, "");
        }
    }

    [Serializable]
    class AOS : IPlatform
    {
        public event ConfirmEvent confirmEvent;
        public void AddEvent(ConfirmEvent @event)
        {
            confirmEvent += @event;
        }
    }

    [Serializable]
    class IOS : IPlatform
    {
        public event ConfirmEvent confirmEvent;
        public void AddEvent(ConfirmEvent @event)
        {
            confirmEvent += @event;
        }
    }
    #endregion

    [SerializeField] Tester platformTester = null;
    [SerializeField] AOS platformAOS = null;
    [SerializeField] IOS platformIOS = null;
    IPlatform Current { get; set; }

    public void Join(string id, string pwd)
    {
        // TODO : 플레이어 검사
        GamePlayer player = MonoSingleton<GameManager>.Instance.Join();
    }

    public override void Initialize ( )
    {
        base.Initialize ( );
    }

    protected override void Construct ( )
    {
#if UNITY_ANDROID
        
#elif UNITY_IOS
        
#else
        Current = platformTester;
#endif
        Current.AddEvent(Join);
    }

}