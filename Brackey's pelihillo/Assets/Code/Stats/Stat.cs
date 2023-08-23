using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    readonly float baseValue;
    public float value => baseValue + increasePerUpgrade * currentLevel;
    [SerializeField]
    private float increasePerUpgrade;
    [SerializeField]
    private int upgradeLevels = 10;
    public int currentLevel = 0;
    public bool canUpgrade => currentLevel < upgradeLevels;
    StatType type;

    public Stat(float value, StatType type)
    {
        baseValue = value;
        this.type = type;
    }

    public void Upgrade()
    {
        currentLevel = Mathf.Clamp(currentLevel++, 0, upgradeLevels);
    }
}
