using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;

public class StoreFinds : MonoBehaviour {

    public static StoreFinds stored;

    public GameObject player;
    public GameObject joyStick;

	// Use this for initialization
	void Start () {
        stored = this;
	}

    //activates or deactivates joystick and character ui when player goes into battle
    public void BattleActivate()
    {
        joyStick.GetComponent<CanvasGroup>().alpha = 0;
        joyStick.GetComponent<CanvasGroup>().interactable = false;
        joyStick.GetComponent<CanvasGroup>().blocksRaycasts = false;

        player.GetComponent<CanvasGroup>().alpha = 0;
        player.GetComponent<CanvasGroup>().interactable = false;
        player.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void BattleDeactivate()
    {
        joyStick.GetComponent<CanvasGroup>().alpha = 1;
        joyStick.GetComponent<CanvasGroup>().interactable = true;
        joyStick.GetComponent<CanvasGroup>().blocksRaycasts = true;

        player.GetComponent<CanvasGroup>().alpha = 1;
        player.GetComponent<CanvasGroup>().interactable = true;
        player.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
