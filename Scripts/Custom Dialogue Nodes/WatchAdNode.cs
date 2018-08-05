using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;

[Category("Custom/Load")]
[Summary("Plays Ad for player to watch")]
public class WatchAdNode : ActionNodeBase {

    [ShowInNode]
    public bool equipDung;
    [ShowInNode]
    public bool beginEquip;

    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        LevelLoad.lLoad.watchAd(equipDung, beginEquip, "rewardedVideo");

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
