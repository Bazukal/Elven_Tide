using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.Advertisements;
using Devdog.General;


public class LevelLoad : MonoBehaviour {

    [SerializeField] string adID = "1641104";

    public static LevelLoad lLoad;

    public GameObject loadScreen;
    public Slider loadingSlider;
    public Text progressText;

    private int questStage = 0;
    private bool equipDungeon = false;
    private bool beginEquip = false;

    // Use this for initialization
    void Start () {
        lLoad = this;
        Advertisement.Initialize(adID, true);
    }

    //opens window for player to select how they want to enter dungeon, or cancel and go back to town
    public void prepareLevelLoad()
    {
        GameObject loadingObject = Manager.manager.getObject();
        loadingObject.GetComponent<Trigger>().Use();
    }

    //loads ad
    public void watchAd(bool isEquip, bool begin, string zone = "")
    {
        equipDungeon = isEquip;
        beginEquip = begin;

        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions();
        options.resultCallback = adResult;

        if(Advertisement.IsReady(zone))
            Advertisement.Show(zone, options);
    }

    private void adResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                loadAdLevel();
                break;
            case ShowResult.Skipped:
                loadLevelNormal(equipDungeon, beginEquip);
                break;
            case ShowResult.Failed:
                loadLevelNormal(equipDungeon, beginEquip);
                break;
        }
    }


    //loads level based on which quest stage the player is on, using no drop buffs
    public void loadLevelNormal(bool isEquip, bool begin)
    {
        equipDungeon = isEquip;
        beginEquip = begin;

        Manager.manager.setAd(false);
        StartCoroutine(loadDungeon());
    }

    //loads level based on which quest stage the player is on, using drop buffs
    private void loadAdLevel()
    {
        Manager.manager.setAd(true);
        StartCoroutine(loadDungeon());
    }

    //loads dungeon
    IEnumerator loadDungeon()
    {
        Manager.manager.setObject(null);
        AsyncOperation asyncLoad;

        Manager.manager.SetDungeonType(equipDungeon);

        if (equipDungeon == true)
        {
            if(beginEquip == true)
                asyncLoad = SceneManager.LoadSceneAsync("HoleDungeonStart");
            else
                asyncLoad = SceneManager.LoadSceneAsync("HoleDungeon");
        }            
        else
        {
            questStage = Manager.manager.getQuestStage();
            StringBuilder sb = new StringBuilder();
            sb.Append("Level" + questStage);
            string levelQuest = sb.ToString();

            asyncLoad = SceneManager.LoadSceneAsync(levelQuest);
        }

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
