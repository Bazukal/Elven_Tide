using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAccept : MonoBehaviour {

    public static QuestAccept qAccept;

    public Text questName;
    public Text questDesc;
    public Text questObj;
    
    public Button cancel;
    public Button accept;

    QuestClass currentClass;
    GameObject player;

    private void Awake()
    {
        qAccept = this;
    }

    //display quest information
    public void displayQuest()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<CanvasGroup>().alpha = 0;
        player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;

        int stage = CharacterManager.charManager.getQuestStage();
        string giver = CharacterManager.charManager.getInRange();

        currentClass = QuestListing.qListing.getQuest(stage, giver);

        questName.text = currentClass.getQuestName();

        int step = currentClass.getQuestPart();

        switch(step)
        {
            case 1:
                questDesc.text = currentClass.getStartChat();
                questObj.text = currentClass.getQuestDesc();
                cancel.interactable = true;
                accept.GetComponentInChildren<Text>().text = "Accept";
                break;
            case 2:
                questDesc.text = currentClass.getInProgChat();
                questObj.text = currentClass.getQuestDesc();
                cancel.interactable = false;
                accept.GetComponentInChildren<Text>().text = "Close";
                break;
            case 3:
                questDesc.text = currentClass.getFinishhcat();
                questObj.text = "";
                cancel.interactable = true;
                accept.GetComponentInChildren<Text>().text = "Finish";
                break;
        }
    }

    //accepts quest
    public void AcceptQuest()
    {
        int step = currentClass.getQuestPart();

        switch (step)
        {
            case 1:
                QuestListing.qListing.setQuestPart(currentClass, 2);
                break;           
            case 3:
                QuestListing.qListing.setQuestPart(currentClass, 4);
                CharacterManager.charManager.advanceQuestStage();

                int stage = CharacterManager.charManager.getQuestStage();
                string giver = CharacterManager.charManager.getInRange();

                currentClass = QuestListing.qListing.getQuest(stage, giver);

                QuestListing.qListing.setQuestPart(currentClass, 1);
                break;
        }

        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        player.GetComponentInChildren<CanvasGroup>().alpha = 1;
        player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = true;

        QuestIcon.qIcon.changeQuest();
    }

    public void closeQuestPanel()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        player.GetComponentInChildren<CanvasGroup>().alpha = 1;
        player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = true;
    }
}
