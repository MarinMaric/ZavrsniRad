// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/PlayerTriggers.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerTriggers : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerTriggers()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerTriggers"",
    ""maps"": [
        {
            ""name"": ""DummyPlayer"",
            ""id"": ""fe89531a-2f86-4e7c-a1b1-9e2df4fa7c3d"",
            ""actions"": [
                {
                    ""name"": ""HideTest"",
                    ""type"": ""Button"",
                    ""id"": ""8b4a2c18-967c-4cb2-8fbb-4b10cc781604"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d457a8c8-9c7c-4ad5-b0ff-4de993e1a034"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HideTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18ea6966-a0b4-4daf-935e-6aa690b7e811"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HideTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // DummyPlayer
        m_DummyPlayer = asset.FindActionMap("DummyPlayer", throwIfNotFound: true);
        m_DummyPlayer_HideTest = m_DummyPlayer.FindAction("HideTest", throwIfNotFound: true);
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

    // DummyPlayer
    private readonly InputActionMap m_DummyPlayer;
    private IDummyPlayerActions m_DummyPlayerActionsCallbackInterface;
    private readonly InputAction m_DummyPlayer_HideTest;
    public struct DummyPlayerActions
    {
        private @PlayerTriggers m_Wrapper;
        public DummyPlayerActions(@PlayerTriggers wrapper) { m_Wrapper = wrapper; }
        public InputAction @HideTest => m_Wrapper.m_DummyPlayer_HideTest;
        public InputActionMap Get() { return m_Wrapper.m_DummyPlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DummyPlayerActions set) { return set.Get(); }
        public void SetCallbacks(IDummyPlayerActions instance)
        {
            if (m_Wrapper.m_DummyPlayerActionsCallbackInterface != null)
            {
                @HideTest.started -= m_Wrapper.m_DummyPlayerActionsCallbackInterface.OnHideTest;
                @HideTest.performed -= m_Wrapper.m_DummyPlayerActionsCallbackInterface.OnHideTest;
                @HideTest.canceled -= m_Wrapper.m_DummyPlayerActionsCallbackInterface.OnHideTest;
            }
            m_Wrapper.m_DummyPlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HideTest.started += instance.OnHideTest;
                @HideTest.performed += instance.OnHideTest;
                @HideTest.canceled += instance.OnHideTest;
            }
        }
    }
    public DummyPlayerActions @DummyPlayer => new DummyPlayerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IDummyPlayerActions
    {
        void OnHideTest(InputAction.CallbackContext context);
    }
}
