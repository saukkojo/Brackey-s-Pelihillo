using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class Stats
{

    [SerializeField]
    private int _baseJumpHeight = 5;
    [SerializeField]
    private float _baseSpeed = 2;
    [SerializeField]
    private float _baseTurnRate = 3;
    [SerializeField]
    private float _baseLightRadius;

    public Stat jumpHeight;
    public Stat swimmingSpeed;
    public Stat turnRate;
    public Stat lightRadius;
    public bool boosterUnlocked = false;
    public bool flippersUnlocked = false;

    public static event Action<StatType> onStatUpgraded;

    public Stats()
    {
        Reset();
    }

    public void Reset()
    {
        jumpHeight = new Stat(_baseJumpHeight, StatType.JumpHeight);
        swimmingSpeed = new Stat(_baseSpeed, StatType.Speed);
        turnRate = new Stat(_baseTurnRate, StatType.TurnRate);
        lightRadius = new Stat(_baseLightRadius, StatType.LightRadius);
        boosterUnlocked = false;
        flippersUnlocked = false;
    }

    public bool CanUpgrade(StatType type)
    {
        switch (type)
        {
            case StatType.Speed:
                return swimmingSpeed.canUpgrade;
            case StatType.TurnRate:
                return turnRate.canUpgrade;
            case StatType.JumpHeight:
                return jumpHeight.canUpgrade;
            case StatType.LightRadius:
                return lightRadius.canUpgrade;
            case StatType.Booster:
                return !boosterUnlocked;
            case StatType.Flippers:
                return !flippersUnlocked;
            default: return false;
        }
    }

    public void UpgradeStat(StatType type)
    {
        switch (type)
        {
            case StatType.Speed:
                swimmingSpeed.Upgrade();
                break;
            case StatType.TurnRate:
                turnRate.Upgrade();
                break;
            case StatType.JumpHeight:
                jumpHeight.Upgrade();
                break;
            case StatType.LightRadius:
                lightRadius.Upgrade();
                break;
            case StatType.Booster:
                boosterUnlocked = true;
                break;
            case StatType.Flippers:
                flippersUnlocked = true;
                break;
        }

        if (onStatUpgraded != null)
        {
            onStatUpgraded(type);
        }
    }
}
