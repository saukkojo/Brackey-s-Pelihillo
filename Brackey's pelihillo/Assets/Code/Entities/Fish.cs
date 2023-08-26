using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Mover _mover;

    private void Awake()
    {
        _mover = this.AddOrGetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        _mover.Move();
        if (Mathf.Abs(transform.position.x) > 30)
        {
            Destroy(gameObject);
        }
    }

    public void Spawn(Vector2 dir)
    {
        _mover.TurnTo(dir);
    }
}
