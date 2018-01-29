using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour {

    public static LevelLoad lLoad;

    public GameObject loadScreen;
    public Slider loadingSlider;
    public Text progressText;

    public GameObject levelPrepare;

    private int questStage = 0;

    GameObject player;

    // Use this for initialization
    void Start () {
        lLoad = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void prepareLevelLoad()
    {
        levelPrepare.SetActive(true);

        player.GetComponentInChildren<CanvasGroup>().alpha = 0;
        player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;
    }
	
	public void loadLevelNormal()
    {
        questStage = CharacterManager.charManager.getQuestStage();

        StartCoroutine(loadDungeon());
    }

    public void cancelLoad()
    {
        levelPrepare.SetActive(false);

        player.GetComponentInChildren<CanvasGroup>().alpha = 1;
        player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = true;
    }

    public void loadAddLevel()
    {

    }

    IEnumerator loadDungeon()
    {
        CharacterManager.charManager.setInRange("");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(questStage);

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
