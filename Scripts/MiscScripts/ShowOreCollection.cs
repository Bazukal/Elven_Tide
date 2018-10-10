using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowOreCollection : MonoBehaviour {

    public static ShowOreCollection showOre;

    public GameObject oreDisplayPanel;
    public Text oreDisplayText;

    // Use this for initialization
    void Start () {
        showOre = this;
	}
	
	public void ShowOre(string ore, int amount)
    {
        StoreFinds.stored.BattleActivate();
        oreDisplayPanel.SetActive(true);
        oreDisplayText.text = string.Format("Found: {0} x {1}", ore, amount);
    }

    public void CloseOre()
    {
        StoreFinds.stored.BattleDeactivate();
        oreDisplayPanel.SetActive(false);
        GameObject collectedOre = Manager.manager.getObject();
        Destroy(collectedOre);
    }
}
