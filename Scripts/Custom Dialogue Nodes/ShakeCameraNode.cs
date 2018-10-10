using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;
using EZCameraShake;

[Category("Custom/Misc")]
[Summary("Shakes the Camera")]
public class ShakeCameraNode : ActionNodeBase {
    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        CameraShaker.Instance.ShakeOnce(10f, 10f, 0.1f, 2.5f);

        Manager.manager.ShakeDevice();

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
