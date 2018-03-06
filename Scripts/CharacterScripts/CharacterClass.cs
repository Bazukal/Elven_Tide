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
    private int buffStr;
    private int buffStrDur;
    private int charBaseAgi;
    private int charEquipAgi;
    private int buffAgi;
    private int buffAgiDur;
    private int charBaseMind;
    private int charEquipMind;
    private int buffMind;
    private int buffMindDur;
    private int charBaseSoul;
    private int charEquipSoul;
    private int buffSoul;
    private int buffSoulDur;
    private int charDef;
    private int charEquipArmor;
    private int buffDef;
    private int buffDefDur;
    private int charCurExp;
    private int charLvlExp;
    private List<SkillClass> charSkills = new List<SkillClass>();
    private EquipableItemClass weapon;
    private EquipableItemClass offHand;
    private EquipableItemClass armor;
    private EquipableItemClass accessory;

    private bool isPoisoned = false;
    private int poisonStr = 0;
    private int poisonRounds = 0;

    private bool isConfused = false;
    private int confusedRounds = 0;

    private bool isParalyzed = false;
    private int paralyzedRounds = 0;

    private bool isBlind = false;
    private int blindRounds = 0;

    private bool isMute = false;
    private int muteRounds = 0;

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

    public int GetCharStr() { return charBaseStr; }
    public int GetEquipStr() { return charEquipStr; }
    public int GetTotalStr()
    {
        int totalStr = charBaseStr + charEquipStr + buffStr;
        return totalStr;
    }

    public int GetCharAgi() { return charBaseAgi; }
    public int GetEquipAgi() { return charEquipAgi; }
    public int GetTotalAgi()
    {
        int totalAgi = charBaseAgi + charEquipAgi + buffAgi;
        return totalAgi;
    }

    public int GetCharMind() { return charBaseMind; }
    public int GetEquipMind() { return charEquipMind; }
    public int GetTotalMind()
    {
        int totalMind = charBaseMind + charEquipMind + buffMind;
        return totalMind;
    }

    public int GetCharSoul() { return charBaseSoul; }
    public int GetEquipSoul() { return charEquipSoul; }
    public int GetTotalSoul()
    {
        int totalSoul = charBaseSoul + charEquipSoul + buffSoul;
        return totalSoul;
    }

    public int GetCharDef() { return charDef; }
    public int GetEquipArmor() { return charEquipArmor; }
    public int GetTotalDef()
    {
        int totalDef = charDef + charEquipArmor + buffDef;
        return totalDef;
    }

    public int GetCurExp() { return charCurExp; }
    public void AddCurExp(int expGain)
    {
        charCurExp += expGain;

        if(charCurExp >= charLvlExp)
        {
            CharLevelUp();
        }
    }
    public int GetLvlExp() { return charLvlExp; }

    public List<SkillClass> GetCharSkills() { return charSkills; }

    public EquipableItemClass GetWeapon() { return weapon; }
    public EquipableItemClass GetOffHand() { return offHand; }
    public EquipableItemClass GetArmor() { return armor; }
    public EquipableItemClass GetAccessory() { return accessory; }

    //apply buffs to character
    public void SetBuffStr(int str, int dur) { buffStr = str; buffStrDur = dur; }
    public void SetBuffAgi(int agi, int dur) { buffAgi = agi; buffAgiDur = dur; }
    public void SetBuffMind(int mind, int dur) { buffMind = mind; buffMindDur = dur; }
    public void SetBuffSoul(int soul, int dur) { buffSoul = soul; buffSoulDur = dur; }
    public void SetBuffDef(int def, int dur) { buffDef = def; buffDefDur = dur; }

    //apply status ailments to character
    public void playerPoisoned(int str, int dur)
    {
        isPoisoned = true;
        poisonStr = str;
        poisonRounds = dur;
    }

    public void playerConfused(int dur)
    {
        isConfused = true;
        confusedRounds = dur;
    }

    public void playerParalyzed(int dur)
    {
        isParalyzed = true;
        paralyzedRounds = dur;
    }

    public void playerBlind(int dur)
    {
        isBlind = true;
        blindRounds = dur;
    }

    public void playerMute(int dur)
    {
        isMute = true;
        muteRounds = dur;
    }

    //checks if player has any ailments at end of round
    public void checkAilments()
    {
        if (isPoisoned)
        {
            charCurrentHp -= poisonStr;
            poisonRounds--;
            if (poisonRounds == 0)
                isPoisoned = false;
        }
        if (isConfused)
        {
            confusedRounds--;
            if (confusedRounds == 0)
                isConfused = false;
        }
        if (isParalyzed)
        {
            paralyzedRounds--;
            if (paralyzedRounds == 0)
                isParalyzed = false;
        }
        if (isBlind)
        {
            blindRounds--;
            if (blindRounds == 0)
                isBlind = false;
        }
        if (isMute)
        {
            muteRounds--;
            if (muteRounds == 0)
                isMute = false;
        }
    }

    //checks if buffs are active, if so decrease duration by one round.  if duration equals 0, remove buff
    public void checkBuffs()
    {
        if(buffStrDur > 0)
        {
            buffStrDur--;
            if (buffStrDur == 0)
                buffStr = 0;
        }
        if (buffAgiDur > 0)
        {
            buffAgiDur--;
            if (buffAgiDur == 0)
                buffAgi = 0;
        }
        if (buffMindDur > 0)
        {
            buffMindDur--;
            if (buffMindDur == 0)
                buffMind = 0;
        }
        if (buffSoulDur > 0)
        {
            buffSoulDur--;
            if (buffSoulDur == 0)
                buffSoul = 0;
        }
        if (buffDefDur > 0)
        {
            buffDefDur--;
            if (buffDefDur == 0)
                buffDef = 0;
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
        int tempStr = weapon.getStr() + armor.getStr() + accessory.getStr();
        int tempAgi = weapon.getAgi() + armor.getAgi() + accessory.getAgi();
        int tempMind = weapon.getMind() + armor.getMind() + accessory.getMind();
        int tempSoul = weapon.getSoul() + armor.getSoul() + accessory.getSoul();
        int tempArmor = weapon.getArmor() + armor.getArmor() + accessory.getArmor();
                
        try
        {
            tempStr += offHand.getStr();
            tempAgi += offHand.getAgi();
            tempMind += offHand.getMind();
            tempSoul += offHand.getSoul();
            tempArmor += offHand.getArmor();
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
