using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasePage : MonoBehaviour
{
    List<BasePopup> popupList = new List<BasePopup> ( );
    List<IGameUI> gameUIList = new List<IGameUI> ( );

    //public BasePopup CurrentPopup { }




    public virtual void Initialize ( )
    {
        gameUIList = GetComponentsInChildren<IGameUI> ( ).ToList();
    }


    public void OnUpdate ( )
    {
        foreach ( var ui in gameUIList )
        {
            ui.OnUpdate ( );
        }
    }


    protected virtual void Start ( )
    {
        Initialize ( );
        OnUpdate ( );
    }

}