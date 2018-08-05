using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeEquipButton : MonoBehaviour {

    EquipableItem upgradeItem;
    Text thisText;

    public void AddItemToButton(EquipableItem item, string charName)
    {
        upgradeItem = item;
        thisText = GetComponentInChildren<Text>();
        string itemName = upgradeItem.name;
        int upgradesDone = upgradeItem.upgradesDone;

        if (upgradesDone > 0)
            thisText.text = string.Format("{0} +{1}- Equipped by {2}", itemName, upgradesDone, charName);
        else
            thisText.text = string.Format("{0}- Equipped by {1}", itemName, charName);
    }

    public void ItemToStats()
    {
        EquipUpgrade.upgrade.openStatsPanel(upgradeItem);
    }
}
