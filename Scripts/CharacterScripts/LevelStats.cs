using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Any Character that changes by Level
public class LevelStats : ScriptableBaseChar
{
    public List<int> levelHp;
    public List<int> levelMp;
    public List<int> levelStrength;
    public List<int> levelAgility;
    public List<int> levelMind;
    public List<int> levelSoul;
    public List<int> levelDefense;

    //Sets Stat Numbers for Enemies that change by level and Players
    public void StatsInit(List<int> SetLevelHp, List<int> SetLevelMp, List<int> SetLevelStr,
        List<int> SetLevelAgi, List<int> SetLevelMind, List<int> SetLevelSoul, List<int> SetLevelDef)
    {
        levelHp = SetLevelHp;
        levelMp = SetLevelMp;
        levelStrength = SetLevelStr;
        levelAgility = SetLevelAgi;
        levelMind = SetLevelMind;
        levelSoul = SetLevelSoul;
        levelDefense = SetLevelDef;

        currentHp = levelHp[level];
        currentMp = levelMp[level];
    }
}

