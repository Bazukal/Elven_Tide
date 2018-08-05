using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Devdog.QuestSystemPro;
using Devdog.General;

public class LoadGame : MonoBehaviour {

    public static LoadGame load;

    public List<ScriptablePlayerClasses> charClasses = new List<ScriptablePlayerClasses>();
    private Dictionary<string, ScriptablePlayerClasses> classDict = new Dictionary<string, ScriptablePlayerClasses>();

    private int townStage = 1;

    public Text save1Info;
    public Text save2Info;
    public Text save3Info;

    BinaryFormatter bf;
    FileStream fsSave;

    public Button load1;
    public Button load2;
    public Button load3;

    PlayerData data;

    public GameObject loadScreen;
    public Slider loadingSlider;
    public Text progressText;

    Dictionary<int, string> saveSlot = new Dictionary<int, string>();

    private GameObject managers;

    private QuestsContainerSerializationModel loadQuests;

    //pulls data from save files if there are any, and displays to player character information for save
    void Start () {
        load = this;

        foreach (ScriptablePlayerClasses classes in charClasses)
        {
            classDict.Add(classes.charClass, classes);
        }

        managers = GameObject.Find("Game Manager");

        saveSlot.Add(1, "/saveSlot1.dat");
        saveSlot.Add(2, "/saveSlot2.dat");
        saveSlot.Add(3, "/saveSlot3.dat");

        bf = new BinaryFormatter();

        load1.interactable = false;
        load2.interactable = false;
        load3.interactable = false;

        for(int i = 1; i <= 3;i++)
        {
            if (File.Exists(Application.persistentDataPath + saveSlot[i]))
            {
                fsSave = File.Open(Application.persistentDataPath + saveSlot[i], FileMode.Open);
                data = (PlayerData)bf.Deserialize(fsSave);
                fsSave.Close();

                string char1Name = data.character1.GetName();
                string char2Name = data.character2.GetName();
                string char3Name = data.character3.GetName();
                string char4Name = data.character4.GetName();

                int char1Lvl = data.character1.GetLevel();
                int char2Lvl = data.character2.GetLevel();
                int char3Lvl = data.character3.GetLevel();
                int char4Lvl = data.character4.GetLevel();

                string char1Class = data.character1.GetClass();
                string char2Class = data.character2.GetClass();
                string char3Class = data.character3.GetClass();
                string char4Class = data.character4.GetClass();

                int gold = data.gold;

                if(i == 1)
                {
                    save1Info.text = string.Format("{0}: Level {1} {2},   {3}: Level {4} {5},   {6}: Level {7} {8}," +
                    "   {9}: Level {10} {11}\n\nGold: {12}", char1Name, char1Lvl, char1Class, char2Name, char2Lvl, char2Class,
                    char3Name, char3Lvl, char3Class, char4Name, char4Lvl, char4Class, gold);

                    load1.interactable = true;
                }
                else if(i == 2)
                {
                    save2Info.text = string.Format("{0}: Level {1} {2},   {3}: Level {4} {5},   {6}: Level {7} {8}," +
                "   {9}: Level {10} {11}\n\nGold: {12}", char1Name, char1Lvl, char1Class, char2Name, char2Lvl, char2Class,
                char3Name, char3Lvl, char3Class, char4Name, char4Lvl, char4Class, gold);

                    load2.interactable = true;
                }
                else
                {
                    save3Info.text = string.Format("{0}: Level {1} {2},   {3}: Level {4} {5},   {6}: Level {7} {8}," +
                "   {9}: Level {10} {11}\n\nGold: {12}", char1Name, char1Lvl, char1Class, char2Name, char2Lvl, char2Class,
                char3Name, char3Lvl, char3Class, char4Name, char4Lvl, char4Class, gold);

                    load3.interactable = true;
                }
            }
        }        
    }

    //get quest stack
    public QuestsContainerSerializationModel GetQuests() { return loadQuests; }

