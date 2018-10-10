using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemies in which will have the same stats, not determined by player level
public class SetStats : ScriptableBaseChar
{
    public int maxHp;
    public int maxMp;
    public int strength;
    public int agility;
    public int mind;
    public int soul;
    public int defense;

    //Sets Stat Numbers for Enemies that do not change by level
    public void StatsInit(int SetHp, int SetMp, int SetStr, int SetAgi, int SetMind, int SetSoul,
        int SetDef)
    {
        maxHp = SetHp;
        maxMp = SetMp;
        strength = SetStr;
        agility = SetAgi;
        mind = SetMind;
        soul = SetSoul;
        defense = SetDef;
    }
}
