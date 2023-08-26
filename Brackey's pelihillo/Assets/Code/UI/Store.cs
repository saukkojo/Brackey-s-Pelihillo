using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Store : MonoBehaviour
{
    public static event Action<StatType> upgradePurchased;
    public UnityEvent<StatType> onUpgradePurchased;

    public void PurchaseUpgrade(StoreItem item)
    {
        StatType type = item.statType;
        int cost = item.cost;

        if (!GameManager.current.bank.HasFundsFor(cost))
        {
            return;
        }

        var stats = GameManager.current.stats;

        if (!stats.CanUpgrade(type))
        {
            return;
        }

        stats.UpgradeStat(type);
        GameManager.current.bank.Take(cost);

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
