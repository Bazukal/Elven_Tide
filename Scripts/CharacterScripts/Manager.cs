using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.QuestSystemPro;
using Devdog.General;
using System.Linq;

public class Manager : MonoBehaviour {

    public static Manager manager;

    private Dictionary<string, ScriptablePlayer> players = new Dictionary<string, ScriptablePlayer>();
    private Dictionary<string, int> ores = new Dictionary<string, int>();

    public List<UsableItem> gameUsables = new List<UsableItem>();
    public List<EquipableItem> gameEquipables = new List<EquipableItem>();

    private Dictionary<string, UsableItem> usableDictionary = new Dictionary<string, UsableItem>();
    private Dictionary<string, EquipableItem> equipableDictionary = new Dictionary<string, EquipableItem>();

    private int gold;
    private int questStage;
    private int townStage;
    private string sceneType;
    private string inRange;
    private GameObject itemRange;
    private GameObject chest;

    private bool adWatched = false;

    private bool isEquipDung = false;

    private Dictionary<string, GameObject> animDict = new Dictionary<string, GameObject>();

    private Dictionary<string, UsableItem> heldUsableInventory = new Dictionary<string, UsableItem>();
    private SortedDictionary<int, EquipableItem> heldEquipableInventory = new SortedDictionary<int, EquipableItem>();

    public int attackNum;
    public int defense;
    public int skillBase;
    public float skillMod;

    //public int enemyGold;
    //public int enemyExp;
    //public int enemyLevel;

    // Use this for initialization
    void Start () {
        //testing purposes
        //float dropExpon = 1.05f;
        //enemyGold = Mathf.RoundToInt(enemyGold * (Mathf.Pow(enemyLevel, dropExpon)));
        //enemyExp = Mathf.RoundToInt(enemyExp * (Mathf.Pow(enemyLevel, dropExpon)));

        //Debug.Log(string.Format("For Enemy Level: {0}\nExp Given: {1}\nGold Given: {2}", enemyLevel, enemyExp, enemyGold));
        
        manager = this;

        DontDestroyOnLoad(gameObject);

        //upgrade ores, Level Range: Small: 1-15, Medium: 16-30, Regular: 31-45, Large: 46-60, Gigantic: 61+
        //weapon upgrade ores
        ores.Add("Small Klogyte", 0);
        ores.Add("Medium Klogyte", 0);
        ores.Add("Klogyte", 0);
        ores.Add("Large Klogyte", 0);
        ores.Add("Gigantic Klogyte", 0);

        //armor upgrade ores
        ores.Add("Small Formatyte", 0);
        ores.Add("Medium Formatyte", 0);
        ores.Add("Formatyte", 0);
        ores.Add("Large Formatyte", 0);
        ores.Add("Gigantic Formatyte", 0);

        //accessory upgrade ores
        ores.Add("Small Ragalyte", 0);
        ores.Add("Medium Ragalyte", 0);
        ores.Add("Ragalyte", 0);
        ores.Add("Large Ragalyte", 0);
        ores.Add("Gigantic Ragalyte", 0);

        foreach(UsableItem usable in gameUsables)
        {
            usableDictionary.Add(usable.name, usable);
        }

        foreach(EquipableItem equipable in gameEquipables)
        {
            equipableDictionary.Add(equipable.name, equipable);
        }
    }

    public void newCharacters(ScriptablePlayer newChar)
    {
        int playerSize = players.Count;

        switch(playerSize)
        {
            case 0:
                players.Add("Player1", newChar);
                break;
            case 1:
                players.Add("Player2", newChar);
                break;
            case 2:
                players.Add("Player3", newChar);
                break;
            case 3:
                players.Add("Player4", newChar);
                break;
        }
    }

    public void inputPlayers(ScriptablePlayer player1, ScriptablePlayer player2,
        ScriptablePlayer player3, ScriptablePlayer player4)
    {
        players.Add("Player1", player1);
        players.Add("Player2", player2);
        players.Add("Player3", player3);
        players.Add("Player4", player4);
    }

    //get select character
    public ScriptablePlayer GetPlayer(string player)
    {
        return players[player];
    }

    //checks whether the dungeon entered is an Equipment Dungeon or not
    public bool getDungeonType() { return isEquipDung; }

    //sets dungeon type
    public void SetDungeonType(bool type) { isEquipDung = type; }

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
        int char1Level = players["Player1"].level;
        int char2Level = players["Player2"].level;
        int char3Level = players["Player3"].level;
        int char4Level = players["Player4"].level;

