using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSkills : MonoBehaviour {

    public static GameSkills skills;

    private Dictionary<string, List<SkillClass>> classSkills = new Dictionary<string, List<SkillClass>>();

    // Use this for initialization
    void Start () {
        skills = this;
	}
	
    //stores class skills from xml file
	public void SetSkills(List<SkillClass> EnemySkills, List<SkillClass> ArcherSkills, List<SkillClass> BlackSkills, 
        List<SkillClass> MonkSkills, List<SkillClass> PaladinSkills, List<SkillClass> ThiefSkills, 
        List<SkillClass> WarriorSkills, List<SkillClass> WhiteSkills)
    {
        classSkills.Add("Enemy", EnemySkills);
        classSkills.Add("Archer", ArcherSkills);
        classSkills.Add("Black Mage", BlackSkills);
        classSkills.Add("Monk", MonkSkills);
        classSkills.Add("Paladin", PaladinSkills);
        classSkills.Add("Thief", ThiefSkills);
        classSkills.Add("Warrior", WarriorSkills);
        classSkills.Add("White Mage", WhiteSkills);
    }
    
    //retrieve skills for selected class and level range from the CharacterClass
    public List<SkillClass> GetSkills(int level, string charClass)
    {
        List<SkillClass> tempSkills = new List<SkillClass>();

        foreach(SkillClass skill in classSkills[charClass])
        {
            if(skill.GetSkillLevel() <= level)
            {
                tempSkills.Add(skill);
            }
        }
        return tempSkills;
    }

    public List<SkillClass> GetEnemySkills(int level, List<string> skill)
    {
        List<SkillClass> enemySkills = new List<SkillClass>();

        foreach(SkillClass enemySkill in classSkills["Enemy"])
        {
            if (enemySkill.GetSkillName().Equals(skill))
                enemySkills.Add(enemySkill);
        }
        return enemySkills;
    }
}
