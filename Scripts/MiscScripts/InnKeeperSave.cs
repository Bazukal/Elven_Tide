using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using Devdog.QuestSystemPro;
using System.Collections.Generic;

public class InnKeeperSave : MonoBehaviour {

    public static InnKeeperSave save;

    public GameObject saveScreen;

    public Text save1Info;
    public Text save2Info;
    public Text save3Info;

    BinaryFormatter bf;
    FileStream fsSave1;
    FileStream fsSave2;
    FileStream fsSave3;

    PlayerData data;

    AndroidJavaObject currentActivity;
    AndroidJavaClass UnityPlayer;
    AndroidJavaObject context;

    private GameObject managers;
    private QuestsContainerSerializationModel saveQuests;

    string toastMessage;

    private void Awake()
    {
        Manager.manager.SetScene("Town");
    }

    private void Start()
    {
        save = this;

        managers = GameObject.Find("Game Manager");

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        }
    }

    //opens save screen
    public void openSave()
    {
        saveScreen.SetActive(true);

        bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/saveSlot1.dat"))
        {
            fsSave1 = File.OpenRead(Application.persistentDataPath + "/saveSlot1.dat");
            data = (PlayerData)bf.Deserialize(fsSave1);
            fsSave1.Close();

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

            save1Info.text = string.Format("{0}: Level {1} {2},   {3}: Level {4} {5},   {6}: Level {7} {8}," +
                "   {9}: Level {10} {11}\n\nGold: {12}", char1Name, char1Lvl, char1Class, char2Name, char2Lvl, char2Class,
                char3Name, char3Lvl, char3Class, char4Name, char4Lvl, char4Class, gold);            
        }

        if (File.Exists(Application.persistentDataPath + "/saveSlot2.dat"))
        {
            fsSave2 = File.OpenRead(Application.persistentDataPath + "/saveSlot2.dat");
            data = (PlayerData)bf.Deserialize(fsSave2);
            fsSave2.Close();

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

            save2Info.text = string.Format("{0}: Level {1} {2},   {3}: Level {4} {5},   {6}: Level {7} {8}," +
                "   {9}: Level {10} {11}\n\nGold: {12}", char1Name, char1Lvl, char1Class, char2Name, char2Lvl, char2Class,
                char3Name, char3Lvl, char3Class, char4Name, char4Lvl, char4Class, gold);
        }
        else

        if (File.Exists(Application.persistentDataPath + "/saveSlot3.dat"))
        {
            fsSave3 = File.OpenRead(Application.persistentDataPath + "/saveSlot3.dat");
            data = (PlayerData)bf.Deserialize(fsSave3);
            fsSave3.Close();

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

            save3Info.text = string.Format("{0}: Level {1} {2},   {3}: Level {4} {5},   {6}: Level {7} {8}," +
                "   {9}: Level {10} {11}\n\nGold: {12}", char1Name, char1Lvl, char1Class, char2Name, char2Lvl, char2Class,
                char3Name, char3Lvl, char3Class, char4Name, char4Lvl, char4Class, gold);            
        }
    }

    public void SetQuests(QuestsContainerSerializationModel quests)
    {
        saveQuests = quests;
    }

    //saves game into save slot based on option player selected
    public void Save(int slot)
    {        
        managers.GetComponent<SaveLoadManager>().SaveAllQuestsAndAchievementsForAll();

        BinaryFormatter bfS = new BinaryFormatter();
        FileStream fs = null;

        //determines which save slot the player is saving to
        switch(slot)
        {
            case 1:
                fs = File.Create(Application.persistentDataPath + "/saveSlot1.dat");
                //showToastMessage("Saved Game to Slot 1");
                break;
            case 2:
                fs = File.Create(Application.persistentDataPath + "/saveSlot2.dat");
                //showToastMessage("Saved Game to Slot 2");
                break;
            case 3:
                fs = File.Create(Application.persistentDataPath + "/saveSlot3.dat");
                //showToastMessage("Saved Game to Slot 3");
                break;
        }
        //stores player data to the PlayerData class
        data = new PlayerData();

        ScriptablePlayer p1 = Manager.manager.GetPlayer("Player1");
        ScriptablePlayer p2 = Manager.manager.GetPlayer("Player2");
        ScriptablePlayer p3 = Manager.manager.GetPlayer("Player3");
        ScriptablePlayer p4 = Manager.manager.GetPlayer("Player4");

        PlayerClass player1 = new PlayerClass(p1.name, p1.charClass.ToString(), p1.level, p1.currentExp, p1.weapon.name,
            p1.weapon.upgradesDone, p1.weapon.ID, p1.armor.name, p1.armor.upgradesDone, p1.armor.ID, p1.accessory.name, 
            p1.accessory.upgradesDone, p1.accessory.ID);

        try
        {
            EquipableItem p1OffHand = p1.offHand;
            player1.SetOffHand(p1OffHand.name);
            player1.SetOffHandUpgrades(p1OffHand.upgradesDone);
            player1.SetOffHandID(p1OffHand.ID);
        }
        catch { }

        PlayerClass player2 = new PlayerClass(p2.name, p2.charClass.ToString(), p2.level, p2.currentExp, p2.weapon.name,
            p2.weapon.upgradesDone, p2.weapon.ID, p2.armor.name, p2.armor.upgradesDone, p2.armor.ID, p2.accessory.name, 
            p2.accessory.upgradesDone, p2.accessory.ID);

        try
        {
            EquipableItem p2OffHand = p2.offHand;
            player2.SetOffHand(p2OffHand.name);
            player2.SetOffHandUpgrades(p2OffHand.upgradesDone);
            player2.SetOffHandID(p2OffHand.ID);
        }
        catch { }

        PlayerClass player3 = new PlayerClass(p3.name, p3.charClass.ToString(), p3.level, p3.currentExp, p3.weapon.name,
            p3.weapon.upgradesDone, p3.weapon.ID, p3.armor.name, p3.armor.upgradesDone, p3.armor.ID, p3.accessory.name, 
            p3.accessory.upgradesDone, p3.accessory.ID);

        try
        {
            EquipableItem p3OffHand = p3.offHand;
            player3.SetOffHand(p3OffHand.name);
            player3.SetOffHandUpgrades(p3OffHand.upgradesDone);
            player3.SetOffHandID(p3OffHand.ID);
        }
        catch { }

        PlayerClass player4 = new PlayerClass(p4.name, p4.charClass.ToString(), p4.level, p4.currentExp, p4.weapon.name,
            p4.weapon.upgradesDone, p4.weapon.ID, p4.armor.name, p4.armor.upgradesDone, p4.armor.ID, p4.accessory.name, 
            p4.accessory.upgradesDone, p4.accessory.ID);

        try
        {
            EquipableItem p4OffHand = p4.offHand;
            player4.SetOffHand(p4OffHand.name);
            player4.SetOffHandUpgrades(p4OffHand.upgradesDone);
            player4.SetOffHandID(p4OffHand.ID);
        }
        catch { }

        data.character1 = player1;
        data.character2 = player2;
        data.character3 = player3;
        data.character4 = player4;


        SortedDictionary<int, EquipableItem> heldEquipableInventory = Manager.manager.getEquipableInventory();
        Dictionary<string, UsableItem> heldUsableInventory = Manager.manager.getUsableInventory();
        List<EquipmentClass> storeEquipment = new List<EquipmentClass>();
        List<ItemClass> storeItems = new List<ItemClass>();

        foreach(KeyValuePair<int, EquipableItem> equipment in heldEquipableInventory)
        {
            int key = equipment.Key;
            EquipableItem storedEquip = heldEquipableInventory[key];

            EquipmentClass tempEquip = new EquipmentClass(storedEquip.name, storedEquip.ID, storedEquip.upgradesDone);

            storeEquipment.Add(tempEquip);
        }

        foreach(KeyValuePair<string, UsableItem> item in heldUsableInventory)
        {
            string key = item.Key;
            UsableItem storedItem = heldUsableInventory[key];

            ItemClass tempItem = new ItemClass(storedItem.name, storedItem.quantity);

            storeItems.Add(tempItem);
        }

        data.heldEquipableInventory = storeEquipment;
        data.heldUsableInventory = storeItems;

        data.gold = Manager.manager.GetGold();

        data.questStack = saveQuests;
        data.questStage = Manager.manager.getQuestStage();

        data.oreStorage = Manager.manager.GetAllOres();

        //saves the PlayerData to the file for the save slot that was chosen
        bfS.Serialize(fs, data);

        fs.Close();
        
        closeSave();
    }

    //shows toast about game being saved
    private void showToastMessage(string toastString)
    {
        toastMessage = toastString;
        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(showToast));
    }

    void showToast()
    {
        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastMessage);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
        toast.Call("show");
    }

    public void closeSave()
    {
        saveScreen.SetActive(false);

        StoreFinds.stored.BattleDeactivate();
    }
}