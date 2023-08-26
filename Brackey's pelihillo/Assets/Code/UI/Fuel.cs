using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    private Slider _slider;
    private Player _player;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _player = Player.current;
        Stats.onStatUpgraded += OnStatUpgraded;
        gameObject.Disable();
    }

    private void OnDestroy()
    {
        Stats.onStatUpgraded -= OnStatUpgraded;
    }

    private void OnStatUpgraded(StatType state)
    {
        if (state == StatType.Booster)
        {
            gameObject.Enable();
        }
    }

    private void Update()
    {
        if (GameManager.current.stats.boosterUnlocked)
        {
            _slider.value = _player.fuel / _player.maxFuel;
        }
    }
}
