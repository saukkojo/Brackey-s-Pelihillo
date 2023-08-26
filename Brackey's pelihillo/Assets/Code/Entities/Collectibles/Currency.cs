using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : Collectible, ICurrency
{
    [SerializeField] private int _value = 10;
    public int value => _value;

    public override void Collect()
    {
        base.Collect();
    }
}
