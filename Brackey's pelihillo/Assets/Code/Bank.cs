using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank
{
    private int _funds = 0;
    public int funds => _funds;

    public Bank(int startingFunds)
    {
        _funds = startingFunds;
    }

    public bool HasFundsFor(int amount) => amount >= _funds;
    public void AddFunds(int amount) => _funds += amount;
    public void AddFunds(ICurrency currency) => AddFunds(currency.value);

    public bool Take(int amount)
    {
        if (!HasFundsFor(amount))
        {
            return false;
        }

        _funds -= amount;
        return true;
    }
}
