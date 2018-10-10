using UnityEngine;
using Devdog.QuestSystemPro;
using Devdog.General;

public class ActionPressed : MonoBehaviour {

    public static ActionPressed pressed;

    GameObject inRange;

    private void Start()
    {
        pressed = this;
    }

    //open panel depending on what object is in range
    public void buttonPressed()
    {
        inRange = Manager.manager.getObject();
        string objectTag = inRange.tag;

        switch(objectTag)
        {             
            case "Healer":
            case "Blacksmith":
            case "NPC":
                inRange.GetComponent<Trigger>().Use();
                StoreFinds.stored.BattleActivate();
                break;
            case "Chest":
                inRange.GetComponent<OpenChest>().open();
                break;
            case "Cave":
                StoreFinds.stored.BattleActivate();
                LevelLoad.lLoad.prepareLevelLoad();
                break;
            case "CaveExit":
                ExitDungeon.exit.activatePanel();
                break;
        }
    }    

    public void DestroyObject()
    {
        Destroy(inRange);
    }
}
