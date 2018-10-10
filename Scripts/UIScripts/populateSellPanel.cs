using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class populateSellPanel : MonoBehaviour {

    EquipableItem equipSelling = null;
    UsableItem useSelling = null;
    Text[] itemTexts;

    public void populateEquipButtonData(EquipableItem item)
    {
        equipSelling = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = equipSelling.name;
        itemTexts[1].text = "Sell Price: " + equipSelling.sellValue;
    }

    public void populateUseButtonData(UsableItem item)
    {
        useSelling = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = useSelling.name + " x " + useSelling.quantity;
        itemTexts[1].text = "Sell Price: " + useSelling.sellValue;
    }

    public void purchaseWindow()
    {
        sellItem.sItem.sellPanel(equipSelling, useSelling);
    }
}
