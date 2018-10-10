using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseSellPanel : MonoBehaviour {

    public static CloseSellPanel closeSellPanel;

    public Text gold;

    private void Awake()
    {
        closeSellPanel = this;
    }

    //turns on or off npc ui panel/turns off player ui
    public void activatePanel()
    {
        string goldAmount = Manager.manager.GetGold().ToString();

        gold.text = string.Format("Gold: {0:n0}", goldAmount);

        if (gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

            StoreFinds.stored.BattleDeactivate();
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

            StoreFinds.stored.BattleActivate();
        }
    }

    //updates gold amount in UI
    public void updateGold()
    {
        string goldAmount = Manager.manager.GetGold().ToString();

        gold.text = string.Format("Gold: {0:n0}", goldAmount);
    }
}
