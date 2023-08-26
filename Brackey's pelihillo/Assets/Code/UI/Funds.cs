using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Funds : MonoBehaviour
{
    private TextMeshProUGUI _txt;

    private void Awake()
    {
        _txt = this.AddOrGetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Bank.onFundChange += OnFundChange;
        _txt.text = "0";
    }

    private void OnDestroy()
    {
        Bank.onFundChange -= OnFundChange;
    }

    private void OnFundChange(int funds)
    {
        _txt.text = funds.ToString();
    }
}
