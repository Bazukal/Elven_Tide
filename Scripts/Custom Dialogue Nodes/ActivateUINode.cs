using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;

[Category("Custom")]
[Summary("Re activates UI")]
public class ActivateUINode : ActionNodeBase
{
    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        StoreFinds.stored.BattleDeactivate();

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
