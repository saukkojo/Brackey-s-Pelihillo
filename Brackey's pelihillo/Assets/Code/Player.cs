using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Mover _movement;
    private InputReader _inputReader;


    private void Awake()
    {
        _movement = this.AddOrGetComponent<Mover>();
        _inputReader = this.AddOrGetComponent<InputReader>();
    }

    private void FixedUpdate()
    {
        _movement.Turn(_inputReader.TurnValue);
        _movement.Move();
    }
}
