using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;

[Category("Custom/Battle")]
[Summary("Starts Boss Battle")]
public class BossBattleNode : ActionNodeBase {

    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        ActivateBattle.active.battle(true);

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
