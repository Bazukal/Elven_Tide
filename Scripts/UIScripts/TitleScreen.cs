using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

    public GameObject credits;

    //Will open or close the credits screen on button press
    public void creditsScreen()
    {        
        if (credits.activeSelf)
            credits.SetActive(false);
        else
            credits.SetActive(true);
    }

    //Starts New Game Scene
    public void newGame()
    {
        StartCoroutine(NewGameScene());
    }

    //Starts Load Game Screen
    public void loadGame()
    {
        StartCoroutine(LoadGameScene());
    }

    IEnumerator NewGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("New Game Character Screen");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LoadGame");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
