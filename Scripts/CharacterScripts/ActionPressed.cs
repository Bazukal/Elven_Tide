using UnityEngine;

public class ActionPressed : MonoBehaviour {    

    //open panel depending on what object is in range
    public void buttonPressed()
    {
        string whosInRange = CharacterManager.charManager.getInRange();

        switch(whosInRange)
        {
            case "InnKeeper":
                SelectionPanelData.panelData.inputData("InnKeeper");
                break;
            case "Healer":
                SelectionPanelData.panelData.inputData("Healer");
                break;
            case "Blacksmith":
                SelectionPanelData.panelData.inputData("Blacksmith");
                break;
            case "Master":
                QuestAccept.qAccept.displayQuest();
                break;
            case "Chest":
                GameObject openChest = CharacterManager.charManager.getChest();
                Debug.Log(openChest.name);
                openChest.GetComponent<OpenChest>().open();
                break;
            case "Cave":
                LevelLoad.lLoad.prepareLevelLoad();
                break;
            case "Citizen1":
                npcChat.chat.startChat("Citizen1");
                break;
            case "Citizen2":
                npcChat.chat.startChat("Citizen2");
                break;
            case "Citizen3":
                npcChat.chat.startChat("Citizen3");
                break;
            case "Citizen4":
                npcChat.chat.startChat("Citizen4");
                break;
            case "CaveExit":
                ExitDungeon.exit.activatePanel();
                break;
        }
    }

    
}
