using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerClass {

    private string name;
    private string charClass;
    private int level;
    private int currentExp;

    private string weapon;
    private int weaponUpgrades;
    private int weaponID;
    private string offHand = null;
    private int offHandUpgrades = 0;
    private int offHandID = 0;
    private string armor;
    private int armorUpgrades;
    private int armorID;
    private string accessory;
    private int accessoryUpgrades;
    private int accessoryID;

    public PlayerClass() { }

    public PlayerClass(string Name, string CharClass, int Level, int CurrentExp, string Weapon, 
        int WeaponUpgrades, int WeaponID, string Armor, int ArmorUpgrades, int ArmorID, string Accessory, int AccessoryUpgrades, 
        int AccessoryID)
    {
        name = Name;
        charClass = CharClass;
        level = Level;
        currentExp = CurrentExp;

        weapon = Weapon;
        weaponUpgrades = WeaponUpgrades;
        weaponID = WeaponID;
        armor = Armor;
        armorUpgrades = ArmorUpgrades;
        armorID = ArmorID;
        accessory = Accessory;
        accessoryUpgrades = AccessoryUpgrades;
        accessoryID = AccessoryID;
    }

    //getters
    public string GetName() { return name; }
    public string GetClass() { return charClass; }
    public int GetLevel() { return level; }
    public int GetExp() { return currentExp; }

    public string GetWeapon() { return weapon; }
    public int GetWeaponUpgrades() { return weaponUpgrades; }
    public int GetWeaponID() { return weaponID; }
    public string GetOffHand() { return offHand; }
    public int GetOffHandUpgrades() { return offHandUpgrades; }
    public int GetOffHandID() { return offHandID; }
    public string GetArmor() { return armor; }
    public int GetArmorUpgrades() { return armorUpgrades; }
    public int GetArmorID() { return armorID; }
    public string GetAccessory() { return accessory; }
    public int GetAccessoryUgrades() { return accessoryUpgrades; }
    public int GetAccessoryID() { return accessoryID; }

    //offhand setters
    public void SetOffHand(string offHandName) { offHand = offHandName; }
    public void SetOffHandUpgrades(int offHandUpgrade) { offHandUpgrades = offHandUpgrade; }
    public void SetOffHandID(int _offHandID) { offHandID = _offHandID; }
}
