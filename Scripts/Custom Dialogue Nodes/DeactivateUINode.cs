using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;

[Category("Custom")]
[Summary("De activates UI")]
public class DeactivateUINode : ActionNodeBase
{
    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        StoreFinds.stored.BattleActivate();

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
