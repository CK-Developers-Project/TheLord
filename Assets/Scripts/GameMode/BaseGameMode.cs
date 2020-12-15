using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AddressableAssets;
using Developers.Util;

public abstract class BaseGameMode : MonoBehaviour
{
    [SerializeField] 
    AssetLabelReference assetLabelReference = null;

    protected List<BasePage> PagePool = new List<BasePage> ( );
    private BasePage currentPage = null;
    public BasePage CurrentPage { get => currentPage; set => SetPage ( value ); }


    public virtual IEnumerator OnStart()
    {
        TransitionManager.Instance.OnWaitSigh ( false );
        GameManager.Instance.OnStart ( );
        yield break;
    }

    public virtual void OnSynchronize<T> ( T data ) where T : class
    {
        GameManager.Instance.OnSynchronize ( );
        currentPage?.OnUpdate ( );
    }

    public virtual void Load()
    {
        TransitionManager.Instance.OnWaitSigh ( true );
        Action action = ( ) =>
        {
            LoadManager.Instance.Register ( assetLabelReference );
            LoadManager.Instance.Run ( );
        };

        if(false == TableManager.Instance.isLoad)
        {
            TableManager.Instance.Load ( action ); 
        }
        else
        {
            action?.Invoke ( );
        }
    }

    /// <summary>현재 게임모드의 UI 페이지에서 선택한 UI 페이지로 전환합니다.</summary>
    public BasePage SetPage(BasePage page)
    {
        if ( CurrentPage != null && CurrentPage.Equals ( page ) )
        {
            return CurrentPage;
        }
        CurrentPage?.InActive ( );
        currentPage = page;
        CurrentPage.Active ( );
        bool bEmpty = PagePool.Find ( _ => _.Equals ( page ) ) == null;
        if( bEmpty )
        {
            PagePool.Add ( page );
        }
        return page;
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