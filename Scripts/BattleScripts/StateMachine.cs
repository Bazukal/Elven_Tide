using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class StateMachine : MonoBehaviour {

    public static StateMachine state;

    public enum BattleStates
    {
        START,
        PLAYERPREPARE,
        PLAYERTURN,
        PLAYERCOUNTER,
        ENEMYTURN,
        ENEMYCOUNTER,
        WIN,
        LOSE,
        FLEE,
        ENDWAIT
    }

    public BattleStates currentState = BattleStates.ENDWAIT;

    public List<GameObject> levelEnemies;
    public GameObject levelBoss;

    //-1 = none, 0 - 3 = enemy 1-4
    private int enemySelected = -1;
    private int enemyTurn = 0;
    private List<GameObject> enemyObjects;

    public List<GameObject> enemyPlace;
    public GameObject bossPlace;

    private int expGained = 0;
    private int goldGained = 0;
    private float adBonus = 0f;

    private List<PlayerClass> characters = new List<PlayerClass>();
    private List<EquipmentClass> equipableDrops = new List<EquipmentClass>();
    private List<ItemClass> usableDrops = new List<ItemClass>();
    public List<GameObject> iconPanel = new List<GameObject>();

    public List<GameObject> charObjects = new List<GameObject>();
    private int charTurn = 0;
    int aveLevel;

    private string lost = "You lost the battle.  Your body will be sent back to town for healing.";
    StringBuilder sb;

    public Button attackButton;
    public Button skillsButton;
    public Button itemsButton;
    public Button defendButton;
    public Button fleeButton;

    private ItemClass healItem = new ItemClass();
    private SkillClass healSkill = new SkillClass();

    public GameObject battleOutcome;
    public Text outcomeText;

    public GameObject skillsNItemsPanel;

    private int rounds;
    private int mainAttack;
    private int offAttack;
    int attackedDef = 0;

    public GameObject defendAnim;

    private bool bossBattle = false;

    // Use this for initialization
    void Start () {
        state = this;

        characters.Add(Manager.manager.GetPlayer("Player1"));
        characters.Add(Manager.manager.GetPlayer("Player2"));
        characters.Add(Manager.manager.GetPlayer("Player3"));
        characters.Add(Manager.manager.GetPlayer("Player4"));

        if (Manager.manager.getAd())
            adBonus = 1.5f;
        else
            adBonus = 1.0f;

        enemyObjects = new List<GameObject>();

        aveLevel = Manager.manager.AveLevel();

        equipableDrops = GameItems.gItems.getEquipableInRange(aveLevel);
        usableDrops = GameItems.gItems.getUsableInRange(aveLevel);         
    }

    public void StartBattle(bool boss)
    {
        if (boss)
            bossBattle = true;
        else
            bossBattle = false;

        currentState = BattleStates.START;
        CheckState();
    }

    private void CheckState()
    {
        switch(currentState)
        {
            case BattleStates.START:
                StoreFinds.stored.BattleActivate();
                BattleStats.stats.updateStats();
                enemySelected = -1;
                goldGained = 0;
                expGained = 0;
                spawnEnemies();
                break;
            case BattleStates.PLAYERPREPARE:
                attackButton.interactable = true;
                skillsButton.interactable = true;
                itemsButton.interactable = true;
                defendButton.interactable = true;
                fleeButton.interactable = true;

                bool poisoned = characters[charTurn].CheckAffliction("Poison");

                if (poisoned)
                {
                    int poisonStr = characters[charTurn].poisonDamage();
                    bool died = characters[charTurn].changeHP(poisonStr);

                    if(died)
                    {
                        checkDead();
                        nextChar();
                    }
                }

                int playerHP = characters[charTurn].GetCurrentHP();
                bool paralyzed = characters[charTurn].CheckAffliction("Paralyzed");

                if(playerHP <= 0 || paralyzed == true)
                    nextChar();

                charObjects[charTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(-50, 0);
                
                rounds = characters[charTurn].numAttacks();
                mainAttack = checkPlayerDamage(characters[charTurn], null, true);
                offAttack = checkPlayerDamage(characters[charTurn], null, false);                

                bool confused = characters[charTurn].CheckAffliction("Confused");

                //perform confused attack
                if (confused)
                {                    
                    toPlayerTurn();

                    int who = Random.Range(0, 4);
                    if(who < 3)  //attacks random player
                    {                        
                        int charAttacked = selectRandomChar();                        

                        attackedDef = checkPlayerDefense(characters[charAttacked]);

                        toPlayerTurn();
                        checkBlind(false, "Attack", mainAttack, offAttack, attackedDef, rounds,
                            charAttacked, true);
                    }
                    else  //attacks random enemy
                    {
                        int enemyAttack = selectRandomEnemy();

                        attackedDef = checkEnemyDefense(enemyObjects[enemyAttack]);

                        toPlayerTurn();
                        checkBlind(true, "Attack", mainAttack, offAttack, attackedDef, rounds, 
                            enemyAttack, true);
                    }
                }
                break;
            case BattleStates.PLAYERTURN:
                attackButton.interactable = false;
                skillsButton.interactable = false;
                itemsButton.interactable = false;
                defendButton.interactable = false;
                fleeButton.interactable = false;
                break;
            case BattleStates.PLAYERCOUNTER:
                foreach(PlayerClass character in characters)
                {
                    int charIndex = characters.IndexOf(character);
                    PlayerIcons playerPanel = iconPanel[charIndex].GetComponent<PlayerIcons>();

                    character.BuffCounter(playerPanel);
                    character.DebuffCounter(playerPanel);
                }

                charTurn = 0;
                currentState = BattleStates.ENEMYTURN;
                CheckState();
                break;
            case BattleStates.ENEMYTURN:
                EnemyClass prepareScript = enemyObjects[enemyTurn].GetComponent<EnemyClass>();
                bool enemyParalyzed = prepareScript.CheckAffliction("Paralyzed");
                bool enemyConfused = prepareScript.CheckAffliction("Confused");

                enemyObjects[enemyTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(50, 0);

                if (enemyParalyzed == true)
                {
                    enemyObjects[enemyTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    nextEnemy();
                }
                else if (enemyConfused == true)
                    confusedEnemy();
                else
                    enemyAction();
                break;
            case BattleStates.ENEMYCOUNTER:
                foreach(GameObject enemy in enemyObjects)
                {
                    EnemyClass script = enemy.GetComponent<EnemyClass>();
                    script.DebuffCounter();
                }

                enemyTurn = 0;
                currentState = BattleStates.PLAYERPREPARE;
                CheckState();
                break;
            case BattleStates.WIN:
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
                Manager.manager.changeGold(totalGold);
                ItemDrop();

                outcomeText.text = sb.ToString();
                currentState = BattleStates.ENDWAIT;
                break;
            case BattleStates.LOSE:
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
        if(bossBattle == false)
        {
            for (int i = 0; i < 4; i++)
            {
                charObjects[i].GetComponent<Image>().sprite = Manager.manager.getCharBattleSprite(characters[i].GetClass());
                charObjects[i].GetComponent<Image>().SetNativeSize();
            }

            int enemyAvail = levelEnemies.Count;
            int enemyNumbers = Random.Range(1, 5);

            for (int i = 0; i < enemyNumbers; i++)
            {
                int whichEnemy = Random.Range(0, enemyAvail);

                GameObject newEnemy = (GameObject)Instantiate(levelEnemies[whichEnemy]) as GameObject;
                newEnemy.transform.SetParent(enemyPlace[i].transform, false);
                EnemyClass setPlace = newEnemy.GetComponent<EnemyClass>();
                setPlace.SetEnemyStats(aveLevel);
                enemyObjects.Add(newEnemy);
            }
        }
        else
        {
            GameObject newBoss = (GameObject)Instantiate(levelBoss) as GameObject;
            newBoss.transform.SetParent(bossPlace.transform, false);
            EnemyClass setBoss = newBoss.GetComponent<EnemyClass>();
            setBoss.SetBossStats(aveLevel);
            enemyObjects.Add(newBoss);
        }

        currentState = BattleStates.PLAYERPREPARE;
        CheckState();
    }

    //select random alive player to attack
    private int selectRandomChar()
    {
        int charAttacked = Random.Range(0, 101);

        if (charAttacked >= 40)
        {
            if (characters[0].GetCurrentHP() > 0)
                return 0;
        }

        if (charAttacked >= 20)
        {
            if (characters[1].GetCurrentHP() > 0)
                return 1;
        }

        if (charAttacked >= 10)
        {
            if (characters[2].GetCurrentHP() > 0)
                return 2;
        }

        if (charAttacked >= 0)
        {
            if (characters[3].GetCurrentHP() > 0)
                return 3;
        }

        return -1;
    }

    //select random enemy to attack
    private int selectRandomEnemy()
    {
        int numEnemies = enemyObjects.Count;
        int enemyAttack = Random.Range(0, numEnemies);

        return enemyAttack;
    }

    //actions taken by a confused enemy
    private void confusedEnemy()
    {
        int sideCheck = Random.Range(0, 4);

        if (sideCheck == 3)
            enemyAction();
        else
        {
            int whichEnemy = selectRandomEnemy();
            int enemyAtt = checkEnemyDamage(enemyObjects[enemyTurn], null);
            int targetDef = checkEnemyDefense(enemyObjects[whichEnemy]);

            checkBlind(true, "Attack", enemyAtt, -1, targetDef, 1, whichEnemy, false);
        }
    }

    //normal actions taken by enemy
    private void enemyAction()
    {
        EnemyClass script = enemyObjects[enemyTurn].GetComponent<EnemyClass>();
        int enemySkills = script.enemySkills.Count;
        string skillName = null;
        int enemyAtt;
        int targetDef;

        if (enemySkills == 0)
        {
            int whichEnemy = selectRandomChar();
            enemyAtt = checkEnemyDamage(enemyObjects[enemyTurn], null);
            targetDef = checkPlayerDefense(characters[whichEnemy]);

            checkBlind(false, "Attack", enemyAtt, -1, targetDef, 1, whichEnemy, false);
        }
        else
        {
            int charAttacked = selectRandomChar();

            targetDef = checkPlayerDefense(characters[charAttacked]);

            int randomAttack = Random.Range(0, 101);
            float rangeForSkills = 50 / enemySkills;

            if (randomAttack >= 50)
                enemyAtt = checkEnemyDamage(enemyObjects[enemyTurn], null);
            else
            {
                float checkedRange = 49;
                int skillIndex = 0;
                do
                {
                    checkedRange -= rangeForSkills;

                    if (randomAttack >= checkedRange)
                        skillName = script.enemySkills[skillIndex];
                    else
                        skillIndex++;
                }
                while (skillName == null);
                enemyAtt = checkEnemyDamage(enemyObjects[enemyTurn], skillName);
                SkillClass skillUsed = script.GetSkill(skillName);
                string affliction = skillUsed.GetCondition();

                if(affliction != "")
                {
                    float chance = skillUsed.GetDebuffChance();

                    float range = Random.Range(0, 1);

                    if (range <= chance)
                    {
                        int rounds = skillUsed.GetRounds();
                        int charStat = script.GetEnemyMind();
                        int strength = skillUsed.healthChange(charStat);

                        characters[charAttacked].AfflictStatus(affliction, rounds, strength);

                        GameObject image = BuffIcons.buffIcons.getBuffIcon(affliction);
                        iconPanel[charAttacked].GetComponent<PlayerIcons>().addIcons(image);
                    }
                }
            }
            checkBlind(false, skillName, enemyAtt, -1, targetDef, 1, charAttacked, false);
        }
    }

    //actions to take when player selects attack
    public void attackAction()
    {
        int attackWho;

        if (enemySelected == -1)
            attackWho = selectRandomEnemy();
        else
            attackWho = enemySelected;

        int enemyDef = checkEnemyDefense(enemyObjects[attackWho]);

        toPlayerTurn();
        checkBlind(true, "Attack", mainAttack, offAttack, enemyDef, rounds, attackWho, true);
    }

    //increases characters defense when player defends
    public void defendAction()
    {
        characters[charTurn].SetDefend();
        charObjects[charTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        GameObject defendObject = (GameObject)Instantiate(defendAnim) as GameObject;
        defendObject.transform.SetParent(charObjects[charTurn].transform, false);
        float animTimer = defendObject.GetComponentInChildren<ParticleSystem>().main.duration;
        Destroy(defendObject, animTimer);

        nextChar();
    }

    //what item is going to be used
    public void itemAction(ItemClass item)
    {
        healItem = item;
    }

    //attacks enemy with skill
    public void skillAction(SkillClass skill)
    {
        int enemyTarget;
        skillsNItemsPanel.SetActive(false);

        int rounds = characters[charTurn].numAttacks();
        int mainAttack = checkPlayerDamage(characters[charTurn], skill, true);
        int offAttack = -1;

        string skillType = skill.GetDamageType();
        if(skillType.Equals("Physical"))
            offAttack = checkPlayerDamage(characters[charTurn], skill, false);

        if (enemySelected == -1)
            enemyTarget = selectRandomEnemy();
        else
            enemyTarget = enemySelected;

        int attackedDef = checkEnemyDefense(enemyObjects[enemyTarget]);

        toPlayerTurn();
        
        string affliction = skill.GetCondition();

        if (affliction != "")
        {
            float chance = skill.GetDebuffChance();

            float range = Random.Range(0, 1);

            if (range <= chance)
            {
                int afflictRounds = skill.GetRounds();
                int charStat = characters[charTurn].GetMind();
                int strength = skill.healthChange(charStat);

                enemyObjects[enemyTarget].GetComponent<EnemyClass>().SetEnemyDebuff(affliction, afflictRounds, strength);
            }
        }

        int skillCost = skill.GetCost();
        characters[charTurn].changeMP(-skillCost);

        checkBlind(true, skill.GetName(), mainAttack, offAttack, attackedDef, rounds, 
            enemyTarget, true);
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

    //indicates skill being used is a heal
    public void healingSkill(SkillClass skill)
    {
        healSkill = skill;
    }

    //view listing of character skills
    public void viewSkills()
    {
        skillsNItemsPanel.SetActive(true);
        BattleSkillsItems.view.showSkills();
    }

    //view listing of items
    public void viewItems()
    {
        skillsNItemsPanel.SetActive(true);
        BattleSkillsItems.view.showItems();
    }

    public void healChar(int character)
    {
        if (healSkill != null)
        {
            if (healSkill.GetSkillType().Equals("Heal"))
            {
                int healAmount = Mathf.RoundToInt((characters[charTurn].GetSoul() * healSkill.GetSkillModifier()) + healSkill.GetSkillBase());
                characters[character].changeHP(healAmount);
            }
            else if (healSkill.GetSkillType().Equals("Cure"))
            {
                string condition = healSkill.GetCondition();
                characters[character].CureStatus(condition);
                iconPanel[character].GetComponent<PlayerIcons>().removeIcons(condition);
            }
            else if (healSkill.GetSkillType().Equals("Buff"))
            {
                string statBuff = healSkill.GetStatChange();
                characters[character].SetBuff(statBuff, healSkill.GetSkillBase(), healSkill.GetRounds());

                GameObject image = BuffIcons.buffIcons.getBuffIcon(statBuff);
                iconPanel[character].GetComponent<PlayerIcons>().addIcons(image);
            }
            else
            {
                int healAmount = Mathf.RoundToInt(characters[character].GetMaxHP() * healSkill.GetSkillModifier());
                characters[character].changeHP(healAmount);
            }

            characters[charTurn].changeMP(-healSkill.GetCost());
        }
        else
        {
            if (!healItem.GetCure().Equals(null))
            {
                characters[character].CureStatus(healItem.GetCure());
            }
            else
            {
                characters[character].changeHP(healItem.GetHeal());
            }

            healItem.ChangeQuantity(-1);
        }
        BattleSkillsItems.view.cancelCharSelect();
        BattleSkillsItems.view.cancelUse();

        nextChar();
    }

    //checks if the attacking character is blind.  performs attack based on outcome
    private void checkBlind(bool enemyTarget, string skillName, int main, int off, int def, int rounds,
        int target, bool playerAttacking)
    {
        bool isBlind = false;

        if (playerAttacking)
            isBlind = characters[charTurn].CheckAffliction("Blind");
        else
            isBlind = enemyObjects[enemyTurn].GetComponent<EnemyClass>().CheckAffliction("Blind");

        GameObject animation = Manager.manager.GetAnimation(skillName);

        if (enemyTarget)
        {
            if (isBlind)
            {
                int blindRoll = Random.Range(0, 11);
                if (blindRoll >= 7)
                    StartCoroutine(EnemyAttacked(animation, main, off, def, rounds, enemyObjects[target],
                        playerAttacking));
                else
                    StartCoroutine(EnemyAttacked(animation, 0, -1, def, rounds, enemyObjects[target],
                        playerAttacking));
            }
            else
                StartCoroutine(EnemyAttacked(animation, main, off, def, rounds, enemyObjects[target],
                    playerAttacking));
        }
        else
        {
            if (isBlind)
            {
                int blindRoll = Random.Range(0, 11);
                if (blindRoll >= 7)
                    StartCoroutine(PlayerAttacked(animation, main, off, def, rounds, characters[target],
                        playerAttacking));
                else
                    StartCoroutine(PlayerAttacked(animation, 0, -1, def, rounds, characters[target],
                        playerAttacking));
            }
            else
                StartCoroutine(PlayerAttacked(animation, main, off, def, rounds, characters[target],
                    playerAttacking));
        }
    }

    //changes state to playerturn
    private void toPlayerTurn()
    {
        currentState = BattleStates.PLAYERTURN;
        CheckState();
    }

    //checks how much damage player will do
    private int checkPlayerDamage(PlayerClass player, SkillClass skill, bool main)
    {
        int damage = 0;

        if (skill != null)
            damage = player.SkillDamage(skill, main);
        else
            damage = player.AttackDamage(main);

        return damage;
    }

    //check player defense
    private int checkPlayerDefense(PlayerClass player)
    {
        return player.GetDefense();
    }

    //checks how much damage enemy will do
    private int checkEnemyDamage(GameObject enemy, string skill)
    {
        EnemyClass script = enemy.GetComponent<EnemyClass>();

        if (skill != null)
            return script.skillDamage(skill);
        else
            return script.GetEnemyStr();
    }

    //check enemy defense
    private int checkEnemyDefense(GameObject enemy)
    {
        EnemyClass script = enemy.GetComponent<EnemyClass>();

        return script.GetEnemyDef();
    }

    //targets specific enemy for attacks
    public void SetEnemySelected(GameObject selected)
    {
        int index = enemyObjects.IndexOf(selected);
        enemySelected = index;
    }

    //deselects an enemy
    public void removeEnemySelected()
    {
        enemySelected = -1;
    }

    //what happens when an enemy dies from poison
    public void poisonKill(GameObject enemy)
    {
        int index = enemyObjects.IndexOf(enemy);

        expGained += enemyObjects[index].GetComponent<EnemyClass>().GetEnemyExp();
        goldGained += Mathf.RoundToInt((enemyObjects[index].GetComponent<EnemyClass>().GetEnemyGold()) * adBonus);
        DestroyObject(enemyObjects[index]);
        enemyObjects.RemoveAt(index);

        checkWin();
    }

    //checks to see if all enemies are dead
    public void checkWin()
    {
        int enemyCount = enemyObjects.Count;

        if (enemyCount == 0)
        {
            currentState = BattleStates.WIN;
            CheckState();
        }
    }

    //checks to see if all the player characters are dead
    private void checkDead()
    {
        int charsAlive = 0;

        foreach (PlayerClass character in characters)
        {
            if (character.GetCurrentHP() > 0)
            {
                charsAlive++;
            }
        }

        if (charsAlive == 0)
        {
            currentState = BattleStates.LOSE;
            CheckState();
        }            
    }

    //retrieve what character turn it is
    public int GetCharTurn() { return charTurn; }

    //moves to the next characters turn
    private void nextChar()
    {
        charObjects[charTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        charTurn++;

        if (charTurn > 3)
            currentState = BattleStates.PLAYERCOUNTER;
        else
            currentState = BattleStates.PLAYERPREPARE;

        CheckState();
    }

    //moves to the next enemies turn
    private void nextEnemy()
    {
        enemyObjects[enemyTurn].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        enemyTurn++;

        if (enemyTurn >= enemyObjects.Count)
            currentState = BattleStates.PLAYERPREPARE;
        else
            currentState = BattleStates.ENEMYTURN;

        CheckState();
    }

    //removes all enemies from list
    public void removeEnemyTargets()
    {
        foreach (GameObject enemy in enemyObjects)
        {
            enemy.GetComponent<EnemyInteractions>().removeTarget();
        }
    }

    //exits from the battle screen
    public void exitBattle()
    {
        charTurn = 0;
        enemyTurn = 0;

        foreach (PlayerClass charas in characters)
        {
            charas.ResetStatus();
        }

        if (currentState == BattleStates.LOSE)
        {
            enemyObjects.Clear();
            battleOutcome.SetActive(false);

            for (int i = 0; i < 4; i++)
            {
                characters[i].changeHP(1);
            }
            currentState = BattleStates.ENDWAIT;

            ExitDungeon.exit.travel();
        }
        enemyObjects.Clear();

        enemySelected = -1;
        battleOutcome.SetActive(false);
        gameObject.SetActive(false);
        StoreFinds.stored.BattleDeactivate();
        currentState = BattleStates.ENDWAIT;
    }

    private void awardExp()
    {
        int charsAlive = 0;

        foreach (PlayerClass character in characters)
        {
            if (character.GetCurrentHP() > 0)
            {
                charsAlive++;
            }
        }

        foreach (PlayerClass character in characters)
        {
            if (character.GetCurrentHP() > 0)
            {
                int currentLvl = character.GetLevel();
                bool levelCheck = character.AddCurExp(Mathf.RoundToInt(expGained / charsAlive));

                if (levelCheck)
                {
                    int newLvl = character.GetLevel();
                    int lvlsGained = newLvl - currentLvl;

                    if(lvlsGained == 1)
                        sb.Append(string.Format("{0} Gained a Level.  {0} is now level {1}!\n", character.GetName(), character.GetLevel()));
                    else
                        sb.Append(string.Format("{0} Gained {1} Levels.  {0} is now level {2}!\n", character.GetName(), lvlsGained, character.GetLevel()));
                }
            }
        }
    }

    private void ItemDrop()
    {
        int dropCheck = Random.Range(0, 101);
        int equipLimit = Mathf.RoundToInt(10 * adBonus);
        int itemLimit = Mathf.RoundToInt(25 * adBonus);

        if(dropCheck >= (100 - equipLimit))
        {
            string rarity = RarityCheck();
            List<EquipmentClass> equipment = new List<EquipmentClass>();

            foreach (EquipmentClass equip in equipableDrops)
            {
                if (equip.GetRarity().Equals(rarity))
                    equipment.Add(equip);
            }

            try
            {
                int choose = Random.Range(0, equipment.Count);
                CharacterInventory.charInven.addEquipableToInventory(equipment[choose]);
                return;
            }
            catch
            {

            }            
        }

        if (dropCheck >= (100 - itemLimit))
        {
            string rarity = RarityCheck();
            List<ItemClass> items = new List<ItemClass>();

            foreach (ItemClass item in usableDrops)
            {
                if (item.GetRarity().Equals(rarity))
                    items.Add(item);
            }

            try
            {
                int choose = Random.Range(0, items.Count);
                CharacterInventory.charInven.addUsableToInventory(items[choose]);
                return;
            }
            catch
            {

            }
        }
    }

    private string RarityCheck()
    {
        int rarityCheck = Random.Range(0, 1001);
        string rarity;

        if (rarityCheck >= 990)
            rarity = "Mythical";
        else if (rarityCheck >= 925)
            rarity = "Rare";
        else if (rarityCheck >= 750)
            rarity = "UnCommon";
        else
            rarity = "Common";

        return rarity;
    }

    IEnumerator EnemyAttacked(GameObject Animation, int mainAttack, int offAttack, int defense, 
        int numRounds, GameObject enemy, bool playerAttacking)
    {
        float time = Animation.GetComponent<ParticleSystem>().main.duration;
        EnemyClass script = enemy.GetComponent<EnemyClass>();
        bool died = false;

        for(int i = 0;i < numRounds; i++)
        {
            GameObject anima = (GameObject)Instantiate(Animation) as GameObject;
            anima.transform.SetParent(enemy.transform, false);

            yield return new WaitForSeconds(time);
            //show main damage here
            int mainDamage = damageDone(mainAttack, defense);
            died = script.ChangeEnemyCurrentHp(-mainDamage);
            Destroy(anima);
            if (offAttack >= 0)
            {
                GameObject offAnima = (GameObject)Instantiate(Animation) as GameObject;
                offAnima.transform.SetParent(enemy.transform, false);

                yield return new WaitForSeconds(time);
                //show off damage here, offAttack will be -1 if there is no weapon to attack with
                int offDamage = damageDone(offAttack, defense);
                died = script.ChangeEnemyCurrentHp(-offDamage);
                Destroy(offAnima);               
            }
        }

        if(died)
        {
            int gold = script.GetEnemyGold();
            int exp = script.GetEnemyExp();
            goldGained += gold;
            expGained += exp;
            enemyObjects.Remove(enemy);
            Destroy(enemy);
            checkWin();
        }

        BattleStats.stats.updateStats();

        if (playerAttacking)
            nextChar();
        else
            nextEnemy();
    }

    IEnumerator PlayerAttacked(GameObject Animation, int mainAttack, int offAttack, int defense,
        int numRounds, PlayerClass player, bool playerAttacking)
    {
        float time = Animation.GetComponent<ParticleSystem>().main.duration;
        bool died = false;

        for (int i = 0; i < numRounds; i++)
        {
            GameObject anima = (GameObject)Instantiate(Animation) as GameObject;
            int index = characters.IndexOf(player);
            anima.transform.SetParent(charObjects[index].transform, false);

            yield return new WaitForSeconds(time);
            //show main damage here
            int mainDamage = damageDone(mainAttack, defense);
            died = player.changeHP(-mainDamage);
            Destroy(anima);

            if (offAttack >= 0)
            {
                GameObject offAnima = (GameObject)Instantiate(Animation) as GameObject;
                offAnima.transform.SetParent(charObjects[index].transform, false);

                yield return new WaitForSeconds(time);
                //show off damage here, offAttack will be -1 if there is no weapon to attack with
                int offDamage = damageDone(offAttack, defense);
                died = player.changeHP(-offDamage);
                Destroy(offAnima);
            }
        }

        BattleStats.stats.updateStats();

        if (died)
        {
            //rotate panel to show character laying face down
            checkDead();
        }

        if (playerAttacking)
            nextChar();
        else
            nextEnemy();
    }

    //main damage
    private int damageDone(int damage, int defense)
    {
        int dam = Mathf.RoundToInt((Mathf.Pow(damage, 2) / 1.75f) / defense);
        return Mathf.RoundToInt(Random.Range(dam * 0.85f, dam * 1.15f));
    }
}
