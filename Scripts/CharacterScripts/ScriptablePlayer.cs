using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Scriptable Object for Player Classes
[CreateAssetMenu(fileName = "New PlayerClass", menuName = "Player Character")]
public class ScriptablePlayer : LevelStats
{
    public enum PlayerClasses
    {
        Archer,
        BlackMage,
        Monk,
        Paladin,
        Thief,
        Warrior,
        WhiteMage
    }

    public PlayerClasses charClass;
    protected string playerClass;

    public enum PlayerArmor
    {
        Cloth,
        Leather,
        Plate
    }

    public PlayerArmor maxArmor;

    public Sprite classHead;

    public int currentExp;

    public CharacterExpChart expChart;

    public bool canShield;
    public bool canDuelWield;
    public bool canSword;
    public bool canDagger;
    public bool canMace;
    public bool canBow;
    public bool canStaff;
    public bool canRod;
    public bool canFists;

    public EquipableItem weapon;
    public EquipableItem offHand;
    public EquipableItem armor;
    public EquipableItem accessory;

    private Dictionary<string, List<int>> buffStats = new Dictionary<string, List<int>>();

    //Sets Player Stats
    public void PlayerInit(PlayerClasses SetClass, PlayerArmor SetMaxArmor, Sprite SetClassHead,
        int SetExp, CharacterExpChart SetExpChart, bool SetShield, bool SetDuel, bool SetSword,
        bool SetDagger, bool SetMace, bool SetBow, bool SetStaff, bool SetRod, bool SetFists,
        EquipableItem SetWeapon, EquipableItem SetOffHand, EquipableItem SetArmor,
        EquipableItem SetAccessory, string SetName, Sprite SetBattleSprite, int SetLevel,
        List<SkillScriptObject> SetSkills, List<int> SetLevelHp, List<int> SetLevelMp,
        List<int> SetLevelStr, List<int> SetLevelAgi, List<int> SetLevelMind,
        List<int> SetLevelSoul, List<int> SetLevelDef)
    {

        BaseInit(SetName, SetBattleSprite, SetLevel, SetSkills);
        StatsInit(SetLevelHp, SetLevelMp, SetLevelStr, SetLevelAgi, SetLevelMind, SetLevelSoul,
            SetLevelDef);

        charClass = SetClass;

        playerClass = SetClass.ToString();

        if (playerClass.Equals("BlackMage"))
            playerClass = "Black Mage";
        else if (playerClass.Equals("WhiteMage"))
            playerClass = "White Mage";

        maxArmor = SetMaxArmor;
        classHead = SetClassHead;
        currentExp = SetExp;
        expChart = SetExpChart;

        canShield = SetShield;
        canDuelWield = SetDuel;
        canSword = SetSword;
        canDagger = SetDagger;
        canMace = SetMace;
        canBow = SetBow;
        canStaff = SetStaff;
        canRod = SetRod;
        canFists = SetFists;
        weapon = SetWeapon;
        offHand = SetOffHand;
        armor = SetArmor;
        accessory = SetAccessory;

        buffStats.Add("Strength", levelStrength);
        buffStats.Add("Agility", levelAgility);
        buffStats.Add("Mind", levelMind);
        buffStats.Add("Soul", levelSoul);
        buffStats.Add("Defense", levelDefense);
        buffStats.Add("Defend", levelDefense);
    }

    //get players class
    public string GetClass()
    {
        playerClass = charClass.ToString();

        if (playerClass.Equals("BlackMage"))
            playerClass = "Black Mage";
        else if (playerClass.Equals("WhiteMage"))
            playerClass = "White Mage";

        return playerClass;
    }

    //Gets Strength
    public int GetStrength()
    {
        int fullStrength = levelStrength[level] + armor.currentStrength + accessory.currentStrength;

        if (weapon != null)
            fullStrength += weapon.currentStrength;
        if (offHand != null)
            fullStrength += offHand.currentStrength;

        if (buffs["Strength"].GetBuffed() == true)
            fullStrength += buffs["Strength"].GetStrength();

        return fullStrength;
    }

    //Gets Agility
    public int GetAgility()
    {
        int fullAgility = levelAgility[level] + armor.currentAgility + accessory.currentAgility;

        if (weapon != null)
            fullAgility += weapon.currentAgility;
        if (offHand != null)
            fullAgility += offHand.currentAgility;

        if (buffs["Agility"].GetBuffed() == true)
            fullAgility += buffs["Agility"].GetStrength();

        return fullAgility;
    }

    //Gets Mind
    public int GetMind()
    {
        int fullMind = levelMind[level] + armor.currentMind + accessory.currentMind;

        if (weapon != null)
            fullMind += weapon.currentMind;
        if (offHand != null)
            fullMind += offHand.currentMind;

        if (buffs["Mind"].GetBuffed() == true)
            fullMind += buffs["Mind"].GetStrength();

        return fullMind;
    }

