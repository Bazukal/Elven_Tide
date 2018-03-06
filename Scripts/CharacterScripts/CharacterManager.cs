using UnityEngine;
using System;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {

    public static CharacterManager charManager;

    public CharacterClass character1;
    public CharacterClass character2;
    public CharacterClass character3;
    public CharacterClass character4;    

    public Sprite archerSprite;
    public Sprite blackSprite;
    public Sprite monkSprite;
    public Sprite paladinSprite;
    public Sprite thiefSprite;
    public Sprite warriorSprite;
    public Sprite whiteSprite;

    public Sprite archerBattleSprite;
    public Sprite blackBattleSprite;
    public Sprite monkBattleSprite;
    public Sprite paladinBattleSprite;
    public Sprite thiefBattleSprite;
    public Sprite warriorBattleSprite;
    public Sprite whiteBattleSprite;

    private Dictionary<string, Sprite> charSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> battleSprites = new Dictionary<string, Sprite>();

    private int gold;

    private string inRange;

    private int questStage;

    private GameObject chest;

    private List<UsableItemClass> heldUsableInventory = new List<UsableItemClass>();
    private List<EquipableItemClass> heldEquipableInventory = new List<EquipableItemClass>();

    //do not destroy game object when loading scenes
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        charManager = this;
    }

    private void Start()
    {
        charSprites.Add("Archer", archerSprite);
        charSprites.Add("Black Mage", blackSprite);
        charSprites.Add("Monk", monkSprite);
        charSprites.Add("Paladin", paladinSprite);
        charSprites.Add("Thief", thiefSprite);
        charSprites.Add("Warrior", warriorSprite);
        charSprites.Add("White Mage", whiteSprite);

        battleSprites.Add("Archer", archerBattleSprite);
        battleSprites.Add("Black Mage", blackBattleSprite);
        battleSprites.Add("Monk", monkBattleSprite);
        battleSprites.Add("Paladin", paladinBattleSprite);
        battleSprites.Add("Thief", thiefBattleSprite);
        battleSprites.Add("Warrior", warriorBattleSprite);
        battleSprites.Add("White Mage", whiteBattleSprite);
    }

    //sets what chest the character is next to
    public void setChest(GameObject nearChest)
    {        
        chest = nearChest;
    }

    //removes what chest when the player leaves the area of a chest, or the chest is opened
    public void removeChest()
    {
        chest = null;
    }

    //gets the specific chest that the player is next to
    public GameObject getChest() { return chest; }

    //loads characters from the new game screen, or from loading a game
    public void loadCharacters(CharacterClass char1, CharacterClass char2, CharacterClass char3, CharacterClass char4)
    {
        character1 = char1;
        character2 = char2;
        character3 = char3;
        character4 = char4;
    }

    //getters and setters
    public void setGold(int goldAmount) { gold = goldAmount; }
    public void changeGold(int changeAmount) { gold += changeAmount; }
    public int getGold() { return gold; }

    //sets what npc the player is next to
    public void setInRange(string objectName)
    {
        inRange = objectName;
    }

    //gets what npc the player is next to
    public string getInRange() { return inRange; }

    //finds the average level of the players characters
    public int aveLevel()
    {
        int char1Lvl = character1.GetCharLevel();
        int char2Lvl = character2.GetCharLevel();
        int char3Lvl = character3.GetCharLevel();
        int char4Lvl = character4.GetCharLevel();

        return Mathf.RoundToInt((char1Lvl + char2Lvl + char3Lvl + char4Lvl) / 4);
    }

    //sets what stage of the quest line the player is on, advances quest line and gets the current quest stage
    public void setQuestStage(int set) { questStage = set; }
    public void advanceQuestStage() { questStage++; }
    public int getQuestStage() { return questStage; } 

    //gets sprites of the character classes the player is playing with
    public Sprite getCharSprite(string charClass) { return charSprites[charClass]; }
    public Sprite getCharBattleSprite(string charClass) { return battleSprites[charClass]; }

    //gets inventory items
    public List<UsableItemClass> getUsableInventory() { return heldUsableInventory; }
    public List<EquipableItemClass> getEquipableInventory() { return heldEquipableInventory; }
    public void setUsableInventory(List<UsableItemClass> inventory) { heldUsableInventory = inventory; }
    public void setEquipableInventory(List<EquipableItemClass> inventory) { heldEquipableInventory = inventory; }
        
}

[Serializable]
class PlayerData
{
    //variables for all data that is to be saved, can use objects here
    public CharacterClass character1;
    public CharacterClass character2;
    public CharacterClass character3;
    public CharacterClass character4;

    public List<UsableItemClass> heldUsableInventory;
    public List<EquipableItemClass> heldEquipableInventory;
    public List<QuestClass> quests;

    public int gold;
    public int questStage;
}