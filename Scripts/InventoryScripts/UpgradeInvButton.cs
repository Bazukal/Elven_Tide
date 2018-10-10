using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInvButton : MonoBehaviour {

    EquipableItem upgradeItem;
    Text thisText;

    public void AddItemToButton(EquipableItem item)
    {
        upgradeItem = item;
        thisText = GetComponentInChildren<Text>();
        string itemName = upgradeItem.name;
        int upgradesDone = upgradeItem.upgradesDone;

        if (upgradesDone > 0)
            thisText.text = string.Format("{0} +{1}", itemName, upgradesDone);
        else
            thisText.text = itemName;
    }

    public void ItemToStats()
    {
        EquipUpgrade.upgrade.openStatsPanel(upgradeItem);
    }
}
