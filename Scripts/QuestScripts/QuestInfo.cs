using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfo : MonoBehaviour {

    public Sprite checkedBox;
    public Sprite uncheckedBox;

    public Image checkImage;

    public Text questName;
    public Text questChat;
    public Text questObj;
    public Text questProg;

    private QuestClass currentQuest;

    //opens players quest log and displays quest info
	public void openQuestLog()
    {
        StoreFinds.stored.BattleActivate();

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        int stage = Manager.manager.getQuestStage();
        currentQuest = QuestListing.qListing.getCurrentQuest(stage);        

        if (currentQuest != null)
        {
            questName.text = currentQuest.getQuestName();
            questChat.text = currentQuest.getStartChat();
            questObj.text = currentQuest.getQuestDesc();

            if(currentQuest.getQuestType().Equals("Kill"))
            {
                int killAmount = currentQuest.getKillAmount();
                int killCount = currentQuest.getKillCount();

                questProg.text = string.Format("{0}/{1}", killCount, killAmount);
            }

            else if (currentQuest.getQuestType().Equals("Collect"))
            {
                int collectAmount = currentQuest.getCollectAmount();
                int collectCount = currentQuest.getCollectCount();

                questProg.text = string.Format("{0}/{1}", collectCount, collectAmount);
            }

            else if(currentQuest.getQuestType().Equals("Find"))
            {
                questProg.text = "";
            }

            int part = currentQuest.getQuestPart();

            checkImage.enabled = true;

            if (part == 3)
                checkImage.sprite = checkedBox;
            else
                checkImage.sprite = uncheckedBox;
        }
        else
        {
            questName.text = "";
            questChat.text = "No Quest Available";
            questObj.text = "";
            questProg.text = "";
            checkImage.enabled = false;
        }
    }

    //closes quest log
    public void closeQuestPanel()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        StoreFinds.stored.BattleDeactivate();
    }
}
