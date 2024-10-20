//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Player/PlayerActionController.inputactions
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

public partial class @PlayerActionController: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActionController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActionController"",
    ""maps"": [
        {
            ""name"": ""GamePlayControl"",
            ""id"": ""ffac1e07-571f-4a6e-879d-c0a9c1665913"",
            ""actions"": [
                {
                    ""name"": ""MoveMent"",
                    ""type"": ""Value"",
                    ""id"": ""2f7a2e46-d9b0-4def-9533-d7132b09e0b6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""NormalAttack"",
                    ""type"": ""Button"",
                    ""id"": ""98a89be2-5e12-4158-9d88-3603db85200c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""AroundAttack"",
                    ""type"": ""Button"",
                    ""id"": ""01a1fa41-15a5-4651-9ef6-8614f54426ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SupperAttack"",
                    ""type"": ""Button"",
                    ""id"": ""27117b05-48e2-46a4-abe0-a99417aa09cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a496994e-1446-4e10-9e90-589ced5fbcb3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Continue"",
                    ""type"": ""Button"",
                    ""id"": ""a71c3bdc-a5a8-4219-95ab-87a399f96156"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9d73e9e4-db11-4ff1-8f71-8835dd3c3394"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveMent"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f1c4b2c8-806b-4232-b130-34fb837322b5"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveMent"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9ce2bd82-00c7-4a96-a664-43e38b4f0c5d"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveMent"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""36b25cd7-d347-416e-87ad-ac0c2270d2ce"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveMent"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""46dd9a75-d7dc-4fed-b183-84251467278f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveMent"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cb496b99-8c22-478c-8bc1-4c773398c75c"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd5f7133-5488-414a-961d-3bd0903b50ea"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NormalAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57276af3-06b8-4bd8-a35c-a1d012152d1a"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AroundAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""152f99eb-6959-455d-8ec9-70e9703e7475"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SupperAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3915ff83-e536-47e1-b4d0-52aa74b864fd"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GamePlayControl
        m_GamePlayControl = asset.FindActionMap("GamePlayControl", throwIfNotFound: true);
        m_GamePlayControl_MoveMent = m_GamePlayControl.FindAction("MoveMent", throwIfNotFound: true);
        m_GamePlayControl_NormalAttack = m_GamePlayControl.FindAction("NormalAttack", throwIfNotFound: true);
        m_GamePlayControl_AroundAttack = m_GamePlayControl.FindAction("AroundAttack", throwIfNotFound: true);
        m_GamePlayControl_SupperAttack = m_GamePlayControl.FindAction("SupperAttack", throwIfNotFound: true);
        m_GamePlayControl_Jump = m_GamePlayControl.FindAction("Jump", throwIfNotFound: true);
        m_GamePlayControl_Continue = m_GamePlayControl.FindAction("Continue", throwIfNotFound: true);
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

    // GamePlayControl
    private readonly InputActionMap m_GamePlayControl;
    private List<IGamePlayControlActions> m_GamePlayControlActionsCallbackInterfaces = new List<IGamePlayControlActions>();
    private readonly InputAction m_GamePlayControl_MoveMent;
    private readonly InputAction m_GamePlayControl_NormalAttack;
    private readonly InputAction m_GamePlayControl_AroundAttack;
    private readonly InputAction m_GamePlayControl_SupperAttack;
    private readonly InputAction m_GamePlayControl_Jump;
    private readonly InputAction m_GamePlayControl_Continue;
    public struct GamePlayControlActions
    {
        private @PlayerActionController m_Wrapper;
        public GamePlayControlActions(@PlayerActionController wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveMent => m_Wrapper.m_GamePlayControl_MoveMent;
        public InputAction @NormalAttack => m_Wrapper.m_GamePlayControl_NormalAttack;
        public InputAction @AroundAttack => m_Wrapper.m_GamePlayControl_AroundAttack;
        public InputAction @SupperAttack => m_Wrapper.m_GamePlayControl_SupperAttack;
        public InputAction @Jump => m_Wrapper.m_GamePlayControl_Jump;
        public InputAction @Continue => m_Wrapper.m_GamePlayControl_Continue;
        public InputActionMap Get() { return m_Wrapper.m_GamePlayControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayControlActions set) { return set.Get(); }
        public void AddCallbacks(IGamePlayControlActions instance)
        {
            if (instance == null || m_Wrapper.m_GamePlayControlActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GamePlayControlActionsCallbackInterfaces.Add(instance);
            @MoveMent.started += instance.OnMoveMent;
            @MoveMent.performed += instance.OnMoveMent;
            @MoveMent.canceled += instance.OnMoveMent;
            @NormalAttack.started += instance.OnNormalAttack;
            @NormalAttack.performed += instance.OnNormalAttack;
            @NormalAttack.canceled += instance.OnNormalAttack;
            @AroundAttack.started += instance.OnAroundAttack;
            @AroundAttack.performed += instance.OnAroundAttack;
            @AroundAttack.canceled += instance.OnAroundAttack;
            @SupperAttack.started += instance.OnSupperAttack;
            @SupperAttack.performed += instance.OnSupperAttack;
            @SupperAttack.canceled += instance.OnSupperAttack;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Continue.started += instance.OnContinue;
            @Continue.performed += instance.OnContinue;
            @Continue.canceled += instance.OnContinue;
        }

        private void UnregisterCallbacks(IGamePlayControlActions instance)
        {
            @MoveMent.started -= instance.OnMoveMent;
            @MoveMent.performed -= instance.OnMoveMent;
            @MoveMent.canceled -= instance.OnMoveMent;
            @NormalAttack.started -= instance.OnNormalAttack;
            @NormalAttack.performed -= instance.OnNormalAttack;
            @NormalAttack.canceled -= instance.OnNormalAttack;
            @AroundAttack.started -= instance.OnAroundAttack;
            @AroundAttack.performed -= instance.OnAroundAttack;
            @AroundAttack.canceled -= instance.OnAroundAttack;
            @SupperAttack.started -= instance.OnSupperAttack;
            @SupperAttack.performed -= instance.OnSupperAttack;
            @SupperAttack.canceled -= instance.OnSupperAttack;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Continue.started -= instance.OnContinue;
            @Continue.performed -= instance.OnContinue;
            @Continue.canceled -= instance.OnContinue;
        }

        public void RemoveCallbacks(IGamePlayControlActions instance)
        {
            if (m_Wrapper.m_GamePlayControlActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGamePlayControlActions instance)
        {
            foreach (var item in m_Wrapper.m_GamePlayControlActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GamePlayControlActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GamePlayControlActions @GamePlayControl => new GamePlayControlActions(this);
    public interface IGamePlayControlActions
    {
        void OnMoveMent(InputAction.CallbackContext context);
        void OnNormalAttack(InputAction.CallbackContext context);
        void OnAroundAttack(InputAction.CallbackContext context);
        void OnSupperAttack(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnContinue(InputAction.CallbackContext context);
    }
}
