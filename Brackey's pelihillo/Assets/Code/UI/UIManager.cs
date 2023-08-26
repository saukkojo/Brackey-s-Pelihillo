using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Store _store;
    private Hud _hud;
    private Endscreen _endScreen;

    private void Awake()
    {
        _store = GetComponentInChildren<Store>();
        _hud = GetComponentInChildren<Hud>();
        _endScreen = GetComponentInChildren<Endscreen>();
    }

    private void Start()
    {
        OnStateChange(GameManager.current.state);
    }

    private void OnEnable()
    {
        GameManager.onStateChange += OnStateChange;
    }

    private void OnDisable()
    {
        GameManager.onStateChange -= OnStateChange;
    }

    private void OnStateChange(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Begun:
                _store.gameObject.Disable();
                _hud.gameObject.Enable();
                _endScreen.gameObject.Disable();
                break;
            case GameManager.GameState.Ended:
                _store.gameObject.Disable();
                _hud.gameObject.Disable();
                _endScreen.gameObject.Enable();
                break;
            case GameManager.GameState.Store:
                _store.gameObject.Enable();
                _hud.gameObject.Disable();
                _endScreen.gameObject.Disable();
                break;
        }
    }

    public void GoStore()
    {
        GameManager.current.Store();
    }

    public void Begin()
    {
        GameManager.current.Begin();
    }
}
