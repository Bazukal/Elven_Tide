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

    public Sprite archer;
    public Sprite blackMage;
    public Sprite monk;
    public Sprite paladin;
    public Sprite thief;
    public Sprite warrior;
    public Sprite whiteMage;

    private List<string> conversation = new List<string>();
    int convPos = 0;

    public Text conversationText;

    public GameObject loadScreen;
    public Slider loadingSlider;
    public Text progressText;

    //adds conversation screens to list
    private void Awake()
    {
        conversation.Add("Ever since you four were small children, you have been training to become the Hosgoth of this village.  Those that are bestowed this title are the Guardians that the rest of our people luck up to for protection.");
        conversation.Add("As many before you have done, possibly some of you will, and as others will in the future, Hosgoth have fallen to the evils of this world.  May they have gone against the village to seek another life, or they have moved on to the Josineth Plains.");
        conversation.Add("You four need to watch over each other, protect each other as you also protect this village.  Without all four of you together, evil can always slip through the shadows.  Taking our young, elders and loved ones.");
        conversation.Add("On this day, your training is over.  To prove that all your training has brought you to the strength needed to protect our village, you are given a task.  One that will send you into the cave within town.");
        conversation.Add("Go within, and make your way to the end of the cave.  At the end of the cave, one of your own will hand you an item. Upon deliverance of the item to me, you will be given the title of Hosgoth.");
    }

    // Use this for initialization
    void Start () {
        string char1 = CharacterManager.charManager.character1.GetCharClass();
        string char2 = CharacterManager.charManager.character2.GetCharClass();
        string char3 = CharacterManager.charManager.character3.GetCharClass();
        string char4 = CharacterManager.charManager.character4.GetCharClass();

        //sets character sprites based on the classes that the player chose
        switch(char1)
        {
            case "Archer":
                character1.GetComponent<SpriteRenderer>().sprite = archer;
                break;
            case "Black Mage":
                character1.GetComponent<SpriteRenderer>().sprite = blackMage;
                break;
            case "Monk":
                character1.GetComponent<SpriteRenderer>().sprite = monk;
                break;
            case "Paladin":
                character1.GetComponent<SpriteRenderer>().sprite = paladin;
                break;
            case "Thief":
                character1.GetComponent<SpriteRenderer>().sprite = thief;
                break;
            case "Warrior":
                character1.GetComponent<SpriteRenderer>().sprite = warrior;
                break;
            case "White Mage":
                character1.GetComponent<SpriteRenderer>().sprite = whiteMage;
                break;
        }

        switch (char2)
        {
            case "Archer":
                character2.GetComponent<SpriteRenderer>().sprite = archer;
                break;
            case "Black Mage":
                character2.GetComponent<SpriteRenderer>().sprite = blackMage;
                break;
            case "Monk":
                character2.GetComponent<SpriteRenderer>().sprite = monk;
                break;
            case "Paladin":
                character2.GetComponent<SpriteRenderer>().sprite = paladin;
                break;
            case "Thief":
                character2.GetComponent<SpriteRenderer>().sprite = thief;
                break;
            case "Warrior":
                character2.GetComponent<SpriteRenderer>().sprite = warrior;
                break;
            case "White Mage":
                character2.GetComponent<SpriteRenderer>().sprite = whiteMage;
                break;
        }

        switch (char3)
        {
            case "Archer":
                character3.GetComponent<SpriteRenderer>().sprite = archer;
                break;
            case "Black Mage":
                character3.GetComponent<SpriteRenderer>().sprite = blackMage;
                break;
            case "Monk":
                character3.GetComponent<SpriteRenderer>().sprite = monk;
                break;
            case "Paladin":
                character3.GetComponent<SpriteRenderer>().sprite = paladin;
                break;
            case "Thief":
                character3.GetComponent<SpriteRenderer>().sprite = thief;
                break;
            case "Warrior":
                character3.GetComponent<SpriteRenderer>().sprite = warrior;
                break;
            case "White Mage":
                character3.GetComponent<SpriteRenderer>().sprite = whiteMage;
                break;
        }

        switch (char4)
        {
            case "Archer":
                character4.GetComponent<SpriteRenderer>().sprite = archer;
                break;
            case "Black Mage":
                character4.GetComponent<SpriteRenderer>().sprite = blackMage;
                break;
            case "Monk":
                character4.GetComponent<SpriteRenderer>().sprite = monk;
                break;
            case "Paladin":
                character4.GetComponent<SpriteRenderer>().sprite = paladin;
                break;
            case "Thief":
                character4.GetComponent<SpriteRenderer>().sprite = thief;
                break;
            case "Warrior":
                character4.GetComponent<SpriteRenderer>().sprite = warrior;
                break;
            case "White Mage":
                character4.GetComponent<SpriteRenderer>().sprite = whiteMage;
                break;
        }

        conversationText.text = conversation[convPos];
        convPos++;
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
        CharacterManager.charManager.setQuestStage(1);

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
