using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

public abstract class BaseGameMode : MonoBehaviour
{
    [SerializeField] 
    AssetLabelReference assetLabelReference = null;

    protected List<BasePage> PagePool = new List<BasePage> ( );
    public BasePage CurrentPage { get; set; }


    public void Load()
    {
        LoadManager.Instance.Register ( assetLabelReference );
        LoadManager.Instance.Run ( );
    }

    /// <summary>현재 게임모드의 UI 페이지에서 선택한 UI 페이지로 전환합니다.</summary>
    public BasePage SetPage(BasePage page)
    {
        if ( CurrentPage != null && CurrentPage.Equals ( page ) )
        {
            return CurrentPage;
        }
        CurrentPage?.InActive ( );
        CurrentPage = page;
        CurrentPage.Active ( );
        bool bEmpty = PagePool.Find ( _ => _.Equals ( page ) ) == null;
        if( bEmpty )
        {
            PagePool.Add ( page );
        }
        return page;
    }

    // Notice Popup 테스트용
    public GameObject Popup;

    public void OnMessageBox ( string msg, bool action_Left, Action callback_Left, string leftMsg, bool action_Right = false, Action callback_Right = null, string rightMsg = "" )
    {
        GameObject obj = Instantiate ( Popup, CurrentPage.transform );
        NoticePopup popup = obj.GetComponent<NoticePopup> ( );
        popup.OnMessageBox ( msg, action_Left, callback_Left, leftMsg, action_Right, callback_Right, rightMsg );
    }


    public virtual void RegisterInput ( ) { }
    public virtual void ReleaseInput ( ) { }

    public virtual void OnEnter ( ) { }
    public virtual void OnUpdate ( ) { }
    public virtual void OnExit ( ) { }

    protected virtual void Update ( )
    {
        OnUpdate ( );
    }
}
