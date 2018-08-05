using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;

[Category("Custom/Quest")]
[Summary("Sets What Quest Player is on")]
public class SetQuestStageNode : ActionNodeBase {

    [ShowInNode]
    public int value;

    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        Manager.manager.setQuestStage(value);

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
