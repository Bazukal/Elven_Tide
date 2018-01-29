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

    private Sprite char1Sprite;
    private Sprite char2Sprite;
    private Sprite char3Sprite;
    private Sprite char4Sprite;

    private int gold;

    private string inRange;

    private int questStage;

    private List<UsableItemClass> heldUsableInventory = new List<UsableItemClass>();
    private List<EquipableItemClass> heldEquipableInventory = new List<EquipableItemClass>();

    //do not destroy game object when loading scenes
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        charManager = this;
    }

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

    public void setInRange(string objectName)
    {
        inRange = objectName;
    }

    public string getInRange() { return inRange; }

    public int aveLevel()
    {
        int char1Lvl = character1.GetCharLevel();
        int char2Lvl = character2.GetCharLevel();
        int char3Lvl = character3.GetCharLevel();
        int char4Lvl = character4.GetCharLevel();

        return Mathf.RoundToInt((char1Lvl + char2Lvl + char3Lvl + char4Lvl) / 4);
    }

    public void setQuestStage(int set) { questStage = set; }
    public void advanceQuestStage() { questStage++; }
    public int getQuestStage() { return questStage; } 

    public void setSprites(string char1Class, string char2Class, string char3Class, string char4Class)
    {
        switch(char1Class)
        {
            case "Archer":
                char1Sprite = archerSprite;
                break;
            case "Black Mage":
                char1Sprite = blackSprite;
                break;
            case "Monk":
                char1Sprite = monkSprite;
                break;
            case "Paladin":
                char1Sprite = paladinSprite;
                break;
            case "Thief":
                char1Sprite = thiefSprite;
                break;
            case "Warrior":
                char1Sprite = warriorSprite;
                break;
            case "White Mage":
                char1Sprite = whiteSprite;
                break;
        }

        switch (char2Class)
        {
            case "Archer":
                char2Sprite = archerSprite;
                break;
            case "Black Mage":
                char2Sprite = blackSprite;
                break;
            case "Monk":
                char2Sprite = monkSprite;
                break;
            case "Paladin":
                char2Sprite = paladinSprite;
                break;
            case "Thief":
                char2Sprite = thiefSprite;
                break;
            case "Warrior":
                char2Sprite = warriorSprite;
                break;
            case "White Mage":
                char2Sprite = whiteSprite;
                break;
        }

        switch (char3Class)
        {
            case "Archer":
                char3Sprite = archerSprite;
                break;
            case "Black Mage":
                char3Sprite = blackSprite;
                break;
            case "Monk":
                char3Sprite = monkSprite;
                break;
            case "Paladin":
                char3Sprite = paladinSprite;
                break;
            case "Thief":
                char3Sprite = thiefSprite;
                break;
            case "Warrior":
                char3Sprite = warriorSprite;
                break;
            case "White Mage":
                char3Sprite = whiteSprite;
                break;
        }

        switch (char4Class)
        {
            case "Archer":
                char4Sprite = archerSprite;
                break;
            case "Black Mage":
                char4Sprite = blackSprite;
                break;
            case "Monk":
                char4Sprite = monkSprite;
                break;
            case "Paladin":
                char4Sprite = paladinSprite;
                break;
            case "Thief":
                char4Sprite = thiefSprite;
                break;
            case "Warrior":
                char4Sprite = warriorSprite;
                break;
            case "White Mage":
                char4Sprite = whiteSprite;
                break;
        }
    }

    public Sprite getChar1Sprite() { return char1Sprite; }
    public Sprite getChar2Sprite() { return char2Sprite; }
    public Sprite getChar3Sprite() { return char3Sprite; }
    public Sprite getChar4Sprite() { return char4Sprite; }

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