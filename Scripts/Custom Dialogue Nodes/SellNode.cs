using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;

[Category("Custom/Town")]
[Summary("Opens Sell Menu")]
public class SellNode : ActionNodeBase {
    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        shopSell.shSell.shopSellPanel();

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
