using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public GameObject character4;

    private Dictionary<string, GameObject> characters = new Dictionary<string, GameObject>();

    public Sprite archer;
    public Sprite blackMage;
    public Sprite monk;
    public Sprite paladin;
    public Sprite thief;
    public Sprite warrior;
    public Sprite whiteMage;

    private Dictionary<string, Sprite> classSprites = new Dictionary<string, Sprite>();

    private List<string> conversation = new List<string>();
    int convPos = 0;

    public Text conversationText;

    public GameObject loadScreen;
    public Slider loadingSlider;
    public Text progressText;

    //adds conversation screens to list
    private void Awake()
    {
        conversation.Add("Ever since you four were small children, you have been training to become the Hosgoth of this village.  Those that are bestowed this title are the Guardians that the rest of our people look up to for protection.");
        conversation.Add("As many before you have done, possibly some of you will, and as others will in the future, Hosgoth have fallen to the evils of this world.  May they have gone against the village to seek another life, or they have moved on to the Josineth Plains.");
        conversation.Add("You four need to watch over each other, protect each other as you also protect this village.  Without all four of you together, evil can always slip through the shadows.  Taking our young, elders and loved ones.");
        conversation.Add("On this day, your training is over.  To prove that all your training has brought you to the strength needed to protect our village, you are given a task.  One that will send you into the cave within town.");
        conversation.Add("Go within, and make your way to the end of the cave.  At the end of the cave, one of your own will hand you an item. Upon deliverance of the item to me, you will be given the title of Hosgoth.");
    }

    // Use this for initialization
    void Start () {
        classSprites.Add("Archer", archer);
        classSprites.Add("Black Mage", blackMage);
        classSprites.Add("Monk", monk);
        classSprites.Add("Paladin", paladin);
        classSprites.Add("Thief", thief);
        classSprites.Add("Warrior", warrior);
        classSprites.Add("White Mage", whiteMage);

        characters.Add("Player1", character1);
        characters.Add("Player2", character2);
        characters.Add("Player3", character3);
        characters.Add("Player4", character4);

        List<string> players = new List<string>() { "Player1", "Player2", "Player3", "Player4" };

        foreach(string player in players)
        {
            setSprite(player);
        }      

        conversationText.text = conversation[convPos];
        convPos++;
    }

    private void setSprite(string player)
    {
        string charClass = Manager.manager.GetPlayer(player).charClass;
        characters[player].GetComponent<SpriteRenderer>().sprite = classSprites[charClass];
    }
    
    //continue with the conversation until the end.  once the end has been met, load the town scene
    public void continueStory()
    {
        int storyLength = conversation.Count;

        if(convPos < storyLength)
        {
            conversationText.text = conversation[convPos];
            convPos++;
        }
        else
        {
            StartCoroutine(StartTown());
        }
    }

    IEnumerator StartTown()
    {
        Manager.manager.setQuestStage(1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TownStage1");

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
