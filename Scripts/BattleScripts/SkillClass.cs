using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillClass {

    private string name;
    private string type;
    private string charClass;
    private string target;
    private string statChange;
    private string ailment;
    private string damageType;
    private string description;
    private int level;
    private int mpCost;
    private int skillBase;
    private int rounds;
    private float debuffChance;
    private float skillModifier;
    private bool aoe;

    public SkillClass() { }

    public SkillClass(string Name, string Type, string CharClass, string Target, string StatChange, 
        string Ailment, string DamageType, string Description, int Level, int MPCost, int SkillBase, 
        int Rounds, float DebuffChance, float SkillModifier, bool AOE)
    {
        name = Name;
        type = Type;
        charClass = CharClass;
        target = Target;
        statChange = StatChange;
        ailment = Ailment;
        damageType = DamageType;
        description = Description;
        level = Level;
        mpCost = MPCost;
        skillBase = SkillBase;
        rounds = Rounds;
        debuffChance = DebuffChance;
        skillModifier = SkillModifier;
        aoe = AOE;
    }

    //getters
    public string GetName() { return name; }
    public string GetSkillType() { return type; }
    public string GetClass() { return charClass; }
    public string GetTarget() { return target; }
    public string GetStatChange() { return statChange; }
    public string GetCondition() { return ailment; }
    public string GetDamageType() { return damageType; }
    public string GetDescription() { return description; }

    public int GetLevel() { return level; }
    public int GetCost() { return mpCost; }
    public int GetSkillBase() { return skillBase; }
    public int GetRounds() { return rounds; }

    public float GetDebuffChance() { return debuffChance; }
    public float GetSkillModifier() { return skillModifier; }

    public bool GetAoe() { return aoe; }

    //determines amount of damage or healing
    public int healthChange(int mainStat)
    {
        Debug.Log(string.Format("Skill Damage Being Calculated.  Character Stat: {0}.  Skill Modifier: {1}.  Skill Damage Base: {2}", mainStat, skillModifier, skillBase));
        int dam = Mathf.RoundToInt((mainStat * skillModifier) + skillBase);
        Debug.Log("Damage before Enemy Modifier is added in: " + dam);
        return Mathf.RoundToInt((mainStat * skillModifier) + skillBase);        
    }
}
