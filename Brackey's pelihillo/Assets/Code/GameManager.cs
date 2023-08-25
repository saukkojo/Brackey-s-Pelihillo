using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public override bool doPersist => true;

    public Bank bank;
    public Stats stats;

    private ModuleHandler _moduleHanlder;

    public static event Action<GameState> onStateChange;
    public GameState state = GameState.Store;

    public enum GameState
    {
        Begun,
        Ended,
        Store
    }

    protected override void Init()
    {
        _moduleHanlder = this.AddOrGetComponent<ModuleHandler>();
        _moduleHanlder.PlaceModules();
        Reset();
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
            bank.AddFunds(currency);
        }
    }

    private void ChangeState(GameState state)
    {
        if (onStateChange != null)
        {
            onStateChange(state);
        }

        this.state = state;
    }

    public void Reset()
    {
        bank = new Bank(0);
        stats = new Stats();
    }

    public void Begin()
    {
        ChangeState(GameState.Begun);
    }

    public void End()
    {
        ChangeState(GameState.Ended);
    }

    public void Store()
    {
        ChangeState(GameState.Store);
    }
}