        return Mathf.RoundToInt((char1Level + char2Level + char3Level + char4Level) / 4);
    }

    //sets gameObject of item in range
    public void setObject(GameObject rangeItem)
    {
        itemRange = rangeItem;
    }

    //gets GameObject in range
    public GameObject getObject() { return itemRange; }

    //sets what stage of the quest line the player is on, advances quest line and gets the current quest stage
    public void setQuestStage(int set) { questStage = set; }
    public int getQuestStage() { return questStage; }

    //gets and sets what town stage the player is on
    public void SetTownStage(int stage) { townStage = stage; }
    public int GetTownStage() { return townStage; }

    //stores type of scene player is in
    public void SetScene(string scene) { sceneType = scene; }
    public string GetScene() { return sceneType; }

    //gets and sets inventory items
    public Dictionary<string, UsableItem> getUsableInventory() { return heldUsableInventory; }
    public SortedDictionary<int, EquipableItem> getEquipableInventory() { return heldEquipableInventory; }
    public void setUsableInventory(Dictionary<string, UsableItem> inventory) { heldUsableInventory = inventory; }
    public void setEquipableInventory(SortedDictionary<int, EquipableItem> inventory) { heldEquipableInventory = inventory; }

    //gets list of equipable items based on the average level of the party
    public List<EquipableItem> getEquipableInRange(int aveLvl)
    {
        List<EquipableItem> equipables = new List<EquipableItem>();

        foreach (KeyValuePair<string, EquipableItem> item in equipableDictionary)
        {
            string key = item.Key;
            EquipableItem currentItem = equipableDictionary[key];

            int min = currentItem.minLevel;
            int max = currentItem.maxLevel;

            if (aveLvl >= min && aveLvl <= max)
            {
                equipables.Add(currentItem);
            }
        }

        return equipables;
    }

    //gets list of usable items based on the average level of the party
    public List<UsableItem> getUsableInRange(int aveLvl)
    {
        List<UsableItem> usables = new List<UsableItem>();

        foreach (KeyValuePair<string, UsableItem> item in usableDictionary)
        {
            string key = item.Key;
            UsableItem currentItem = usableDictionary[key];

            int min = currentItem.minLevel;
            int max = currentItem.maxLevel;

            if (aveLvl >= min && aveLvl <= max)
            {
                usables.Add(currentItem);
            }
        }

        return usables;
    }

    public EquipableItem findEquipItem(string name)
    {
        return equipableDictionary[name];
    }

    public UsableItem findUseItem(string name)
    {
        return usableDictionary[name];
    }

    //ore getters and setters
    public int GetOre(string ore) { return ores[ore]; }
    public Dictionary<string, int> GetAllOres() { return ores; }
    public void ChangeOre(string ore, int amount) { ores[ore] += amount; }
    public void SetOres(Dictionary<string, int> loadedOres) { ores = loadedOres; }

    public void ShakeDevice()
    {
        Handheld.Vibrate();
    }

    //destroy GameObject when quiting game from Innkeeper
    public void DestroyManager()
    {
        Destroy(gameObject);
    }

    //add usable item to the inventory
    public void addUsableToInventory(string itemAdded, int quantity)
    {
        UsableItem usable = usableDictionary[itemAdded];
        string itemName = usable.name;

        bool hasItem = heldUsableInventory.ContainsKey(itemName);

        if (hasItem)
            heldUsableInventory[itemName].addItem(quantity);
        else
        {
            UsableItem newItem = ScriptableObject.CreateInstance(typeof(UsableItem)) as UsableItem;
            newItem.Init(usable.name, usable.type, usable.rarity, usable.cureAilment, usable.description,
                quantity, usable.healAmount, usable.reviveAmount, usable.minLevel, usable.maxLevel,
                usable.buyValue, usable.sellValue, usable.dropped, usable.bought, usable.chest);
            heldUsableInventory.Add(itemName, newItem);
        }
    }

    //add equipable item to the inventory
    public void addEquipableToInventory(EquipableItem itemAdded)
    {
        EquipableItem newInstance = null;

        if (itemAdded.ID == 0)
        {
            newInstance = ScriptableObject.CreateInstance(typeof(EquipableItem)) as EquipableItem;
            newInstance.Init(itemAdded.name, itemAdded.type, itemAdded.equipType, itemAdded.rarity,
                itemAdded.damage, itemAdded.armor, itemAdded.strength, itemAdded.agility, itemAdded.mind,
                itemAdded.soul, itemAdded.minLevel, itemAdded.maxLevel, itemAdded.buyValue,
                itemAdded.sellValue, itemAdded.bought, itemAdded.chest, itemAdded.maxUpgrades, 
                itemAdded.upgradeCosts, itemAdded.minRange, itemAdded.maxRange);

            int newID = 0;
            try
            {
                int itemID = heldEquipableInventory.Keys.Last();
                newID = itemID + 1;
            }
            catch { }

            SortedDictionary<int, EquipableItem> equippedItems = new SortedDictionary<int, EquipableItem>();

            foreach(KeyValuePair<string, ScriptablePlayer> player in players)
            {
                string key = player.Key;
                ScriptablePlayer tempPlayer = players[key];                

                equippedItems.Add(tempPlayer.weapon.ID, tempPlayer.weapon);
                try
                {
                    equippedItems.Add(tempPlayer.offHand.ID, tempPlayer.offHand);
                }
                catch { }
                equippedItems.Add(tempPlayer.armor.ID, tempPlayer.armor);
                equippedItems.Add(tempPlayer.accessory.ID, tempPlayer.accessory);
            }

            int equippedIDs = equippedItems.Keys.Last();

            if (equippedIDs >= newID)
                newID = equippedIDs + 1;

            newInstance.ID = newID;
        }
        else
            newInstance = itemAdded;        

        heldEquipableInventory.Add(newInstance.ID, newInstance);
    }


    //removes usable item from inventory
    public void removeUsableFromInventory(UsableItem item)
    {
        heldUsableInventory.Remove(item.name);

        try
        {
            StatsScreen.stats.ClearInventory();
            StatsScreen.stats.PopulateInv();
        }
        catch { }
    }

    //removes equipable item from inventory
    public void removeEquipableFromInventory(EquipableItem item)
    {
        heldEquipableInventory.Remove(item.ID);

        try
        {
            StatsScreen.stats.ClearEquipment();
            StatsScreen.stats.PopulateEquipment();
        }
        catch { }
    }

    //refreshes inventory after item is equipped
    public void afterUseRefresh()
    {
        StatsScreen.stats.ClearInventory();
        StatsScreen.stats.PopulateInv();
        EquippedButtonEvent.butEvent.SetButtonSprites();
        StatsScreen.stats.SetStats();
    }
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

    public int gold;
    public int questStage;

    public QuestsContainerSerializationModel questStack;

    public Dictionary<string, int> oreStorage;
}
