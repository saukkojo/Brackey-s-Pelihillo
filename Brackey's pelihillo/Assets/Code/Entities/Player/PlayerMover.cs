using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMover : Mover
{
    [SerializeField]
    private VisualEffect boosterEffect;

    [SerializeField]
    private HashSet<Boost> _boosts = new HashSet<Boost>();

    public void AddBoost(Boost boost)
    {
        _boosts.Add(boost);
        boosterEffect.SendEvent("Start");
    }

    public void RemoveBoost(Boost boost)
    {
        _boosts.Remove(boost);
        boosterEffect.SendEvent("Stop");
    }

    private void Update()
    {
        _speed = baseSpeed;
        foreach (var boost in _boosts)
        {
            _speed += boost.amount;
        }
    }
    private void OnEnable()
    {
        Collectible.onCollect += OnCollect;
    }

    private void OnDisable()
    {
        Collectible.onCollect -= OnCollect;
    }

    private void OnCollect(ICollectible obj)
    {
        var bubble = obj as IBubble;
        if (bubble != null)
        {
            StartCoroutine(BoostRoutine(bubble.speedBoost));
        }
    }

    public class Boost
    {
        public float amount;

        public Boost(float amount)
        {
            this.amount = amount;
        }
    }

    private IEnumerator BoostRoutine(float boostAmount, float duration = 4)
    {
        float timer = 0;
        float additionalSpeed = baseSpeed * boostAmount;

        var boost = new Boost(boostAmount);
        _boosts.Add(boost);
        do
        {
            float t = timer / duration;
            boost.amount = Mathf.Lerp(additionalSpeed, 0, t);
            timer += Time.deltaTime;
            yield return null;
        }
        while (timer < duration);
        _boosts.Remove(boost);
    }
}
