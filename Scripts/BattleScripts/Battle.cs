using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour {

    public static Battle battle;

    public List<GameObject> levelEnemies;
    public GameObject levelBoss;

    public List<GameObject> enemyPlace;

    public Button char1;
    public Button char2;
    public Button char3;
    public Button char4;

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

    private CharacterClass character1;
    private CharacterClass character2;
    private CharacterClass character3;
    private CharacterClass character4;

    private int enemyCount;

    private List<GameObject> spawnedEnemies;

    //0 = none, 1 - 4 = enemy1-4, 5 = boss
    private int enemySelected = 0;

    public int expGained = 0;
    public int goldGained = 0;

    private void Start()
    {
        if (battle == null)
            battle = this;
        else
            Destroy(this);

        character1 = CharacterManager.charManager.character1;
        character2 = CharacterManager.charManager.character2;
        character3 = CharacterManager.charManager.character3;
        character4 = CharacterManager.charManager.character4;

        Debug.Log(character1.GetCharName());

        spawnedEnemies = new List<GameObject>();
    }

    //spawns enemies into battle
    public void spawnEnemies()
    {
        char1.GetComponent<Image>().sprite = CharacterManager.charManager.getCharBattleSprite(character1.GetCharClass());
        char2.GetComponent<Image>().sprite = CharacterManager.charManager.getCharBattleSprite(character2.GetCharClass());
        char3.GetComponent<Image>().sprite = CharacterManager.charManager.getCharBattleSprite(character3.GetCharClass());
        char4.GetComponent<Image>().sprite = CharacterManager.charManager.getCharBattleSprite(character4.GetCharClass());

        updateText();
        
        int enemyAvail = levelEnemies.Count;
        enemyCount = enemyAvail;
        int enemyNumbers = Random.Range(1, 5);

        for(int i = 0; i < enemyNumbers;i++)
        {
            int whichEnemy = Random.Range(0, enemyCount);

            GameObject newEnemy = Instantiate(levelEnemies[whichEnemy], gameObject.transform.position, 
                gameObject.transform.rotation) as GameObject;
            spawnedEnemies.Add(newEnemy);

            newEnemy.transform.SetParent(enemyPlace[i].transform, false);
        }
    }

    //flees battle is flee option is selected
    public void flee()
    {        
        for(int i = 0; i < spawnedEnemies.Count; i++)
        {
            Destroy(spawnedEnemies[i]);
        }

        spawnedEnemies.Clear();
        enemyCount = 0;
        expGained = 0;
        goldGained = 0;
        enemySelected = 0;

        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        StoreFinds.stored.battleActivate();
    }

    //updates character text in battle screen
    public void updateText()
    {
        char1Name.text = character1.GetCharName();
        char1HP.text = string.Format("HP: {0}/{1}", character1.GetCharCurrentHp(), character1.GetCharMaxHp());
        char1MP.text = string.Format("MP: {0}/{1}", character1.GetCharCurrentMp(), character1.GetCharMaxMp());

        char2Name.text = character2.GetCharName();
        char2HP.text = string.Format("HP: {0}/{1}", character2.GetCharCurrentHp(), character2.GetCharMaxHp());
        char2MP.text = string.Format("MP: {0}/{1}", character2.GetCharCurrentMp(), character2.GetCharMaxMp());

        char3Name.text = character3.GetCharName();
        char3HP.text = string.Format("HP: {0}/{1}", character3.GetCharCurrentHp(), character3.GetCharMaxHp());
        char3MP.text = string.Format("MP: {0}/{1}", character3.GetCharCurrentMp(), character3.GetCharMaxMp());

        char4Name.text = character4.GetCharName();
        char4HP.text = string.Format("HP: {0}/{1}", character4.GetCharCurrentHp(), character4.GetCharMaxHp());
        char4MP.text = string.Format("MP: {0}/{1}", character4.GetCharCurrentMp(), character4.GetCharMaxMp());
    }
}
