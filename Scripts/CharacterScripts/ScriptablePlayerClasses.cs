using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.QuestSystemPro;

[CreateAssetMenu(fileName = "Player Class", menuName = "Player Class")]
public class ScriptablePlayerClasses : ScriptableObject {

    public new string name;
    public string charClass;
    public Sprite classHead;
    public Sprite battleSprite;
    public string maxArmor;
    public int level;
    public int currentHp;
    public int currentMp;
    public int currentExp;
    public List<int> enemyGold;
    public List<int> enemyExp;
    public CharacterExpChart expChart;
    public List<int> levelHp;
    public List<int> levelMp;
    public List<int> levelStrength;
    public List<int> levelAgility;
    public List<int> levelMind;
    public List<int> levelSoul;
    public List<int> levelDefense;
    private Dictionary<string, List<int>> buffStats = new Dictionary<string, List<int>>();
    public bool isEnemy;
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

    public List<SkillScriptObject> knownSkills = new List<SkillScriptObject>();

    public QuestProgressDecorator progress;
    public string questIndicator;

    private Dictionary<string, SkillScriptObject> skills = new Dictionary<string, SkillScriptObject>();
    private Dictionary<string, BuffClass> buffs = new Dictionary<string, BuffClass>();
    private Dictionary<string, DebuffClass> debuffs = new Dictionary<string, DebuffClass>();

    public void Init(string SetName, string SetClass, int SetLevel, int SetExp, Sprite SetClassHead, 
        Sprite SetBattleSprite, string SetMaxArmor, CharacterExpChart SetChart, List<int> SetHP, 
        List<int> SetMP, List<int> SetStr, List<int> SetAgi, List<int> SetMind, List<int> SetSoul, 
        List<int> SetDef, bool SetEnemy, bool SetShield, bool SetDuelWield, bool SetSword, 
        bool SetDagger, bool SetMace, bool SetBow, bool SetStaff, bool SetRod, bool SetFist, 
        EquipableItem SetWeapon, EquipableItem SetOffHand, EquipableItem SetArmor, 
        EquipableItem SetAccessory, List<SkillScriptObject> SetSkills, QuestProgressDecorator SetQuest,
        string SetIndicator)
    {
        name = SetName;
        charClass = SetClass;
        level = SetLevel;
        currentExp = SetExp;
        classHead = SetClassHead;
        battleSprite = SetBattleSprite;
        maxArmor = SetMaxArmor;
        expChart = SetChart;
        levelHp = SetHP;
        levelMp = SetMP;
        levelStrength = SetStr;
        levelAgility = SetAgi;
        levelMind = SetMind;
        levelSoul = SetSoul;
        levelDefense = SetDef;
        isEnemy = SetEnemy;
        canShield = SetShield;
        canDuelWield = SetDuelWield;
        canSword = SetSword;
        canDagger = SetDagger;
        canMace = SetMace;
        canBow = SetBow;
        canStaff = SetStaff;
        canRod = SetRod;
        canFists = SetFist;
        weapon = SetWeapon;        
        offHand = SetOffHand;
        armor = SetArmor;
        accessory = SetAccessory;
        knownSkills = SetSkills;
        
        currentHp = levelHp[level];
        currentMp = levelMp[level];

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

        updateSkills();

        buffStats.Add("Strength", levelStrength);
        buffStats.Add("Agility", levelAgility);
        buffStats.Add("Mind", levelMind);
        buffStats.Add("Soul", levelSoul);
        buffStats.Add("Defense", levelDefense);
        buffStats.Add("Defend", levelDefense);

        progress = SetQuest;
        questIndicator = SetIndicator;
    }

    public Dictionary<string, SkillScriptObject> GetAllSkills() { return skills; }
    public SkillScriptObject GetSkill(string skillName) { return skills[skillName]; }

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
        int fullDefense = levelDefense[level] + armor.currentArmor + accessory.currentArmor;

        if (weapon != null)
            fullDefense += weapon.currentArmor;
        if (offHand != null)
            fullDefense += offHand.currentArmor;

        if (buffs["Defense"].GetBuffed() == true)
            fullDefense += buffs["Defense"].GetStrength();

        if (buffs["Defend"].GetBuffed() == true)
            fullDefense += buffs["Defend"].GetStrength();

        return fullDefense;
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

        if(isPhysical)
        {
            fullDefense = GetStatDefense();
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

    //afflict character with a debuff
    public void AfflictStatus(SkillScriptObject debuff, int stat)
    {
        string debuffAil = debuff.ailment;
        int rounds = debuff.effectTurns;
        int debuffStrength = debuff.getLifeChange(stat);

        debuffs[debuffAil].SetRounds(rounds);
        debuffs[debuffAil].SetStrength(debuffStrength);
        debuffs[debuffAil].SetEffected(true);
        debuffs[debuffAil].SetIcon(debuff.buffIcon);
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
    public void BuffCounter(GameObject panel)
    {
        foreach (KeyValuePair<string, BuffClass> buff in buffs)
        {
            string key = buff.Key;
            if (buffs[key].GetBuffed() == true)
            {
                bool isFinished = buffs[key].UpdateRounds();

                if (isFinished)
                {
                    if (key != "Defend")
                        panel.GetComponent<PlayerIcons>().removeIcons(key);
                }
            }
        }
    }

    //reduce debuffs by one round.  check if debuff is finished, removed if so
    public void DebuffCounter(GameObject panel)
    {
        foreach (KeyValuePair<string, DebuffClass> debuff in debuffs)
        {
            string key = debuff.Key;

            if (debuffs[key].GetEffected() == true)
            {
                bool isFinished = debuffs[key].reduceRound();
                if (isFinished)
                    panel.GetComponent<PlayerIcons>().removeIcons(key);
            }
        }
    }

    //get poison damage
    public int poisonDamage()
    {
        return debuffs["Poison"].GetStrength();
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
                statTran += offHand.currentDamage;

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

    private void updateSkills()
    {
        foreach (SkillScriptObject skill in knownSkills)
        {
            if(skill.skillLevel <= level)
            {
                if (!skills.ContainsKey(skill.skillName))
                {
                    skills.Add(skill.skillName, skill);
                }                    
            }            
        }
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

    public void OnKilled()
    {
        progress.Execute();
    }
}
