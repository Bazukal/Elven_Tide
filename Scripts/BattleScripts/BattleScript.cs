using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using Devdog.QuestSystemPro;

public class BattleScript : MonoBehaviour {

    public static BattleScript battleOn;

    public enum BattleStates
    {
        NONBATTLE,
        START,
        NEXTTURN,
        PLAYERTURN,
        ENEMYTURN,
        WIN,
        LOSE,
        FLEE
    }

    public BattleStates currentState = BattleStates.NONBATTLE;

    public List<ScriptablePlayerClasses> levelEnemies;
    public ScriptablePlayerClasses levelBoss;

    public List<GameObject> enemyPlace;
    public List<GameObject> enemyTargets;
    public GameObject bossPlace;

    public GameObject terrain;

    public GameObject normalBattleMusic;
    public GameObject bossBattleMusic;

    private int expGained = 0;
    private int goldGained = 0;
    private float adBonus = 0f;

    private List<ScriptablePlayerClasses> characters = new List<ScriptablePlayerClasses>();
    private List<ScriptablePlayerClasses> enemies = new List<ScriptablePlayerClasses>();
    private List<ScriptablePlayerClasses> battleOrder = new List<ScriptablePlayerClasses>();

    private int turn = 0;
    private ScriptablePlayerClasses currentTurn;
    private ScriptablePlayerClasses enemySelected;

    public List<GameObject> charObjects = new List<GameObject>();
    int aveLevel;

    public List<EquipableItem> equipableDrops = new List<EquipableItem>();
    public List<UsableItem> usableDrops = new List<UsableItem>();
    public List<string> oreDrops = new List<string>();
    public List<GameObject> playerDebuffIconPanel = new List<GameObject>();
    public List<GameObject> enemyDebuffIconPanel = new List<GameObject>();
    public GameObject bossDebuffIconPanel;

    private string lost = "You lost the battle.  Your body will be sent back to town for healing.";
    StringBuilder sb;

    public Button attackButton;
    public Button skillsButton;
    public Button itemsButton;
    public Button defendButton;
    public Button fleeButton;

    private UsableItem healItem;

    private SkillScriptObject healSkill;

    public GameObject battleOutcome;
    public Text outcomeText;

    public Text skillName;

    public GameObject skillsNItemsPanel;

    public GameObject defendAnim;
    public GameObject damageDisplay;

    private bool bossBattle = false;

    public Scrollbar resultScroll;

    private List<string> questStrings = new List<string>();

    public Canvas battleCanvas;

    private void Awake()
    {
        battleOn = this;

        Manager.manager.SetScene("Dungeon");
    }

    // Use this for initialization
    void Start () {
        characters.Add(Manager.manager.GetPlayer("Player1"));
        characters.Add(Manager.manager.GetPlayer("Player2"));
        characters.Add(Manager.manager.GetPlayer("Player3"));
        characters.Add(Manager.manager.GetPlayer("Player4"));

        healItem = ScriptableObject.CreateInstance(typeof(UsableItem)) as UsableItem;

        healSkill = ScriptableObject.CreateInstance(typeof(SkillScriptObject)) as SkillScriptObject;

        foreach (ScriptablePlayerClasses character in characters)
        {
            int index = characters.IndexOf(character);
            int hp = character.currentHp;

            if (hp > 0)
                charObjects[index].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
            else
                charObjects[index].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 90);
        }

        if (Manager.manager.getAd())
            adBonus = 2;
        else
            adBonus = 1;

        aveLevel = Manager.manager.AveLevel();

        setSprites();

        gameObject.SetActive(false);

