using System.Collections.Generic;
using UnityEngine;

public class EnemyClass {

    private Sprite enemySprite;
    private string enemyName;
    private int enemyMaxHp;
    private int enemyCurrentHp;
    private int enemyStr;
    private int enemyAgi;
    private int enemyMind;
    private int enemySoul;
    private int enemyDef;
    private List<string> enemySkills = new List<string>();

    //constructors for Enemy Class
    public EnemyClass() { }

    public EnemyClass(Sprite EnemySprite, string EnemyName, int EnemyHp, int EnemyStr, int EnemyAgi, int EnemyMind, int EnemySoul,
        int EnemyDef, List<string> EnemySkills)
    {
        enemySprite = EnemySprite;
        enemyName = EnemyName;
        enemyMaxHp = EnemyHp;
        enemyCurrentHp = enemyMaxHp;
        enemyStr = EnemyStr;
        enemyAgi = EnemyAgi;
        enemyMind = EnemyMind;
        enemySoul = EnemySoul;
        enemyDef = EnemyDef;
        enemySkills = EnemySkills;
    }

    //getters and setters for Enemy Class
    public Sprite GetEnemySprite() { return enemySprite; }
    public string GetEnemyName() { return enemyName; }    
    public int GetEnemyMaxHp() { return enemyMaxHp; }
    public int GetEnemyCurrentHp() { return enemyCurrentHp; }
    public int GetEnemyStr() { return enemyStr; }
    public int GetEnemyAgi() { return enemyAgi; }
    public int GetEnemyMind() { return enemyMind; }
    public int GetEnemySoul() { return enemySoul; }
    public int GetEnemyDef() { return enemyDef; }
    public List<string> GetEnemySkills() { return enemySkills; }

    public void ChangeEnemyCurrentHp(int hpChange) { enemyCurrentHp += hpChange; }
}
