using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public override bool doPersist => true;

    public Bank bank;
    public Stats stats;

    public ModuleHandler moduleHandler;

    public static event Action<GameState> onStateChange;
    public GameState state = GameState.Begun;

    private int _fundsAccumulated = 0;
    private int _pearlsCollected = 0;
    public int fundsAccumulated => _fundsAccumulated;
    public int pearlsCollected => _pearlsCollected;

    public enum GameState
    {
        Begun,
        Ended,
        Store
    }

    protected override void Init()
    {
        moduleHandler = this.AddOrGetComponent<ModuleHandler>();
        stats = this.AddOrGetComponent<Stats>();
        moduleHandler.PlaceModules();
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
        if (state != GameState.Begun)
        {
            return;
        }


        ICurrency currency = collectible as ICurrency;
        if (currency != null)
        {
            bank.AddFunds(currency);
            _fundsAccumulated += currency.value;
        }
    }

    private void ChangeState(GameState state)
    {
        if (onStateChange != null)
        {
            onStateChange(state);
        }

        switch (state)
        {
            case GameState.Begun:
                moduleHandler.PlaceModules();
                _fundsAccumulated = 0;
                _pearlsCollected = 0;
                break;
            case GameState.Ended:
                break;
            case GameState.Store:
                moduleHandler.CleanUp();
                break;
        }

        this.state = state;
    }

    public void Reset()
    {
        bank = new Bank(0);
        stats.Reset();
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
