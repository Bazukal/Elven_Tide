using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatClass {

    private string whichCitizen;
    private int questStage;
    private string chatString;

    public ChatClass()
    {

    }

    public ChatClass(string citi, int stage, string chat)
    {
        whichCitizen = citi;
        questStage = stage;
        chatString = chat;
    }

    public string getCitizen() { return whichCitizen; }
    public int getStage() { return questStage; }
    public string getChat() { return chatString; }
}
