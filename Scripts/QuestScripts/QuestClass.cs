using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class QuestClass{

    private string questName;
    private string questGiver;
    private string questType;
    private string findNPC;
    private string killNPC;
    private string collectItem;
    private string collectWhere;
    private string startChat;
    private string inProgressChat;
    private string endChat;
    private string questDescription;

    //what part of the quest line is the player at
    private int questStage;
    //what part of the quest is the player at.  1 = quest available, 2 = quest underway, 3 = quest objectives completed
    //4 = quest turned in
    private int questPart = 0;
    private int killAmount;
    private int killCount = 0;
    private int collectAmount;
    private int collectCount = 0;

    //how far in progression is the player.  For find NPC quests, 0 = not found, 1 = found.  Kill and Collection
    //shows how many kills or items have been collected
    private int questProgress = 0;


    public QuestClass()
    {

    }

    public QuestClass(string qName, string qgiver, string qType, string fNPC, string sChat, string ipChat,
        string eChat, string qDesc, int qStage)
    {
        questName = qName;
        questGiver = qgiver;
        questType = qType;
        findNPC = fNPC;
        startChat = sChat;
        inProgressChat = ipChat;
        endChat = eChat;
        questDescription = qDesc;
        questStage = qStage;
    }

    public QuestClass(string qName, string qgiver, string qType, string kNPC, string sChat, string ipChat,
        string eChat, string qDesc, int qStage, int kAmount)
    {
        questName = qName;
        questGiver = qgiver;
        questType = qType;
        killNPC = kNPC;
        startChat = sChat;
        inProgressChat = ipChat;
        endChat = eChat;
        questDescription = qDesc;
        questStage = qStage;
        killAmount = kAmount;
    }

    public QuestClass(string qName, string qgiver, string qType, string cItem, string cWhere, string sChat, string ipChat,
        string eChat, string qDesc, int qStage, int cAmount)
    {
        questName = qName;
        questGiver = qgiver;
        questType = qType;
        collectItem = cItem;
        collectWhere = cWhere;
        startChat = sChat;
        inProgressChat = ipChat;
        endChat = eChat;
        questDescription = qDesc;
        questStage = qStage;
        collectAmount = cAmount;
    }

    public string getQuestName() { return questName; }
    public string getQuestGiver() { return questGiver; }
    public string getQuestType() { return questType; }
    public string getFindNPC() { return findNPC; }
    public string getKillNPC() { return killNPC; }
    public string getCollectItem() { return collectItem; }
    public string getCollectWhere() { return collectWhere; }
    public string getStartChat() { return startChat; }
    public string getInProgChat() { return inProgressChat; }
    public string getFinishhcat() { return endChat; }
    public string getQuestDesc() { return questDescription; }

    public int getQuestStage() { return questStage; }
    public int getQuestPart() { return questPart; }
    public int getKillAmount() { return killAmount; }
    public int getCollectAmount() { return collectAmount; }
    public int getQuestProgress() { return questProgress; }
    public int getKillCount() { return killCount; }
    public int getCollectCount() { return collectCount; }

    public void advanceQuestPart(int part) { questPart = part; }
    public void advanceQuestProgress() { questProgress++; }
    public void advanceKillCount() { killCount++; }
    public void advanceCollectCount() { collectCount++; }
}
