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
        },
        {
            ""name"": ""Combat"",
            ""id"": ""179e40f0-aa94-4b7c-ba06-6ed12edb27b6"",
            ""actions"": [
                {
                    ""name"": ""Primary"",
                    ""type"": ""Button"",
                    ""id"": ""d88abae2-64c2-4c57-8f89-401ea3a71407"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Secondary"",
                    ""type"": ""Button"",
                    ""id"": ""2354bca9-6d58-4d2c-b085-cc659271fa19"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PassivePrimary"",
                    ""type"": ""Button"",
                    ""id"": ""ed13c52e-ba94-41ae-abc7-c2b032e19bc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PassiveSecondary"",
                    ""type"": ""Button"",
                    ""id"": ""9f962a9b-681e-4dc0-9acd-136430f9f6e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PassiveThird"",
                    ""type"": ""Button"",
                    ""id"": ""54b1f513-4af6-46a4-8551-6c318ec689be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""3ec88d32-476c-4787-a7c8-14ccce5b9a8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Heal"",
                    ""type"": ""Button"",
                    ""id"": ""baa778fe-dbef-45cc-9dd0-7f9f101e155a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""7807408e-893b-46a6-805a-61dec57eeeb4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c081f53b-05c0-4276-9831-c7da3c0c75cf"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba4c83bc-cdb3-40ac-b40e-78ada9bdbb30"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7c3c67e-9456-4ac5-af1c-f78f92c3f3be"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c2dd814-7b90-44ab-8fc6-cb9f0951f98a"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1dc96f67-aaf5-4497-95c6-35f2578a1a64"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PassivePrimary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf5c40ad-1025-4ccc-a51d-1caa959c3041"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PassivePrimary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53ec016c-17c3-424d-b7c5-b30642c0e7f8"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PassiveSecondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7173ba9d-738e-4ff7-8008-85eeb1a785fa"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PassiveSecondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0eb8e3e-a0f0-49f1-a593-7b0f93d21af2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f350237-ceca-4cb9-a36c-0b7f04b52419"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4bcd8b4-983d-4d38-ad2e-0e87427716ec"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Heal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""368faa67-3b15-4fa6-96db-419de1b02d6e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Heal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea4fe440-e606-4055-979f-bdfc38b3b227"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb6fd9cb-2455-4de5-9776-c8f1f6b52190"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3cd4784-ef8c-4c2a-8386-2ac3ca673ac1"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PassiveThird"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53ef94a4-6a91-4f08-89c6-64e7584c9255"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PassiveThird"",
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
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_Primary = m_Combat.FindAction("Primary", throwIfNotFound: true);
        m_Combat_Secondary = m_Combat.FindAction("Secondary", throwIfNotFound: true);
        m_Combat_PassivePrimary = m_Combat.FindAction("PassivePrimary", throwIfNotFound: true);
        m_Combat_PassiveSecondary = m_Combat.FindAction("PassiveSecondary", throwIfNotFound: true);
        m_Combat_PassiveThird = m_Combat.FindAction("PassiveThird", throwIfNotFound: true);
        m_Combat_Shoot = m_Combat.FindAction("Shoot", throwIfNotFound: true);
        m_Combat_Heal = m_Combat.FindAction("Heal", throwIfNotFound: true);
        m_Combat_PickUp = m_Combat.FindAction("PickUp", throwIfNotFound: true);
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

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_Primary;
    private readonly InputAction m_Combat_Secondary;
    private readonly InputAction m_Combat_PassivePrimary;
    private readonly InputAction m_Combat_PassiveSecondary;
    private readonly InputAction m_Combat_PassiveThird;
    private readonly InputAction m_Combat_Shoot;
    private readonly InputAction m_Combat_Heal;
    private readonly InputAction m_Combat_PickUp;
    public struct CombatActions
    {
        private @PlayerTriggers m_Wrapper;
        public CombatActions(@PlayerTriggers wrapper) { m_Wrapper = wrapper; }
        public InputAction @Primary => m_Wrapper.m_Combat_Primary;
        public InputAction @Secondary => m_Wrapper.m_Combat_Secondary;
        public InputAction @PassivePrimary => m_Wrapper.m_Combat_PassivePrimary;
        public InputAction @PassiveSecondary => m_Wrapper.m_Combat_PassiveSecondary;
        public InputAction @PassiveThird => m_Wrapper.m_Combat_PassiveThird;
        public InputAction @Shoot => m_Wrapper.m_Combat_Shoot;
        public InputAction @Heal => m_Wrapper.m_Combat_Heal;
        public InputAction @PickUp => m_Wrapper.m_Combat_PickUp;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @Primary.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnPrimary;
                @Primary.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnPrimary;
                @Primary.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnPrimary;
                @Secondary.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnSecondary;
                @Secondary.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnSecondary;
                @Secondary.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnSecondary;
                @PassivePrimary.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnPassivePrimary;
                @PassivePrimary.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnPassivePrimary;
                @PassivePrimary.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnPassivePrimary;
                @PassiveSecondary.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnPassiveSecondary;
                @PassiveSecondary.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnPassiveSecondary;
                @PassiveSecondary.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnPassiveSecondary;
                @PassiveThird.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnPassiveThird;
                @PassiveThird.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnPassiveThird;
                @PassiveThird.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnPassiveThird;
                @Shoot.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnShoot;
                @Heal.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnHeal;
                @Heal.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnHeal;
                @Heal.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnHeal;
                @PickUp.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnPickUp;
                @PickUp.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnPickUp;
                @PickUp.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnPickUp;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Primary.started += instance.OnPrimary;
                @Primary.performed += instance.OnPrimary;
                @Primary.canceled += instance.OnPrimary;
                @Secondary.started += instance.OnSecondary;
                @Secondary.performed += instance.OnSecondary;
                @Secondary.canceled += instance.OnSecondary;
                @PassivePrimary.started += instance.OnPassivePrimary;
                @PassivePrimary.performed += instance.OnPassivePrimary;
                @PassivePrimary.canceled += instance.OnPassivePrimary;
                @PassiveSecondary.started += instance.OnPassiveSecondary;
                @PassiveSecondary.performed += instance.OnPassiveSecondary;
                @PassiveSecondary.canceled += instance.OnPassiveSecondary;
                @PassiveThird.started += instance.OnPassiveThird;
                @PassiveThird.performed += instance.OnPassiveThird;
                @PassiveThird.canceled += instance.OnPassiveThird;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Heal.started += instance.OnHeal;
                @Heal.performed += instance.OnHeal;
                @Heal.canceled += instance.OnHeal;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);
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
    public interface ICombatActions
    {
        void OnPrimary(InputAction.CallbackContext context);
        void OnSecondary(InputAction.CallbackContext context);
        void OnPassivePrimary(InputAction.CallbackContext context);
        void OnPassiveSecondary(InputAction.CallbackContext context);
        void OnPassiveThird(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnHeal(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
    }
}
