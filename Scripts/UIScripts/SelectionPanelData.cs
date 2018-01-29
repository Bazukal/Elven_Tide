using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionPanelData : MonoBehaviour {

    public static SelectionPanelData panelData;

    public Text npcInfo;
    public Button restBuyButton;
    public Button saveSellButton;

    private const string INNKEEPER = "Welcome to my Inn.  From here you can rest for the night to replenish your strength, or I can save your progress so you can come back at a later time.  What would you like to do?";
    private const string BLACKSMITH = "So you need some weapons and armor do you?  You have come to the right place.  Here you will find everything you need to fight the denizens of that dreaded cave, for the right price that is.";
    private const string HEALER = "Welcome to my shop.  We have all of your healing item needs.  From healing potions, to items that cure different ailments, and even some that can bring back the dead.  Go ahead and take a look.";

    private void Awake()
    {
        panelData = this;
    }


    //populates data into the selection panel based on which npc the player is in range of
    public void inputData(string whichNpc)
    {
        CloseNpcPanel.closeNpcPanel.activatePanel();

        switch (whichNpc)
        {
            case "InnKeeper":                
                npcInfo.text = INNKEEPER;
                int aveLvl = CharacterManager.charManager.aveLevel();
                int restCost = aveLvl * 5;
                int goldAvail = CharacterManager.charManager.getGold();
                restBuyButton.GetComponentInChildren<Text>().text = string.Format("Rest ({0} Gold)", restCost);
                if (goldAvail < restCost)
                    restBuyButton.interactable = false;
                else
                    restBuyButton.interactable = true;

                saveSellButton.GetComponentInChildren<Text>().text = "Save";
                break;
            case "Blacksmith":
                npcInfo.text = BLACKSMITH;
                restBuyButton.GetComponentInChildren<Text>().text = "Buy";
                if(restBuyButton.interactable == false)
                    restBuyButton.interactable = true;
                saveSellButton.GetComponentInChildren<Text>().text = "Sell";
                break;
            case "Healer":
                npcInfo.text = HEALER;
                restBuyButton.GetComponentInChildren<Text>().text = "Buy";
                if (restBuyButton.interactable == false)
                    restBuyButton.interactable = true;
                saveSellButton.GetComponentInChildren<Text>().text = "Sell";
                break;
        }
    }
}
