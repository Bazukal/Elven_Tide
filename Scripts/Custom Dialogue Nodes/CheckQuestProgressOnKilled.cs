using UnityEngine;
using System.Collections;
using Devdog.General;
using Devdog.QuestSystemPro.Dialogue;

namespace Devdog.QuestSystemPro
{
    public class CheckQuestProgressOnKilled : MonoBehaviour
    {
        [ShowInNode]
        public string questProgress;

        public QuestProgressDecorator progress;

        public void OnKilled()
        {
            int range = Random.Range(0, 101);

            if(range >= 40)
            {
                BattleScript.battleOn.QuestUpdates(questProgress);
                progress.Execute();
            }
        }
    }
}

