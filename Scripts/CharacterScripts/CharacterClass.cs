using System.Collections.Generic;
using System;
using UnityEngine;

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
    private int charBaseStr;
    private int charEquipStr;
    private int charBaseAgi;
    private int charEquipAgi;
    private int charBaseMind;
    private int charEquipMind;
    private int charBaseSoul;
    private int charEquipSoul;
    private int charDef;
    private int charEquipArmor;
    private int defendAmount;
    private int charCurExp;
    private int charLvlExp;
    private List<SkillClass> charSkills = new List<SkillClass>();
    private EquipableItemClass weapon;
    private EquipableItemClass offHand;
    private EquipableItemClass armor;
    private EquipableItemClass accessory;
    private Dictionary<string, DebuffClass> debuffs = new Dictionary<string, DebuffClass>();
    private Dictionary<string, BuffClass> buffs = new Dictionary<string, BuffClass>();

    //constructors for Character Class
    public CharacterClass()
    {

    }

    public CharacterClass(string CharName, string CharClass, string MaxArmor, bool CanShield, int CharLevel, 
        int CharHP, int CharMP, int CharSTR, int CharAGI, int CharMind, int CharSoul, int CharDef, 
        EquipableItemClass Weapon, EquipableItemClass OffHand, EquipableItemClass Armor, 
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
        charBaseStr = CharSTR;
        charBaseAgi = CharAGI;
        charBaseMind = CharMind;
        charBaseSoul = CharSoul;
        charDef = CharDef;
        charCurExp = 0;
        charLvlExp = levelExp(charLevel);
        weapon = Weapon;
        offHand = OffHand;
        armor = Armor;
        accessory = Accessory;

        updateEquipStats();
        updateSkills();

        debuffs.Add("Poison", new DebuffClass());
        debuffs.Add("Confused", new DebuffClass());
        debuffs.Add("Paralyzed", new DebuffClass());
        debuffs.Add("Blind", new DebuffClass());
        debuffs.Add("Mute", new DebuffClass());

        buffs.Add("Strength", new BuffClass());
        buffs.Add("Agility", new BuffClass());
        buffs.Add("Mind", new BuffClass());
        buffs.Add("Soul", new BuffClass());
        buffs.Add("Defense", new BuffClass());
    }

    //getters and setters of Character Class
    public string GetCharName() { return charName; }
    public string GetCharClass() { return charClass; }
    public string GetMaxArmor() { return maxArmor; }
    public bool GetShield() { return canShield; }
    public int GetCharLevel() { return charLevel; }
    public void SetCharLevel(int level) { charLevel = level; }

    public int GetCharMaxHp() { return charMaxHp; }
    public int GetCharCurrentHp() { return charCurrentHp; }
    public void ChangeCharCurrentHp(int hpChange)
    {        
        charCurrentHp += hpChange;

        if (charCurrentHp > charMaxHp)
            charCurrentHp = charMaxHp;

        if (charCurrentHp < 0)
            charCurrentHp = 0;
    }

    public void healToFull() { charCurrentHp = charMaxHp; charCurrentMp = charMaxMp; }

    public int GetCharMaxMp() { return charMaxMp; }
    public int GetCharCurrentMp() { return charCurrentMp; }
    public void ChangeCharCurrentMp(int mpChange)
    {
        charCurrentMp += mpChange;

        if (charCurrentMp > charMaxMp)
            charCurrentMp = charMaxMp;

        if (charCurrentMp < 0)
            charCurrentMp = 0;
    }

    public int GetTotalStr()
    {
        int totalStr = charBaseStr + charEquipStr + buffs["Strength"].GetStrength();
        return totalStr;
    }
    
    public int GetTotalAgi()
    {
        int totalAgi = charBaseAgi + charEquipAgi + buffs["Agility"].GetStrength();
        return totalAgi;
    }
    
    public int GetTotalMind()
    {
        int totalMind = charBaseMind + charEquipMind + buffs["Mind"].GetStrength();
        return totalMind;
    }
    
    public int GetTotalSoul()
    {
        int totalSoul = charBaseSoul + charEquipSoul + buffs["Soul"].GetStrength();
        return totalSoul;
    }
    
    public void SetDefendAmount() { defendAmount = Mathf.RoundToInt(charDef * 1.5f); }
    public void ResetDefendAmount() { defendAmount = 0; }
    public int GetTotalDef()
    {
        int totalDef = charDef + charEquipArmor + buffs["Defense"].GetStrength() + defendAmount;
        return totalDef;
    }

    public int GetCurExp() { return charCurExp; }
    public bool AddCurExp(int expGain)
    {
        charCurExp += expGain;

        if (charCurExp >= charLvlExp)
        {
            CharLevelUp();
            return true;
        }
        else
            return false;
    }
    public int GetLvlExp() { return charLvlExp; }

    public List<SkillClass> GetCharSkills() { return charSkills; }

    public EquipableItemClass GetWeapon() { return weapon; }
    public EquipableItemClass GetOffHand() { return offHand; }
    public EquipableItemClass GetArmor() { return armor; }
    public EquipableItemClass GetAccessory() { return accessory; }

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

    //cures status ailment
    public void CureStatus(string type)
    {
        debuffs[type].SetEffected(false);
        debuffs[type].SetStrength(0);
        debuffs[type].SetRounds(0);
    }

    //get listing of chars debuffs
    public Dictionary<string, DebuffClass> GetDebuffs()
    {
        return debuffs;
    }

    //clears all status at the end of battle
    public void ResetStatus()
    {
        foreach(KeyValuePair<string, DebuffClass> debuff in debuffs)
        {
            string key = debuff.Key;

            debuffs[key].SetEffected(false);
            debuffs[key].SetStrength(0);
            debuffs[key].SetRounds(0);
        }
    }

    //reduces buffs by one round.  if buff is finished, remove buff
    public void BuffCounter()
    {
        foreach (KeyValuePair<string, BuffClass> buff in buffs)
        {
            string key = buff.Key;

            if (buffs[key].GetBuffed() == true)
            {
                buffs[key].UpdateRounds();
            }
        }
    }

    //reduce debuffs by one round.  check if debuff is finished, removed if so
    public void DebuffCounter()
    {
        foreach (KeyValuePair<string, DebuffClass> debuff in debuffs)
        {
            string key = debuff.Key;

            if (debuffs[key].GetEffected() == true)
            {
                if (key.Equals("Poison"))
                    ChangeCharCurrentHp(-debuffs[key].GetStrength());

                debuffs[key].reduceRound();
            }
        }
    }

    //changes equipment on the character
    public EquipableItemClass ChangeWeapon(EquipableItemClass newWeapon)
    {
        EquipableItemClass currentWeapon = weapon;

        weapon = newWeapon;
        updateEquipStats();
        return currentWeapon;
    }

    //changes what off hand is currently equipped
    public EquipableItemClass ChangeOffHand(EquipableItemClass newOffHand)
    {
        EquipableItemClass currentOffHand = offHand;

        offHand = newOffHand;
        updateEquipStats();
        return currentOffHand;
    }

    //changes what armor is currently equipped
    public EquipableItemClass ChangeArmor(EquipableItemClass newArmor)
    {
        EquipableItemClass currentArmor = armor;

        armor = newArmor;
        updateEquipStats();
        return currentArmor;
    }

    //changes what accessory is currently equipped
    public EquipableItemClass ChangeAccessory(EquipableItemClass newAccessory)
    {
        EquipableItemClass currentAccessory = accessory;

        accessory = newAccessory;
        updateEquipStats();
        return currentAccessory;
    }

    //updates the amount of stats that all the equipment gives the character
    private void updateEquipStats()
    {
        int tempStr = armor.GetStr() + accessory.GetStr();
        int tempAgi = armor.GetAgi() + accessory.GetAgi();
        int tempMind = armor.GetMind() + accessory.GetMind();
        int tempSoul = armor.GetSoul() + accessory.GetSoul();
        int tempArmor = armor.GetArmor() + accessory.GetArmor();

        try
        {
            tempStr += weapon.GetStr();
            tempAgi += weapon.GetAgi();
            tempMind += weapon.GetMind();
            tempSoul += weapon.GetSoul();
            tempArmor += weapon.GetArmor();
        }
        catch
        {

        }
                
        try
        {
            tempStr += offHand.GetStr();
            tempAgi += offHand.GetAgi();
            tempMind += offHand.GetMind();
            tempSoul += offHand.GetSoul();
            tempArmor += offHand.GetArmor();
        }
        catch
        {
           
        }
        
        charEquipStr = tempStr;
        charEquipAgi = tempAgi;
        charEquipMind = tempMind;
        charEquipSoul = tempSoul;
        charEquipArmor = tempArmor;
    }

    //character level up
    public void CharLevelUp()
    {
        float hpExpon = 0.75f;
        float mpExpon = 0.25f;
        float statExpon = 0.1f;
        
        charLevel++;
        updateSkills();        
        charLvlExp = levelExp(charLevel);
        StatClass charStats = LevelGrowth.growth.returnStats(charClass);

        int hpRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getHPMin(), charStats.getHPMax()) * Mathf.Pow(charLevel, hpExpon));
        int mpRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getMPMin(), charStats.getMPMax()) * Mathf.Pow(charLevel, mpExpon));
        int strRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getStrMin(), charStats.getStrMax()) * Mathf.Pow(charLevel, statExpon));
        int agiRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getAgiMin(), charStats.getAgiMax()) * Mathf.Pow(charLevel, statExpon));
        int minRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getMindMin(), charStats.getMindMax()) * Mathf.Pow(charLevel, statExpon));
        int soulRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getSoulMin(), charStats.getSoulMax()) * Mathf.Pow(charLevel, statExpon));
        int defRoll = Mathf.RoundToInt(UnityEngine.Random.Range(charStats.getDefMin(), charStats.getDefMax()) * Mathf.Pow(charLevel, statExpon));

        charMaxHp += hpRoll;
        charMaxMp += mpRoll;
        charBaseStr += strRoll;
        charBaseAgi += agiRoll;
        charBaseMind += minRoll;
        charBaseSoul += soulRoll;
        charDef += defRoll;
    }

    private int levelExp(int level)
    {
        float expon = 1.25f;
        int baseExp = 25;                

        return Mathf.RoundToInt(baseExp * (Mathf.Pow(level, expon)));
    }

    private void updateSkills()
    {
        charSkills = GameSkills.skills.GetSkills(charLevel, charClass);        
    }
}
