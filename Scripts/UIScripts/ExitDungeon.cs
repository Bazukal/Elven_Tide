using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitDungeon : MonoBehaviour {

    public static ExitDungeon exit;

    public GameObject exitPanel;

    public GameObject loadScreen;
    public Slider loadingSlider;
    public Text progressText;

    private void Start()
    {
        if (exit == null)
            exit = this;
        else
            Destroy(this);
    }

    //activates or deactivates window to leave dungeon
    public void activatePanel()
    {
        if(exitPanel.activeSelf == true)
        {
            exitPanel.SetActive(false);
            StoreFinds.stored.activate();
        }
        else
        {
            exitPanel.SetActive(true);
            StoreFinds.stored.activate();
        }
    }

    //takes player back to town
    public void travel()
    {
        StartCoroutine(townTravel());
    }

    IEnumerator townTravel()
    {
        CharacterManager.charManager.setInRange("");

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
