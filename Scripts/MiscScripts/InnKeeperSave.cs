using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

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

    string toastMessage;

    private void Start()
    {
        save = this;

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

    //saves game into save slot based on option player selected
    public void Save(int slot)
    {
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

        data.character1 = Manager.manager.GetPlayer("Player1");
        data.character2 = Manager.manager.GetPlayer("Player2");
        data.character3 = Manager.manager.GetPlayer("Player3");
        data.character4 = Manager.manager.GetPlayer("Player4");

        data.heldEquipableInventory = CharacterInventory.charInven.getEquipableInventory();
        data.heldUsableInventory = CharacterInventory.charInven.getUsableInventory();

        data.questStage = Manager.manager.getQuestStage();

        data.quests = QuestListing.qListing.getQuests();

        data.gold = Manager.manager.GetGold();

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
    }
}