    //Gets Soul
    public int GetSoul()
    {
        int fullSoul = levelSoul[level] + armor.currentSoul + accessory.currentSoul;

        if (weapon != null)
            fullSoul += weapon.currentSoul;
        if (offHand != null)
            fullSoul += offHand.currentSoul;

        if (buffs["Soul"].GetBuffed() == true)
            fullSoul += buffs["Soul"].GetStrength();

        return fullSoul;
    }

    //Gets Defense shown in Stat Window
    public int GetStatDefense()
    {
        return levelDefense[level];
    }

    //Gets Defense
    public int GetDefense(SkillScriptObject skillUsed)
    {
        bool isPhysical;

        if (skillUsed.damageType.Equals("Physical"))
            isPhysical = true;
        else
            isPhysical = false;

        int fullDefense = 0;

        if (isPhysical)
        {
            fullDefense = GetStatDefense() + armor.currentArmor + accessory.currentArmor;

            if (weapon != null)
                fullDefense += weapon.currentArmor;
            if (offHand != null)
                fullDefense += offHand.currentArmor;

            if (buffs["Defense"].GetBuffed() == true)
                fullDefense += buffs["Defense"].GetStrength();

            if (buffs["Defend"].GetBuffed() == true)
                fullDefense += buffs["Defend"].GetStrength();
        }
        else
            fullDefense = Mathf.RoundToInt(GetSoul() * 1.25f);

        return fullDefense;
    }

    public void SetDefend()
    {
        buffs["Defend"].SetBuffed(true);
        buffs["Defend"].SetStrength(Mathf.RoundToInt(levelDefense[level] * 1.5f));
        buffs["Defend"].SetRounds(2);
    }

    //apply buffs to character
    public void SetBuff(SkillScriptObject buff)
    {
        string buffStat = buff.buffStat;
        int rounds = buff.effectTurns;
        List<int> levelStat = buffStats[buffStat];
        int buffStrength = buff.getLifeChange(levelStat[level]);

        buffs[buffStat].SetBuffed(true);
        buffs[buffStat].SetStrength(buffStrength);
        buffs[buffStat].SetRounds(rounds);
    }

    //heals char to max
    public void healToFull() { currentHp = levelHp[level]; currentMp = levelMp[level]; }

    //changes characters current hp
    public bool changeHP(int hpChange)
    {
        currentHp += hpChange;
        if (currentHp > levelHp[level])
        {
            currentHp = levelHp[level];
            return false;
        }
        else if (currentHp <= 0)
        {
            currentHp = 0;
            return true;
        }
        else
            return false;
    }

    //changes characters current mp
    public void changeMP(int mpChange)
    {
        currentMp += mpChange;
        if (currentMp > levelMp[level])
        {
            currentMp = levelMp[level];
        }
        else if (currentMp <= 0)
        {
            currentMp = 0;
        }
    }

    //calculates base damage for skill
    public int SkillDamage(SkillScriptObject skill)
    {
        int damage = 0;

        string damType = skill.damageType;
        if (damType.Equals("Physical"))
        {
            int statTran = GetStrength() + weapon.currentDamage;
            if (offHand != null && offHand.type == "Weapon")
                statTran += Mathf.RoundToInt(offHand.currentDamage * 0.25f);

            damage = skill.getLifeChange(statTran);
        }
        else
            damage = skill.getLifeChange(GetMind());
        return damage;
    }

    //increase experience
    public bool incExp(int increase)
    {
        currentExp += increase;

        if (currentExp >= expChart.experience[level])
        {
            do
            {
                currentExp = 0;
                level++;
            }
            while (currentExp >= expChart.experience[level]);

            return true;
        }
        else
            return false;
    }

    //changes equipment on the character
    public EquipableItem ChangeWeapon(EquipableItem newWeapon)
    {
        EquipableItem currentWeapon = weapon;

        weapon = newWeapon;
        return currentWeapon;
    }

    //changes what off hand is currently equipped
    public EquipableItem ChangeOffHand(EquipableItem newOffHand)
    {
        EquipableItem currentOffHand = offHand;

        offHand = newOffHand;
        return currentOffHand;
    }

    //changes what armor is currently equipped
    public EquipableItem ChangeArmor(EquipableItem newArmor)
    {
        EquipableItem currentArmor = armor;

        armor = newArmor;
        return currentArmor;
    }

    //changes what accessory is currently equipped
    public EquipableItem ChangeAccessory(EquipableItem newAccessory)
    {
        EquipableItem currentAccessory = accessory;
        accessory = newAccessory;
        return currentAccessory;
    }
}

