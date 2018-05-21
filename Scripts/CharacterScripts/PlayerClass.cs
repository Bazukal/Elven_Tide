using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerClass {

    private string name;
    private string charClass;
    private string maxArmor;
	private int currentHP;
    private int level;
    private int maxHP;
    private int currentMP;
    private int maxMP;
    private int strength;
    private int agility;
    private int mind;
    private int soul;
    private int defense;
    private int currentExp;
    private int levelExp;
    private bool canShield;
    private Dictionary<string, SkillClass> skills = new Dictionary<string, SkillClass>();
    private Dictionary<string, EquipmentClass> equipment = new Dictionary<string, EquipmentClass>();
    private Dictionary<string, BuffClass> buffs = new Dictionary<string, BuffClass>();
    private Dictionary<string, DebuffClass> debuffs = new Dictionary<string, DebuffClass>();

    public PlayerClass() { }

    public PlayerClass(string CharClass, string MaxArmor, int MaxHP, int MaxMP, int Strength, int Agility, int Mind, int Soul, 
        int Defense, bool CanShield,  EquipmentClass Weapon, EquipmentClass Offhand, EquipmentClass Armor, 
        EquipmentClass Accessory)
    {
        charClass = CharClass;
        maxArmor = MaxArmor;
        level = 1;
        maxHP = MaxHP;
        currentHP = maxHP;
        maxMP = MaxMP;
        currentMP = maxMP;
        strength = Strength;
        agility = Agility;
        mind = Mind;
        soul = Soul;
        defense = Defense;
        canShield = CanShield;

        equipment.Add("Weapon", Weapon);
        equipment.Add("Offhand", Offhand);
        equipment.Add("Armor", Armor);
        equipment.Add("Accessory", Accessory);

        buffs.Add("Strength", new BuffClass());
        buffs.Add("Agility", new BuffClass());
        buffs.Add("Mind", new BuffClass());
        buffs.Add("Soul", new BuffClass());
        buffs.Add("Defense", new BuffClass());
        buffs.Add("Defend", new BuffClass());

        debuffs.Add("Poison", new DebuffClass());
        debuffs.Add("Confused", new DebuffClass());
        debuffs.Add("Paralyzed", new DebuffClass());
        debuffs.Add("Blind", new DebuffClass());
        debuffs.Add("Mute", new DebuffClass());

        levelExp = increaseLevelExp(1);
        currentExp = 0;
        updateSkills();
    }

    //setters
    public void SetName(string Name) { name = Name; }

    //getters
    public string GetName() { return name; }
    public string GetClass() { return charClass; }
    public string GetMaxArmor() { return maxArmor; }
    public bool CanShield() { return canShield; }
    public int GetLevel() { return level; }
    public int GetMaxHP() { return maxHP; }
    public int GetCurrentHP() { return currentHP; }
    public int GetMaxMP() { return maxMP; }
    public int GetCurrentMP() { return currentMP; }
    public Dictionary<string, SkillClass> GetAllSkills() { return skills; }
    public SkillClass GetSkill(string skillName) { return skills[skillName]; }
    public EquipmentClass GetEquipment(string whichEquipment) { return equipment[whichEquipment]; }

    public int GetStrength()
    {
        int fullStrength = strength;

        foreach (KeyValuePair<string, EquipmentClass> equip in equipment)
        {
            string key = equip.Key;
            try
            {
                fullStrength += equipment[key].GetStr();
            }
            catch { }
        }

        if (buffs["Strength"].GetBuffed() == true)
            fullStrength += buffs["Strength"].GetStrength();

       return fullStrength;
    }

    public int GetAgility()
    {
        int fullAgility = agility;

        foreach (KeyValuePair<string, EquipmentClass> equip in equipment)
        {
            string key = equip.Key;
            try
            {
                fullAgility += equipment[key].GetAgi();
            }
            catch { }
        }

        if (buffs["Agility"].GetBuffed() == true)
            fullAgility += buffs["Agility"].GetStrength();

        return fullAgility;
    }

    public int GetMind()
    {
        int fullMind = mind;

        foreach (KeyValuePair<string, EquipmentClass> equip in equipment)
        {
            string key = equip.Key;
            try
            {
                fullMind += equipment[key].GetMind();
            }
            catch { }
        }

        if (buffs["Mind"].GetBuffed() == true)
            fullMind += buffs["Mind"].GetStrength();

        return fullMind;
    }

    public int GetSoul()
    {
        int fullSoul = soul;

        foreach (KeyValuePair<string, EquipmentClass> equip in equipment)
        {
            string key = equip.Key;
            try
            {
                fullSoul += equipment[key].GetSoul();
            }
            catch { }
        }

        if (buffs["Soul"].GetBuffed() == true)
            fullSoul += buffs["Soul"].GetStrength();

        return fullSoul;
    }

    public int GetDefense()
    {
        int fullDefense = defense;

        foreach (KeyValuePair<string, EquipmentClass> equip in equipment)
        {
            string key = equip.Key;
            try
            {
                fullDefense += equipment[key].GetArmor();
            }
            catch { }
        }

        if (buffs["Defense"].GetBuffed() == true)
            fullDefense += buffs["Defense"].GetStrength();

        if (buffs["Defend"].GetBuffed() == true)
            fullDefense += buffs["Defend"].GetStrength();

        return fullDefense;
    }

    public void SetDefend()
    {
        buffs["Defend"].SetBuffed(true);
        buffs["Defend"].SetStrength(Mathf.RoundToInt(defense * 1.5f));
        buffs["Defend"].SetRounds(2);
    }

    //apply buffs to character
    public void SetBuff(string stat, int str, int dur)
    {
        buffs[stat].SetBuffed(true);
        buffs[stat].SetStrength(str);
        buffs[stat].SetRounds(dur);
    }

    //afflict character with a debuff
    public void AfflictStatus(string type, int rounds, int strength)
    {
        debuffs[type].SetRounds(rounds);
        debuffs[type].SetStrength(strength);
        debuffs[type].SetEffected(true);
    }

    //check if character is afflicted with a debuff
    public bool CheckAffliction(string affliction)
    {
        return debuffs[affliction].GetEffected();
    }

    //gets characters debuffs
    public Dictionary<string, DebuffClass> GetDebuffs()
    {
        return debuffs;
    }

    //gets characters buffs
    public Dictionary<string, BuffClass> GetBuffs()
    {
        return buffs;
    }

    //cures status ailment
    public void CureStatus(string type)
    {
        debuffs[type].SetEffected(false);
        debuffs[type].SetStrength(0);
        debuffs[type].SetRounds(0);
    }

    //clears all status at the end of battle
    public void ResetStatus()
    {
        foreach (KeyValuePair<string, DebuffClass> debuff in debuffs)
        {
            string key = debuff.Key;

            debuffs[key].SetEffected(false);
            debuffs[key].SetStrength(0);
            debuffs[key].SetRounds(0);
        }
    }

    //reduces buffs by one round.  if buff is finished, remove buff
    public void BuffCounter(PlayerIcons panel)
    {
        foreach (KeyValuePair<string, BuffClass> buff in buffs)
        {
            string key = buff.Key;

            if (buffs[key].GetBuffed() == true)
            {
                bool isFinished = buffs[key].UpdateRounds();

                if (isFinished)
                    panel.removeIcons(key);
            }
        }
    }

    //reduce debuffs by one round.  check if debuff is finished, removed if so
    public void DebuffCounter(PlayerIcons panel)
    {
        foreach (KeyValuePair<string, DebuffClass> debuff in debuffs)
        {
            string key = debuff.Key;

            if (debuffs[key].GetEffected() == true)
            {
                bool isFinished = debuffs[key].reduceRound();

                if (isFinished)
                    panel.removeIcons(key);
            }
        }
    }

    //get poison damage
    public int poisonDamage()
    {
        return debuffs["Poison"].GetStrength();
    }

    //calculates base damage for attack
    public int AttackDamage(bool main)
    {
        int damage = 0;

        if(charClass.Equals("Monk"))
        {
            damage = Mathf.RoundToInt(strength * 1.5f);
        }
        else
        {
            if (main == true)
            {
                damage = GetStrength() + equipment["Weapon"].GetDamage();
            }
            else 
            {
                try
                {
                    if (equipment["Offhand"].GetItemType().Equals("Weapon"))
                        damage = GetStrength() + equipment["Offhand"].GetDamage();
                    else
                        damage = -1;
                }
                catch
                {
                    damage = -1;
                }
            }                
        }
        return damage;
    }

    //calculates base damage for skill
    public int SkillDamage(SkillClass skill, bool main)
    {
        int damage = 0;

        string damType = skill.GetDamageType();
        if (damType.Equals("Physical"))
        {
            if (charClass.Equals("Monk"))
            {
                float levelMod = (level * 0.01f) + 1;
                int statTran = Mathf.RoundToInt(strength * levelMod);
                damage = skill.healthChange(statTran);
            }
            else
            {
                if (main == true)
                {
                    int statTran = GetStrength() + equipment["Weapon"].GetDamage();
                    damage = skill.healthChange(statTran);
                }
                else 
                {
                    try
                    {
                        if (equipment["Offhand"].GetItemType().Equals("Weapon"))
                        {
                            int statTran = GetStrength() + equipment["Offhand"].GetDamage();
                            damage = skill.healthChange(statTran);
                        }
                        else
                            damage = -1;
                    }
                    catch
                    {
                        damage = -1;
                    }
                }
            }
        }
        else
            damage = skill.healthChange(GetMind());

        return damage;
    }

    //find how many rounds character attacks based on agility
    public int numAttacks()
    {
        return (GetAgility() / 30) + 1;
    }

    //changes characters current hp
    public bool changeHP(int hpChange)
    {
        currentHP += hpChange;
        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    //changes characters current mp
    public void changeMP(int mpChange) { currentMP += mpChange; }

    //heals char to max
    public void healToFull() { currentHP = maxHP; currentMP = maxMP; }

    public int GetCurExp() { return currentExp; }
    public bool AddCurExp(int expGain)
    {
        currentExp += expGain;

        if (currentExp >= levelExp)
        {
            do
            {
                CharLevelUp();
            }
            while (currentExp >= levelExp);

            return true;
        }
        else
            return false;
    }
    public int GetLvlExp() { return levelExp; }

    //character level up
    public void CharLevelUp()
    {
        float hpExpon = 0.75f;
        float mpExpon = 0.25f;
        float statExpon = 0.1f;

        level++;
        updateSkills();
        int tempMaxExp = levelExp;
        levelExp = increaseLevelExp(level);
        currentExp = currentExp - tempMaxExp;
        
        StatClass charStats = LevelGrowth.growth.returnStats(charClass);

        int hpRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getHPMin(), charStats.getHPMax()) * Mathf.Pow(level, hpExpon));
        int mpRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getMPMin(), charStats.getMPMax()) * Mathf.Pow(level, mpExpon));
        int strRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getStrMin(), charStats.getStrMax()) * Mathf.Pow(level, statExpon));
        int agiRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getAgiMin(), charStats.getAgiMax()) * Mathf.Pow(level, statExpon));
        int minRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getMindMin(), charStats.getMindMax()) * Mathf.Pow(level, statExpon));
        int soulRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getSoulMin(), charStats.getSoulMax()) * Mathf.Pow(level, statExpon));
        int defRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getDefMin(), charStats.getDefMax()) * Mathf.Pow(level, statExpon));

        maxHP += hpRoll;
        maxMP += mpRoll;
        strength += strRoll;
        agility += agiRoll;
        mind += minRoll;
        soul += soulRoll;
        defense += defRoll;
    }

    private int increaseLevelExp(int level)
    {
        float expon = 1.25f;
        int baseExp = 25;

        return Mathf.RoundToInt(baseExp * (Mathf.Pow(level, expon)));
    }

    private void updateSkills()
    {
        List<SkillClass>newSkills = GameSkills.skills.GetSkills(level, charClass);

        foreach (SkillClass skill in newSkills)
        {
            if (!skills.ContainsKey(skill.GetName()))
                skills.Add(skill.GetName(), skill);
        }
    }

    //changes equipment on the character
    public EquipmentClass ChangeWeapon(EquipmentClass newWeapon)
    {
        EquipmentClass currentWeapon = equipment["Weapon"];

        equipment["Weapon"] = newWeapon;
        return currentWeapon;
    }

    //changes what off hand is currently equipped
    public EquipmentClass ChangeOffHand(EquipmentClass newOffHand)
    {
        EquipmentClass currentOffHand = equipment["Offhand"];

        equipment["Offhand"] = newOffHand;
        return currentOffHand;
    }

    //changes what armor is currently equipped
    public EquipmentClass ChangeArmor(EquipmentClass newArmor)
    {
        EquipmentClass currentArmor = equipment["Armor"];

        equipment["Armor"] = newArmor;
        return currentArmor;
    }

    //changes what accessory is currently equipped
    public EquipmentClass ChangeAccessory(EquipmentClass newAccessory)
    {
        EquipmentClass currentAccessory = equipment["Accessory"];

        equipment["Accessory"] = newAccessory;
        return currentAccessory;
    }
}
