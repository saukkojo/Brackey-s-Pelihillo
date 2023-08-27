using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{

    public const float increasePerPurchase = 0.1f;
    public StatType statType;
    public int baseCost = 50;
    public int cost => Mathf.RoundToInt(baseCost + baseCost * increasePerPurchase * GameManager.current.stats.GetLevel(statType));
    private Button _button;
    [SerializeField]
    private TextMeshProUGUI _buttonTxt;

    private void Awake()
    {
        _button = GetComponent<Button>();
        Debug.Assert(_buttonTxt != null);
        _buttonTxt.text = baseCost.ToString();
    }

    private void OnValidate()
    {
        if (_buttonTxt != null)
        {
            _buttonTxt.text = baseCost.ToString();
        }
    }

    private void OnEnable()
    {
        Stats.onStatUpgraded += OnStatUpgraded;
    }

    private void OnDestroy()
    {
        Stats.onStatUpgraded -= OnStatUpgraded;
    }

    private void OnStatUpgraded(StatType type)
    {
        if (type != statType)
        {
            return;
        }

        _buttonTxt.text = cost.ToString();
        if (!GameManager.current.stats.CanUpgrade(statType))
        {
            _button.interactable = false;
        }
    }
}
