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
                },
                {
                    ""name"": ""Sneak"",
                    ""type"": ""Value"",
                    ""id"": ""5c2f28f3-1e42-4c3d-9fdc-843921d3ac39"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveDetector"",
                    ""type"": ""Value"",
                    ""id"": ""a1f6a8ea-1066-400f-a698-cbdd2dae6c73"",
                    ""expectedControlType"": ""Vector2"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""fc7ca6dd-70b1-4930-b04f-e53a45898883"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""64df6f0f-aa66-48ad-acf8-a441dff6cf96"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDetector"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dae4603b-9fdd-4b6c-8f6d-6e37db5ab656"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDetector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""15bfb078-99a5-4b8a-bd54-a382da493341"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDetector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7ee3a817-4bca-439c-9dbc-d90181749c52"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDetector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""595040d7-38df-49f4-ac90-f668a449d07a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDetector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6001f095-6375-4961-b990-688ccc449338"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDetector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ed184acf-1949-4b1c-be4a-4530b5e9091f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDetector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""92cff01e-53e3-4675-83de-307192f44f7a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDetector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b6b4a140-10f0-4dec-b558-890717121c89"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDetector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ebc5bf63-fe7c-4791-bc59-827facbf4012"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveDetector"",
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
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": []
        }
    ]
}");
        // DummyPlayer
        m_DummyPlayer = asset.FindActionMap("DummyPlayer", throwIfNotFound: true);
        m_DummyPlayer_HideTest = m_DummyPlayer.FindAction("HideTest", throwIfNotFound: true);
        m_DummyPlayer_Sneak = m_DummyPlayer.FindAction("Sneak", throwIfNotFound: true);
        m_DummyPlayer_MoveDetector = m_DummyPlayer.FindAction("MoveDetector", throwIfNotFound: true);
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
    private readonly InputAction m_DummyPlayer_Sneak;
    private readonly InputAction m_DummyPlayer_MoveDetector;
    public struct DummyPlayerActions
    {
        private @PlayerTriggers m_Wrapper;
        public DummyPlayerActions(@PlayerTriggers wrapper) { m_Wrapper = wrapper; }
        public InputAction @HideTest => m_Wrapper.m_DummyPlayer_HideTest;
        public InputAction @Sneak => m_Wrapper.m_DummyPlayer_Sneak;
        public InputAction @MoveDetector => m_Wrapper.m_DummyPlayer_MoveDetector;
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
                @Sneak.started -= m_Wrapper.m_DummyPlayerActionsCallbackInterface.OnSneak;
                @Sneak.performed -= m_Wrapper.m_DummyPlayerActionsCallbackInterface.OnSneak;
                @Sneak.canceled -= m_Wrapper.m_DummyPlayerActionsCallbackInterface.OnSneak;
                @MoveDetector.started -= m_Wrapper.m_DummyPlayerActionsCallbackInterface.OnMoveDetector;
                @MoveDetector.performed -= m_Wrapper.m_DummyPlayerActionsCallbackInterface.OnMoveDetector;
                @MoveDetector.canceled -= m_Wrapper.m_DummyPlayerActionsCallbackInterface.OnMoveDetector;
            }
            m_Wrapper.m_DummyPlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HideTest.started += instance.OnHideTest;
                @HideTest.performed += instance.OnHideTest;
                @HideTest.canceled += instance.OnHideTest;
                @Sneak.started += instance.OnSneak;
                @Sneak.performed += instance.OnSneak;
                @Sneak.canceled += instance.OnSneak;
                @MoveDetector.started += instance.OnMoveDetector;
                @MoveDetector.performed += instance.OnMoveDetector;
                @MoveDetector.canceled += instance.OnMoveDetector;
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
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IDummyPlayerActions
    {
        void OnHideTest(InputAction.CallbackContext context);
        void OnSneak(InputAction.CallbackContext context);
        void OnMoveDetector(InputAction.CallbackContext context);
    }
}
