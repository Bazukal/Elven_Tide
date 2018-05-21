using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.Advertisements;

public class LevelLoad : MonoBehaviour {

    [SerializeField] string adID = "1641104";

    public static LevelLoad lLoad;

    public GameObject loadScreen;
    public Slider loadingSlider;
    public Text progressText;

    public GameObject levelPrepare;

    private int questStage = 0;

    private string levelReady = "Before venturing into the dungeon on your quest, you can watch an Ad that will boost your Drop Rate and Gold found.";
    private string levelBlocked = "A Forcefield is blocking you from entering the cave.";

    public Text loadText;
    public GameObject normalButton;
    public GameObject adButton;

    // Use this for initialization
    void Start () {
        lLoad = this;
        Advertisement.Initialize(adID, true);
    }

    //opens window for player to select how they want to enter dungeon, or cancel and go back to town
    public void prepareLevelLoad()
    {
        levelPrepare.SetActive(true);

        int qStage = Manager.manager.getQuestStage();
        QuestClass current = QuestListing.qListing.getCurrentQuest(qStage);

        if(current != null)
        {
            loadText.text = levelReady;
            normalButton.SetActive(true);
            adButton.SetActive(true);
        }
        else
        {
            loadText.text = levelBlocked;
            normalButton.SetActive(false);
            adButton.SetActive(false);
        }
        
    }
	
    //cancels selection to go into dungeon
    public void cancelLoad()
    {
        levelPrepare.SetActive(false);
    }

    //loads ad
    public void watchAd(string zone = "")
    {
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
                loadLevelNormal();
                break;
            case ShowResult.Failed:
                loadLevelNormal();
                break;
        }
    }


    //loads level based on which quest stage the player is on, using no drop buffs
    public void loadLevelNormal()
    {
        questStage = Manager.manager.getQuestStage();
        Manager.manager.setAd(false);
        StartCoroutine(loadDungeon());
    }

    //loads level based on which quest stage the player is on, using drop buffs
    private void loadAdLevel()
    {
        questStage = Manager.manager.getQuestStage();
        Manager.manager.setAd(true);
        StartCoroutine(loadDungeon());
    }

    //loads dungeon
    IEnumerator loadDungeon()
    {
        Manager.manager.setInRange("");

        StringBuilder sb = new StringBuilder();
        sb.Append("Level" + questStage);
        string levelQuest = sb.ToString();
       
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelQuest);

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
