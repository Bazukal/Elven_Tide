using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class XMLData : MonoBehaviour {
    
    public const string SKILLPATH = "skills";
    public const string STATPATH = "levelStats";
    
    private List<SkillClass> enemySkills = new List<SkillClass>();
    private List<SkillClass> archerSkills = new List<SkillClass>();
    private List<SkillClass> blackSkills = new List<SkillClass>();
    private List<SkillClass> monkSkills = new List<SkillClass>();
    private List<SkillClass> paladinSkills = new List<SkillClass>();
    private List<SkillClass> thiefSkills = new List<SkillClass>();
    private List<SkillClass> warriorSkills = new List<SkillClass>();
    private List<SkillClass> whiteSkills = new List<SkillClass>();

    public Slider loadingSlider;
    public Text progressText;

    // Use this for initialization
    void Start () {

        SetSkills();

        SetStats();

        StartCoroutine(loadXML());
	}    

    private void SetSkills()
    {
        //reading skills from XML, and store in manager
        SkillClass newSkill;

        SkillContainer sc = SkillContainer.Load(SKILLPATH);

        foreach (Skill skill in sc.skills)
        {
            string charClass = skill.charClass;
            
            string skillName = skill.name;

            newSkill = new SkillClass(skillName, skill.type, charClass, skill.target, skill.stat,
                skill.ailment, skill.damageType, skill.desc, skill.level, skill.mana, skill.skillBase,
                skill.turns, skill.debuffChance, skill.modifier, skill.aoe);

            switch(charClass)
            {
                case "Enemy":
                    enemySkills.Add(newSkill);
                    break;
                case "Archer":
                    archerSkills.Add(newSkill);
                    break;
                case "Black Mage":
                    blackSkills.Add(newSkill);
                    break;
                case "Monk":
                    monkSkills.Add(newSkill);
                    break;
                case "Paladin":
                    paladinSkills.Add(newSkill);
                    break;
                case "Thief":
                    thiefSkills.Add(newSkill);
                    break;
                case "Warrior":
                    warriorSkills.Add(newSkill);
                    break;
                case "White Mage":
                    whiteSkills.Add(newSkill);
                    break;
            }
        }

        GameSkills skillManager = FindObjectOfType<GameSkills>();
        skillManager.SetSkills(enemySkills, archerSkills, blackSkills, monkSkills, paladinSkills,
            thiefSkills, warriorSkills, whiteSkills);     
    }

    private void SetStats()
    {
        StatClass newStats;
        LevelStatContainer lc = LevelStatContainer.Load(STATPATH);
        foreach(Stat stats in lc.stats)
        {
            string charClass = stats.charClass;
            newStats = new StatClass(stats.hpMinValue, stats.hpMaxValue, stats.mpMinValue, stats.mpMaxValue,
                stats.strMinValue, stats.strMaxValue, stats.agiMinValue, stats.agiMaxValue, stats.mindMinValue,
                stats.mindMaxValue, stats.soulMinValue, stats.soulMaxValue, stats.defMinValue, stats.defMaxValue);

            LevelGrowth growth = FindObjectOfType<LevelGrowth>();
            growth.addDictionary(charClass, newStats);
        }
    }

    //loads xml files, while displaying a loading bar during the process
    IEnumerator loadXML()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TitleScreen");

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            loadingSlider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
