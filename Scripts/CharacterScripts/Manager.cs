using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager manager;

    private Dictionary<string, PlayerClass> players = new Dictionary<string, PlayerClass>();

    private int gold;
    private int questStage;
    private string inRange;
    private GameObject chest;

    private bool adWatched = false;

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

    public List<GameObject> animations;
    private Dictionary<string, GameObject> animDict = new Dictionary<string, GameObject>();

    private Dictionary<string, Sprite> charSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> battleSprites = new Dictionary<string, Sprite>();

    private List<ItemClass> heldUsableInventory = new List<ItemClass>();
    private List<EquipmentClass> heldEquipableInventory = new List<EquipmentClass>();

    // Use this for initialization
    void Start () {
        manager = this;

        DontDestroyOnLoad(gameObject);

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

        foreach(GameObject anim in animations)
        {
            animDict.Add(anim.name, anim);
        }
    }

    public void inputPlayers(PlayerClass player1, PlayerClass player2, PlayerClass player3, PlayerClass player4)
    {
        players.Add("Player1", player1);
        players.Add("Player2", player2);
        players.Add("Player3", player3);
        players.Add("Player4", player4);
    }

    //get select character
    public PlayerClass GetPlayer(string player)
    {
        return players[player];
    }    

    public GameObject GetAnimation(string animation)
    {
        return animDict[animation];
    }

    public void setGold(int Gold) { gold = Gold; }
    public void changeGold(int Gold) { gold += Gold; }
    public int GetGold() { return gold; }

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

    //sets if player watched an ad or not
    public void setAd(bool watched)
    {
        adWatched = watched;
    }

    //gets if player watched an ad or not
    public bool getAd() { return adWatched; }

    //get average level of characters
    public int AveLevel()
    {
        int char1Level = players["Player1"].GetLevel();
        int char2Level = players["Player2"].GetLevel();
        int char3Level = players["Player3"].GetLevel();
        int char4Level = players["Player4"].GetLevel();

        return Mathf.RoundToInt((char1Level + char2Level + char3Level + char4Level) / 4);
    }

    //gets sprites of the character classes the player is playing with
    public Sprite getCharSprite(string charClass) { return charSprites[charClass]; }
    public Sprite getCharBattleSprite(string charClass) { return battleSprites[charClass]; }

    //sets what npc the player is next to
    public void setInRange(string objectName)
    {
        inRange = objectName;
    }

    //gets what npc the player is next to
    public string getInRange() { return inRange; }

    //sets what stage of the quest line the player is on, advances quest line and gets the current quest stage
    public void setQuestStage(int set) { questStage = set; }
    public void advanceQuestStage() { questStage++; }
    public int getQuestStage() { return questStage; }

    //gets inventory items
    public List<ItemClass> getUsableInventory() { return heldUsableInventory; }
    public List<EquipmentClass> getEquipableInventory() { return heldEquipableInventory; }
    public void setUsableInventory(List<ItemClass> inventory) { heldUsableInventory = inventory; }
    public void setEquipableInventory(List<EquipmentClass> inventory) { heldEquipableInventory = inventory; }
}

[Serializable]
class PlayerData
{
    //variables for all data that is to be saved, can use objects here
    public PlayerClass character1;
    public PlayerClass character2;
    public PlayerClass character3;
    public PlayerClass character4;

    public List<ItemClass> heldUsableInventory;
    public List<EquipmentClass> heldEquipableInventory;
    public List<QuestClass> quests;

    public int gold;
    public int questStage;
}
