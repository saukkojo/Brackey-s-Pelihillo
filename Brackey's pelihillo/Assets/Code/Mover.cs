using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    public float Speed = 1.0f;
    public float TurnRate = 1.0f;
    public float _currentRot, _targetRot;
    public float LerpAbuse = 3.0f;
    public Quaternion Rotation;

    private void Awake()
    {
        _rigidbody = this.AddOrGetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _currentRot = Mathf.Lerp(_currentRot, _targetRot, LerpAbuse * Time.fixedDeltaTime);
        _rigidbody.rotation = _currentRot;
    }

    public void Turn(float direction)
    {
        _targetRot -= direction * TurnRate;
    }

    public virtual void Move()
    {
        Vector2 direction = transform.right;
        Debug.DrawLine(_rigidbody.position, _rigidbody.position + direction);
        //_rigidbody.position += Direction * Speed * deltaTime;
        _rigidbody.velocity = direction * Speed;
    }
}
