using Devdog.QuestSystemPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Base Scriptable Object For Enemy and Player Characters
public class ScriptableBaseChar : ScriptableObject {

    public new string name;

    public Sprite battleSprite;

    public int level;

    public int currentHp;
    public int currentMp;

    public List<SkillScriptObject> knownSkills = new List<SkillScriptObject>();

    private Dictionary<string, SkillScriptObject> activeSkills = new Dictionary<string, SkillScriptObject>();
    private Dictionary<string, SkillScriptObject> passiveSkills = new Dictionary<string, SkillScriptObject>();
    protected Dictionary<string, BuffClass> buffs = new Dictionary<string, BuffClass>();
    protected Dictionary<string, DebuffClass> debuffs = new Dictionary<string, DebuffClass>();

    //Sets Base Stats
    public void BaseInit(string SetName, Sprite SetBattleSprite, int SetLevel,
        List<SkillScriptObject> SetSkills)
    {
        name = SetName;
        battleSprite = SetBattleSprite;
        level = SetLevel;
        knownSkills = SetSkills;

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
    }

    //Updates Skill List
    private void updateSkills()
    {
        foreach (SkillScriptObject skill in knownSkills)
        {
            if (skill.skillLevel <= level)
            {
                if (skill.skillType.Equals("Passive") && !passiveSkills.ContainsKey(skill.skillName))
                    passiveSkills.Add(skill.skillName, skill);
                else if (!skill.skillType.Equals("Passive") && !activeSkills.ContainsKey(skill.skillName))
                    activeSkills.Add(skill.skillName, skill);
            }
        }
    }

    //Getting Skills
    public Dictionary<string, SkillScriptObject> GetAllActiveSkills() { return activeSkills; }
    public Dictionary<string, SkillScriptObject> GetAllPassiveSkills() { return passiveSkills; }
    public SkillScriptObject GetSkill(string skillName) { return activeSkills[skillName]; }

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

        foreach(KeyValuePair<string, BuffClass> buff in buffs)
        {
            string key = buff.Key;

            buffs[key].SetBuffed(false);
            buffs[key].SetStrength(0);
            buffs[key].SetRounds(0);
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
}