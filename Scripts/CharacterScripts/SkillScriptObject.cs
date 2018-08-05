using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "New Skill")]
public class SkillScriptObject : ScriptableObject {

    public string skillName;
    public string skillType;
    public string damageType;
    public string ailment;
    public string buffStat;
    public string skillDescription;

    public int skillLevel;
    public int manaCost;
    public int skillBase;
    public int effectTurns;

    public float debuffChance;
    public float skillModifier;

    public bool isAOE;
    public bool targetEnemy;

    public GameObject skillAnimation;
    public GameObject buffIcon;

    public int getLifeChange(int stat)
    {
        return Mathf.RoundToInt((stat * skillModifier) + skillBase);
    }
}
