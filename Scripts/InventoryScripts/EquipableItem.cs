using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Equipable", menuName = "Item/Equipable")]
public class EquipableItem : ScriptableObject {

    public int ID;

    public new string name;
    public string type;
    public string equipType;
    public string rarity;

    public int maxUpgrades;
    public int upgradesDone = 0;
    public List<int> upgradeCosts;

    public int currentDamage;
    public int currentArmor;
    public int currentStrength;
    public int currentAgility;
    public int currentMind;
    public int currentSoul;

    public List<int> damage;
    public List<int> armor;
    public List<int> strength;
    public List<int> agility;
    public List<int> mind;
    public List<int> soul;

    public int minLevel;
    public int maxLevel;

    public int buyValue;
    public int sellValue;
    
    public bool bought;
    public bool chest;

    public float minRange;
    public float maxRange;

    public void Init(string setName, string setType, string setEquipType, string setRarity, 
        List<int> setDamage, List<int> setArmor, List<int> setStr, List<int> setAgi,
        List<int> setMind, List<int> setSoul, int setMinLevel, int setMaxLevel, int setBuy, 
        int setSell, bool setBought, bool setChest, int setMaxUpgrades, 
        List<int> setCosts, float setMin, float setMax)
    {
        name = setName;
        type = setType;
        equipType = setEquipType;
        rarity = setRarity;
        damage = setDamage;
        armor = setArmor;
        strength = setStr;
        agility = setAgi;
        mind = setMind;
        soul = setSoul;
        minLevel = setMinLevel;
        maxLevel = setMaxLevel;
        buyValue = setBuy;
        sellValue = setSell;
        bought = setBought;
        chest = setChest;
        maxUpgrades = setMaxUpgrades;
        upgradeCosts = setCosts;

        minRange = setMin;
        maxRange = setMax;

        SetStats();
    }

    public void UpgradeDone()
    {
        upgradesDone++;
        SetStats();
    }

    public void SetStats()
    {
        currentDamage = damage[upgradesDone];
        currentArmor = armor[upgradesDone];
        currentStrength = strength[upgradesDone];
        currentAgility = agility[upgradesDone];
        currentMind = mind[upgradesDone];
        currentSoul = soul[upgradesDone];
    }
}
