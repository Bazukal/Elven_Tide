using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;

public class Battle : MonoBehaviour {

    public static Battle battle;

    public enum BattleStates
    {
        WAIT,
        START,
        COUNTER,
        PLAYERTURN,
        ENEMYCOUNTER,
        ENEMYACTIONS,
        WIN,
        LOSE,
        FLEE,
        ENDWAIT
    }

    public BattleStates currentState;

    public List<GameObject> levelEnemies;
    public GameObject levelBoss;

    public List<string> itemDrops;
    private List<EquipableItemClass> equipableDrops = new List<EquipableItemClass>();
    private List<UsableItemClass> usableDrops = new List<UsableItemClass>();

    public List<GameObject> enemyPlace;
    public GameObject bossPlace;

    public List<GameObject> charObjects = new List<GameObject>();
    private int charTurn = 0;

    public Button attackButton;
    public Button skillsButton;
    public Button itemsButton;
    public Button defendButton;
    public Button fleeButton;

    public Text char1Name;
    public Text char1HP;
    public Text char1MP;

    public Text char2Name;
    public Text char2HP;
    public Text char2MP;

    public Text char3Name;
    public Text char3HP;
    public Text char3MP;

    public Text char4Name;
    public Text char4HP;
    public Text char4MP;

    private List<CharacterClass> characters = new List<CharacterClass>();
    
    //-1 = none, 0 - 3 = enemy 1-4
    private int enemySelected = -1;
    private List<GameObject> enemyObjects;

    public int expGained = 0;
    public int goldGained = 0;
    private float adBonus = 0f;

    public GameObject battleOutcome;
    public Text outcomeText;

    public GameObject skillsNItemsPanel;

    private string lost = "You lost the battle.  Your body will be sent back to town for healing.";
    StringBuilder sb;

    private SkillClass attack;
    private SkillClass healSkill = new SkillClass();
    private UsableItemClass healItem = new UsableItemClass();

    private void Awake()
    {      
        battle = this;        

        attack = new SkillClass("Attack", null, "Damage", "Physical", null, "Enemy", null, null, null, 
            0.0f, 1.0f, 0, 0, 1, 0, false);

        characters.Add(CharacterManager.charManager.character1);
        characters.Add(CharacterManager.charManager.character2);
        characters.Add(CharacterManager.charManager.character3);
        characters.Add(CharacterManager.charManager.character4);

        enemyObjects = new List<GameObject>();

        for(int i = 0; i < itemDrops.Count;i++)
        {
            EquipableItemClass newEquip = new EquipableItemClass();
            newEquip = GameItems.gItems.findEquipItem(itemDrops[i]);

            UsableItemClass newUsable = new UsableItemClass();
            newUsable = GameItems.gItems.findUseItem(itemDrops[i]);

            if (newEquip != null)
                equipableDrops.Add(newEquip);
            else if (newUsable != null)
                usableDrops.Add(newUsable);
        }

        if (CharacterManager.charManager.getAd())
            adBonus = 1.5f;
        else
            adBonus = 1.0f;        

        currentState = BattleStates.START;
    }

    private void OnEnable()
    {
        currentState = BattleStates.START;
    }