        Fade.fade.fadeIn();
    }

    //sets the terrain in the Equipment Dungeon
    public void setTerrain()
    {
        terrain = GameObject.FindGameObjectWithTag("Terrain");
    }
	
	//sets character battle sprites
    private void setSprites()
    {
        for (int i = 0; i < 4; i++)
        {
            charObjects[i].GetComponent<Image>().sprite = characters[i].battleSprite;
            charObjects[i].GetComponent<Image>().SetNativeSize();
        }
    }

    public void StartBattle(bool boss)
    {
        RandomMusic.music.StopMusic();

        if (boss)
        {
            bossBattle = true;
            bossBattleMusic.GetComponent<AudioSource>().Play();
        }
        else
        {
            bossBattle = false;
            normalBattleMusic.GetComponent<AudioSource>().Play();
        }

        terrain.SetActive(false);
        questStrings.Clear();

        currentState = BattleStates.START;
        CheckState();
    }

    private void SpawnEnemies()
    {
        if(bossBattle == false)
        {
            int enemyAvail = levelEnemies.Count;
            int numRoll = Random.Range(0, 101);
            int enemyNumbers = numRoll > 65 ? 1 : numRoll > 35 ? 2 : numRoll > 15 ? 3 : 4;

            for(int i = 0; i < enemyNumbers; i++)
            {
                int whichEnemy = Random.Range(0, enemyAvail);
                ScriptablePlayerClasses temp = levelEnemies[whichEnemy];
                ScriptablePlayerClasses newEnemy = CreateEnemy(temp);

                enemies.Add(newEnemy);
            }
        }
        else
        {
            ScriptablePlayerClasses temp = levelBoss;
            ScriptablePlayerClasses newEnemy = CreateEnemy(temp);

            enemies.Add(newEnemy);
        }

        RefreshEnemyButtons();
    }

    private ScriptablePlayerClasses CreateEnemy(ScriptablePlayerClasses temp)
    {
        ScriptablePlayerClasses newEnemy = ScriptableObject.CreateInstance(typeof(ScriptablePlayerClasses)) as ScriptablePlayerClasses;

        newEnemy.Init(temp.name, temp.charClass, temp.level, temp.currentExp, null,
                temp.battleSprite, temp.maxArmor, temp.expChart, temp.levelHp, temp.levelMp,
                temp.levelStrength, temp.levelAgility, temp.levelMind, temp.levelSoul,
                temp.levelDefense, temp.isEnemy, temp.canShield, temp.canDuelWield, temp.canSword,
                temp.canDagger, temp.canMace, temp.canBow, temp.canStaff, temp.canRod, temp.canFists,
                temp.weapon, temp.offHand, temp.armor, temp.accessory, temp.knownSkills, temp.progress,
                temp.questIndicator);
        newEnemy.currentHp = newEnemy.levelHp[newEnemy.level];
        newEnemy.enemyExp = temp.enemyExp;
        newEnemy.enemyGold = temp.enemyGold;

        bool isEquipDung = Manager.manager.getDungeonType();

        if (isEquipDung)
            newEnemy.level = aveLevel;

        return newEnemy;
    }

    private void RefreshEnemyButtons()
    {
        Color color;

        foreach(GameObject button in enemyPlace)
        {
            button.GetComponent<Button>().interactable = false;
            button.GetComponent<Image>().sprite = null;
            color = button.GetComponent<Image>().color;
            color.a = 0;
            button.GetComponent<Image>().color = color;
        }

        if(bossBattle)
        {
            placeEnemy(bossPlace, enemies[0]);
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject enemyLoc = enemyPlace[i];

                placeEnemy(enemyLoc, enemies[i]);
            }
        }        
    }

    //fill enemy into button
    private void placeEnemy(GameObject button, ScriptablePlayerClasses enemy)
    {
        Color color;

        button.GetComponent<Button>().interactable = true;
        button.GetComponent<Image>().sprite = enemy.battleSprite;
        color = button.GetComponent<Image>().color;
        color.a = 1;
        button.GetComponent<Image>().color = color;
    }

    //sets character order of battle based on agility
    private void SetBattleOrder()
    {
        foreach(ScriptablePlayerClasses character in characters)
        {
            battleOrder.Add(character);
        }

        foreach(ScriptablePlayerClasses enemy in enemies)
        {
            battleOrder.Add(enemy);
        }

        battleOrder.Sort(delegate (ScriptablePlayerClasses a, ScriptablePlayerClasses b)
        {
            return (a.GetAgility()).CompareTo(b.GetAgility());
        }
        );

        battleOrder.Reverse();
    }

    //state machine
    private void CheckState()
    {
        switch(currentState)
        {
            case BattleStates.NONBATTLE:
                gameObject.SetActive(false);
                break;
            case BattleStates.START:
                StoreFinds.stored.BattleActivate();
                BattleStats.stats.updateStats();
                turn = -1;
                enemySelected = null;
                goldGained = 0;
                expGained = 0;
                SpawnEnemies();
                SetBattleOrder();
                nextTurn();
                break;
            case BattleStates.NEXTTURN:
                checkDead();
                checkEnemiesDead();

                healSkill = null;
                healItem = null;

                attackButton.interactable = false;
                skillsButton.interactable = false;
                itemsButton.interactable = false;
                defendButton.interactable = false;
                fleeButton.interactable = false;

                try
                {
                    bool enemyTurn = currentTurn.isEnemy;

                    if(enemyTurn)
                    {
                        int lastIndex = enemies.IndexOf(currentTurn);
                        enemyPlace[lastIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    }
                    else
                    {
                        int lastIndex = characters.IndexOf(currentTurn);
                        charObjects[lastIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    }
                    
                }
                catch { }
                

                turn++;
                if (battleOrder.Count <= turn)
                    turn = 0;

                currentTurn = battleOrder[turn];

                bool isEnemy = currentTurn.isEnemy;
                int index = isEnemy == true ? enemies.IndexOf(currentTurn) : 
                    characters.IndexOf(currentTurn);

                bool poisoned = currentTurn.CheckAffliction("Poison");

                if (poisoned)
                {
                    Poisoned(index);
                }

                bool paralyzed = currentTurn.CheckAffliction("Paralyzed");

                if (paralyzed)
                {
                    nextTurn();
                }

                currentState = isEnemy ? BattleStates.ENEMYTURN : currentTurn.currentHp == 0 ? 
                    BattleStates.NEXTTURN : BattleStates.PLAYERTURN;

                CheckState();
                break;
            case BattleStates.PLAYERTURN:
                int playerIndex = characters.IndexOf(currentTurn);
                GameObject buffPanel = playerDebuffIconPanel[playerIndex];

                charObjects[playerIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(-50, 0);

                bool confused = currentTurn.CheckAffliction("Confused");

                currentTurn.BuffCounter(buffPanel);
                currentTurn.DebuffCounter(buffPanel);

                if (confused)
                    Confused();

                attackButton.interactable = true;
                skillsButton.interactable = true;
                itemsButton.interactable = true;
                defendButton.interactable = true;
                if (bossBattle == false)
                    fleeButton.interactable = true;
                else
                    fleeButton.interactable = false; 
                break;
            case BattleStates.ENEMYTURN:
                attackButton.interactable = false;
                skillsButton.interactable = false;
                itemsButton.interactable = false;
                defendButton.interactable = false;
                fleeButton.interactable = false;

                int enemyIndex = enemies.IndexOf(currentTurn);
                enemyPlace[enemyIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(50, 0);

                StartCoroutine(EnemyPause());                
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
                itemDrops();

                foreach (string questString in questStrings)
                {
                    sb.Append(questString + "\n");
                }

                outcomeText.text = sb.ToString();
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

    //actions taken by a confused character
    private void Confused()
    {
        List<ScriptablePlayerClasses> aliveChars = new List<ScriptablePlayerClasses>();
        foreach(ScriptablePlayerClasses alive in battleOrder)
        {
            if (alive.currentHp > 0)
                aliveChars.Add(alive);
        }

        int attacking = Random.Range(0, aliveChars.Count);
        ScriptablePlayerClasses attacked = aliveChars[attacking];
        SkillScriptObject attackSkill = currentTurn.GetSkill("Attack");

        int attack = currentTurn.SkillDamage(attackSkill);
        int defense = attacked.GetDefense(attackSkill);

        int realDamage = damageDone(attack, defense);

        StartCoroutine(PerformAttack(realDamage, attackSkill, attacked));

        nextTurn();
    }

    //what happens if a character is poisoned
    private void Poisoned(int index)
    {
        int poisonStr = currentTurn.poisonDamage();
        bool isEnemy = currentTurn.isEnemy;
        GameObject damagePanel;

        if (isEnemy)
            damagePanel = enemyPlace[index];
        else
            damagePanel = charObjects[index];

        GameObject showDamage = (GameObject)Instantiate(damageDisplay) as GameObject;
        showDamage.transform.SetParent(damagePanel.transform, false);
        showDamage.GetComponent<DamageMovement>().movement(-poisonStr);

        bool died = currentTurn.changeHP(-poisonStr);
        BattleStats.stats.updateStats();

        if (died)
        {
            if (isEnemy)
                poisonKill();
            else
                checkDead();            
        }
    }

    //what happens when an enemy dies from poison
    private void poisonKill()
    {
        enemyDied(currentTurn);

        checkEnemiesDead();
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

    //enemy uses skill on player
    private void enemySkill()
    {        
        int whichPlayer = selectRandomChar();
        ScriptablePlayerClasses player = characters[whichPlayer];
        Dictionary<string, SkillScriptObject> enemySkills = currentTurn.GetAllSkills();
        List<SkillScriptObject> usableSkills = new List<SkillScriptObject>();

        foreach(KeyValuePair<string, SkillScriptObject> skill in enemySkills)
        {
            string key = skill.Key;

            usableSkills.Add(enemySkills[key]);
        }

        int skillAmount = usableSkills.Count;

        int useSkill = Random.Range(0, skillAmount);
        SkillScriptObject usingSkill = usableSkills[useSkill];

        int damage = currentTurn.SkillDamage(usingSkill);
        int defense = player.GetDefense(usingSkill);

        int realDamage = damageDone(damage, defense);
        realDamage = Mathf.RoundToInt((realDamage * usingSkill.skillModifier) + usingSkill.skillBase);

        StartCoroutine(PerformAttack(realDamage, usingSkill, player));

        string affliction = usingSkill.ailment;

        if (affliction != "")
        {
            float chance = usingSkill.debuffChance;

            float range = Random.Range(0, 1);

            if (range <= chance)
            {
                int charStat = currentTurn.GetMind();

                player.AfflictStatus(usingSkill, charStat);

                GameObject image = usingSkill.buffIcon;
                playerDebuffIconPanel[whichPlayer].GetComponent<PlayerIcons>().addIcons(image);
            }
        }
    }

    //select random alive player to attack
    private int selectRandomChar()
    {
        int charAttacked = Random.Range(0, 101);

        if (charAttacked >= 40)
        {
            if (characters[0].currentHp > 0)
                return 0;
        }

        if (charAttacked >= 20)
        {
            if (characters[1].currentHp > 0)
                return 1;
        }

        if (charAttacked >= 10)
        {
            if (characters[2].currentHp > 0)
                return 2;
        }

        if (charAttacked >= 0)
        {
            if (characters[3].currentHp > 0)
                return 3;
        }

        return -1;
    }

    //use an attack on an enemy
    public void attackSkill(SkillScriptObject skillUsed)
    {
        attackButton.interactable = false;
        skillsButton.interactable = false;
        itemsButton.interactable = false;
        defendButton.interactable = false;
        fleeButton.interactable = false;

        int damage = currentTurn.SkillDamage(skillUsed);
        int defense;
        ScriptablePlayerClasses enemyTarget;

        if (enemySelected != null)
        {
            defense = enemySelected.GetDefense(skillUsed);
            enemyTarget = enemySelected;
        }            
        else
        {
            enemyTarget = selectRandomEnemy();
            defense = enemyTarget.GetDefense(skillUsed);
        }

        int realDamage = damageDone(damage, defense);
        realDamage = Mathf.RoundToInt((realDamage * skillUsed.skillModifier) + skillUsed.skillBase);

        StartCoroutine(PerformAttack(realDamage, skillUsed, enemyTarget));

        string affliction = skillUsed.ailment;

        if (affliction != "")
        {
            float chance = skillUsed.debuffChance;

            float range = Random.Range(0, 1);

            if (range <= chance)
            {
                int charStat = currentTurn.GetMind();

                enemyTarget.AfflictStatus(skillUsed, charStat);

                int enemyIndex = enemies.IndexOf(enemyTarget);

                GameObject image = skillUsed.buffIcon;
                enemyDebuffIconPanel[enemyIndex].GetComponent<PlayerIcons>().addIcons(image);
            }
        }

        skillsNItemsPanel.SetActive(false);
    }

    //player defends
    public void defendChosen(SkillScriptObject defend)
    {
        currentTurn.SetDefend();

        int playerIndex = characters.IndexOf(currentTurn);

        GameObject image = defend.buffIcon;
        playerDebuffIconPanel[playerIndex].GetComponent<PlayerIcons>().addIcons(image);

        nextTurn();
    }

    //flees battle if flee option is selected
    public void flee()
    {
        int fleeChance = Random.Range(1, 101);

        if (fleeChance >= 75)
        {
            exitBattle();
        }
        else
        {
            nextTurn();
        }
    }

    //what item is going to be used
    public void itemAction(UsableItem item)
    {
        healItem = item;
    }

    //indicates skill being used is a heal
    public void healingSkill(SkillScriptObject skill)
    {
        healSkill = skill;
    }

    //player healing
    public void healChar(int character)
    {
        if (healSkill != null)
        {
            if (healSkill.skillType.Equals("Heal"))
            {
                int healAmount = Mathf.RoundToInt((currentTurn.GetSoul() * healSkill.skillModifier) + healSkill.skillBase);

                GameObject showHeal = (GameObject)Instantiate(damageDisplay) as GameObject;
                showHeal.transform.SetParent(charObjects[character].transform, false);
                showHeal.GetComponent<DamageMovement>().movement(healAmount);

                characters[character].changeHP(healAmount);
            }
            else if (healSkill.skillType.Equals("Cure"))
            {
                string condition = healSkill.ailment;
                characters[character].CureStatus(condition);
                playerDebuffIconPanel[character].GetComponent<PlayerIcons>().removeIcons(condition);
            }
            else if (healSkill.skillType.Equals("Buff"))
            {
                characters[character].SetBuff(healSkill);

                GameObject image = healSkill.buffIcon;
                playerDebuffIconPanel[character].GetComponent<PlayerIcons>().addIcons(image);
            }
            else
            {
                ScriptablePlayerClasses playerChar = characters[character];
                int healAmount = Mathf.RoundToInt(playerChar.levelHp[playerChar.level] * healSkill.skillModifier);
                charObjects[character].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
                characters[character].changeHP(healAmount);
            }

            currentTurn.changeMP(-healSkill.manaCost);
        }
        else
        {
            if (!healItem.cureAilment.Equals(""))
            {
                characters[character].CureStatus(healItem.cureAilment);
            }
            else
            {
                int itemHeal = healItem.healAmount;

                GameObject showHeal = (GameObject)Instantiate(damageDisplay) as GameObject;
                showHeal.transform.SetParent(charObjects[character].transform, false);
                showHeal.GetComponent<DamageMovement>().movement(itemHeal);
                charObjects[character].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);

                characters[character].changeHP(itemHeal);
            }

            healItem.useItem();
        }
        BattleSkillsItems.view.cancelCharSelect();
        BattleSkillsItems.view.cancelUse();
        BattleStats.stats.updateStats();
        skillsNItemsPanel.SetActive(false);

        nextTurn();
    }

    //select a random enemy when no enemy is selected by player
    private ScriptablePlayerClasses selectRandomEnemy()
    {
        ScriptablePlayerClasses selectedEnemy = null;

        while (selectedEnemy == null)
        {
            int random = Random.Range(0, enemies.Count);
            selectedEnemy = enemies[random];
        }        
        
        return selectedEnemy;
    }

    //which enemy did the player just select
    public void selectedEnemy(int selected)
    {
        enemySelected = enemies[selected];
        removeTargets();
        enemyTargets[selected].SetActive(true);
    }

    //deselects an enemy
    public void removeEnemySelected()
    {
        enemySelected = null;
    }

    //sets all target arrows to inactive
    private void removeTargets()
    {
        foreach(GameObject target in enemyTargets)
        {
            if (target.activeInHierarchy)
                target.SetActive(false);
        }
    }

    //checks to see if all the player characters are dead
    private void checkDead()
    {
        int charsAlive = 0;

        foreach (ScriptablePlayerClasses character in characters)
        {
            if (character.currentHp > 0)
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

    //checks if enemies are all dead
    private void checkEnemiesDead()
    {
        bool enemiesAlive = false;

        foreach(ScriptablePlayerClasses enemy in enemies)
        {
            if (enemy != null)
                enemiesAlive = true;
        }

        if(enemiesAlive == false)
        {
            currentState = BattleStates.WIN;
            CheckState();
        }
    }

    //damage done
    private int damageDone(int damage, int defense)
    {
        int dam;

        bool isBlind = currentTurn.CheckAffliction("Blind");
        int blindRoll = Random.Range(0, 11);

        if (isBlind && blindRoll >= 3)
        {
            dam = 0;
        }
        else
        {
            dam = Mathf.RoundToInt((Mathf.Pow(damage, 2) / defense));

            if (dam == 0)
                dam = 1;
        }
        return dam;
    }

    //removes enemy once killed
    private void enemyDied(ScriptablePlayerClasses enemy)
    {
        int level = enemy.level;

        expGained += enemy.enemyExp[level];
        goldGained += enemy.enemyGold[level];
        
        int enemyIndex = enemies.IndexOf(enemy);
        turnOffEnemyButton(enemyIndex);

        //if killing enemy increases task progress, increase the progress and include status message at end of battle
        if (enemy.progress.quest != null)
        {
            enemy.OnKilled();

            string taskName = enemy.progress.taskName;
            Task task = enemy.progress.quest.GetTask(taskName);

            if(!task.isCompleted)
                QuestUpdates(enemy.questIndicator);
        }
            

        if (enemySelected == enemy)
        {
            enemySelected = null;
            enemyTargets[enemyIndex].SetActive(false);
        }

        battleOrder.Remove(enemy);
        enemies[enemyIndex] = null;
    }

    //turns off the enemyButton once enemy is defeated
    private void turnOffEnemyButton(int index)
    {
        GameObject button;

        if (bossBattle)
            button = bossPlace;
        else
            button = enemyPlace[index];

        Color color;

        button.GetComponent<Button>().interactable = false;
        button.GetComponent<Image>().sprite = null;
        color = button.GetComponent<Image>().color;
        color.a = 0;
        button.GetComponent<Image>().color = color;
    }

    //awards experience to alive characters
    private void awardExp()
    {
        int charsAlive = 0;

        foreach (ScriptablePlayerClasses character in characters)
        {
            if (character.currentHp > 0)
            {
                charsAlive++;
            }
        }

        foreach (ScriptablePlayerClasses character in characters)
        {
            if (character.currentHp > 0)
            {
                int currentLvl = character.level;
                bool levelCheck = character.incExp(Mathf.RoundToInt(expGained / charsAlive));

                if (levelCheck)
                {
                    int newLvl = character.level;
                    int lvlsGained = newLvl - currentLvl;

                    if (lvlsGained == 1)
                        sb.Append(string.Format("{0} Gained a Level.  {0} is now level {1}!\n", character.name, character.level));
                    else
                        sb.Append(string.Format("{0} Gained {1} Levels.  {0} is now level {2}!\n", character.name, lvlsGained, character.level));
                }
            }
        }
    }

    //rolls for items
    private void itemDrops()
    {
        int dropCheck = Random.Range(0, 101);
        int equipLimit = Mathf.RoundToInt(4 * adBonus);
        int itemLimit = Mathf.RoundToInt(10 * adBonus);

        bool equipDung = Manager.manager.getDungeonType();

        if(equipDung)
        {
            int oreLimit = Mathf.RoundToInt(10 * adBonus);

            if(dropCheck >= (100 - oreLimit))
            {
                int oreAmount = 0;
                int oreAmountCheck = Random.Range(0, 101);

                int aveLevel = Manager.manager.AveLevel();
                int minDrop = aveLevel >= 1 ? 0 : aveLevel >= 16 ? 3 : aveLevel >= 31 ? 6 :
                    aveLevel >= 46 ? 9 : 13;

                int maxDrop = aveLevel >= 1 ? 3 : aveLevel >= 16 ? 6 : aveLevel >= 31 ? 9 :
                    aveLevel >= 46 ? 12 : 15;

                int oreType = Random.Range(minDrop, maxDrop);
                string whichOre = oreDrops[oreType];

                if (oreAmountCheck >= 30)
                    oreAmount = 1;
                else if (oreAmountCheck >= 5)
                    oreAmount = 2;
                else
                    oreAmount = 3;

                Manager.manager.ChangeOre(whichOre, oreAmount);
            }
        }
        else
        {
            if (dropCheck >= (100 - equipLimit))
            {
                string rarity = RarityCheck();
                List<EquipableItem> equipment = new List<EquipableItem>();

                foreach (EquipableItem equip in equipableDrops)
                {
                    if (equip.rarity.Equals(rarity))
                        equipment.Add(equip);
                }

                try
                {
                    int choose = Random.Range(0, equipment.Count);
                    Manager.manager.addEquipableToInventory(equipment[choose]);
                    sb.Append(string.Format("{0} found!\n", equipment[choose].name));
                    return;
                }
                catch
                {

                }
            }

            if (dropCheck >= (100 - itemLimit))
            {
                string rarity = RarityCheck();
                List<UsableItem> items = new List<UsableItem>();

                foreach (UsableItem item in usableDrops)
                {
                    if (item.rarity.Equals(rarity))
                        items.Add(item);
                }

                try
                {
                    int choose = Random.Range(0, items.Count);
                    string itemName = items[choose].name;
                    Manager.manager.addUsableToInventory(itemName, 1);
                    sb.Append(string.Format("{0} found!\n", itemName));
                    return;
                }
                catch
                {

                }
            }
        }
    }

    //checks what rarity of item will drop
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

    //call for when the next turn should happen
    private void nextTurn()
    {
        currentState = BattleStates.NEXTTURN;
        CheckState();
    }

    IEnumerator EnemyPause()
    {
        yield return new WaitForSeconds(0.5f);

        int enemyIndex = enemies.IndexOf(currentTurn);
        GameObject enemyBuffPanel = enemyDebuffIconPanel[enemyIndex];        

        bool enemyConfused = currentTurn.CheckAffliction("Confused");

        currentTurn.BuffCounter(enemyBuffPanel);
        currentTurn.DebuffCounter(enemyBuffPanel);

        if (enemyConfused)
            Confused();

        enemySkill();
    }

    IEnumerator PerformAttack(int damageDealt, SkillScriptObject skillUsed, ScriptablePlayerClasses target)
    {
        GameObject skillAnim = skillUsed.skillAnimation;
        GameObject animTarget;
        float time = skillAnim.GetComponent<ParticleSystem>().main.duration;
        int attackingIndex;

        skillName.text = skillUsed.skillName;

        bool enemyTarget = target.isEnemy;
        if(enemyTarget)
        {
            attackingIndex = enemies.IndexOf(target);
            animTarget = enemyPlace[attackingIndex];
        }
        else
        {
            attackingIndex = characters.IndexOf(target);
            animTarget = charObjects[attackingIndex];
        }

        GameObject instAnim = Instantiate(skillAnim) as GameObject;

        instAnim.transform.SetParent(battleCanvas.transform, false);
        Vector2 targetLoc = animTarget.GetComponent<RectTransform>().localPosition;
        instAnim.GetComponent<RectTransform>().anchoredPosition = new Vector2(targetLoc.x, targetLoc.y);

        yield return new WaitForSeconds(time);

        GameObject showDamage = Instantiate(damageDisplay) as GameObject;
        showDamage.transform.SetParent(animTarget.transform, false);
        showDamage.GetComponent<DamageMovement>().movement(-damageDealt);
        yield return new WaitForSeconds(1);

        currentTurn.changeMP(-skillUsed.manaCost);
        bool died = target.changeHP(-damageDealt);

        if (died && target.isEnemy)
            enemyDied(target);

        Destroy(instAnim);

        BattleStats.stats.updateStats();
        skillName.text = "";
        nextTurn();
    }

    //adds string to display for quest activity
    public void QuestUpdates(string update)
    {
        if (update.Length > 1)
            questStrings.Add(update);
    }

    //exits from the battle screen
    public void exitBattle()
    {
        terrain.SetActive(true);

        foreach (ScriptablePlayerClasses charas in characters)
        {
            charas.ResetStatus();
        }

        if (currentState == BattleStates.LOSE)
        {
            enemies.Clear();
            battleOutcome.SetActive(false);

            for (int i = 0; i < 4; i++)
            {
                characters[i].changeHP(1);
            }
            currentState = BattleStates.NONBATTLE;
            ExitDungeon.exit.travel();
        }

        enemies.Clear();
        battleOrder.Clear();

        foreach (GameObject charOb in charObjects)
        {
            PlayerIcons iconPanel = charOb.GetComponentInChildren<PlayerIcons>();
            iconPanel.removeAllIcons();

            charOb.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        MiniStats.stats.updateSliders();

        if (bossBattle)
            bossBattleMusic.GetComponent<AudioSource>().Stop();
        else
            normalBattleMusic.GetComponent<AudioSource>().Stop();

        RandomMusic.music.StartMusic();

        SetCharSprite.sprites.SetCharacter();
        resultScroll.value = 1;
        enemySelected = null;
        battleOutcome.SetActive(false);
        gameObject.SetActive(false);
        StoreFinds.stored.BattleDeactivate();
        currentState = BattleStates.NONBATTLE;
    }

    //retrieves the character that has the current turn
    public ScriptablePlayerClasses getCharactersTurn()
    {
        return currentTurn;
    }
}
