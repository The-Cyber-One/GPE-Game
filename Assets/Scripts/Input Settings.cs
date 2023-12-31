//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/_Infinity Cards/Settings/Input Settings.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputSettings: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSettings()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input Settings"",
    ""maps"": [
        {
            ""name"": ""Interactions"",
            ""id"": ""e9028c54-2bc3-4820-826d-08fc9d7d306e"",
            ""actions"": [
                {
                    ""name"": ""Point Main"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fce0cf70-e9e1-446c-be63-aadedc0f6367"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Press Main"",
                    ""type"": ""PassThrough"",
                    ""id"": ""87ae5264-1713-42cf-a77b-dcb3608d17f0"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Press Secondary"",
                    ""type"": ""Button"",
                    ""id"": ""6120d4af-1192-49ca-97ad-35c9188b774d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Point Secondary"",
                    ""type"": ""PassThrough"",
                    ""id"": ""73bd76f9-b66c-4650-8964-c148c89b3bf1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Mouse Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""39b2352f-9690-4f03-959e-fe1be02e7681"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Normalize(min=-120,max=120),Scale(factor=0.1)"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Mouse Press Secondary"",
                    ""type"": ""Button"",
                    ""id"": ""1a0d7da1-e9ae-4b84-9b47-c07b2467b851"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c52c8e0b-8179-41d3-b8a1-d149033bbe86"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point Main"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1394cbc-336e-44ce-9ea8-6007ed6193f7"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point Main"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5693e57a-238a-46ed-b5ae-e64e6e574302"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Point Main"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb59f552-d569-4294-bcc2-b39fc1ef91ac"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Press Main"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""868103f8-a6f0-4da3-abfa-cad2687e1d7b"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Press Main"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0767d15-0136-4494-8948-10e3ba5949bf"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Press Main"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4584a123-7aab-44f3-822c-55c7902bdc0e"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Point Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9412a7aa-8216-4388-8f89-1b22eb5c7ff0"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc76b384-aeb6-4a91-bb2b-1eb29773a240"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Mouse Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ca108f2-e0fa-4fef-9c46-c9cd0ec6216c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Mouse Press Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Interactions
        m_Interactions = asset.FindActionMap("Interactions", throwIfNotFound: true);
        m_Interactions_PointMain = m_Interactions.FindAction("Point Main", throwIfNotFound: true);
        m_Interactions_PressMain = m_Interactions.FindAction("Press Main", throwIfNotFound: true);
        m_Interactions_PressSecondary = m_Interactions.FindAction("Press Secondary", throwIfNotFound: true);
        m_Interactions_PointSecondary = m_Interactions.FindAction("Point Secondary", throwIfNotFound: true);
        m_Interactions_MouseScroll = m_Interactions.FindAction("Mouse Scroll", throwIfNotFound: true);
        m_Interactions_MousePressSecondary = m_Interactions.FindAction("Mouse Press Secondary", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Interactions
    private readonly InputActionMap m_Interactions;
    private List<IInteractionsActions> m_InteractionsActionsCallbackInterfaces = new List<IInteractionsActions>();
    private readonly InputAction m_Interactions_PointMain;
    private readonly InputAction m_Interactions_PressMain;
    private readonly InputAction m_Interactions_PressSecondary;
    private readonly InputAction m_Interactions_PointSecondary;
    private readonly InputAction m_Interactions_MouseScroll;
    private readonly InputAction m_Interactions_MousePressSecondary;
    public struct InteractionsActions
    {
        private @InputSettings m_Wrapper;
        public InteractionsActions(@InputSettings wrapper) { m_Wrapper = wrapper; }
        public InputAction @PointMain => m_Wrapper.m_Interactions_PointMain;
        public InputAction @PressMain => m_Wrapper.m_Interactions_PressMain;
        public InputAction @PressSecondary => m_Wrapper.m_Interactions_PressSecondary;
        public InputAction @PointSecondary => m_Wrapper.m_Interactions_PointSecondary;
        public InputAction @MouseScroll => m_Wrapper.m_Interactions_MouseScroll;
        public InputAction @MousePressSecondary => m_Wrapper.m_Interactions_MousePressSecondary;
        public InputActionMap Get() { return m_Wrapper.m_Interactions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionsActions set) { return set.Get(); }
        public void AddCallbacks(IInteractionsActions instance)
        {
            if (instance == null || m_Wrapper.m_InteractionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InteractionsActionsCallbackInterfaces.Add(instance);
            @PointMain.started += instance.OnPointMain;
            @PointMain.performed += instance.OnPointMain;
            @PointMain.canceled += instance.OnPointMain;
            @PressMain.started += instance.OnPressMain;
            @PressMain.performed += instance.OnPressMain;
            @PressMain.canceled += instance.OnPressMain;
            @PressSecondary.started += instance.OnPressSecondary;
            @PressSecondary.performed += instance.OnPressSecondary;
            @PressSecondary.canceled += instance.OnPressSecondary;
            @PointSecondary.started += instance.OnPointSecondary;
            @PointSecondary.performed += instance.OnPointSecondary;
            @PointSecondary.canceled += instance.OnPointSecondary;
            @MouseScroll.started += instance.OnMouseScroll;
            @MouseScroll.performed += instance.OnMouseScroll;
            @MouseScroll.canceled += instance.OnMouseScroll;
            @MousePressSecondary.started += instance.OnMousePressSecondary;
            @MousePressSecondary.performed += instance.OnMousePressSecondary;
            @MousePressSecondary.canceled += instance.OnMousePressSecondary;
        }

        private void UnregisterCallbacks(IInteractionsActions instance)
        {
            @PointMain.started -= instance.OnPointMain;
            @PointMain.performed -= instance.OnPointMain;
            @PointMain.canceled -= instance.OnPointMain;
            @PressMain.started -= instance.OnPressMain;
            @PressMain.performed -= instance.OnPressMain;
            @PressMain.canceled -= instance.OnPressMain;
            @PressSecondary.started -= instance.OnPressSecondary;
            @PressSecondary.performed -= instance.OnPressSecondary;
            @PressSecondary.canceled -= instance.OnPressSecondary;
            @PointSecondary.started -= instance.OnPointSecondary;
            @PointSecondary.performed -= instance.OnPointSecondary;
            @PointSecondary.canceled -= instance.OnPointSecondary;
            @MouseScroll.started -= instance.OnMouseScroll;
            @MouseScroll.performed -= instance.OnMouseScroll;
            @MouseScroll.canceled -= instance.OnMouseScroll;
            @MousePressSecondary.started -= instance.OnMousePressSecondary;
            @MousePressSecondary.performed -= instance.OnMousePressSecondary;
            @MousePressSecondary.canceled -= instance.OnMousePressSecondary;
        }

        public void RemoveCallbacks(IInteractionsActions instance)
        {
            if (m_Wrapper.m_InteractionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInteractionsActions instance)
        {
            foreach (var item in m_Wrapper.m_InteractionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InteractionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InteractionsActions @Interactions => new InteractionsActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    public interface IInteractionsActions
    {
        void OnPointMain(InputAction.CallbackContext context);
        void OnPressMain(InputAction.CallbackContext context);
        void OnPressSecondary(InputAction.CallbackContext context);
        void OnPointSecondary(InputAction.CallbackContext context);
        void OnMouseScroll(InputAction.CallbackContext context);
        void OnMousePressSecondary(InputAction.CallbackContext context);
    }
}
