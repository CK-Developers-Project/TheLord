using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Developers.Util;
using Developers.Net;
using Developers.Net.Protocol;
using System.Collections;

public class LoginPage : BasePage
{
    delegate void ConfirmEvent(string id, string pwd);

    #region Platform Class
    interface IPlatform
    {
        void Active ( bool active );
        void AddEvent(ConfirmEvent @event);
    }

    [Serializable]
    class Tester : IPlatform
    {
        public GameObject canvas = null;
        public TMP_InputField IDinputField = null;
        public Button loginButton = null;
        public string ID {get => IDinputField.text;}
        event ConfirmEvent confirmEvent;

        public void Active ( bool active )
        {
            canvas.SetActive ( true );
        }

        public void AddEvent(ConfirmEvent @event)
        {
            loginButton.onClick.AddListener(Confirm);
            confirmEvent += @event;
        }
        
        void Confirm()
        {
            confirmEvent.Invoke(ID, "");
        }
    }
    #endregion

    [SerializeField] Tester platformTester = null;
    //[SerializeField] AOS platformAOS = null;
    //[SerializeField] IOS platformIOS = null;

    IPlatform Current { get; set; }
    

    public void Join(string id, string pwd)
    {
        LoginRequest request = new LoginRequest ( id, pwd );
        request.SendPacket ( );
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
#endif
        Current = platformTester;
        Current.AddEvent(Join);
        StartCoroutine ( OnConnect ( ) );
    }

    protected override void Hidden ( )
    {
        StopAllCoroutines ( );
    }


    IEnumerator OnConnect()
    {
        while ( !PhotonEngine.Instance.isServerConnect )
        {
            PhotonEngine.Instance.Connect ( );
            if ( PhotonEngine.Instance.isServerConnect )
            {
                break;
            }
            yield return new WaitForSeconds ( 1.0F );
        }
        Current.Active ( true );
    }
}