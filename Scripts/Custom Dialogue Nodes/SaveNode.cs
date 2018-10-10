using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;

[Category("Custom/Town")]
[Summary("Opens the Save Window")]
public class SaveNode : ActionNodeBase {
    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        InnKeeperSave.save.openSave();

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
