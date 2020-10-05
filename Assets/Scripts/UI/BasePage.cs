using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Developers.Util;

public class BasePage : MonoBehaviour
{
    protected List<BasePopup> popupList = new List<BasePopup> ( );
    protected List<IGameUI> gameUIList = new List<IGameUI> ( );

    //public BasePopup CurrentPopup { }


    public virtual void Initialize ( )
    {
        gameUIList = GetComponentsInChildren<IGameUI> ( ).ToList();
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

    protected virtual void Start ( )
    {
        Initialize ( );
    }
}