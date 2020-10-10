using System;
using System.Collections.Generic;
using Developers.Util;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoSingleton<InputManager>
{
    Camera mainCamera;

    MainInputActions mainInputActions;
    public MainInputActions MainInputActions {
        get
        {
            if( mainInputActions  == null)
            {
                Initialize ( );
            }

            return mainInputActions;
        }
    }

    public delegate void FTouchEvent<T> ( T target ) where T : IActor;
    public event FTouchEvent<IActor> TouchEvent;

    public Vector2 Position { get; private set; }
    public int LayerMask { get; set; }
    public bool isIgnore { get; set; }


    void Initialize()
    {
        mainCamera = Camera.main;
        if (mainInputActions != null)
        {
            return;
        }

        mainInputActions = new MainInputActions ( );

#if UNITY_ANDROID || UNITY_IOS
        mainInputActions.Main.Touch.performed += InputTouch;
#elif UNITY_EDITOR || UNITY_STANDALONE
        mainInputActions.Main.Mouse.performed += InputMouseClick;
        mainInputActions.Main.Position.performed += InputMousePosition;
#endif
    }

    private void InputTouch ( InputAction.CallbackContext context )
    {
        Position = context.ReadValue<Vector2> ( );
        if ( isIgnore )
        {
            return;
        }
        Vector2 pos = mainCamera.ScreenToWorldPoint ( Position );
        RaycastHit2D hit = Physics2D.CircleCast ( pos, 0.2f, Vector2.zero, Mathf.Infinity, LayerMask );
        if ( hit )
        {
            IActor actor = hit.transform.GetComponent<IActor> ( );
            if ( actor != null )
            {
                TouchEvent ( actor );
            }
        }
    }

    private void InputMouseClick ( InputAction.CallbackContext context )
    {
        Vector2 pos = mainCamera.ScreenToWorldPoint ( Position );
        if ( isIgnore )
        {
            return;
        }
        RaycastHit2D hit = Physics2D.CircleCast ( pos, 0.2f, Vector2.zero, Mathf.Infinity, LayerMask );
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
    }

    protected override void Awake ( )
    {
        base.Awake ( );
        Initialize ( );
    }
}