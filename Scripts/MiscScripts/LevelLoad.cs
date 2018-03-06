using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class LevelLoad : MonoBehaviour {

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

    GameObject player;

    // Use this for initialization
    void Start () {
        lLoad = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //opens window for player to select how they want to enter dungeon, or cancel and go back to town
    public void prepareLevelLoad()
    {
        levelPrepare.SetActive(true);

        player.GetComponentInChildren<CanvasGroup>().alpha = 0;
        player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;

        int qStage = CharacterManager.charManager.getQuestStage();
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
	
    //loads level based on which quest stage the player is on, using no drop buffs
	public void loadLevelNormal()
    {
        questStage = CharacterManager.charManager.getQuestStage();
        StartCoroutine(loadDungeon());
    }

    //cancels selection to go into dungeon
    public void cancelLoad()
    {
        levelPrepare.SetActive(false);

        player.GetComponentInChildren<CanvasGroup>().alpha = 1;
        player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = true;
    }

    //loads level based on which quest stage the player is on, using drop buffs
    public void loadAdLevel()
    {

    }

    //loads dungeon
    IEnumerator loadDungeon()
    {
        CharacterManager.charManager.setInRange("");

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
