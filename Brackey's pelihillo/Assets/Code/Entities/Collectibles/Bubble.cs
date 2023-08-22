using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Collectible, IBubble
{
    public const float MIN_AIR = 5, MAX_AIR = 20;

    [SerializeField, Range(MIN_AIR, MAX_AIR)]
    private float _air = 10f;
    [SerializeField, Range(0, 1f)]
    private float _speedBoost = 0.05f;
    private float velocity = 0.3f;

    public float air => _air;
    public float speedBoost => _speedBoost;

    private Rigidbody2D _rigidbody;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = this.AddOrGetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.up * velocity * (1 + _air * 0.1f);
    }
}
