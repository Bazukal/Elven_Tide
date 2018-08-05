using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;

[Category("Custom/Town")]
[Summary("Heals the Party to Full")]
public class RestNode : ActionNodeBase {
    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        Manager.manager.GetPlayer("Player1").healToFull();
        Manager.manager.GetPlayer("Player2").healToFull();
        Manager.manager.GetPlayer("Player3").healToFull();
        Manager.manager.GetPlayer("Player4").healToFull();

        MiniStats.stats.updateSliders();

        Fade.fade.fadeNow();

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
