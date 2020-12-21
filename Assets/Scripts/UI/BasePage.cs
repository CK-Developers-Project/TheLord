using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Developers.Util;

public class BasePage : MonoBehaviour, IGameUI
{
    bool isInitialize = false;

    //protected List<BasePopup> popupList = new List<BasePopup> ( );
    protected List<IGameUI> gameUIList = new List<IGameUI> ( );


    static GameObject Prefab_NoticePopup;

    public static void OnMessageBox ( string msg, bool action_Left, Action callback_Left, string leftMsg, bool action_Right = false, Action callback_Right = null, string rightMsg = "" )
    {
        GameObject obj = Instantiate ( Prefab_NoticePopup, GameManager.Instance.GameMode.CurrentPage.transform );
        NoticePopup popup = obj.GetComponent<NoticePopup> ( );

        popup.OnMessageBox ( msg, action_Left, callback_Left, leftMsg, action_Right, callback_Right, rightMsg );
    }

    public void RemovePopup(BasePopup popup)
    {
        gameUIList.Remove ( popup );
        Destroy ( popup.gameObject );
    }

    public virtual void Initialize ( )
    {
        if ( Prefab_NoticePopup == null )
        {
            Prefab_NoticePopup = LoadManager.Instance.Get ( typeof ( NoticePopup ) );
        }


        var gameUIs = GetComponentsInChildren<IGameUI> ( true );
        foreach(var gameUI in gameUIs )
        {
            if(!gameUI.Equals(this))
            {
                gameUIList.Add ( gameUI );
            }
        }

        isInitialize = true;
    }


    public virtual void Active()
    {
        gameObject.SetActive ( true );
    }

    public virtual void InActive()
    {
        gameObject.SetActive ( false );
    }


    public virtual void OnUpdate ( )
    {
        foreach ( var ui in gameUIList )
        {
            ui.OnUpdate ( );
        }
    }

    protected virtual void Construct ( ) { }
    protected virtual void Hidden ( ) { }

    IEnumerator Start()
    {
        yield return new WaitUntil ( ( ) => GameManager.Instance.IsGameStart );
        Initialize ( );
    }

    IEnumerator Enable()
    {
        yield return new WaitUntil ( ( ) => isInitialize );
        Construct ( );
    }

    void OnEnable ( )
    {
        StartCoroutine ( Enable ( ) );
    }

    void OnDisable ( )
    {
        Hidden ( );
    }
}