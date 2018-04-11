using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;

public class StoreFinds : MonoBehaviour {

    public static StoreFinds stored;

    public GameObject player;
    public GameObject joyStick;

	// Use this for initialization
	void Start () {
        stored = this;
        
        player = GameObject.FindGameObjectWithTag("Player");
	}

    //activates and deactivates joystick when player is in npc chat, shop, inventory or stat screen
    public void activate()
    {
        if(joyStick.GetComponent<CanvasGroup>().alpha == 1)
        {
            joyStick.GetComponent<CanvasGroup>().alpha = 0;
            joyStick.GetComponent<CanvasGroup>().interactable = false;
            joyStick.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            joyStick.GetComponent<CanvasGroup>().alpha = 1;
            joyStick.GetComponent<CanvasGroup>().interactable = true;
            joyStick.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    //activates or deactivates joystick and character ui when player goes into battle
    public void battleActivate()
    {
        if (joyStick.GetComponent<CanvasGroup>().alpha == 1)
        {
            joyStick.GetComponent<CanvasGroup>().alpha = 0;
            joyStick.GetComponent<CanvasGroup>().interactable = false;
            joyStick.GetComponent<CanvasGroup>().blocksRaycasts = false;

            player.GetComponentInChildren<CanvasGroup>().alpha = 0;
            player.GetComponentInChildren<CanvasGroup>().interactable = false;
            player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            joyStick.GetComponent<CanvasGroup>().alpha = 1;
            joyStick.GetComponent<CanvasGroup>().interactable = true;
            joyStick.GetComponent<CanvasGroup>().blocksRaycasts = true;

            player.GetComponentInChildren<CanvasGroup>().alpha = 1;
            player.GetComponentInChildren<CanvasGroup>().interactable = true;
            player.GetComponentInChildren<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
