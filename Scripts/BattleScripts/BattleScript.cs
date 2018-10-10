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
    
    public List<GameObject> enemyPlace;
    public List<GameObject> enemyTargets;
    public GameObject bossPlace;

    public GameObject terrain;

    public GameObject normalBattleMusic;
    public GameObject bossBattleMusic;

    protected int expGained = 0;
    protected int goldGained = 0;
    protected float adBonus = 0f;

    protected List<ScriptablePlayer> characters = new List<ScriptablePlayer>();
    protected List<ScriptableBaseChar> battleOrder = new List<ScriptableBaseChar>();

    protected int turn = 0;
    protected ScriptableBaseChar currentTurn;
    protected ScriptableBaseChar enemySelected;

    public List<GameObject> charObjects = new List<GameObject>();

    public List<GameObject> playerDebuffIconPanel = new List<GameObject>();
    public List<GameObject> enemyDebuffIconPanel = new List<GameObject>();
    public GameObject bossDebuffIconPanel;

    protected string lost = "You lost the battle.  Your body will be sent back to town for healing.";
    protected StringBuilder sb;

    public Button attackButton;
    public Button skillsButton;
    public Button itemsButton;
    public Button defendButton;
    public Button fleeButton;

    protected UsableItem healItem;

    protected SkillScriptObject healSkill;

    public GameObject battleOutcome;
    public Text outcomeText;

    public Text skillName;

    public GameObject skillsNItemsPanel;

    public GameObject defendAnim;
    public GameObject damageDisplay;

    protected bool bossBattle = false;

    public Scrollbar resultScroll;

    protected List<string> questStrings = new List<string>();

    public Canvas battleCanvas;

    public GameObject lifePanel;
    public Slider enemyLife;
    public Text enemyName;

    private float targetLifeValue;

    private void Update()
    {
        if(lifePanel.activeInHierarchy)
        {
            if(!Mathf.Approximately(enemyLife.value, targetLifeValue))
            {
                enemyLife.value = Mathf.MoveTowards(enemyLife.value, targetLifeValue, Time.deltaTime);
            }

            if (Mathf.Approximately(enemyLife.value, 0))
                removeLifeBar();
        }
    }

    protected void startSetUp()
    {
        battleOn = this;

        Manager.manager.SetScene("Dungeon");

        characters.Add(Manager.manager.GetPlayer("Player1"));
        characters.Add(Manager.manager.GetPlayer("Player2"));
        characters.Add(Manager.manager.GetPlayer("Player3"));
        characters.Add(Manager.manager.GetPlayer("Player4"));

        healItem = ScriptableObject.CreateInstance(typeof(UsableItem)) as UsableItem;

        healSkill = ScriptableObject.CreateInstance(typeof(SkillScriptObject)) as SkillScriptObject;

        foreach (ScriptablePlayer character in characters)
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

    protected void RefreshEnemyButtons()
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
    }

    //gets character Agility
    protected int getAgility(ScriptableBaseChar thisChar)
    {
        int thisAgility = 0;

        if (thisChar is ScriptablePlayer)
        {
            ScriptablePlayer newA = thisChar as ScriptablePlayer;
            thisAgility = newA.GetAgility();
        }
        else if (thisChar is LevelEnemies)
        {
            LevelEnemies newA = thisChar as LevelEnemies;
            thisAgility = newA.GetAgility();
        }
        else if(thisChar is SetEnemy)
        {
            SetEnemy newA = thisChar as SetEnemy;
            thisAgility = newA.GetAgility();
        }

        return thisAgility;
    }

    //what happens if a character is poisoned
    protected bool Poisoned(int index)
    {
        int poisonStr = currentTurn.poisonDamage();
        GameObject damagePanel;
        bool died = false;

        if(currentTurn is ScriptablePlayer)
        {
            damagePanel = charObjects[index];
            ScriptablePlayer player = currentTurn as ScriptablePlayer;
            died = player.changeHP(-poisonStr);
        }
        else if(currentTurn is LevelEnemies)
        {
            damagePanel = enemyPlace[index];
            LevelEnemies levEnemy = currentTurn as LevelEnemies;
            died = levEnemy.changeHP(-poisonStr);
        }
        else
        {
            damagePanel = enemyPlace[index];
            SetEnemy setEnemy = currentTurn as SetEnemy;
            died = setEnemy.changeHP(-poisonStr);
        }

        GameObject showDamage = (GameObject)Instantiate(damageDisplay) as GameObject;
        showDamage.transform.SetParent(damagePanel.transform, false);
        showDamage.GetComponent<DamageMovement>().movement(-poisonStr);
        
        BattleStats.stats.updateStats();
        
        return died;
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

    

    //select random alive player to attack
    protected int selectRandomChar()
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

    //deselects an enemy
    public void removeEnemySelected()
    {
        removeLifeBar();
        enemySelected = null;
    }

    //sets all target arrows to inactive
    protected void removeTargets()
    {
        foreach(GameObject target in enemyTargets)
        {
            if (target.activeInHierarchy)
                target.SetActive(false);
        }
    }

    //damage done
    protected int damageDone(int damage, int defense)
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

    

    //turns off the enemyButton once enemy is defeated
    protected void turnOffEnemyButton(int index)
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
    protected void awardExp()
    {
        int charsAlive = 0;

        foreach (ScriptablePlayer character in characters)
        {
            if (character.currentHp > 0)
            {
                charsAlive++;
            }
        }

        foreach (ScriptablePlayer character in characters)
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

                    List<SkillScriptObject> skills = new List<SkillScriptObject>();

                    foreach(SkillScriptObject skill in character.knownSkills)
                    {
                        int skillLevel = skill.skillLevel;

                        if (skillLevel >= currentLvl && skillLevel <= newLvl)
                            skills.Add(skill);
                    }

                    foreach(SkillScriptObject skill in skills)
                    {
                        sb.Append(string.Format("{0} learned a new skill: {1}", character.name, skill.name));
                    }
                }
            }
        }
    }

    

    //checks what rarity of item will drop
    protected string RarityCheck()
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

    //adds string to display for quest activity
    public void QuestUpdates(string update)
    {
        if (update.Length > 1)
            questStrings.Add(update);
    }

    //retrieves the character that has the current turn
    public ScriptablePlayer getCharactersTurn()
    {
        return currentTurn as ScriptablePlayer;
    }

    //sets life bar
    protected void removeLifeBar()
    {
        lifePanel.SetActive(false);
    }

    protected void hardSetLifeBar(float value, string targetName)
    {
        if (!lifePanel.activeInHierarchy)
            lifePanel.SetActive(true);
        
        enemyLife.value = value;
        targetLifeValue = value;

        enemyName.text = targetName;
    }

    protected void animateLifeBar(float value)
    {
        targetLifeValue = value;
    }
}
