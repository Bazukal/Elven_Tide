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
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        string goldAmount = FindObjectOfType<CharacterManager>().getGold().ToString();

        gold.text = string.Format("Gold: {0:n0}", goldAmount);

        if (gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            CloseNpcPanel.closeNpcPanel.activatePanel();

            player.GetComponentInChildren<CanvasGroup>().alpha = 1;
            player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

            player.GetComponentInChildren<CanvasGroup>().alpha = 0;
            player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;
        }
    }

    //updates gold amount in UI
    public void updateGold()
    {
        string goldAmount = CharacterManager.charManager.getGold().ToString();

        gold.text = string.Format("Gold: {0:n0}", goldAmount);
    }
}
