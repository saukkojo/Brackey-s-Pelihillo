using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public override bool doPersist => true;

    private Bank _bank;
    private Stats _stats;

    protected override void Init()
    {
        _bank = new Bank(0);
    }

    private void OnEnable()
    {
        Collectible.onCollect += OnCollect;
    }

    private void OnDisable()
    {
        Collectible.onCollect -= OnCollect;
    }

    private void OnCollect(ICollectible collectible)
    {
        ICurrency currency = collectible as ICurrency;
        if (currency != null)
        {
            _bank.AddFunds(currency);
        }
    }

    public void Restart()
    {
        Init();
    }
}
