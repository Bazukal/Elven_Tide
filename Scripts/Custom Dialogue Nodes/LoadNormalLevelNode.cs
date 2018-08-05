using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;

[Category("Custom/Load")]
[Summary("Enters Dungeon under normal rates")]
public class LoadNormalLevelNode : ActionNodeBase {

    [ShowInNode]
    public bool equipDung;
    [ShowInNode]
    public bool beginEquip;

    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        LevelLoad.lLoad.loadLevelNormal(equipDung, beginEquip);

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
