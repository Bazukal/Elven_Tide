using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class populateSellPanel : MonoBehaviour {

    EquipableItemClass equipSelling = null;
    UsableItemClass useSelling = null;
    Text[] itemTexts;

    public void populateEquipButtonData(EquipableItemClass item)
    {
        equipSelling = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = equipSelling.getName() + " x " + equipSelling.getQuantity();
        itemTexts[1].text = "Sell Price: " + equipSelling.getSellPrice();
    }

    public void populateUseButtonData(UsableItemClass item)
    {
        useSelling = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = useSelling.getName() + " x " + useSelling.getQuantity();
        itemTexts[1].text = "Sell Price: " + useSelling.getSellPrice();
    }

    public void purchaseWindow()
    {
        sellItem.sItem.sellPanel(equipSelling, useSelling);
    }
}
