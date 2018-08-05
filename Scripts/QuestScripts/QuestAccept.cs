using UnityEngine;
using Devdog.General;

public class QuestAccept : MonoBehaviour {

    public static QuestAccept qAccept;

    private void Awake()
    {
        qAccept = this;
    }

    //display quest information
    public void displayQuest(GameObject questScreen)
    {
        questScreen.GetComponent<Trigger>().Use();
    }

    
}
