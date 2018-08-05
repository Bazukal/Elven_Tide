using Devdog.QuestSystemPro.Dialogue;
using Devdog.General;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Category("Custom/Interaction")]
[Summary("Destroys What Object Dialogue is Attached to")]
public class DestroyObjectNode : ActionNodeBase
{
    public override void OnExecute(IDialogueOwner dialogueOwner)
    {
        ActionPressed.pressed.DestroyObject();

        Finish(true);
    }

    public override NodeBase GetNextNode()
    {
        return base.GetNextNode();
    }
}
