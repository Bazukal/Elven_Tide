using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;

public class StoreFinds : MonoBehaviour {

    public static StoreFinds stored;

    public GameObject player;
    public GameObject joyStick;
    private GameObject miniStats;

    // Use this for initialization
    void Start () {
        stored = this;

        miniStats = GameObject.Find("MiniStatsCanvas");

        miniStats.GetComponent<CanvasGroup>().alpha = 1;
    }

    //activates or deactivates joystick and character ui when player goes into battle
    public void BattleActivate()
    {
        JoystickMovement.joystick.setMove(false);

        joyStick.GetComponent<CanvasGroup>().alpha = 0;
        joyStick.GetComponent<CanvasGroup>().interactable = false;
        joyStick.GetComponent<CanvasGroup>().blocksRaycasts = false;

        player.GetComponent<CanvasGroup>().alpha = 0;
        player.GetComponent<CanvasGroup>().interactable = false;
        player.GetComponent<CanvasGroup>().blocksRaycasts = false;

        miniStats.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void BattleDeactivate()
    {
        JoystickMovement.joystick.setMove(true);

        joyStick.GetComponent<CanvasGroup>().alpha = 1;
        joyStick.GetComponent<CanvasGroup>().interactable = true;
        joyStick.GetComponent<CanvasGroup>().blocksRaycasts = true;

        player.GetComponent<CanvasGroup>().alpha = 1;
        player.GetComponent<CanvasGroup>().interactable = true;
        player.GetComponent<CanvasGroup>().blocksRaycasts = true;

        miniStats.GetComponent<CanvasGroup>().alpha = 1;
    }
}
