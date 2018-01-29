using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseNpcPanel : MonoBehaviour {

    public static CloseNpcPanel closeNpcPanel;

    private void Awake()
    {
        closeNpcPanel = this;
    }

    //turns on or off npc ui panel/turns off player ui
    public void activatePanel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

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
}
