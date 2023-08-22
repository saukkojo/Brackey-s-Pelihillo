using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private Controls _controls;
    public float turnValue;
    public Action<bool> boostCallback;

    private void Awake()
    {
        _controls = new Controls();
        _controls.Enable();
        _controls.Player.Boost.performed += OnBoost;
        _controls.Player.Boost.canceled += OnBoostCancel;
    }

    private void OnBoostCancel(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (boostCallback != null)
        {
            boostCallback(false);
        }
    }

    private void OnBoost(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (boostCallback != null)
        {
            boostCallback(true);
        }
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void OnDestroy()
    {
        _controls.Player.Boost.performed -= OnBoost;
        _controls.Player.Boost.canceled -= OnBoostCancel;
    }

    private void Update()
    {
        turnValue = _controls.Player.Turn.ReadValue<float>();
    }
}
