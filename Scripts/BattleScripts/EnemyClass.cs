using System.Collections.Generic;
using UnityEngine;

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

    private bool isPoisoned = false;
    private int poisonStr = 0;
    private int poisonRounds = 0;

    private bool isConfused = false;
    private int confusedRounds = 0;

    private bool isParalyzed = false;
    private int paralyzedRounds = 0;

    private bool isBlind = false;
    private int blindRounds = 0;

    private bool isMute = false;
    private int muteRounds = 0;

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
    public List<string> GetEnemySkills() { return enemySkills; }

    public void ChangeEnemyCurrentHp(int hpChange) { enemyCurrentHp += hpChange; }

    //applies status effects to enemy
    public void enemyPoisoned(int str, int dur)
    {
        isPoisoned = true;
        poisonStr = str;
        poisonRounds = dur;
    }

    public void enemyConfused(int dur)
    {
        isConfused = true;
        confusedRounds = dur;
    }

    public void enemyParalyzed(int dur)
    {
        isParalyzed = true;
        paralyzedRounds = dur;
    }

    public void enemyBlind(int dur)
    {
        isBlind = true;
        blindRounds = dur;
    }

    public void enemyMute(int dur)
    {
        isMute = true;
        muteRounds = dur;
    }

    //checks what status effects the enemy has
    public void checkAilments()
    {
        if(isPoisoned)
        {
            enemyCurrentHp -= poisonStr;
            poisonRounds--;
            if (poisonRounds == 0)
                isPoisoned = false;
        }
        if(isConfused)
        {
            confusedRounds--;
            if (confusedRounds == 0)
                isConfused = false;
        }
        if(isParalyzed)
        {
            paralyzedRounds--;
            if (paralyzedRounds == 0)
                isParalyzed = false;
        }
        if(isBlind)
        {
            blindRounds--;
            if (blindRounds == 0)
                isBlind = false;
        }
        if(isMute)
        {
            muteRounds--;
            if (muteRounds == 0)
                isMute = false;
        }
    }
}
