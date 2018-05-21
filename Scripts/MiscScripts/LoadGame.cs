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
    FileStream fsSave;

    public Button load1;
    public Button load2;
    public Button load3;

    PlayerData data;

    public GameObject loadScreen;
    public Slider loadingSlider;
    public Text progressText;

    Dictionary<int, string> saveSlot = new Dictionary<int, string>();

    //pulls data from save files if there are any, and displays to player character information for save
    void Start () {
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
        Manager.manager.inputPlayers(data.character1, data.character2, data.character3, data.character4);

        List<EquipmentClass> loadEquipInventory = data.heldEquipableInventory;
        List<ItemClass> loadUseInventory = data.heldUsableInventory;
        Manager.manager.setEquipableInventory(loadEquipInventory);
        Manager.manager.setUsableInventory(loadUseInventory);

        List<QuestClass> loadQuests = data.quests;
        QuestListing.qListing.setQuests(loadQuests);

        int gold = data.gold;
        Manager.manager.setGold(gold);

        int stage = data.questStage;
        Manager.manager.setQuestStage(stage);

        StartCoroutine(loaded());
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