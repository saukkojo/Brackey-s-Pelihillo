using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    public float baseSpeed = 1.0f;
    protected float _currentSpeed, _speed;
    public float turnRate = 1.0f;
    public float currentRot, targetRot;
    protected float _deltaRot;
    public bool slowOnTurn = true;
    [Range(0f, 1f)]
    public float maxTurnSlow = 0.5f;
    public float lerpAbuse = 3.0f;

    protected virtual void Awake()
    {
        _rigidbody = this.AddOrGetComponent<Rigidbody2D>();
        _speed = baseSpeed;
    }

    protected virtual void FixedUpdate()
    {
        currentRot = Mathf.Lerp(currentRot, targetRot, lerpAbuse * Time.fixedDeltaTime);
        _deltaRot = Mathf.Abs(targetRot - currentRot);
        _rigidbody.rotation = currentRot;
        _currentSpeed = Mathf.Lerp(_currentSpeed, _speed, 5 * Time.fixedDeltaTime);
    }

    public virtual void Turn(float direction)
    {
        targetRot -= direction * turnRate;
    }

    public virtual void TurnTo(Vector2 direction)
    {
        float dot = Vector2.Dot(transform.up, direction);
        bool clockWise = dot < 0;

        dot = Vector2.Dot(transform.right, direction);
        bool over90 = dot < 0;

        float degrees = 0;
        if (over90)
        {
            degrees += 90f + 90f * Mathf.Abs(dot);
        }
        else
        {
            degrees += 90f * (1 - dot);
        }

        if (clockWise)
        {
            targetRot -= degrees;
        }
        else
        {
            targetRot += degrees;
        }
    }

    public virtual void Move()
    {
        Vector2 direction = transform.right;
        Debug.DrawLine(_rigidbody.position, _rigidbody.position + direction, Color.green);
        Vector2 velocity = slowOnTurn ? direction * (_currentSpeed * 1 - (_deltaRot / 360) * maxTurnSlow) : direction * _currentSpeed;
        _rigidbody.velocity = velocity;
    }
}
