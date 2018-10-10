using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;
using UnityEngine.SceneManagement;

[Category("Custom/Quest")]
[Summary("Resets Town")]
public class ResetTownNode : ActionNodeBase
{
    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        Manager.manager.SetTownStage(2);
        SceneManager.LoadScene("TownStage2");

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
