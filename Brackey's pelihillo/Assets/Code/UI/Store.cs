using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Store : MonoBehaviour
{
    public static event Action<StatType> upgradePurchased;
    public UnityEvent<StatType> onUpgradePurchased;

    public void PurchaseUpgrade(StatType type, int cost)
    {
        if (!GameManager.current.bank.Take(cost))
        {
            return;
        }

        var stats = GameManager.current.stats;

        if (!stats.CanUpgrade(type))
        {
            return;
        }

        stats.UpgradeStat(type);

        if (upgradePurchased != null)
        {
            upgradePurchased(type);
        }

        if (onUpgradePurchased != null)
        {
            onUpgradePurchased.Invoke(type);
        }
    }
}
