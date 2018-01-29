using UnityEngine;
using UnityEngine.UI;

public class PopulateBuyPanel : MonoBehaviour {

    EquipableItemClass equipBuying;
    UsableItemClass useBuying;
    Text[] itemTexts;

    //adds name of equipable item in inventory to button
    public void populateEquipButtonData(EquipableItemClass item)
    {
        equipBuying = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = equipBuying.getName();
        itemTexts[1].text = "Cost: " + equipBuying.getBuyPrice();        
    }

    //adds name of equipable item in inventory to button
    public void populateUseButtonData(UsableItemClass item)
    {
        useBuying = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = useBuying.getName();
        itemTexts[1].text = "Cost: " + useBuying.getBuyPrice();
    }

    public void purchaseEquipWindow()
    {
        PurchaseItem.pItem.purchaseEquipPanel(equipBuying);
    }

    public void purchaseUseWindow()
    {
        PurchaseItem.pItem.purchaseUseItem(useBuying);
    }
}
