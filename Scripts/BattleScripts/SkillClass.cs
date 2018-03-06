using System;

[Serializable]
public class SkillClass {

    private string skillName;
    private string skillCharClass;
    private string skillType;
    private string skillTarget;
    private string skillDesc;
    private string skillCure;
    private string skillBuffStat;

    private float skillMod;

    private int skillModPlus;
    private int skillTurnEffect;
    private int skillLevel;
    private int skillMana;

    private bool skillAOE;

    public SkillClass() { }

    public SkillClass(string SkillName, string SkillCharClass, string SkillType, string SkillTarget, string SkillDesc,
        string SkillCure, string BuffStat, float SkillMod, int ModPlus, int TurnEffect, int SkillLevel, int SkillMana, 
        bool SkillAOE)
    {
        skillName = SkillName;
        skillCharClass = SkillCharClass;
        skillType = SkillType;
        skillTarget = SkillTarget;
        skillDesc = SkillDesc;
        skillCure = SkillCure;
        skillBuffStat = BuffStat;
        skillMod = SkillMod;
        skillModPlus = ModPlus;
        skillTurnEffect = TurnEffect;
        skillLevel = SkillLevel;
        skillMana = SkillMana;
        skillAOE = SkillAOE;
    }

    //getters
    public string GetSkillName() { return skillName; }
    public string GetCharClass() { return skillCharClass; }
    public string GetSkillType() { return skillType; }
    public string GetSkillTarget() { return skillTarget; }
    public string GetSkillDesc() { return skillDesc; }
    public string GetSkillCure() { return skillCure; }
    public string GetBuffStat() { return skillBuffStat; }

    public float GetSkillMod() { return skillMod; }

    public int GetModPlus() { return skillModPlus; }
    public int GetTurnEffect() { return skillTurnEffect; }
    public int GetSkillLevel() { return skillLevel; }
    public int GetSkillMana() { return skillMana; }

    public bool GetSkillAOE() { return skillAOE; }
}

