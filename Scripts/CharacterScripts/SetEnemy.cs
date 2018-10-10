using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.QuestSystemPro;

//Enemies that are set to the level that they are found in
[CreateAssetMenu(fileName = "New Enemy", menuName = "Set Enemy")]
public class SetEnemy : SetStats
{
    public EquipableItem weapon;
    public EquipableItem offHand;
    public EquipableItem armor;
    public EquipableItem accessory;

    public int gold;
    public int exp;

    public QuestProgressDecorator progress;
    public string questIndicator;

    public void SetInit(string SetName, Sprite SetBattleSprite, int SetLevel,
        List<SkillScriptObject> SetSkills, int SetHp, int SetMp, int SetStr, int SetAgi,
        int SetMind, int SetSoul, int SetDef, EquipableItem SetWeapon,
        EquipableItem SetOffHand, EquipableItem SetArmor, EquipableItem SetAccessory,
        QuestProgressDecorator SetProgress, string SetIndicator)
    {
        BaseInit(SetName, SetBattleSprite, SetLevel, SetSkills);
        StatsInit(SetHp, SetMp, SetStr, SetAgi, SetMind, SetSoul, SetDef);

        weapon = SetWeapon;
        offHand = SetOffHand;
        armor = SetArmor;
        accessory = SetAccessory;

        progress = SetProgress;
        questIndicator = SetIndicator;
    }

    //Gets Strength
    public int GetStrength()
    {
        int fullStrength = strength + armor.currentStrength + accessory.currentStrength;

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
        int fullAgility = agility + armor.currentAgility + accessory.currentAgility;

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
        int fullMind = mind + armor.currentMind + accessory.currentMind;

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
        int fullSoul = soul + armor.currentSoul + accessory.currentSoul;

        if (weapon != null)
            fullSoul += weapon.currentSoul;
        if (offHand != null)
            fullSoul += offHand.currentSoul;

        if (buffs["Soul"].GetBuffed() == true)
            fullSoul += buffs["Soul"].GetStrength();

        return fullSoul;
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
            fullDefense = defense + armor.currentArmor + accessory.currentArmor;

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

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        if (currentHp <= 0)
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

    //Quest Progress when Enemy is killed
    public void OnKilled()
    {
        progress.Execute();
    }
}
