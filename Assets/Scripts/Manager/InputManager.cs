﻿using System;
using System.Collections.Generic;
using Developers.Util;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputManager : MonoSingleton<InputManager>
{
    MainInputActions mainInputActions = null;
    EventSystem eventSystem = null;

    public MainInputActions MainInputActions {
        get
        {
            if( mainInputActions == null)
            {
                Initialize ( );
            }

            return mainInputActions;
        }
    }

    public EventSystem EventSystem {
        get
        {
            if(eventSystem == null)
            {
                eventSystem = GameObject.FindObjectOfType<EventSystem> ( );
            }
            return eventSystem;
        }
    }

    public delegate void FTouchEvent<T> ( T target ) where T : IActor;
    public event FTouchEvent<IActor> TouchEvent;

    public Vector2 StartPoint { get; set; }
    public Vector2 Position { get; private set; }
    public int layerMask = Physics2D.DefaultRaycastLayers;
    public bool isIgnore = false;
    public bool isStarted = false;
    public bool isPressed = false;
    public int overlayCount = 0;
    public bool Impossible {
        get
        {
            return isIgnore || overlayCount > 0;
        }
    }


    void Initialize()
    {
        if (mainInputActions != null)
        {
            return;
        }

        mainInputActions = new MainInputActions ( );

#if UNITY_ANDROID || UNITY_IOS
        mainInputActions.Main.Touch.performed += InputTouch;
#elif UNITY_EDITOR || UNITY_STANDALONE
        mainInputActions.Main.Mouse.started += InputMouseStarted;
        mainInputActions.Main.Mouse.canceled += InputMouseCanceled;
        mainInputActions.Main.Position.performed += InputMousePosition;
#endif
    }

    private void InputTouch ( InputAction.CallbackContext context )
    {
        Position = context.ReadValue<Vector2> ( );
        if ( Impossible )
        {
            return;
        }
        Vector2 pos = GameManager.Instance.MainCamera.ScreenToWorldPoint ( Position );
        RaycastHit2D hit = Physics2D.CircleCast ( pos, 0.2f, Vector2.zero, Mathf.Infinity, layerMask );
        if ( hit )
        {
            IActor actor = hit.transform.GetComponent<IActor> ( );
            if ( actor != null )
            {
                TouchEvent ( actor );
            }
        }
    }

    private void InputMouseStarted ( InputAction.CallbackContext obj )
    {
        isStarted = true;
        StartPoint = new Vector2 ( Position.x, Position.y );
    }

    private void InputMouseCanceled ( InputAction.CallbackContext context )
    {
        isStarted = false;

        Vector2 pos = GameManager.Instance.MainCamera.ScreenToWorldPoint ( Position );
        if ( Impossible || isPressed )
        {
            return;
        }

        RaycastHit2D hit = Physics2D.CircleCast ( pos, 0.2f, Vector2.zero, Mathf.Infinity, layerMask );
        if ( hit )
        {
            IActor actor = hit.transform.GetComponent<IActor> ( );
            if ( actor != null )
            {
                TouchEvent ( actor );
            }
        }
    }

    private void InputMousePosition ( InputAction.CallbackContext context )
    {
        Position = context.ReadValue<Vector2> ( );

        if(isStarted && !isPressed)
        {
            Vector3 start = GameManager.Instance.MainCamera.ScreenToWorldPoint ( Position );
            Vector3 end = GameManager.Instance.MainCamera.ScreenToWorldPoint ( StartPoint );
            Vector3 drag = start - end;
            if ( drag.magnitude > 1f )
            {
                isPressed = true;
            }
        }
        else if(!isStarted)
        {
            isPressed = false;
        }
    }

    protected override void Awake ( )
    {
        base.Awake ( );
        if(this == instance)
        {
            Initialize ( );
        }
    }
}