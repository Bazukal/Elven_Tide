using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterClass{

    private string charName;
    private string charClass;
    private string maxArmor;
    private bool canShield;
    private int charLevel;
    private int charMaxHp;
    private int charCurrentHp;
    private int charMaxMp;
    private int charCurrentMp;
    private int charStr;
    private int charAgi;
    private int charMind;
    private int charSoul;
    private int charDef;
    private int charCurExp;
    private int charLvlExp;
    private List<string> charSkills = new List<string>();
    private EquipableItemClass weapon;
    private EquipableItemClass offHand;
    private EquipableItemClass armor;
    private EquipableItemClass accessory;

    //constructors for Character Class
    public CharacterClass()
    {

    }

    public CharacterClass(string CharName, string CharClass, string MaxArmor, bool CanShield, int CharLevel, int CharHP, 
        int CharMP, int CharSTR, int CharAGI, int CharMind, int CharSoul, int CharDef, int CharCurExp, int CharLvlExp, 
        List<string> CharSkills, EquipableItemClass Weapon, EquipableItemClass OffHand, EquipableItemClass Armor,
        EquipableItemClass Accessory)
    {
        charName = CharName;
        charClass = CharClass;
        maxArmor = MaxArmor;
        canShield = CanShield;
        charLevel = CharLevel;
        charMaxHp = CharHP;
        charCurrentHp = charMaxHp;
        charMaxMp = CharMP;
        charCurrentMp = charMaxMp;
        charStr = CharSTR;
        charAgi = CharAGI;
        charMind = CharMind;
        charSoul = CharSoul;
        charDef = CharDef;
        charCurExp = CharCurExp;
        charLvlExp = CharLvlExp;
        charSkills = CharSkills;
        weapon = Weapon;
        offHand = OffHand;
        armor = Armor;
        accessory = Accessory;
    }

    //getters and setters of Character Class
    public string GetCharName() { return charName; }

    public string GetCharClass() { return charClass; }

    public string GetMaxArmor() { return maxArmor; }

    public bool GetShield() { return canShield; }

    public int GetCharLevel() { return charLevel; }

    public int GetCharMaxHp() { return charMaxHp; }

    public int GetCharCurrentHp() { return charCurrentHp; }
    public void ChangeCharCurrentHp(int hpChange) { charCurrentHp += hpChange; }
    public void healToFull() { charCurrentHp = charMaxHp; charCurrentMp = charMaxMp; }

    public int GetCharMaxMp() { return charMaxMp; }

    public int GetCharCurrentMp() { return charCurrentMp; }
    public void ChangeCharCurrentMp(int mpChange) { charCurrentMp += mpChange; }

    public int GetCharStr() { return charStr; }

    public int GetCharAgi() { return charAgi; }

    public int GetCharMind() { return charMind; }

    public int GetCharSoul() { return charSoul; }

    public int GetCharDef() { return charDef; }

    public int GetCurExp() { return charCurExp; }
    public void AddCurExp(int expGain) { charCurExp += expGain; }

    public int GetLvlExp() { return charLvlExp; }

    public List<string> GetCharSkills() { return charSkills; }
    public void AddCharSkill(string skillName) { charSkills.Add(skillName); } 

    public EquipableItemClass GetWeapon() { return weapon; }

    public EquipableItemClass GetOffHand() { return offHand; }

    public EquipableItemClass GetArmor() { return armor; }

    public EquipableItemClass GetAccessory() { return accessory; }

    //changes equipment on the character
    public EquipableItemClass ChangeWeapon(EquipableItemClass newWeapon)
    {
        EquipableItemClass currentWeapon = weapon;

        weapon = newWeapon;
        return currentWeapon;
    }

    public EquipableItemClass ChangeOffHand(EquipableItemClass newOffHand)
    {
        EquipableItemClass currentOffHand = offHand;

        offHand = newOffHand;
        return currentOffHand;
    }

    public EquipableItemClass ChangeArmor(EquipableItemClass newArmor)
    {
        EquipableItemClass currentArmor = armor;

        armor = newArmor;
        return currentArmor;
    }

    public EquipableItemClass ChangeAccessory(EquipableItemClass newAccessory)
    {
        EquipableItemClass currentAccessory = accessory;

        accessory = newAccessory;
        return currentAccessory;
    }


    //character level up
    public void CharLevelUp()
    {
        int hpRoll = 0;
        int mpRoll = 0;
        int strRoll = 0;
        int agiRoll = 0;
        int minRoll = 0;
        int soulRoll = 0;
        int defRoll = 0;

        charLevel++;
        
        switch(charClass)
        {
            case "Archer":
                hpRoll = UnityEngine.Random.Range(6, 10);
                strRoll = UnityEngine.Random.Range(2, 4);
                agiRoll = UnityEngine.Random.Range(2, 5);
                minRoll = UnityEngine.Random.Range(0, 2);
                soulRoll = UnityEngine.Random.Range(0, 3);
                defRoll = UnityEngine.Random.Range(1, 3);

                charMaxHp += hpRoll;
                charStr += strRoll;
                charAgi += agiRoll;
                charMind += minRoll;
                charSoul += soulRoll;
                charDef += defRoll;
                break;
            case "Black Mage":
                hpRoll = UnityEngine.Random.Range(3, 8);
                mpRoll = UnityEngine.Random.Range(4, 7);
                strRoll = UnityEngine.Random.Range(0, 3);
                agiRoll = UnityEngine.Random.Range(1, 3);
                minRoll = UnityEngine.Random.Range(3, 6);
                soulRoll = UnityEngine.Random.Range(1, 4);
                defRoll = UnityEngine.Random.Range(0, 3);

                charMaxHp += hpRoll;
                charMaxMp += mpRoll;
                charStr += strRoll;
                charAgi += agiRoll;
                charMind += minRoll;
                charSoul += soulRoll;
                charDef += defRoll;
                break;
            case "Monk":
                hpRoll = UnityEngine.Random.Range(6, 11);
                strRoll = UnityEngine.Random.Range(2, 5);
                agiRoll = UnityEngine.Random.Range(2, 4);
                minRoll = UnityEngine.Random.Range(0, 2);
                soulRoll = UnityEngine.Random.Range(0, 3);
                defRoll = UnityEngine.Random.Range(1, 3);

                charMaxHp += hpRoll;
                charStr += strRoll;
                charAgi += agiRoll;
                charMind += minRoll;
                charSoul += soulRoll;
                charDef += defRoll;
                break;
            case "Paladin":
                hpRoll = UnityEngine.Random.Range(8, 13);
                strRoll = UnityEngine.Random.Range(2, 5);
                agiRoll = UnityEngine.Random.Range(1, 3);
                minRoll = UnityEngine.Random.Range(0, 2);
                soulRoll = UnityEngine.Random.Range(2, 5);
                defRoll = UnityEngine.Random.Range(3, 6);

                charMaxHp += hpRoll;
                charStr += strRoll;
                charAgi += agiRoll;
                charMind += minRoll;
                charSoul += soulRoll;
                charDef += defRoll;
                break;
            case "Thief":
                hpRoll = UnityEngine.Random.Range(5, 10);
                strRoll = UnityEngine.Random.Range(2, 4);
                agiRoll = UnityEngine.Random.Range(3, 6);
                minRoll = UnityEngine.Random.Range(0, 2);
                soulRoll = UnityEngine.Random.Range(0, 3);
                defRoll = UnityEngine.Random.Range(1, 3);

                charMaxHp += hpRoll;
                charStr += strRoll;
                charAgi += agiRoll;
                charMind += minRoll;
                charSoul += soulRoll;
                charDef += defRoll;
                break;
            case "Warrior":
                hpRoll = UnityEngine.Random.Range(7, 12);
                strRoll = UnityEngine.Random.Range(3, 6);
                agiRoll = UnityEngine.Random.Range(1, 4);
                minRoll = UnityEngine.Random.Range(0, 2);
                soulRoll = UnityEngine.Random.Range(0, 3);
                defRoll = UnityEngine.Random.Range(2, 5);

                charMaxHp += hpRoll;
                charStr += strRoll;
                charAgi += agiRoll;
                charMind += minRoll;
                charSoul += soulRoll;
                charDef += defRoll;
                break;
            case "White Mage":
                hpRoll = UnityEngine.Random.Range(3, 8);
                mpRoll = UnityEngine.Random.Range(4, 7);
                strRoll = UnityEngine.Random.Range(0, 3);
                agiRoll = UnityEngine.Random.Range(1, 3);
                minRoll = UnityEngine.Random.Range(1, 4);
                soulRoll = UnityEngine.Random.Range(3, 6);
                defRoll = UnityEngine.Random.Range(0, 3);

                charMaxHp += hpRoll;
                charMaxMp += mpRoll;
                charStr += strRoll;
                charAgi += agiRoll;
                charMind += minRoll;
                charSoul += soulRoll;
                charDef += defRoll;
                break;
        }
    }
}
