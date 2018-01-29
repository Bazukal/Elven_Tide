using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class LoadGame : MonoBehaviour {

    public Text save1Info;
    public Text save2Info;
    public Text save3Info;

    BinaryFormatter bf;
    FileStream fsSave1;
    FileStream fsSave2;
    FileStream fsSave3;

    public Button load1;
    public Button load2;
    public Button load3;

    PlayerData data;

    public GameObject loadScreen;
    public Slider loadingSlider;
    public Text progressText;

    // Use this for initialization
    void Start () {
        bf = new BinaryFormatter();

        load1.interactable = false;
        load2.interactable = false;
        load3.interactable = false;

        if (File.Exists(Application.persistentDataPath + "/saveSlot1.dat"))
        {
            fsSave1 = File.Open(Application.persistentDataPath + "/saveSlot1.dat", FileMode.Open);
            data = (PlayerData)bf.Deserialize(fsSave1);
            fsSave1.Close();

            save1Info.text = "Save 1 slot loaded";

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

            load1.interactable = true;
        }

        if (File.Exists(Application.persistentDataPath + "/saveSlot2.dat"))
        {
            fsSave2 = File.Open(Application.persistentDataPath + "/saveSlot2.dat", FileMode.Open);
            data = (PlayerData)bf.Deserialize(fsSave2);
            fsSave2.Close();

            save2Info.text = "Save 2 slot loaded";

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

            load2.interactable = true;
        }

        if (File.Exists(Application.persistentDataPath + "/saveSlot3.dat"))
        {
            save3Info.text = "Save 3 slot loaded";

            fsSave3 = File.Open(Application.persistentDataPath + "/saveSlot3.dat", FileMode.Open);
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

            load3.interactable = true;
        }
    }
	
	public void LoadData(int slot)
    {

        switch (slot)
        {
            case 1:
                if (File.Exists(Application.persistentDataPath + "/saveSlot1.dat"))
                {
                    fsSave1 = File.Open(Application.persistentDataPath + "/saveSlot1.dat", FileMode.Open);
                    data = (PlayerData)bf.Deserialize(fsSave1);
                    fsSave1.Close();
                }
                    break;
            case 2:
                if (File.Exists(Application.persistentDataPath + "/saveSlot2.dat"))
                {
                    fsSave2 = File.Open(Application.persistentDataPath + "/saveSlot2.dat", FileMode.Open);
                    data = (PlayerData)bf.Deserialize(fsSave2);
                    fsSave2.Close();
                }
                break;
            case 3:
                if (File.Exists(Application.persistentDataPath + "/saveSlot3.dat"))
                {
                    fsSave3 = File.Open(Application.persistentDataPath + "/saveSlot3.dat", FileMode.Open);
                    data = (PlayerData)bf.Deserialize(fsSave3);
                    fsSave3.Close();
                }
                break;
        }

        //set data to in game classes
        CharacterManager.charManager.character1 = data.character1;
        CharacterManager.charManager.character2 = data.character2;
        CharacterManager.charManager.character3 = data.character3;
        CharacterManager.charManager.character4 = data.character4;

        List<EquipableItemClass> loadEquipInventory = data.heldEquipableInventory;
        List<UsableItemClass> loadUseInventory = data.heldUsableInventory;
        CharacterManager.charManager.setEquipableInventory(loadEquipInventory);
        CharacterManager.charManager.setUsableInventory(loadUseInventory);

        List<QuestClass> loadQuests = data.quests;
        QuestListing.qListing.setQuests(loadQuests);

        int gold = data.gold;
        CharacterManager.charManager.setGold(gold);

        int stage = data.questStage;
        CharacterManager.charManager.setQuestStage(stage);

        string char1Class = CharacterManager.charManager.character1.GetCharClass();
        string char2Class = CharacterManager.charManager.character2.GetCharClass();
        string char3Class = CharacterManager.charManager.character3.GetCharClass();
        string char4Class = CharacterManager.charManager.character4.GetCharClass();

        CharacterManager.charManager.setSprites(char1Class, char2Class, char3Class, char4Class);

        StartCoroutine(loaded());
    }

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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Town");

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