using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Store _store;
    [SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _endScreen;

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
                _hud.Enable();
                _endScreen.Disable();
                break;
            case GameManager.GameState.Ended:
                _store.gameObject.Disable();
                _hud.Disable();
                _endScreen.Enable();
                break;
            case GameManager.GameState.Store:
                _store.gameObject.Enable();
                _hud.Disable();
                _endScreen.Disable();
                break;
        }
    }
}