    void Update()
    {
        switch (currentState)
        {
            case BattleStates.WAIT:
                attackButton.interactable = false;
                skillsButton.interactable = false;
                itemsButton.interactable = false;
                defendButton.interactable = false;
                fleeButton.interactable = false;

                battleOutcome.SetActive(false);
                break;
            case BattleStates.START:
                enemySelected = -1;
                goldGained = 0;
                expGained = 0;
                spawnEnemies();
                break;
            case BattleStates.COUNTER:
                foreach (CharacterClass chars in characters)
                {
                    chars.ResetDefendAmount();
                    chars.BuffCounter();
                    chars.DebuffCounter();
                }

                charTurn = 0;
                currentState = BattleStates.PLAYERTURN;
                break;
            case BattleStates.PLAYERTURN:
                //player is selecting actions for each of their characters
                if (characters[charTurn].GetCharCurrentHp() == 0)
                    nextChar();
                else
                {
                    charObjects[charTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(-50, 0);

                    bool isConfused = characters[charTurn].CheckAffliction("Confused");
                    bool isParalyzed = characters[charTurn].CheckAffliction("Paralyzed");
                    bool isMuted = characters[charTurn].CheckAffliction("Mute");

                    if (isMuted)
                        skillsButton.interactable = false;
                    else
                        skillsButton.interactable = true;

                    if (isConfused)
                    {
                        attackButton.interactable = false;
                        skillsButton.interactable = false;
                        itemsButton.interactable = false;
                        defendButton.interactable = false;
                        fleeButton.interactable = false;

                        confusedAttack();
                    }
                    else if (isParalyzed)
                    {
                        nextChar();
                    }
                    else
                    {
                        attackButton.interactable = true;
                        skillsButton.interactable = true;
                        itemsButton.interactable = true;
                        defendButton.interactable = true;
                        fleeButton.interactable = true;
                    }
                }
                break;
            case BattleStates.ENEMYCOUNTER:
                Debug.Log("EnemyCounter state started");
                //ai selects their actions
                attackButton.interactable = false;
                skillsButton.interactable = false;
                itemsButton.interactable = false;
                defendButton.interactable = false;
                fleeButton.interactable = false;

                foreach (GameObject enemy in enemyObjects)
                {
                    EnemyClass enemyScript = enemy.GetComponent<EnemyClass>();

                    enemyScript.DebuffCounter();
                }

                currentState = BattleStates.ENEMYACTIONS;
                break;
            case BattleStates.ENEMYACTIONS:
                Debug.Log("EnemyActions state started");



                StartCoroutine(attackWait(1));
                //ai performs selected actions 
                foreach (GameObject enemy in enemyObjects)
                {
                    checkDead();
                    EnemyClass enemyScript = enemy.GetComponent<EnemyClass>();

                    //check enemy for confusion or paralyze.  otherwise pick action and perform it
                    if(enemyScript.CheckAffliction("Confused"))
                    {
                        enemyConfused(enemy);
                    }
                    else if(enemyScript.CheckAffliction("Paralyzed"))
                    {

                    }
                    else
                    {
                        List<SkillClass> enemySkills = enemyScript.GetEnemySkills();
                        int skillAmount = enemySkills.Count;

                        if (skillAmount == 0)
                            enemyAttack(enemy);
                        else
                        {
                            int randomAttack = Random.Range(0, 101);
                            float rangeForSkills = 50 / skillAmount;

                            SkillClass selectedSkill = null;

                            if (randomAttack >= 50)
                            {
                                enemyAttack(enemy);
                            }
                            else
                            {
                                float checkedRange = 49;
                                int skillIndex = 0;
                                do
                                {
                                    checkedRange -= rangeForSkills;

                                    if (randomAttack >= checkedRange)
                                        selectedSkill = enemySkills[skillIndex];
                                    else
                                        skillIndex++;
                                }
                                while (selectedSkill == null);

                                enemySkill(enemy, selectedSkill);
                            }
                        }
                    }
                }
                currentState = BattleStates.COUNTER;
                break;
            case BattleStates.WIN:
                //award player with exp, gold and possible loot, advance quest if appropriate
                attackButton.interactable = false;
                skillsButton.interactable = false;
                itemsButton.interactable = false;
                defendButton.interactable = false;
                fleeButton.interactable = false;
                battleOutcome.SetActive(true);

                sb = new StringBuilder();
                sb.Append("Experience Earned: " + expGained + "\n");

                awardExp();

                int totalGold = Mathf.RoundToInt(goldGained * adBonus);
                sb.Append("Gold Found: " + totalGold + "\n");
                CharacterManager.charManager.changeGold(totalGold);
                itemDrop();

                outcomeText.text = sb.ToString();
                currentState = BattleStates.ENDWAIT;
                break;
            case BattleStates.LOSE:
                //give death scene and take player back to town.
                attackButton.interactable = false;
                skillsButton.interactable = false;
                itemsButton.interactable = false;
                defendButton.interactable = false;
                fleeButton.interactable = false;

                battleOutcome.SetActive(true);

                outcomeText.text = lost;
                break;
            case BattleStates.FLEE:
                attackButton.interactable = false;
                skillsButton.interactable = false;
                itemsButton.interactable = false;
                defendButton.interactable = false;
                fleeButton.interactable = false;

                exitBattle();
                break;
        }
    }

    //spawns enemies into battle
    public void spawnEnemies()
    {
        for(int i = 0; i < 4; i++)
        {
            charObjects[i].GetComponent<Image>().sprite = CharacterManager.charManager.getCharBattleSprite(characters[i].GetCharClass());
            charObjects[i].GetComponent<Image>().SetNativeSize();
        }

        updateText();
        
        int enemyAvail = levelEnemies.Count;
        int enemyNumbers = Random.Range(1, 5);

        for (int i = 0; i < enemyNumbers;i++)
        {
            int whichEnemy = Random.Range(0, enemyAvail);
            
            GameObject newEnemy = (GameObject)Instantiate(levelEnemies[whichEnemy]) as GameObject;
            newEnemy.transform.SetParent(enemyPlace[i].transform, false);
            EnemyClass setPlace = newEnemy.GetComponent<EnemyClass>();
            setPlace.SetEnemyPlace(i);
            setPlace.SetEnemyStats(CharacterManager.charManager.aveLevel());
            enemyObjects.Add(newEnemy);
        }

        currentState = BattleStates.PLAYERTURN;
    }

    //attacks enemy with regular attack if attack is selected in battle
    public void attackAction()
    {
        int randomEnemy;
        bool enemyAlive = true;
        
        if (enemySelected == -1)
        {
            do
            {
                randomEnemy = Random.Range(0, enemyObjects.Count);

                if (enemyObjects[randomEnemy].GetComponent<EnemyClass>().GetEnemyCurrentHp() <= 0)
                {
                    enemyAlive = false;
                }
                    
            } while (enemyAlive == false);
            
            dealDamage(randomEnemy, attack);
        }
        else
            dealDamage(enemySelected, attack);
    }

    public void defendAction()
    {
        characters[charTurn].SetDefendAmount();
        charObjects[charTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        charTurn++;
    }

    public void viewSkills()
    {
        skillsNItemsPanel.SetActive(true);
        BattleSkillsItems.view.showSkills();
    }

    public void viewItems()
    {
        skillsNItemsPanel.SetActive(true);
        BattleSkillsItems.view.showItems();
    }

    public void skillAction(SkillClass skill)
    {
        int randomEnemy;
        bool enemyAlive = true;
        skillsNItemsPanel.SetActive(false);

        if (enemySelected == -1)
        {
            do
            {
                randomEnemy = Random.Range(0, enemyObjects.Count);

                if (enemyObjects[randomEnemy].GetComponent<EnemyClass>().GetEnemyCurrentHp() <= 0)
                {
                    enemyAlive = false;
                }

            } while (enemyAlive == false);

            dealDamage(randomEnemy, skill);
        }
        else
            dealDamage(enemySelected, skill);
    }

    public void healingSkill(SkillClass skill)
    {
        healSkill = skill;
    }

    public void itemAction (UsableItemClass item)
    {
        healItem = item;
    }

    public void healChar(int character)
    {
        if(healSkill != null)
        {
            if (healSkill.GetSkillType().Equals("Heal"))
            {
                int healAmount = Mathf.RoundToInt((characters[charTurn].GetTotalSoul() * healSkill.GetSkillMod()) + healSkill.GetModPlus());
                characters[character].ChangeCharCurrentHp(healAmount);
            }
            else if (healSkill.GetSkillType().Equals("Cure"))
            {
                characters[character].CureStatus(healSkill.GetSkillCure());
            }
            else if(healSkill.GetSkillType().Equals("Buff"))
            {
                characters[character].SetBuff(healSkill.GetBuffStat(), healSkill.GetModPlus(), healSkill.GetTurnEffect());
            }
            else
            {
                int healAmount = Mathf.RoundToInt(characters[character].GetCharMaxHp() * healSkill.GetSkillMod());
                characters[character].ChangeCharCurrentHp(healAmount);
            }

            characters[charTurn].ChangeCharCurrentMp(-healSkill.GetSkillMana());
        }
        else
        {
            if(!healItem.GetCure().Equals(null))
            {
                characters[character].CureStatus(healItem.GetCure());
            }
            else
            {
                characters[character].ChangeCharCurrentHp(healItem.GetHeal());
            }

            healItem.ChangeQuantity(-1);
        }

        updateText();
        BattleSkillsItems.view.cancelCharSelect();
        BattleSkillsItems.view.cancelUse();

        charObjects[charTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        nextChar();
    }

    //deals damage to the enemy attacked
    private void dealDamage(int enemyAttacked, SkillClass skill)
    {
        int playerMainAttack;
        int playerOffAttack = 0;
        int enemyDef;
        string damageType = skill.GetSkillDamageType();
        int numberAttacks;

        if (damageType.Equals("Physical"))
        {
            try
            {
                playerMainAttack = Mathf.RoundToInt((characters[charTurn].GetTotalStr() + characters[charTurn].GetWeapon().GetDamage()) * skill.GetSkillMod()) + skill.GetModPlus();

            }
            catch
            {
                playerMainAttack = Mathf.RoundToInt((characters[charTurn].GetTotalStr() * 2) * skill.GetSkillMod()) + skill.GetModPlus();

            }

            try
            {
                if (characters[charTurn].GetOffHand().GetItemType().Equals("Weapon"))
                    playerOffAttack = Mathf.RoundToInt((characters[charTurn].GetTotalStr() + characters[charTurn].GetOffHand().GetDamage()) * skill.GetSkillMod()) + skill.GetModPlus();
                else
                    playerOffAttack = 0;
            }
            catch
            {
                playerOffAttack = 0;
            }
            

            enemyDef = enemyObjects[enemyAttacked].GetComponent<EnemyClass>().GetEnemyDef();
            numberAttacks = (characters[charTurn].GetTotalAgi() / 30) + 1;
        }
        else
        {
            playerMainAttack = Mathf.RoundToInt((characters[charTurn].GetTotalMind()) * skill.GetSkillMod()) + skill.GetModPlus();
            enemyDef = enemyObjects[enemyAttacked].GetComponent<EnemyClass>().GetEnemySoul();
            numberAttacks = 1;
        }

        int totalMainDamage = Mathf.RoundToInt(playerMainAttack * playerMainAttack / (playerMainAttack + enemyDef));
        int totalOffDamage = Mathf.RoundToInt(playerOffAttack * playerOffAttack / (playerOffAttack + enemyDef));
        bool enemyKilled = false;

        StartCoroutine(attackWait(1));

        for (int i = 0; i < numberAttacks; i++)
        {
            int damageMainDealt = Mathf.RoundToInt(Random.Range(totalMainDamage * 0.8f, totalMainDamage * 1.2f));
            int damageOffDealt = Mathf.RoundToInt(Random.Range(totalOffDamage * 0.8f, totalOffDamage * 1.2f));

            bool isBlind = characters[charTurn].CheckAffliction("Blind");

            if(isBlind)
            {
                int hitChance = Random.Range(0, 101);

                if(hitChance >= 75)
                {
                    enemyKilled = enemyObjects[enemyAttacked].GetComponent<EnemyClass>().ChangeEnemyCurrentHp(-damageMainDealt);
                    enemyKilled = enemyObjects[enemyAttacked].GetComponent<EnemyClass>().ChangeEnemyCurrentHp(-damageOffDealt);
                }
            }
            else
            {
                enemyKilled = enemyObjects[enemyAttacked].GetComponent<EnemyClass>().ChangeEnemyCurrentHp(-damageMainDealt);
                enemyKilled = enemyObjects[enemyAttacked].GetComponent<EnemyClass>().ChangeEnemyCurrentHp(-damageOffDealt);
            }            
        }

        string debuffType = skill.GetSkillDebuffType();

        if (debuffType != null)
        {
            float chance = skill.GetSkillDebuffChance();

            float range = Random.Range(0, 1);

            if (range <= chance)
            {
                int rounds = skill.GetTurnEffect();
                float skillMod = skill.GetSkillMod();
                int skillPlus = skill.GetModPlus();
                int strength = (int)((characters[charTurn].GetTotalMind() * skillMod) + skillPlus);

                enemyObjects[enemyAttacked].GetComponent<EnemyClass>().SetEnemyDebuff(debuffType, rounds, strength);
            }
        }

        characters[charTurn].ChangeCharCurrentMp(-skill.GetSkillMana());

        if (enemyKilled)
        {
            expGained += enemyObjects[enemyAttacked].GetComponent<EnemyClass>().GetEnemyExp();
            goldGained += Mathf.RoundToInt((enemyObjects[enemyAttacked].GetComponent<EnemyClass>().GetEnemyGold()) * adBonus);
            DestroyObject(enemyObjects[enemyAttacked]);
            enemyObjects.RemoveAt(enemyAttacked);

            if (enemySelected != -1)
                enemySelected = -1;

            try
            {
                int enemyCount = enemyObjects.Count;

                if (enemyCount == 0)
                    currentState = BattleStates.WIN;
            }
            catch
            {
                currentState = BattleStates.WIN;
            }
            
        }

        charObjects[charTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        nextChar();
        updateText();
    }

    private void confusedAttack()
    {
        List<SkillClass> skills = characters[charTurn].GetCharSkills();
        int numSkills = skills.Count;
        int charAttacked;

        bool charAlive = true;
        do
        {
            charAttacked = Random.Range(0, 4);

            if (characters[charAttacked].GetCharCurrentHp() <= 0)
            {
                charAlive = false;
            }

        } while (charAlive == false);
        
        int randomAttack = Random.Range(0, 101);
        float rangeForSkills = 50 / numSkills;

        SkillClass selectedSkill = null;

        int playerMainAttack;
        int playerOffAttack = 0;
        int enemyDef;
        int numberAttacks;        

        if (randomAttack >= 50)
        {
            selectedSkill = attack;
        }
        else
        {
            float checkedRange = 49;
            int skillIndex = 0;
            do
            {
                checkedRange -= rangeForSkills;

                if (randomAttack >= checkedRange)
                    selectedSkill = skills[skillIndex];
                else
                    skillIndex++;
            }
            while (selectedSkill == null);
        }

        string damageType = selectedSkill.GetSkillDamageType();

        StartCoroutine(attackWait(1));

        if (damageType.Equals("Physical"))
        {
            playerMainAttack = Mathf.RoundToInt((characters[charTurn].GetTotalStr() + characters[charTurn].GetWeapon().GetDamage()) * selectedSkill.GetSkillMod()) + selectedSkill.GetModPlus();

            try
            {
                if (characters[charTurn].GetOffHand().GetItemType().Equals("Weapon"))
                    playerOffAttack = Mathf.RoundToInt((characters[charTurn].GetTotalStr() + characters[charTurn].GetOffHand().GetDamage()) * selectedSkill.GetSkillMod()) + selectedSkill.GetModPlus();
                else
                    playerOffAttack = 0;
            }
            catch
            {
                playerOffAttack = 0;
            }

            enemyDef = characters[charAttacked].GetTotalDef();
            numberAttacks = (characters[charTurn].GetTotalAgi() / 30) + 1;
        }
        else
        {
            playerMainAttack = Mathf.RoundToInt((characters[charTurn].GetTotalMind()) * selectedSkill.GetSkillMod()) + selectedSkill.GetModPlus();
            enemyDef = characters[charAttacked].GetTotalSoul();
            numberAttacks = 1;
        }

        int totalMainDamage = Mathf.RoundToInt(playerMainAttack * playerMainAttack / (playerMainAttack + enemyDef));
        int totalOffDamage = Mathf.RoundToInt(playerOffAttack * playerOffAttack / (playerOffAttack + enemyDef));

        for (int i = 0; i < numberAttacks; i++)
        {
            int damageMainDealt = Mathf.RoundToInt(Random.Range(totalMainDamage * 0.8f, totalMainDamage * 1.2f));
            int damageOffDealt = Mathf.RoundToInt(Random.Range(totalOffDamage * 0.8f, totalOffDamage * 1.2f));

            characters[charAttacked].ChangeCharCurrentMp(-damageMainDealt);
            characters[charAttacked].ChangeCharCurrentMp(-damageOffDealt);
        }

        string debuffType = selectedSkill.GetSkillDebuffType();

        if(debuffType != null)
        {
            float chance = selectedSkill.GetSkillDebuffChance();

            float range = Random.Range(0, 1);

            if(range <= chance)
            {
                int rounds = selectedSkill.GetTurnEffect();
                float skillMod = selectedSkill.GetSkillMod();
                int skillPlus = selectedSkill.GetModPlus();
                int strength = (int)((characters[charTurn].GetTotalMind() * skillMod) + skillPlus);

                characters[charAttacked].AfflictStatus(debuffType, rounds, strength);
            }
        }

        characters[charTurn].ChangeCharCurrentMp(-selectedSkill.GetSkillMana());
        charObjects[charTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        checkDead();
        nextChar();
    }

    private void enemyConfused(GameObject enemy)
    {
        int enemyTargeted = Random.Range(0, enemyObjects.Count);
        EnemyClass targetScript = enemyObjects[enemyTargeted].GetComponent<EnemyClass>();
        int confusedAttack = enemy.GetComponent<EnemyClass>().GetEnemyStr();
        int targetDef = targetScript.GetEnemyDef();

        int totalDamage = Mathf.RoundToInt(confusedAttack * confusedAttack / (confusedAttack + targetDef));

        targetScript.ChangeEnemyCurrentHp(-totalDamage);

        try
        {
            int enemyCount = enemyObjects.Count;

            if (enemyCount == 0)
                currentState = BattleStates.WIN;
        }
        catch
        {
            currentState = BattleStates.WIN;
        }
    }

    private void enemyAttack(GameObject enemy)
    {
        CharacterClass target = targetPlayer();
        int enemyStr = enemy.GetComponent<EnemyClass>().GetEnemyStr();
        int playerDef = target.GetTotalDef();

        int damage = Mathf.RoundToInt(Mathf.Pow(enemyStr, 2) / (enemyStr + playerDef));
        target.ChangeCharCurrentHp(-damage);
        checkDead();
        updateText();
    }

    private void enemySkill(GameObject enemy, SkillClass skill)
    {
        CharacterClass target = targetPlayer();
        EnemyClass enemyScript = enemy.GetComponent<EnemyClass>();
        string debuffType = skill.GetSkillDebuffType();
        string damageType = skill.GetSkillDamageType();
        float skillMod = skill.GetSkillMod();
        int modPlus = skill.GetModPlus();
        int enemyStr;
        int playerDef;

        if(damageType.Equals("Physical"))
        {
            enemyStr = enemyScript.GetEnemyStr();
            playerDef = target.GetTotalDef();
        }
        else
        {
            enemyStr = enemyScript.GetEnemyMind();
            playerDef = Mathf.RoundToInt(target.GetTotalSoul() * 1.25f);
        }

        enemyToPlayer(target, skillMod, modPlus, enemyStr, playerDef);

        if(debuffType != null)
        {
            float chance = skill.GetSkillDebuffChance();

            float range = Random.Range(0, 1);

            if (range <= chance)
            {
                int rounds = skill.GetTurnEffect();
                int strength = (int)((characters[charTurn].GetTotalMind() * skillMod) + modPlus);

                target.AfflictStatus(debuffType, rounds, strength);
            }
        }
    }

    private void enemyToPlayer(CharacterClass target, float skillMod, int modPlus, int enemyStr, int playerDef)
    {
        int damage = Mathf.RoundToInt(((Mathf.Pow(enemyStr, 2) / (enemyStr + playerDef)) * skillMod) + modPlus);
        int damageDealt = Mathf.RoundToInt(Random.Range(damage * 0.8f, damage * 1.2f));

        StartCoroutine(attackWait(1));

        target.ChangeCharCurrentHp(-damageDealt);

        checkDead();

        updateText();
    }

    private CharacterClass targetPlayer()
    {
        CharacterClass player = null;

        int charAttacked = Random.Range(0, 101);

        if (charAttacked >= 60)
        {
            if (characters[0].GetCharCurrentHp() > 0)
            {
                player = characters[0];
                return player;
            }                
        }

        if (charAttacked >= 30)
        {
            if (characters[1].GetCharCurrentHp() > 0)
            {
                player = characters[1];
                return player;
            }
            else if(characters[0].GetCharCurrentHp() > 0)
            {
                player = characters[0];
                return player;
            }
        }

        if (charAttacked >= 10)
        {
            if (characters[2].GetCharCurrentHp() > 0)
            {
                player = characters[2];
                return player;
            }
            else if (characters[1].GetCharCurrentHp() > 0)
            {
                player = characters[1];
                return player;
            }
            else if (characters[0].GetCharCurrentHp() > 0)
            {
                player = characters[0];
                return player;
            }
        }

        if (charAttacked >= 0)
        {
            if (characters[3].GetCharCurrentHp() > 0)
            {
                player = characters[3];
                return player;
            }
            else
                if (characters[2].GetCharCurrentHp() > 0)
            {
                player = characters[2];
                return player;
            }
            else if (characters[1].GetCharCurrentHp() > 0)
            {
                player = characters[1];
                return player;
            }
            else if (characters[0].GetCharCurrentHp() > 0)
            {
                player = characters[0];
                return player;
            }
        }
        else
        {
            currentState = BattleStates.LOSE;
        }

        return null;
    }

    //flees battle if flee option is selected
    public void flee()
    {
        int fleeChance = Random.Range(1, 101);
        charObjects[charTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        if (fleeChance >= 75)
        {
            expGained = 0;
            goldGained = 0;
            enemySelected = 0;

            exitBattle();
        }
        else
            nextChar();
    }

    //updates character text in battle screen
    public void updateText()
    {
        char1Name.text = characters[0].GetCharName();
        char1HP.text = string.Format("HP: {0}/{1}", characters[0].GetCharCurrentHp(), characters[0].GetCharMaxHp());
        char1MP.text = string.Format("MP: {0}/{1}", characters[0].GetCharCurrentMp(), characters[0].GetCharMaxMp());

        char2Name.text = characters[1].GetCharName();
        char2HP.text = string.Format("HP: {0}/{1}", characters[1].GetCharCurrentHp(), characters[1].GetCharMaxHp());
        char2MP.text = string.Format("MP: {0}/{1}", characters[1].GetCharCurrentMp(), characters[1].GetCharMaxMp());

        char3Name.text = characters[2].GetCharName();
        char3HP.text = string.Format("HP: {0}/{1}", characters[2].GetCharCurrentHp(), characters[2].GetCharMaxHp());
        char3MP.text = string.Format("MP: {0}/{1}", characters[2].GetCharCurrentMp(), characters[2].GetCharMaxMp());

        char4Name.text = characters[3].GetCharName();
        char4HP.text = string.Format("HP: {0}/{1}", characters[3].GetCharCurrentHp(), characters[3].GetCharMaxHp());
        char4MP.text = string.Format("MP: {0}/{1}", characters[3].GetCharCurrentMp(), characters[3].GetCharMaxMp());
    }

    //exits from the battle screen
    public void exitBattle()
    {
        charTurn = 0;
        healSkill = null;
        healItem = null;

        foreach(CharacterClass charas in characters)
        {
            charas.ResetStatus();
        }

        if (currentState == BattleStates.LOSE)
        {
            enemyObjects.Clear();
            battleOutcome.SetActive(false);
            StoreFinds.stored.battleActivate();

            for (int i = 0;i < 4;i++)
            {
                characters[i].ChangeCharCurrentHp(1);
            }
            currentState = BattleStates.WAIT;

            ExitDungeon.exit.travel();
        }
        enemyObjects.Clear();

        StoreFinds.stored.battleActivate();
        battleOutcome.SetActive(false);
        gameObject.SetActive(false);
        currentState = BattleStates.WAIT;
    }

    private void awardExp()
    {
        int charsAlive = 0;

        foreach (CharacterClass character in characters)
        {
            if (character.GetCharCurrentHp() > 0)
            {
                charsAlive++;
            }
        }

        foreach (CharacterClass character in characters)
        {
            if (character.GetCharCurrentHp() > 0)
            {
                bool levelCheck = character.AddCurExp(Mathf.RoundToInt(expGained / charsAlive));

                if (levelCheck)
                {
                    sb.Append(string.Format("{0} Gained a Level.  {0} is now level {1}!\n", character.GetCharName(), character.GetCharLevel()));
                }
            }
        }
    }

    private void nextChar()
    {
        charObjects[charTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        charTurn++;
        healSkill = null;
        healItem = null;
        Debug.Log("Character Turn: " + (charTurn + 1));

        if (charTurn > 3)
        {
            Debug.Log("Character Turn = " + charTurn + " moving to Enemy Counter stage");
            currentState = BattleStates.ENEMYCOUNTER;
        }
        else
        {
            Debug.Log("Character Turn = " + charTurn + " staying on Player Turn stage");
        }
            
    }

    private void itemDrop()
    {
        int dropCheck = Random.Range(0, 101);
        int equipLimit = Mathf.RoundToInt(5 * adBonus);
        int usableLimit = Mathf.RoundToInt(15 * adBonus);

        if (dropCheck >= (100 - equipLimit))
        {
            int equipCheck = Random.Range(0, equipableDrops.Count);
            CharacterInventory.charInven.addEquipableToInventory(equipableDrops[equipCheck]);
            sb.Append(string.Format("Found Item: {0} x 1", equipableDrops[equipCheck].GetName()));
        }
        else if (dropCheck >= (100 - usableLimit))
        {
            int usableCheck = Random.Range(0, usableDrops.Count);
            CharacterInventory.charInven.addUsableToInventory(usableDrops[usableCheck]);
            sb.Append(string.Format("Found Item: {0} x 1", usableDrops[usableCheck].GetName()));
        }
    }

    private void checkDead()
    {
        int charsAlive = 0;

        foreach (CharacterClass character in characters)
        {
            if (character.GetCharCurrentHp() > 0)
            {
                charsAlive++;
            }
        }

        if (charsAlive == 0)
            currentState = BattleStates.LOSE;
    }



    public void SetEnemySelected(int selected)
    {
        enemySelected = selected;
    }

    public int GetCharTurn() { return charTurn; }

    IEnumerator attackWait(float waitTime)
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(waitTime);
    }
}
