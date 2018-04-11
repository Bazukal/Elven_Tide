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
    private List<SkillClass> skills = new List<SkillClass>();
    private int enemyPlace;
    private Dictionary<string, DebuffClass> debuffs = new Dictionary<string, DebuffClass>();

    private void Start()
    {
        debuffs.Add("Poison", new DebuffClass());
        debuffs.Add("Confused", new DebuffClass());
        debuffs.Add("Paralyzed", new DebuffClass());
        debuffs.Add("Blind", new DebuffClass());
        debuffs.Add("Mute", new DebuffClass());

        try
        {
            int level = CharacterManager.charManager.aveLevel();
            skills = GameSkills.skills.GetEnemySkills(level, enemySkills);
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
    public List<SkillClass> GetEnemySkills() { return skills; }
    public void SetEnemyPlace(int place) { enemyPlace = place; }

    public void SetEnemyDebuff(string type, int rounds, int strength)
    {
        debuffs[type].SetEffected(true);
        debuffs[type].SetRounds(rounds);
        debuffs[type].SetStrength(strength);
    }

    public bool CheckAffliction(string affliction)
    {
        return debuffs[affliction].GetEffected();
    }

    public void DebuffCounter()
    {
        foreach (KeyValuePair<string, DebuffClass> debuff in debuffs)
        {
            string key = debuff.Key;

            if (debuffs[key].GetEffected() == true)
            {
                if (key.Equals("Poison"))
                    ChangeEnemyCurrentHp(-debuffs[key].GetStrength());

                debuffs[key].reduceRound();
            }
        }
    }

    public bool ChangeEnemyCurrentHp(int hpChange)
    {
        enemyCurrentHp += hpChange;
        Debug.Log("Player Dealt Damage: " + hpChange + ", Enemy Life Left: " + enemyCurrentHp);
        if (enemyCurrentHp <= 0)
            return true;
        else
            return false;
    }

    public void SetEnemyStats(int level)
    {
        float statAdd = 1 - (1 / CharacterManager.charManager.aveLevel());

        enemyMaxHp = Mathf.RoundToInt((enemyMaxHp * level) * (0.75f + statAdd));
        enemyStr = Mathf.RoundToInt((enemyStr * level) * (0.7f + statAdd));
        enemyAgi = Mathf.RoundToInt((enemyAgi * level) * (0.7f + statAdd));
        enemyMind = Mathf.RoundToInt((enemyMind * level) * (0.7f + statAdd));
        enemySoul = Mathf.RoundToInt((enemySoul * level) * (0.7f + statAdd));
        enemyDef = Mathf.RoundToInt((enemyDef * level) * (0.7f + statAdd));

        float expon = 1.15f;
        gold = Mathf.RoundToInt(gold * (Mathf.Pow(level, expon)));
        exp = Mathf.RoundToInt(exp * (Mathf.Pow(level, expon)));

        enemyCurrentHp = enemyMaxHp;
    }

    public void SetBossStats(int level)
    {
        float statAdd = 1 / CharacterManager.charManager.aveLevel();

        enemyMaxHp = Mathf.RoundToInt((enemyMaxHp * level) * (1 + statAdd));
        enemyStr = Mathf.RoundToInt((enemyStr * level) * (1 + statAdd));
        enemyAgi = Mathf.RoundToInt((enemyAgi * level) * (1 + statAdd));
        enemyMind = Mathf.RoundToInt((enemyMind * level) * (1 + statAdd));
        enemySoul = Mathf.RoundToInt((enemySoul * level) * (1 + statAdd));
        enemyDef = Mathf.RoundToInt((enemyDef * level) * (1 + statAdd));

        enemyCurrentHp = enemyMaxHp;
    }

    public void SetEnemySelected()
    {
        Battle.battle.SetEnemySelected(enemyPlace);
    }
}
