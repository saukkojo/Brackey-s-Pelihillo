using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private PlayerMover _mover;
    private PlayerMover.Boost _boost;
    private PlayerSprite _sprite;
    [SerializeField]
    private ParticleSystem _bubbleParticle;
    private InputReader _inputReader;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    [SerializeField] private AudioSource _diveSource;
    [SerializeField] private AudioSource _hurtSource;
    [SerializeField] private AudioSource _bubbleSource;
    [SerializeField] private AudioSource _dingSource;
    public float depth = 0;

    private bool useBooster = false;
    private bool waterHit = false;
    public bool invertTurning = false;

    public float air = 100;
    private float _maxAir;
    public float maxAir => _maxAir;

    public float fuel = 15;
    public float maxFuel;
    private bool _boosting = false;

    private enum PlayerState
    {
        None,
        Freefall,
        Swim
    }

    [SerializeField]
    private PlayerState _state = PlayerState.None;

    public override bool doPersist => false;

    protected override void Init()
    {
        _mover = this.AddOrGetComponent<PlayerMover>();
        _inputReader = this.AddOrGetComponent<InputReader>();
        _rigidbody = this.AddOrGetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _sprite = GetComponentInChildren<PlayerSprite>();
        _boost = new PlayerMover.Boost(_mover.baseSpeed * 0.75f);
        _inputReader.boostCallback = (value) =>
        {
            if (!useBooster || fuel < 0)
            {
                return;
            }

            if (value)
            {
                _mover.AddBoost(_boost);
                _boosting = true;
            }
            else
            {
                _mover.RemoveBoost(_boost);
                _boosting = false;
            }
        };
        _maxAir = air;
        maxFuel = fuel;
        ChangeState(_state);
    }

    private void OnEnable()
    {
        GameManager.onStateChange += OnGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.onStateChange -= OnGameStateChange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_state != PlayerState.Swim)
        {
            return;
        }

        if (collision.TryGetComponent<ICollectible>(out var collectible))
        {
            var bubble = collectible as IBubble;
            if (bubble != null)
            {
                air = Mathf.Clamp(air + bubble.air, 0, _maxAir);
                if (_bubbleSource != null)
                {
                    _bubbleSource.Play();
                }
            }

            var currency = collectible as Currency;
            if (currency != null && _dingSource != null)
            {
                _dingSource.Play();
            }
            collectible.Collect();
        }

        if (collision.TryGetComponent<Obstacle>(out var obstacle))
        {
            air -= obstacle.damage;
            _sprite.Hit();
            if (_bubbleParticle != null)
            {
                _bubbleParticle.Play();
            }

            if (_hurtSource != null)
            {
                _hurtSource.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case PlayerState.None:
                if (_rigidbody.position.y < 0)
                {
                    _rigidbody.velocity = Vector2.up;
                }
                else
                {
                    _rigidbody.velocity = Vector2.zero;
                }
                break;

            case PlayerState.Freefall:
                if (transform.position.y < 0 && waterHit == false)
                {
                    waterHit = true;
                    if (_diveSource != null)
                    {
                        _diveSource.Play();
                    }
                    StartCoroutine(SlowRoutine());
                }
                break;

            case PlayerState.Swim:
                if (_rigidbody.position.y > 0)
                {
                    _rigidbody.gravityScale = 1;
                }
                else
                {
                    _rigidbody.gravityScale = 0;
                    air -= Time.fixedDeltaTime;
                    _mover.Move();
                }
                _mover.Turn(invertTurning ? -_inputReader.turnValue : _inputReader.turnValue);
                if (air < 0)
                {
                    ChangeState(PlayerState.None);
                    GameManager.current.End();
                }
                break;
        }
        if (_boosting)
        {
            fuel -= Time.fixedDeltaTime;
            if (fuel < 0)
            {
                _mover.RemoveBoost(_boost);
                _boosting = false;
            }
        }
        depth = transform.position.y < 0 ? Mathf.Abs(transform.position.y) : 0;
    }

    private IEnumerator SlowRoutine()
    {
        float duration = 3;
        float timer = 0;
        Vector2 startVelocity = _rigidbody.velocity;
        _rigidbody.gravityScale = 0;
        do
        {
            float t = timer / duration;
            _rigidbody.velocity = Vector2.Lerp(startVelocity, Vector2.zero, t);
            timer += Time.deltaTime;
            yield return null;
        }
        while (timer < duration);
        ChangeState(PlayerState.Swim);
    }

    private void ChangeState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.None:
                _collider.isTrigger = true;
                _mover.TurnTo(Vector2.up);
                break;

            case PlayerState.Freefall:
                _collider.isTrigger = true;
                air = _maxAir;
                fuel = maxFuel;
                _mover.TurnTo(Vector2.down);
                waterHit = false;
                _rigidbody.gravityScale = 1;
                break;

            case PlayerState.Swim:
                _collider.isTrigger = false;
                _rigidbody.gravityScale = 0;
                break;
        }

        _state = state;
    }

    private void OnGameStateChange(GameManager.GameState state)
    {
        if (state != GameManager.GameState.Begun)
        {
            return;
        }
        var stats = GameManager.current.stats;
        transform.position = new Vector2(0, stats.jumpHeight.value);
        _mover.turnRate = stats.turnRate.value;
        _mover.baseSpeed = stats.swimmingSpeed.value;
        useBooster = stats.boosterUnlocked;
        if (stats.flippersUnlocked)
        {
            _mover.lerpAbuse = 4f;
        }
        else
        {
            _mover.lerpAbuse = 2f;
        }

        ChangeState(PlayerState.Freefall);
    }
}
