using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBuyPanel : MonoBehaviour {

    public static CloseBuyPanel closeBuyPanel;

    public Text gold;

    private void Awake()
    {
        closeBuyPanel = this;
    }

    //turns on or off npc ui panel/turns off player ui
    public void activatePanel()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        StoreFinds.stored.BattleDeactivate();
    }

    //updates gold amount in UI
    public void updateGold()
    {
        string goldAmount = Manager.manager.GetGold().ToString();

        gold.text = string.Format("Gold: {0:n0}", goldAmount);
    }
}
