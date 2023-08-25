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
    private Vector2 _spawnPos;
    private float _randomness = 1;

    private bool _onScreen = false;

    public float air => _air;
    public float speedBoost => _speedBoost;

    private Rigidbody2D _rigidbody;

    protected override void Awake()
    {
        base.Awake();
        _spawnPos = transform.position;
        _rigidbody = this.AddOrGetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = true;
        _randomness = Random.Range(0.5f, 2f);
    }

    private void FixedUpdate()
    {
        if (!_onScreen)
        {
            return;
        }

        Vector2 movement = new Vector2(Mathf.Sin(Time.timeSinceLevelLoad * _randomness) * 0.5f, velocity * (1 + _air * 0.1f));
        movement *= Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }

    private void OnWillRenderObject()
    {
        _onScreen = true;
    }
}
