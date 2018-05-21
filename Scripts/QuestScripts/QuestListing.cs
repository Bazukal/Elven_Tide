using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//holds quest data
[Serializable]
public class QuestListing : MonoBehaviour {

    public static QuestListing qListing;

    private List<QuestClass> allQuests = new List<QuestClass>();

    private void Awake()
    {
        qListing = this;
    }

    public void setQuests(List<QuestClass> quests)
    {
        allQuests = quests;
    }

    public List<QuestClass> getQuests() { return allQuests; }

    public QuestClass getQuest(int qStage, string qGiver)
    {
        List<QuestClass> tempQuests = new List<QuestClass>();

        foreach (QuestClass quest in allQuests)
        {
            if (quest.getQuestStage() == qStage)
                tempQuests.Add(quest);
        }

        foreach (QuestClass collectQuest in tempQuests)
        {
            if (collectQuest.getQuestGiver().Equals(qGiver))
                return collectQuest;
        }

        return null;
    }

    public QuestClass getCurrentQuest(int qStage)
    {
        List<QuestClass> tempQuests = new List<QuestClass>();

        foreach (QuestClass quest in allQuests)
        {
            if (quest.getQuestStage() == qStage)
                tempQuests.Add(quest);
        }

        foreach (QuestClass collectQuest in tempQuests)
        {
            if (collectQuest.getQuestPart() == 2 || collectQuest.getQuestPart() == 3)
                return collectQuest;
        }

        return null;
    }

    public void setQuestPart(QuestClass quest, int stage)
    {
        int index = allQuests.IndexOf(quest);

        allQuests[index].advanceQuestPart(stage);
    }
}
