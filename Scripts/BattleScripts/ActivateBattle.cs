using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBattle : MonoBehaviour {

    public static ActivateBattle active;

    public GameObject battleScreen;

    private void Start()
    {
        active = this;
    }
    
    //activates battle and spawns enemies
    public void battle()
    {
        battleScreen.GetComponent<CanvasGroup>().alpha = 1;
        battleScreen.GetComponent<CanvasGroup>().interactable = true;
        battleScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;

        StoreFinds.stored.battleActivate();

        Battle.battle.spawnEnemies();
    }
}
