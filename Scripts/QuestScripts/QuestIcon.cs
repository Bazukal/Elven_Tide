using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour {

    public static QuestIcon qIcon;

    public Sprite questReady;
    public Sprite questFinished;
    public Sprite questInProgress;

	// Use this for initialization
	void Start () {
        changeQuest();

        qIcon = this;
	}
	
    //displays or hides quest icon dependant on what stage of the current quest the player is on
	public void changeQuest()
    {
        QuestClass questStatus = QuestListing.qListing.getCurrentQuest(CharacterManager.charManager.getQuestStage());

        if(questStatus != null)
        {
            int questStage = questStatus.getQuestPart();

            if (questStage == 2)
                gameObject.GetComponent<SpriteRenderer>().sprite = questInProgress;
            else if(questStage == 3)
                gameObject.GetComponent<SpriteRenderer>().sprite = questFinished;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = questReady;
        }
    }
}
