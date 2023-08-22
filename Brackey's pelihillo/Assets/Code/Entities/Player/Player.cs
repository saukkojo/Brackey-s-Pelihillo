using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private InputReader _inputReader;

    private PlayerMover.Boost _boost;

    private void Awake()
    {
        _mover = this.AddOrGetComponent<PlayerMover>();
        _inputReader = this.AddOrGetComponent<InputReader>();
        _boost = new PlayerMover.Boost(_mover.baseSpeed * 0.75f);
        _inputReader.boostCallback = (value) =>
        {
            Debug.Log(value);
            if (value)
            {
                _mover.AddBoost(_boost);
            }
            else
            {
                _mover.RemoveBoost(_boost);
            }
        };
    }

    private void FixedUpdate()
    {
        _mover.Turn(_inputReader.turnValue);
        _mover.Move();
    }
}
