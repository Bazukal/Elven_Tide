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
        itemTexts[0].text = equipSelling.GetName() + " x " + equipSelling.GetQuantity();
        itemTexts[1].text = "Sell Price: " + equipSelling.GetSellPrice();
    }

    public void populateUseButtonData(UsableItemClass item)
    {
        useSelling = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = useSelling.GetName() + " x " + useSelling.GetQuantity();
        itemTexts[1].text = "Sell Price: " + useSelling.GetSellPrice();
    }

    public void purchaseWindow()
    {
        sellItem.sItem.sellPanel(equipSelling, useSelling);
    }
}
