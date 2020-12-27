using System;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour, IGameUI
{
    public virtual void OnUpdate ( ) { }
    protected virtual void OnEnable ( )
    {
        InputManager.Instance.overlayCount++;
    }
    protected virtual void OnDisable ( )
    {
        InputManager.Instance.overlayCount--;
    }
}