using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;
using UnityEngine.SceneManagement;
using UnityEngine;

[Category("Custom/Load")]
[Summary("Heals the Party to Full")]
public class TitleScreenNode : ActionNodeBase {
    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        Manager.manager.DestroyManager();
        MiniStats.stats.destroyThis();

        SceneManager.LoadScene("TitleScreen");

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
