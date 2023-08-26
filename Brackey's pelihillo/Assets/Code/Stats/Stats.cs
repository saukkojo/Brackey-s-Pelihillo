using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Stats : MonoBehaviour
{

    [Header("Jump")]
    [SerializeField] private int _baseJumpHeight = 5;
    [SerializeField] private int _jumpIncreasePerUpgrade = 5;

    [Header("Speed")]
    [SerializeField] private float _baseSpeed = 2;
    [SerializeField] private float _speedIncreasePerUpgrade = 0.25f;

    [Header("Turn")]
    [SerializeField] private float _baseTurnRate = 3;
    [SerializeField] private float _turnIncreasePerUpgrade = 0.2f;

    [Header("Light")]
    [SerializeField] private float _baseLightRadius;
    [SerializeField] private float _lightIncreasePerUpgrade = 1;

    public Stat jumpHeight;
    public Stat swimmingSpeed;
    public Stat turnRate;
    public Stat lightRadius;

    [Header("Other")]
    public bool boosterUnlocked = false;
    public bool flippersUnlocked = false;

    public static event Action<StatType> onStatUpgraded;

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        jumpHeight = new Stat(_baseJumpHeight, _jumpIncreasePerUpgrade, StatType.JumpHeight);
        swimmingSpeed = new Stat(_baseSpeed, _speedIncreasePerUpgrade, StatType.Speed);
        turnRate = new Stat(_baseTurnRate, _turnIncreasePerUpgrade, StatType.TurnRate);
        lightRadius = new Stat(_baseLightRadius, _lightIncreasePerUpgrade, StatType.LightRadius);
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

    public int GetLevel(StatType type)
    {
        switch (type)
        {
            case StatType.Speed:
                return swimmingSpeed.currentLevel;
            case StatType.TurnRate:
                return turnRate.currentLevel;
            case StatType.JumpHeight:
                return jumpHeight.currentLevel;
            case StatType.LightRadius:
                return lightRadius.currentLevel;
        }
        return 0;
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