    //loads game from save slot that player selected
    public void LoadData(int slot)
    {
        if (File.Exists(Application.persistentDataPath + saveSlot[slot]))
        {
            fsSave = File.Open(Application.persistentDataPath + saveSlot[slot], FileMode.Open);
            data = (PlayerData)bf.Deserialize(fsSave);
            fsSave.Close();
        }

        //set data to in game classes
        PlayerClass p1 = data.character1;
        PlayerClass p2 = data.character2;
        PlayerClass p3 = data.character3;
        PlayerClass p4 = data.character4;

        List<PlayerClass> players = new List<PlayerClass>();
        players.Add(p1);
        players.Add(p2);
        players.Add(p3);
        players.Add(p4);

        EquipableItem weapon;
        EquipableItem offHand;
        EquipableItem armor;
        EquipableItem accessory;

        EquipableItem usingWeapon;
        EquipableItem usingOffHand = null;
        EquipableItem usingArmor;
        EquipableItem usingAccessory;

        List<ScriptablePlayerClasses> loadingPlayers = new List<ScriptablePlayerClasses>();

        foreach(PlayerClass player in players)
        {
            ScriptablePlayerClasses pLoad = ScriptableObject.CreateInstance(typeof(ScriptablePlayerClasses)) as ScriptablePlayerClasses;
            ScriptablePlayerClasses pTemp = classDict[player.GetClass()];

            weapon = Manager.manager.findEquipItem(player.GetWeapon());
            usingWeapon = createEquip(weapon, player);
            usingWeapon.ID = player.GetWeaponID();
            try
            {
                offHand = Manager.manager.findEquipItem(player.GetOffHand());
                usingOffHand = createEquip(offHand, player);
                usingOffHand.ID = player.GetOffHandID();
            }
            catch
            {
                usingOffHand = null;
            }
            armor = Manager.manager.findEquipItem(player.GetArmor());
            usingArmor = createEquip(armor, player);
            usingArmor.ID = player.GetArmorID();

            accessory = Manager.manager.findEquipItem(player.GetAccessory());
            usingAccessory = createEquip(accessory, player);
            usingAccessory.ID = player.GetAccessoryID();

            pLoad.Init(player.GetName(), player.GetClass(), player.GetLevel(), player.GetExp(), 
                pTemp.classHead, pTemp.battleSprite, pTemp.maxArmor, pTemp.expChart, pTemp.levelHp, 
                pTemp.levelMp, pTemp.levelStrength, pTemp.levelAgility, pTemp.levelMind, 
                pTemp.levelSoul, pTemp.levelDefense, false, pTemp.canShield, pTemp.canDuelWield, 
                pTemp.canSword, pTemp.canDagger, pTemp.canMace, pTemp.canBow, pTemp.canStaff, 
                pTemp.canRod, pTemp.canFists, usingWeapon, usingOffHand, usingArmor, usingAccessory,
                pTemp.knownSkills, null, null);

            loadingPlayers.Add(pLoad);
        }        

        Manager.manager.inputPlayers(loadingPlayers[0], loadingPlayers[1], loadingPlayers[2],
            loadingPlayers[3]);

        List<EquipmentClass> savedEquipment = data.heldEquipableInventory;
        List<ItemClass> savedItems = data.heldUsableInventory;
        SortedDictionary<int, EquipableItem> loadingEquipment = new SortedDictionary<int, EquipableItem>();
        Dictionary<string, UsableItem> loadingItems = new Dictionary<string, UsableItem>();

        foreach(EquipmentClass equip in savedEquipment)
        {
            string equipName = equip.GetName();

            EquipableItem loadEquip = Manager.manager.findEquipItem(equipName);
            EquipableItem freshEquip = ScriptableObject.CreateInstance(typeof(EquipableItem)) as EquipableItem;
            freshEquip.ID = equip.GetID();
            freshEquip.upgradesDone = equip.GetUpgrades();
            freshEquip.Init(equipName, loadEquip.type, loadEquip.equipType, loadEquip.rarity,
                loadEquip.damage, loadEquip.armor, loadEquip.strength, loadEquip.agility,
                loadEquip.mind, loadEquip.soul, loadEquip.minLevel, loadEquip.maxLevel,
                loadEquip.buyValue, loadEquip.sellValue, loadEquip.bought, loadEquip.chest, 
                loadEquip.maxUpgrades, loadEquip.upgradeCosts, loadEquip.minRange,
                loadEquip.maxRange);
            loadingEquipment.Add(freshEquip.ID, freshEquip);
        }

        foreach(ItemClass item in savedItems)
        {
            string itemName = item.GetName();

            UsableItem usable = Manager.manager.findUseItem(itemName);

            UsableItem newItem = ScriptableObject.CreateInstance(typeof(UsableItem)) as UsableItem;
            newItem.Init(usable.name, usable.type, usable.rarity, usable.cureAilment, usable.description,
                item.GetQuantity(), usable.healAmount, usable.reviveAmount, usable.minLevel, usable.maxLevel,
                usable.buyValue, usable.sellValue, usable.dropped, usable.bought, usable.chest);
            newItem.quantity = item.GetQuantity();

            loadingItems.Add(itemName, newItem);
        }

        Manager.manager.setEquipableInventory(loadingEquipment);
        Manager.manager.setUsableInventory(loadingItems);

        loadQuests = data.questStack;

        managers.GetComponent<SaveLoadManager>().LoadAllQuestsAndAchievementsForAll();

        int gold = data.gold;
        Manager.manager.setGold(gold);

        int stage = data.questStage;
        Manager.manager.setQuestStage(stage);

        Quest[] quests = QuestManager.instance.quests;
        string phase2Name = "Talk to Town Folk";
        string completeCheck = "Completed";
        

        foreach (Quest quest in quests)
        {
            string questName = quest.name.ToString();
            string questStatus = quest.status.ToString();
            if (questName.Equals(phase2Name))
            {
                if (questStatus.Equals(completeCheck))
                    townStage = 2;
            }
        }

        Manager.manager.SetTownStage(townStage);

        Manager.manager.SetOres(data.oreStorage);

        StartCoroutine(loaded());
    }

    private EquipableItem createEquip(EquipableItem item, PlayerClass player)
    {
        EquipableItem newItem = ScriptableObject.CreateInstance(typeof(EquipableItem)) as EquipableItem;
        newItem.Init(item.name, item.type, item.equipType, item.rarity, item.damage,
                item.armor, item.strength, item.agility, item.mind, item.soul, item.minLevel,
                item.maxLevel, item.buyValue, item.sellValue, item.bought, item.chest, 
                item.maxUpgrades, item.upgradeCosts, item.minRange, item.maxRange);
        newItem.upgradesDone = player.GetWeaponUpgrades();
        newItem.ID = item.ID;
        newItem.SetStats();

        return newItem;
    }

    //cancels load and goes back to title screen
    public void cancelLoad()
    {
        StartCoroutine(canceled());
    }

    IEnumerator canceled()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TitleScreen");

        while (!asyncLoad.isDone)
        {            
            yield return null;
        }
    }

    IEnumerator loaded()
    {
        AsyncOperation asyncLoad;

        if(townStage == 1)
            asyncLoad = SceneManager.LoadSceneAsync("TownStage1");
        else
            asyncLoad = SceneManager.LoadSceneAsync("TownStage2");

        loadScreen.SetActive(true);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            loadingSlider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}