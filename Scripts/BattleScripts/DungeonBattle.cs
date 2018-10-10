using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Devdog.QuestSystemPro;
using UnityEngine.UI;

public class DungeonBattle : BattleScript
{
    public static DungeonBattle battle;

    public List<SetEnemy> availEnemies;
    public SetEnemy availBoss;

    public EquipDrops equipableDrops;
    public ItemDrops usableDrops;

    protected List<SetEnemy> battleEnemies = new List<SetEnemy>();
    protected List<SetEnemy> enemyOrder = new List<SetEnemy>();


    ScriptablePlayer current = null;
    SetEnemy enemyTurn = null;

    private bool battleOver = false;

    // Use this for initialization
    void Start () {
        battle = this;
        startSetUp();
        gameObject.SetActive(false);
	}

    //state machine
    private void CheckState()
    {
        switch (currentState)
        {
            case BattleStates.NONBATTLE:
                gameObject.SetActive(false);
                break;
            case BattleStates.START:
                battleOver = false;
                StoreFinds.stored.BattleActivate();
                BattleStats.stats.updateStats();
                enemySelected = null;
                goldGained = 0;
                expGained = 0;
                SpawnEnemies();
                SetBattleOrder();
                turn = -1;
                nextTurn();
                break;
            case BattleStates.NEXTTURN:
                checkDead();
                checkEnemiesDead();
                SetBattleOrder();

                healSkill = null;
                healItem = null;

                attackButton.interactable = false;
                skillsButton.interactable = false;
                itemsButton.interactable = false;
                defendButton.interactable = false;
                fleeButton.interactable = false;

                try
                {
                    if (currentTurn is ScriptablePlayer)
                    {
                        int lastIndex = characters.IndexOf(current);
                        charObjects[lastIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    }
                    else
                    {
                        int lastIndex = battleEnemies.IndexOf(enemyTurn);
                        enemyPlace[lastIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    }

                }
                catch { }

                turn++;

                if (battleOrder.Count <= turn)
                    turn = 0;

                currentTurn = battleOrder[turn];


                setTurn();

                int index = 0;

                if (currentTurn is ScriptablePlayer)
                    index = characters.IndexOf(current);
                else
                {
                    if (currentTurn == null)
                        nextTurn();
                    else
                        index = battleEnemies.IndexOf(enemyTurn);
                }
                    
                bool poisoned = currentTurn.CheckAffliction("Poison");

                if (poisoned)
                {
                    bool ifPoisoned = Poisoned(index);

                    if (ifPoisoned)
                    {
                        if (currentTurn is ScriptablePlayer)
                            checkDead();
                        else if(currentTurn is SetEnemy)
                        {
                            SetEnemy enemy = currentTurn as SetEnemy;
                            enemyDied(enemy);
                        }                                      
                    }
                }

                bool paralyzed = currentTurn.CheckAffliction("Paralyzed");

                if (paralyzed)
                {
                    nextTurn();
                }

                if (currentTurn is ScriptablePlayer)
                    currentState = currentTurn.currentHp == 0 ? BattleStates.NEXTTURN : BattleStates.PLAYERTURN;
                else
                    currentState = BattleStates.ENEMYTURN;

                CheckState();
                break;
            case BattleStates.PLAYERTURN:
                ScriptablePlayer currentPlayer = currentTurn as ScriptablePlayer;
                int playerIndex = characters.IndexOf(currentPlayer);
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

                removeLifeBar();
                int enemyIndex = battleEnemies.IndexOf(enemyTurn);

                if (enemyIndex == -1)
                    nextTurn();
                
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

                foreach (ScriptablePlayer character in characters)
                {
                    Dictionary<string, SkillScriptObject> passives = character.GetAllPassiveSkills();

                    foreach (KeyValuePair<string, SkillScriptObject> passive in passives)
                    {
                        string key = passive.Key;

                        if (passives[key].passiveType.Equals("Extra Gold"))
                        {
                            float goldChance = Random.Range(0.0f, 1.0f);

                            if (goldChance >= passives[key].passiveChance)
                                totalGold = Mathf.RoundToInt(totalGold * (passives[key].skillModifier));
                        }
                    }
                }

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

    //sets whos turn it is
    private void setTurn()
    {
        if (currentTurn is ScriptablePlayer)
        {
            current = currentTurn as ScriptablePlayer;
        }
        else
        {
            enemyTurn = currentTurn as SetEnemy;
        }
    }

    //starts battle
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

        foreach (ScriptablePlayer player in characters)
        {
            int index = characters.IndexOf(player);

            if (player.currentHp == 0)
                charObjects[index].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 90);
            else
                charObjects[index].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        }

        currentState = BattleStates.START;
        CheckState();
    }



    //call for when the next turn should happen
    protected void nextTurn()
    {
        currentState = BattleStates.NEXTTURN;
        CheckState();
    }

    //spawns enemies available for the level
    private void SpawnEnemies()
    {
        battleEnemies.Clear();

        if (bossBattle == false)
        {
            int enemyAvail = availEnemies.Count;
            int numRoll = Random.Range(0, 101);
            int enemyNumbers = numRoll > 65 ? 1 : numRoll > 35 ? 2 : numRoll > 15 ? 3 : 4;

            for (int i = 0; i < enemyNumbers; i++)
            {
                int whichEnemy = Random.Range(0, enemyAvail);
                SetEnemy temp = availEnemies[whichEnemy];
                SetEnemy newEnemy = CreateEnemy(temp);

                battleEnemies.Add(newEnemy);
            }
        }
        else
        {
            SetEnemy temp = availBoss;
            SetEnemy newEnemy = CreateEnemy(temp);

            battleEnemies.Add(newEnemy);
        }

        RefreshButtons();
    }

    //instantiates enemies assigned for the battle
    protected SetEnemy CreateEnemy(SetEnemy temp)
    {
        SetEnemy setTemp = temp as SetEnemy;

        SetEnemy newSetEnemy = ScriptableObject.CreateInstance(typeof(SetEnemy)) as SetEnemy;

        newSetEnemy.SetInit(setTemp.name, setTemp.battleSprite, setTemp.level, setTemp.knownSkills,
            setTemp.maxHp, setTemp.maxMp, setTemp.strength, setTemp.agility, setTemp.mind,
            setTemp.soul, setTemp.defense, setTemp.weapon, setTemp.offHand, setTemp.armor,
            setTemp.accessory, setTemp.progress, setTemp.questIndicator);

        newSetEnemy.gold = setTemp.gold;
        newSetEnemy.exp = setTemp.exp;

        newSetEnemy.currentHp = newSetEnemy.maxHp;

        return newSetEnemy;
    }

    //resets enemy buttons for battle
    protected void RefreshButtons()
    {
        RefreshEnemyButtons();

        if (bossBattle)
        {
            placeEnemy(bossPlace, battleEnemies[0]);
        }
        else
        {
            for (int i = 0; i < battleEnemies.Count; i++)
            {
                placeEnemy(enemyPlace[i], battleEnemies[i]);
            }
        }
    }

    //fill enemy into button
    protected void placeEnemy(GameObject button, SetEnemy enemy)
    {
        Color color;

        button.GetComponent<Button>().interactable = true;
        button.GetComponent<Image>().sprite = enemy.battleSprite;
        color = button.GetComponent<Image>().color;
        color.a = 1;
        button.GetComponent<Image>().color = color;
    }    

    //clears enemy button
    protected void clearButton(GameObject button)
    {
        Color color;

        button.GetComponent<Button>().interactable = true;
        color = button.GetComponent<Image>().color;
        color.a = 0;
        button.GetComponent<Image>().color = color;
    }

    //sets character order of battle based on agility
    private void SetBattleOrder()
    {
        battleOrder = new List<ScriptableBaseChar>();

        foreach (ScriptablePlayer character in characters)
        {
            battleOrder.Add(character);
        }

        foreach (SetEnemy enemy in battleEnemies)
        {
            battleOrder.Add(enemy);
        }

        battleOrder.Sort(delegate (ScriptableBaseChar a, ScriptableBaseChar b)
        {
            int agility1 = getAgility(a);
            int agility2 = getAgility(b);

            return (agility1).CompareTo(agility2);
        }
        );

        battleOrder.Reverse();
    }

    //checks to see if all the player characters are dead
    protected void checkDead()
    {
        int charsAlive = 0;

        foreach (ScriptablePlayer character in characters)
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
    protected void checkEnemiesDead()
    {
        bool enemiesAlive = false;

        foreach (SetEnemy enemy in battleEnemies)
        {
            if (enemy != null)
                enemiesAlive = true;
        }

        Debug.Log("Is any enemy Alive? " + enemiesAlive);

        if (enemiesAlive == false)
        {
            battleOver = true;
        }
        else
            battleOver = false;
    }

    //enemy uses skill on player
    protected void enemySkill()
    {
        int whichPlayer = selectRandomChar();
        ScriptablePlayer player = characters[whichPlayer];
        Dictionary<string, SkillScriptObject> enemySkills = currentTurn.GetAllActiveSkills();
        List<SkillScriptObject> usableSkills = new List<SkillScriptObject>();

        foreach (KeyValuePair<string, SkillScriptObject> skill in enemySkills)
        {
            string key = skill.Key;

            usableSkills.Add(enemySkills[key]);
        }

        int skillAmount = usableSkills.Count;

        int useSkill = Random.Range(0, skillAmount);
        SkillScriptObject usingSkill = usableSkills[useSkill];

        int damage = enemyTurn.SkillDamage(usingSkill);
        int defense = player.GetDefense(usingSkill);

        int realDamage = damageDone(damage, defense);
        realDamage = Mathf.RoundToInt((realDamage * usingSkill.skillModifier) + usingSkill.skillBase);

        StartCoroutine(PerformAttack(realDamage, usingSkill, player, false));

        string affliction = usingSkill.ailment;

        if (affliction != "")
        {
            float chance = usingSkill.debuffChance;

            float range = Random.Range(0, 1);

            if (range <= chance)
            {
                int charStat = enemyTurn.GetMind();

                player.AfflictStatus(usingSkill, charStat);

                GameObject image = usingSkill.buffIcon;
                playerDebuffIconPanel[whichPlayer].GetComponent<PlayerIcons>().addIcons(image);
            }
        }
    }

    //instantiates attack animations at target
    private List<GameObject> attackAnimations(List<ScriptableBaseChar> targets, GameObject animation)
    {
        List<GameObject> animations = new List<GameObject>();        

        foreach (ScriptableBaseChar target in targets)
        {
            int index;
            GameObject instAnim = Instantiate(animation) as GameObject;
            animations.Add(instAnim);

            if (target is SetEnemy)
            {
                index = battleEnemies.IndexOf(target as SetEnemy);
                instAnim.transform.SetParent(enemyPlace[index].transform, false);
            }
            else
            {
                index = characters.IndexOf(target as ScriptablePlayer);
                instAnim.transform.SetParent(charObjects[index].transform, false);
            }     
        }
        return animations;
    }

    //adds a pause to the enemy attack before actions are taken place
    IEnumerator EnemyPause()
    {
        yield return new WaitForSeconds(0.5f);

        int enemyIndex = battleEnemies.IndexOf(enemyTurn);
        GameObject enemyBuffPanel = enemyDebuffIconPanel[enemyIndex];

        bool enemyConfused = currentTurn.CheckAffliction("Confused");

        currentTurn.BuffCounter(enemyBuffPanel);
        currentTurn.DebuffCounter(enemyBuffPanel);

        if (enemyConfused)
            Confused();

        enemySkill();
    }

    //performs attack
    IEnumerator PerformAttack(int damageDealt, SkillScriptObject skillUsed, ScriptableBaseChar target, bool counter)
    {
        GameObject skillAnim = skillUsed.skillAnimation;
        float time = skillAnim.GetComponent<ParticleSystem>().main.duration;
        List<GameObject> animations = new List<GameObject>();
        ScriptablePlayer pTarget = null;
        SetEnemy eTarget = null;
        bool isEnemy;
        List<ScriptableBaseChar> targets = new List<ScriptableBaseChar>();

        if (target is ScriptablePlayer)
        {
            pTarget = target as ScriptablePlayer;
            isEnemy = false;
        }
        else
        {
            eTarget = target as SetEnemy;
            isEnemy = true;
        }

        skillName.text = skillUsed.skillName;

        if (!isEnemy && skillUsed.isAOE)
        {
            foreach (ScriptablePlayer chara in characters)
                targets.Add(chara);
        }
        else if (isEnemy && skillUsed.isAOE)
        {
            foreach (ScriptableBaseChar enemy in battleOrder)
            {
                if(enemy is SetEnemy)
                    targets.Add(enemy);
            }                
        }
        else if (!isEnemy && !skillUsed.isAOE)
            targets.Add(pTarget);
        else if (isEnemy && !skillUsed.isAOE)
            targets.Add(eTarget);

        animations = attackAnimations(targets, skillAnim);

        StartCoroutine(DamageDealt(targets, damageDealt, counter, skillUsed, time));

        if (currentTurn is ScriptablePlayer)
        {
            ScriptablePlayer player = currentTurn as ScriptablePlayer;
            player.changeMP(-skillUsed.manaCost);
        }

        foreach (GameObject anim in animations)
        {
            Destroy(anim, time);
        }

        BattleStats.stats.updateStats();
        skillName.text = "";

        if (battleOver)
        {
            currentState = BattleStates.WIN;
            CheckState();
        }
        else
            nextTurn();

        yield return new WaitForEndOfFrame();
    }

    //deals damage to character
    IEnumerator DamageDealt(List<ScriptableBaseChar> targets, int damageDealt, bool counter, SkillScriptObject skillUsed, float time)
    {
        List<GameObject> damNums = new List<GameObject>();
        List<int> enemyIndex = new List<int>();

        foreach (ScriptableBaseChar attackedTarget in targets)
        {
            if (attackedTarget is SetEnemy)
            {
                SetEnemy enemy = attackedTarget as SetEnemy;
                enemyIndex.Add(battleEnemies.IndexOf(enemy));
            }

            checkIfDied(attackedTarget, damageDealt, counter, skillUsed);
        }

        yield return new WaitForSeconds(time);

        damNums = attackDamageNum(targets, damageDealt);

        foreach (GameObject num in damNums)
        {
            Destroy(num, 1);
        }
    }

    //instantiate damage numbers at target
    protected List<GameObject> attackDamageNum(List<ScriptableBaseChar> targets, int damageDealt)
    {
        List<GameObject> damages = new List<GameObject>();

        foreach (ScriptableBaseChar target in targets)
        {
            try
            {
                int index;
                GameObject showDamage = Instantiate(damageDisplay) as GameObject;
                damages.Add(showDamage);
                GameObject targetObj;

                if (target is SetEnemy)
                {
                    index = battleEnemies.IndexOf(target as SetEnemy);
                    targetObj = enemyPlace[index];
                }
                else
                {
                    index = characters.IndexOf(target as ScriptablePlayer);
                    targetObj = charObjects[index];
                }
                showDamage.transform.SetParent(targetObj.transform, false);
                showDamage.GetComponent<DamageMovement>().movement(-damageDealt);
            }
            catch { }
        }
        return damages;
    }

    //actions taken by a confused character
    protected void Confused()
    {
        List<ScriptableBaseChar> aliveChars = new List<ScriptableBaseChar>();
        foreach (ScriptableBaseChar alive in battleOrder)
        {
            if (alive.currentHp > 0)
                aliveChars.Add(alive);
        }

        int attacking = Random.Range(0, aliveChars.Count);
        ScriptableBaseChar attacked = aliveChars[attacking];
        SkillScriptObject attackSkill = currentTurn.GetSkill("Attack");

        int attack = 0;
        int defense = 0;

        ScriptablePlayer player;
        SetEnemy enemy;

        if (currentTurn is ScriptablePlayer)
        {
            player = currentTurn as ScriptablePlayer;
            attack = player.SkillDamage(attackSkill);
        }
        else
        {
            enemy = currentTurn as SetEnemy;
            attack = enemy.SkillDamage(attackSkill);
        }

        if (attacked is ScriptablePlayer)
        {
            player = attacked as ScriptablePlayer;
            defense = player.GetDefense(attackSkill);
        }
        else
        {
            enemy = attacked as SetEnemy;
            defense = enemy.GetDefense(attackSkill);
        }

        int realDamage = damageDone(attack, defense);

        StartCoroutine(PerformAttack(realDamage, attackSkill, attacked, false));

        nextTurn();
    }

    //removes enemy once killed
    protected void enemyDied(SetEnemy enemy)
    {
        expGained += enemy.exp;
        goldGained += enemy.gold;

        int enemyIndex = battleEnemies.IndexOf(enemy);
        clearButton(enemyPlace[enemyIndex]);

        PlayerIcons iconPanel = enemyDebuffIconPanel[enemyIndex].GetComponentInChildren<PlayerIcons>();
        iconPanel.removeAllIcons();

        //if killing enemy increases task progress, increase the progress and include status message at end of battle
        if (enemy.progress.quest != null)
        {
            enemy.OnKilled();

            string taskName = enemy.progress.taskName;
            Task task = enemy.progress.quest.GetTask(taskName);

            if (!task.isCompleted)
                QuestUpdates(enemy.questIndicator);
        }

        if (enemySelected == enemy)
        {
            enemySelected = null;
            enemyTargets[enemyIndex].SetActive(false);            
        }

        battleOrder.Remove(enemy);
        battleEnemies[enemyIndex] = null;        

        checkEnemiesDead();
    }

    //exits from the battle screen
    public void exitBattle()
    {
        terrain.SetActive(true);

        foreach (ScriptablePlayer charas in characters)
        {
            charas.ResetStatus();
        }

        foreach (GameObject debuffPanel in enemyDebuffIconPanel)
        {
            PlayerIcons iconPanel = debuffPanel.GetComponentInChildren<PlayerIcons>();
            iconPanel.removeAllIcons();
        }

        if (currentState == BattleStates.LOSE)
        {
            battleEnemies.Clear();
            battleOutcome.SetActive(false);

            for (int i = 0; i < 4; i++)
            {
                characters[i].changeHP(1);
            }
            currentState = BattleStates.NONBATTLE;
            ExitDungeon.exit.travel();
        }

        battleEnemies.Clear();
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
        removeLifeBar();
        StoreFinds.stored.BattleDeactivate();
        currentState = BattleStates.NONBATTLE;
    }

    //use an attack on an enemy
    public void attackSkill(SkillScriptObject skillUsed)
    {
        attackButton.interactable = false;
        skillsButton.interactable = false;
        itemsButton.interactable = false;
        defendButton.interactable = false;
        fleeButton.interactable = false;

        ScriptablePlayer player = currentTurn as ScriptablePlayer;
        SetEnemy enemy = enemySelected as SetEnemy;

        int damage = player.SkillDamage(skillUsed);
        int defense;

        if (enemySelected != null)
        {
            defense = enemy.GetDefense(skillUsed);
        }
        else
        {
            enemy = selectRandomEnemy();
            defense = enemy.GetDefense(skillUsed);
        }

        int realDamage = damageDone(damage, defense);
        realDamage = Mathf.RoundToInt((realDamage * skillUsed.skillModifier) + skillUsed.skillBase);

        string affliction = skillUsed.ailment;

        if (affliction != "")
        {
            float chance = skillUsed.debuffChance;

            float range = Random.Range(0, 1);

            if (range <= chance)
            {
                int charStat = player.GetMind();

                enemy.AfflictStatus(skillUsed, charStat);

                GameObject image = skillUsed.buffIcon;

                if (bossBattle)
                    bossDebuffIconPanel.GetComponent<PlayerIcons>().addIcons(image);
                else
                {
                    int enemyIndex = battleEnemies.IndexOf(enemy);

                    enemyDebuffIconPanel[enemyIndex].GetComponent<PlayerIcons>().addIcons(image);
                }
            }
        }
        StartCoroutine(PerformAttack(realDamage, skillUsed, enemy, false));

        skillsNItemsPanel.SetActive(false);
    }

    //select a random enemy when no enemy is selected by player
    private SetEnemy selectRandomEnemy()
    {
        SetEnemy selectedEnemy = null;

        while (selectedEnemy == null)
        {
            int random = Random.Range(0, battleEnemies.Count);
            selectedEnemy = battleEnemies[random];
        }

        return selectedEnemy;
    }

    //player defends
    public void defendChosen(SkillScriptObject defend)
    {
        ScriptablePlayer player = currentTurn as ScriptablePlayer;

        player.SetDefend();

        int playerIndex = characters.IndexOf(player);

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

    //player healing
    public void healChar(int character)
    {
        ScriptablePlayer player = currentTurn as ScriptablePlayer;
        List<ScriptableBaseChar> targets = new List<ScriptableBaseChar>();

        if (healSkill != null)
        {
            bool isAOE = healSkill.isAOE;

            if (isAOE)
            {
                foreach (ScriptablePlayer eachPlayer in characters)
                {
                    targets.Add(eachPlayer as ScriptableBaseChar);
                }
            }
            else
                targets.Add(characters[character]);

            if (healSkill.skillType.Equals("Heal"))
            {
                int healAmount = Mathf.RoundToInt((player.GetSoul() * healSkill.skillModifier) + healSkill.skillBase);                

                for(int i = 0; i < targets.Count; i++)
                {
                    GameObject showHeal = (GameObject)Instantiate(damageDisplay) as GameObject;
                    showHeal.transform.SetParent(charObjects[i].transform, false);
                    showHeal.GetComponent<DamageMovement>().movement(healAmount);

                    characters[i].changeHP(healAmount);
                }
            }
            else if (healSkill.skillType.Equals("Cure"))
            {
                string condition = healSkill.ailment;

                for (int i = 0; i < targets.Count; i++)
                {
                    characters[i].CureStatus(condition);
                    playerDebuffIconPanel[i].GetComponent<PlayerIcons>().removeIcons(condition);
                }                    
            }
            else if (healSkill.skillType.Equals("Buff"))
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    characters[i].SetBuff(healSkill);

                    GameObject image = healSkill.buffIcon;
                    playerDebuffIconPanel[i].GetComponent<PlayerIcons>().addIcons(image);
                }
            }
            else
            {
                ScriptablePlayer playerChar = characters[character];
                int healAmount = Mathf.RoundToInt(playerChar.levelHp[playerChar.level] * healSkill.skillModifier);
                charObjects[character].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
                characters[character].changeHP(healAmount);
            }

            attackAnimations(targets, healSkill.skillAnimation);

            player.changeMP(-healSkill.manaCost);
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

    //which enemy did the player just select
    public void selectedEnemy(int selected)
    {
        enemySelected = battleEnemies[selected];
        SetEnemy temp = enemySelected as SetEnemy;
        float barValue = (float)temp.currentHp / (float)temp.maxHp;
        hardSetLifeBar(barValue, temp.name);
        removeTargets();
    }

    //deals damage to a target and checks if it died
    protected void checkIfDied(ScriptableBaseChar target, int damage, bool _canCounter, SkillScriptObject skillUsed)
    {
        bool died = false;

        if (target is SetEnemy)
        {
            SetEnemy enemy = target as SetEnemy;
            float barValue = (float)enemy.currentHp / (float)enemy.maxHp;
            hardSetLifeBar(barValue, enemy.name);
            died = enemy.changeHP(-damage);

            float animValue = (float)enemy.currentHp / (float)enemy.maxHp;
            animateLifeBar(animValue);

            if (died)
                enemyDied(enemy);
        }
        else if (target is ScriptablePlayer)
        {
            ScriptablePlayer player = target as ScriptablePlayer;
            died = player.changeHP(-damage);

            if(died)
            {
                int index = characters.IndexOf(player);
                charObjects[index].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 90);
                
                checkDead();
            }            
        }

        if (!died)
        {
            if (_canCounter == true)
            {
                Dictionary<string, SkillScriptObject> counterCheck = target.GetAllPassiveSkills();

                foreach (KeyValuePair<string, SkillScriptObject> skill in counterCheck)
                {
                    string key = skill.Key;

                    if (counterCheck[key].passiveType.Equals("Counter"))
                    {
                        if (skillUsed.damageType == counterCheck[key].damageType)
                        {
                            canCounter(target, counterCheck[key].passiveChance);
                            break;
                        }
                    }
                }
            }
        }
    }

    //if target can counter attack
    private void canCounter(ScriptableBaseChar target, float chance)
    {
        float chanceCheck = Random.Range(0.0f, 1.0f);

        ScriptablePlayer player;
        SetEnemy enemy;

        if (chanceCheck >= chance)
        {
            SkillScriptObject attack = target.GetSkill("Attack");
            int attackDam;
            int defense;

            if (target is ScriptablePlayer)
            {
                player = target as ScriptablePlayer;
                attackDam = player.SkillDamage(attack);
            }
            else
            {
                enemy = target as SetEnemy;
                attackDam = enemy.SkillDamage(attack);
            }

            if (currentTurn is ScriptablePlayer)
            {
                player = currentTurn as ScriptablePlayer;
                defense = player.GetDefense(attack);
            }
            else
            {
                enemy = target as SetEnemy;
                defense = enemy.GetDefense(attack);
            }

            int realDamage = damageDone(attackDam, defense);

            StartCoroutine(PerformAttack(realDamage, attack, currentTurn, true));
        }
        else
            nextTurn();
    }

    //rolls for items
    protected void itemDrops()
    {
        int dropCheck = Random.Range(0, 101);
        int equipLimit;
        int itemLimit; 
        
        if(bossBattle)
        {
            equipLimit = 65;
            itemLimit = 100;
        }
        else
        {
            equipLimit = Mathf.RoundToInt(4 * adBonus);
            itemLimit = Mathf.RoundToInt(10 * adBonus);
        }

        if (dropCheck >= (100 - equipLimit))
        {
            EquipableItem droppedEquip = equipableDrops.getEquip();
            Manager.manager.addEquipableToInventory(droppedEquip);
            sb.Append(string.Format("{0} found!\n", droppedEquip.name));
        }
        else if (dropCheck >= (100 - itemLimit))
        {
            UsableItem droppedItem = usableDrops.getItem();
            string itemName = droppedItem.name;
            Manager.manager.addUsableToInventory(itemName, 1);
            sb.Append(string.Format("{0} found!\n", itemName));
        }
    }
}
