using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Air : MonoBehaviour
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
    }

    private void Update()
    {
        _slider.value = _player.air / _player.maxAir;
    }
}
