using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class populateSellPanel : MonoBehaviour {

    EquipmentClass equipSelling = null;
    ItemClass useSelling = null;
    Text[] itemTexts;

    public void populateEquipButtonData(EquipmentClass item)
    {
        equipSelling = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = equipSelling.GetName();
        itemTexts[1].text = "Sell Price: " + equipSelling.GetSellPrice();
    }

    public void populateUseButtonData(ItemClass item)
    {
        useSelling = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = useSelling.GetName() + " x " + useSelling.GetQuantity();
        itemTexts[1].text = "Sell Price: " + useSelling.GetSell();
    }

    public void purchaseWindow()
    {
        sellItem.sItem.sellPanel(equipSelling, useSelling);
    }
}
