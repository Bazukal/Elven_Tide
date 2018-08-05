using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStatScreen : MonoBehaviour {

    public GameObject statScreen;

    //opens stat screen
    public void OpenStat()
    {
        StoreFinds.stored.BattleActivate();
        statScreen.SetActive(true);
        StatsScreen.stats.ScreenOpened();
    }
}
