using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Developers.Util;

public class BasePage : MonoBehaviour, IGameUI
{
    protected List<BasePopup> popupList = new List<BasePopup> ( );
    protected List<IGameUI> gameUIList = new List<IGameUI> ( );

    //public BasePopup CurrentPopup { }
    GameObject Prefab_NoticePopup;

    public void OnMessageBox ( string msg, bool action_Left, Action callback_Left, string leftMsg, bool action_Right = false, Action callback_Right = null, string rightMsg = "" )
    {
        GameObject obj = Instantiate ( Prefab_NoticePopup, transform );
        NoticePopup popup = obj.GetComponent<NoticePopup> ( );
        popup.OnMessageBox ( msg, action_Left, callback_Left, leftMsg, action_Right, callback_Right, rightMsg );
    }


    public virtual void Initialize ( )
    {
        Prefab_NoticePopup = MonoSingleton<LoadManager>.Instance.Core.Find ( x =>
         {
             GameObject obj = x as GameObject;
             if ( obj != null )
             {
                 NoticePopup popup = obj.GetComponent<NoticePopup> ( );
                 if ( popup != null )
                 {
                     return true;
                 }
             }
             return false;
         } ) as GameObject;

        var gameUIs = GetComponentsInChildren<IGameUI> ( );
        foreach(var gameUI in gameUIs )
        {
            if(!gameUI.Equals(this))
            {
                gameUIList.Add ( gameUI );
            }
        }
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

    IEnumerator Enable()
    {
        yield return new WaitUntil ( ( ) => MonoSingleton<GameManager>.Instance.IsGameStart );
        Construct ( );
    }

    void OnEnable ( )
    {
        StartCoroutine ( Enable ( ) );
    }
}