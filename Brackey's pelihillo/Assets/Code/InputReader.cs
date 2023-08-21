using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private Controls _controls;
    public float TurnValue;

    private void Awake()
    {
        _controls = new Controls();
        _controls.Enable();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        TurnValue = _controls.Player.Turn.ReadValue<float>();
    }
}
