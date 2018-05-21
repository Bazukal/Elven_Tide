using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcChat : MonoBehaviour {

    public static npcChat chat;

    public Text chatText;
    GameObject player;

    private void Awake()
    {
        chat = this;
    }

    //activates chat with npc that player is next to
    public void startChat(string citi)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<CanvasGroup>().alpha = 0;
        player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;

        string npc = citi;
        int questStage = Manager.manager.getQuestStage();

        switch(npc)
        {
            case "Citizen1":
                List<ChatClass> citi1 = GameChats.gChats.getCitizen1();

                foreach(ChatClass chat in citi1)
                {
                    if(chat.getStage() == questStage)
                    {
                        chatText.text = chat.getChat();
                        break;
                    }
                }
                break;
            case "Citizen2":
                List<ChatClass> citi2 = GameChats.gChats.getCitizen2();

                foreach (ChatClass chat in citi2)
                {
                    if (chat.getStage() == questStage)
                    {
                        chatText.text = chat.getChat();
                        break;
                    }
                }
                break;
            case "Citizen3":
                List<ChatClass> citi3 = GameChats.gChats.getCitizen3();

                foreach (ChatClass chat in citi3)
                {
                    if (chat.getStage() == questStage)
                    {
                        chatText.text = chat.getChat();
                        break;
                    }
                }
                break;
            case "Citizen4":
                List<ChatClass> citi4 = GameChats.gChats.getCitizen4();

                foreach (ChatClass chat in citi4)
                {
                    if (chat.getStage() == questStage)
                    {
                        chatText.text = chat.getChat();
                        break;
                    }
                }
                break;
        }
    }

    //closes chat
    public void closeChat()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        player.GetComponentInChildren<CanvasGroup>().alpha = 1;
        player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = true;
    }
}
