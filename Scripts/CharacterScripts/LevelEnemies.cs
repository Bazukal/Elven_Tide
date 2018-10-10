using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.QuestSystemPro;


//Enemies found in the Hole that level based on the Player
[CreateAssetMenu(fileName = "New Enemy", menuName = "Level Enemy")]
public class LevelEnemies : LevelStats
{
    public EnemyEquipList equipList;

    public List<int> enemyGold;
    public List<int> enemyExp;

    private EquipableItem weapon;
    private EquipableItem offHand;
    private EquipableItem armor;
    private EquipableItem accessory;

    public int gold;
    public int exp;

    public void LevelInit(string SetName, Sprite SetBattleSprite,
        List<SkillScriptObject> SetSkills, List<int> SetLevelHp, List<int> SetLevelMp,
        List<int> SetLevelStr, List<int> SetLevelAgi, List<int> SetLevelMind,
        List<int> SetLevelSoul, List<int> SetLevelDef, List<int> SetGold, List<int> SetExp)
    {
        StatsInit(SetLevelHp, SetLevelMp, SetLevelStr, SetLevelAgi, SetLevelMind, SetLevelSoul,
            SetLevelDef);

        int aveLevel = Manager.manager.AveLevel();
        int index = aveLevel <= 5 ? 0 : aveLevel <= 10 ? 1 : aveLevel <= 20 ? 2 : aveLevel <= 30 ? 3 :
            aveLevel <= 40 ? 4 : aveLevel <= 50 ? 5 : aveLevel <= 60 ? 6 : aveLevel <= 70 ? 7 :
            aveLevel <= 80 ? 8 : 9;

        BaseInit(SetName, SetBattleSprite, aveLevel, SetSkills);

        weapon = equipList.returnWeapon(level);
        offHand = equipList.returnOffHand(level);
        armor = equipList.returnArmor(level);
        accessory = equipList.returnAccessory(level);

        gold = enemyGold[index];
        exp = enemyExp[index];
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
}
