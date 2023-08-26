using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank
{
    private int _funds = 0;
    public int funds
    {
        get => _funds;
        private set
        {
            if (onFundChange != null)
            {
                onFundChange(value);
            }

            _funds = value;
        }
    }

    public static event Action<int> onFundChange;

    public Bank(int startingFunds)
    {
        _funds = startingFunds;
    }

    public bool HasFundsFor(int amount) => amount <= funds;
    public void AddFunds(int amount) => funds += amount;
    public void AddFunds(ICurrency currency) => AddFunds(currency.value);

    public bool Take(int amount)
    {
        if (!HasFundsFor(amount))
        {
            return false;
        }

        funds -= amount;
        return true;
    }
}
