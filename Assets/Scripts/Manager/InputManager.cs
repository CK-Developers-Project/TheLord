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

    public delegate void FTouchEvent ( Transform target );
    public event FTouchEvent TouchEvent;

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

        mainInputActions.Main.Mouse.started += InputMouseStarted;
        mainInputActions.Main.Mouse.canceled += InputMouseCanceled;
        mainInputActions.Main.Position.performed += InputMousePosition;
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
            TouchEvent ( hit.transform );
        }
    }

    private void InputMouseStarted ( InputAction.CallbackContext obj )
    {
        Debug.Log ( "Input Started" );
        if ( Impossible )
        {
            return;
        }
        isStarted = true;
        StartPoint = new Vector2 ( Position.x, Position.y );
    }

    private void InputMouseCanceled ( InputAction.CallbackContext context )
    {
        Debug.Log ( "Input Canceled" );
        isStarted = false;

        Vector2 pos = GameManager.Instance.MainCamera.ScreenToWorldPoint ( Position );
        if ( Impossible || isPressed )
        {
            return;
        }

        RaycastHit2D hit = Physics2D.CircleCast ( pos, 0.2f, Vector2.zero, Mathf.Infinity, layerMask );
        if ( hit )
        {
            TouchEvent ( hit.transform );
        }
    }

    private void InputMousePosition ( InputAction.CallbackContext context )
    {
        Debug.Log ( "Input Position" );

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