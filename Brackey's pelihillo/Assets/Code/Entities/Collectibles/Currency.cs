using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Currency : Collectible, ICurrency
{
    [SerializeField] private int _value = 10;
    public int value => _value;
    [SerializeField] private GameObject pickUpEffect;

    public override void Collect()
    {
        base.Collect();

        GameObject effect = Instantiate(pickUpEffect, this.transform.position, Quaternion.identity);
        //Destroy(pickUpEffect, 3f);
    }
}
