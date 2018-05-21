using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyClass : MonoBehaviour{

    public string enemyName;
    public int enemyMaxHp;
    private int enemyCurrentHp;
    public int enemyStr;
    public int enemyAgi;
    public int enemyMind;
    public int enemySoul;
    public int enemyDef;
    public int gold;
    public int exp;
    public List<string> enemySkills = new List<string>();
    private Dictionary<string, SkillClass> skills = new Dictionary<string, SkillClass>();
    private int enemyPlace;
    private Dictionary<string, DebuffClass> debuffs = new Dictionary<string, DebuffClass>();
    public GameObject debuffPanel;
    private Dictionary<string, GameObject> debuffIcons = new Dictionary<string, GameObject>();

    private void Start()
    {
        debuffs.Add("Poison", new DebuffClass());
        debuffs.Add("Confused", new DebuffClass());
        debuffs.Add("Paralyzed", new DebuffClass());
        debuffs.Add("Blind", new DebuffClass());
        debuffs.Add("Mute", new DebuffClass());

        try
        {
            int level = Manager.manager.AveLevel();
            List<SkillClass> newSkills = GameSkills.skills.GetEnemySkills(level, enemySkills);

            foreach(SkillClass skill in newSkills)
            {
                skills.Add(skill.GetName(), skill);
            }
        }
        catch { }
    }

    //getters and setters for Enemy Class
    public string GetEnemyName() { return enemyName; }    
    public int GetEnemyMaxHp() { return enemyMaxHp; }
    public int GetEnemyCurrentHp() { return enemyCurrentHp; }
    public int GetEnemyStr() { return enemyStr; }
    public int GetEnemyAgi() { return enemyAgi; }
    public int GetEnemyMind() { return enemyMind; }
    public int GetEnemySoul() { return enemySoul; }
    public int GetEnemyDef() { return enemyDef; }
    public int GetEnemyGold() { return gold; }
    public int GetEnemyExp() { return exp; }

    public SkillClass GetSkill(string name)
    {
        return skills[name];
    }

    //gets damage done by enemy from skills
    public int skillDamage(string skill)
    {
        int damage = 0;

        string damType = skills[skill].GetDamageType();
        if(damType.Equals("Physical"))
            damage = skills[skill].healthChange(Mathf.RoundToInt(enemyStr * 1.5f));
        else
            damage = skills[skill].healthChange(Mathf.RoundToInt(enemyMind * 1.5f));

        return damage;
    }

    //sets any debuffs casted onto enemy
    public void SetEnemyDebuff(string type, int rounds, int strength)
    {
        debuffs[type].SetEffected(true);
        debuffs[type].SetRounds(rounds);
        debuffs[type].SetStrength(strength);

        GameObject image = BuffIcons.buffIcons.getBuffIcon(type);
        GameObject instantImage = (GameObject)Instantiate(image) as GameObject;
        instantImage.transform.SetParent(debuffPanel.transform, false);
        debuffIcons.Add(type, instantImage);
    }

    //checks if a debuff is active or not
    public bool CheckAffliction(string affliction)
    {
        return debuffs[affliction].GetEffected();
    }

    //ticks down debuffs that are on the enemy
    public void DebuffCounter()
    {
        bool isKilled = false;
        foreach (KeyValuePair<string, DebuffClass> debuff in debuffs)
        {
            string key = debuff.Key;
            DebuffClass value = debuff.Value;
            
            if (debuffs[key].GetEffected() == true)
            {
                if (key.Equals("Poison"))
                {
                    isKilled = ChangeEnemyCurrentHp(-debuffs[key].GetStrength());
                }                    

                bool debuffDone = debuffs[key].reduceRound();

                if (debuffDone == true)
                {
                    debuffs[key].SetEffected(false);
                    debuffs[key].SetRounds(0);
                    debuffs[key].SetStrength(0);

                    Destroy(debuffIcons[key]);
                    debuffIcons.Remove(key);
                }
            }
        }
        Debug.Log("Enemies Current HP: " + enemyCurrentHp);

        if (isKilled == true)
        {
            StateMachine.state.poisonKill(gameObject);
        }
    }

    //changed hp of enemy
    public bool ChangeEnemyCurrentHp(int hpChange)
    {
        enemyCurrentHp += hpChange;
        Debug.Log(string.Format("{0}, Damage Dealt: {1}, Life Left: {2}.", enemyName, hpChange, enemyCurrentHp));
        if (enemyCurrentHp <= 0)
        {
            StateMachine.state.removeEnemySelected();
            return true;
        }
        else
            return false;
    }

    //sets normal enemy stats
    public void SetEnemyStats(int level)
    {
        float statGain = (level * 0.01f) + 1;

        enemyMaxHp = Mathf.RoundToInt((enemyMaxHp * level) * statGain);
        enemyStr = Mathf.RoundToInt((enemyStr * level) * statGain);
        enemyAgi = Mathf.RoundToInt((enemyAgi * level) * statGain);
        enemyMind = Mathf.RoundToInt((enemyMind * level) * statGain);
        enemySoul = Mathf.RoundToInt((enemySoul * level) * statGain);
        enemyDef = Mathf.RoundToInt((enemyDef * level) * statGain);

        float expon = 1.05f;
        gold = Mathf.RoundToInt(gold * (Mathf.Pow(level, expon)));
        exp = Mathf.RoundToInt(exp * (Mathf.Pow(level, expon)));

        enemyCurrentHp = enemyMaxHp;
    }

    //sets boss enemy stats
    public void SetBossStats(int level)
    {
        float statGain = (level * 0.01f) + 1.25f;

        enemyMaxHp = Mathf.RoundToInt((enemyMaxHp * level) * statGain);
        enemyStr = Mathf.RoundToInt((enemyStr * level) * statGain);
        enemyAgi = Mathf.RoundToInt((enemyAgi * level) * statGain);
        enemyMind = Mathf.RoundToInt((enemyMind * level) * statGain);
        enemySoul = Mathf.RoundToInt((enemySoul * level) * statGain);
        enemyDef = Mathf.RoundToInt((enemyDef * level) * statGain);

        float expon = 1.05f;
        gold = Mathf.RoundToInt(gold * (Mathf.Pow(level, expon)));
        exp = Mathf.RoundToInt(exp * (Mathf.Pow(level, expon)));

        enemyCurrentHp = enemyMaxHp;
    }
}
