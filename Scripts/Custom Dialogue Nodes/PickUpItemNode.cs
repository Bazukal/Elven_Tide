using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Category("Custom/Interaction")]
[Summary("For Picking Up Items")]
public class PickUpItemNode : ActionNodeBase {

    private List<string> oreType = new List<string>() { "Klogyte", "Formatyte", "Ragalyte" };
    private List<string> oreSize = new List<string>() { "Small ", "Medium ", "", "Large ", "Gigantic " };
    public string oreGained;
    public int oreAmount;

    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        int aveLevel = Manager.manager.AveLevel();
        StringBuilder sb = new StringBuilder();

        if (aveLevel <= 15)
            sb.Append(oreSize[0]);
        else if(aveLevel <= 30)
            sb.Append(oreSize[1]);
        else if(aveLevel <= 45)
            sb.Append(oreSize[2]);
        else if(aveLevel <= 60)
            sb.Append(oreSize[3]);
        else
            sb.Append(oreSize[4]);

        int oreKind = Random.Range(0, 3);
        int amountRoll = Random.Range(1, 11);

        if (amountRoll >= 5)
            oreAmount = 1;
        else if (amountRoll >= 2)
            oreAmount = 2;
        else
            oreAmount = 3;

        sb.Append(oreType[oreKind]);

        oreGained = sb.ToString();

        Manager.manager.ChangeOre(oreGained, oreAmount);

        ShowOreCollection.showOre.ShowOre(oreGained, oreAmount);

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
