// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/MainInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MainInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainInputActions"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""6f6bc52e-6bc1-4221-8c95-fbad3f06daaf"",
            ""actions"": [
                {
                    ""name"": ""Touch"",
                    ""type"": ""Value"",
                    ""id"": ""c7be166f-c15b-47e7-9d9d-ec81fd1b561a"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Chat"",
                    ""type"": ""Button"",
                    ""id"": ""71fc4b3e-ad2b-40f0-a7f9-c0c36523e44f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""d9cd6b0b-830f-460e-9f7e-6cebc2333cdb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Button"",
                    ""id"": ""bbd8fabb-65d0-4219-8968-b9e56029c632"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9baf938c-bf90-47a1-a216-b6f738fc63e6"",
                    ""path"": ""<Pointer>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e82d235-8299-41b5-83aa-74f7509651fb"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Chat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fae6e3c5-9d98-408d-a028-36dd889e741c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01e4ab59-68c5-4dce-818a-373598d3e237"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Touch = m_Main.FindAction("Touch", throwIfNotFound: true);
        m_Main_Chat = m_Main.FindAction("Chat", throwIfNotFound: true);
        m_Main_Position = m_Main.FindAction("Position", throwIfNotFound: true);
        m_Main_Mouse = m_Main.FindAction("Mouse", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_Touch;
    private readonly InputAction m_Main_Chat;
    private readonly InputAction m_Main_Position;
    private readonly InputAction m_Main_Mouse;
    public struct MainActions
    {
        private @MainInputActions m_Wrapper;
        public MainActions(@MainInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Touch => m_Wrapper.m_Main_Touch;
        public InputAction @Chat => m_Wrapper.m_Main_Chat;
        public InputAction @Position => m_Wrapper.m_Main_Position;
        public InputAction @Mouse => m_Wrapper.m_Main_Mouse;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @Touch.started -= m_Wrapper.m_MainActionsCallbackInterface.OnTouch;
                @Touch.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnTouch;
                @Touch.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnTouch;
                @Chat.started -= m_Wrapper.m_MainActionsCallbackInterface.OnChat;
                @Chat.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnChat;
                @Chat.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnChat;
                @Position.started -= m_Wrapper.m_MainActionsCallbackInterface.OnPosition;
                @Position.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnPosition;
                @Position.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnPosition;
                @Mouse.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMouse;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Touch.started += instance.OnTouch;
                @Touch.performed += instance.OnTouch;
                @Touch.canceled += instance.OnTouch;
                @Chat.started += instance.OnChat;
                @Chat.performed += instance.OnChat;
                @Chat.canceled += instance.OnChat;
                @Position.started += instance.OnPosition;
                @Position.performed += instance.OnPosition;
                @Position.canceled += instance.OnPosition;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
            }
        }
    }
    public MainActions @Main => new MainActions(this);
    public interface IMainActions
    {
        void OnTouch(InputAction.CallbackContext context);
        void OnChat(InputAction.CallbackContext context);
        void OnPosition(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
    }
}
