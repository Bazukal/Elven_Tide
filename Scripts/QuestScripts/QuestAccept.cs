using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Devdog.QuestSystemPro;
using Devdog.General;
using Devdog.General.UI;
using Devdog.QuestSystemPro.Dialogue;

public class QuestAccept : MonoBehaviour {

    public static QuestAccept qAccept;

    public GameObject questScreen;

    private void Awake()
    {
        qAccept = this;
    }

    //display quest information
    public void displayQuest()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        questScreen.GetComponent<DialogueOwner>().OnTriggerUsed(player);
    }

    
}
