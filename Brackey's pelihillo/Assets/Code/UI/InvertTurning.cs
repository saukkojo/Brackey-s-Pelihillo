using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvertTurning : MonoBehaviour
{
    private Button _button;
    private TextMeshProUGUI _buttonTxt;

    private void Awake()
    {
        _button = this.AddOrGetComponent<Button>();
        _button.onClick.AddListener(Toggle);
        _buttonTxt = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetText(Player.current.invertTurning);
    }

    private void Toggle()
    {
        Player.current.invertTurning = !Player.current.invertTurning;
        SetText(Player.current.invertTurning);
    }

    private void SetText(bool value)
    {
        _buttonTxt.text = value ? "Yes" : "No";
    }
}
