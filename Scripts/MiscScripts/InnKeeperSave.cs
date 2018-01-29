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

    public void openSave()
    {
        saveScreen.SetActive(true);

        bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/saveSlot1.dat"))
        {
            fsSave1 = File.OpenRead(Application.persistentDataPath + "/saveSlot1.dat");
            data = (PlayerData)bf.Deserialize(fsSave1);
            fsSave1.Close();

            string char1Name = data.character1.GetCharName();
            string char2Name = data.character2.GetCharName();
            string char3Name = data.character3.GetCharName();
            string char4Name = data.character4.GetCharName();

            int char1Lvl = data.character1.GetCharLevel();
            int char2Lvl = data.character2.GetCharLevel();
            int char3Lvl = data.character3.GetCharLevel();
            int char4Lvl = data.character4.GetCharLevel();

            int gold = data.gold;

            save1Info.text = string.Format("{0}: Level {1},   {2}: Level {3},   {4}: Level {5},   {6}: Level {7}" +
                "\n\nGold: {8}", char1Name, char1Lvl, char2Name, char2Lvl, char3Name, char3Lvl, char4Name, char4Lvl, gold);            
        }

        if (File.Exists(Application.persistentDataPath + "/saveSlot2.dat"))
        {
            fsSave2 = File.OpenRead(Application.persistentDataPath + "/saveSlot2.dat");
            data = (PlayerData)bf.Deserialize(fsSave2);
            fsSave2.Close();

            string char1Name = data.character1.GetCharName();
            string char2Name = data.character2.GetCharName();
            string char3Name = data.character3.GetCharName();
            string char4Name = data.character4.GetCharName();

            int char1Lvl = data.character1.GetCharLevel();
            int char2Lvl = data.character2.GetCharLevel();
            int char3Lvl = data.character3.GetCharLevel();
            int char4Lvl = data.character4.GetCharLevel();

            int gold = data.gold;

            save2Info.text = string.Format("{0}: Level {1},   {2}: Level {3},   {4}: Level {5},   {6}: Level {7}" +
                "\n\nGold: {8}", char1Name, char1Lvl, char2Name, char2Lvl, char3Name, char3Lvl, char4Name, char4Lvl, gold);
        }
        else

        if (File.Exists(Application.persistentDataPath + "/saveSlot3.dat"))
        {
            fsSave3 = File.OpenRead(Application.persistentDataPath + "/saveSlot3.dat");
            data = (PlayerData)bf.Deserialize(fsSave3);
            fsSave3.Close();

            string char1Name = data.character1.GetCharName();
            string char2Name = data.character2.GetCharName();
            string char3Name = data.character3.GetCharName();
            string char4Name = data.character4.GetCharName();

            int char1Lvl = data.character1.GetCharLevel();
            int char2Lvl = data.character2.GetCharLevel();
            int char3Lvl = data.character3.GetCharLevel();
            int char4Lvl = data.character4.GetCharLevel();

            int gold = data.gold;

            save3Info.text = string.Format("{0}: Level {1},   {2}: Level {3},   {4}: Level {5},   {6}: Level {7}" +
                "\n\nGold: {8}", char1Name, char1Lvl, char2Name, char2Lvl, char3Name, char3Lvl, char4Name, char4Lvl, gold);            
        }
    }

    public void Save(int slot)
    {
        BinaryFormatter bfS = new BinaryFormatter();
        FileStream fs = null;

        //determines which save slot the player is saving to
        switch(slot)
        {
            case 1:
                fs = File.Create(Application.persistentDataPath + "/saveSlot1.dat");
                showToastMessage("Saved Game to Slot 1");
                break;
            case 2:
                fs = File.Create(Application.persistentDataPath + "/saveSlot2.dat");
                showToastMessage("Saved Game to Slot 2");
                break;
            case 3:
                fs = File.Create(Application.persistentDataPath + "/saveSlot3.dat");
                showToastMessage("Saved Game to Slot 3");
                break;
        }
        //stores player data to the PlayerData class
        data = new PlayerData();

        data.character1 = CharacterManager.charManager.character1;
        data.character2 = CharacterManager.charManager.character2;
        data.character3 = CharacterManager.charManager.character3;
        data.character4 = CharacterManager.charManager.character4;

        data.heldEquipableInventory = CharacterInventory.charInven.getEquipableInventory();
        data.heldUsableInventory = CharacterInventory.charInven.getUsableInventory();

        data.questStage = CharacterManager.charManager.getQuestStage();

        data.quests = QuestListing.qListing.getQuests();

        data.gold = CharacterManager.charManager.getGold();

        //saves the PlayerData to the file for the save slot that was chosen
        bfS.Serialize(fs, data);

        fs.Close();

        showToast();
        closeSave();
    }

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