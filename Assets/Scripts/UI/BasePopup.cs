using System;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour, IGameUI
{
    public virtual void OnUpdate ( ) { }
    public void OnEnable ( )
    {
        InputManager.Instance.OverlayCount++;
    }
    public void OnDisable ( )
    {
        InputManager.Instance.OverlayCount--;
    }
}