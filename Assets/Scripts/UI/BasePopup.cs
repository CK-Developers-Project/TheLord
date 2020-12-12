using System;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour, IGameUI
{
    public virtual void OnUpdate ( ) { }
    public void OnEnable ( )
    {
        InputManager.Instance.overlayCount++;
    }
    public void OnDisable ( )
    {
        InputManager.Instance.overlayCount--;
    }
}