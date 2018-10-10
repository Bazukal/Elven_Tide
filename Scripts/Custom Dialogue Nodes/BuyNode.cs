using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;

[Category("Custom/Town")]
[Summary("Opens Buy Menu")]
public class BuyNode : ActionNodeBase {
    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        shopBuy.sBuy.shopBuyPanel();

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
