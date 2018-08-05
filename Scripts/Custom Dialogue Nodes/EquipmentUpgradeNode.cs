using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;

[Category("Custom/Town")]
[Summary("Opens Screen to Upgrade Equipment")]
public class EquipmentUpgradeNode : ActionNodeBase {
    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        EquipUpgrade.upgrade.openUpgrade();

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
