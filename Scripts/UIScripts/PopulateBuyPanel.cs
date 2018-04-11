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
        itemTexts[0].text = equipBuying.GetName();
        itemTexts[1].text = "Cost: " + equipBuying.GetBuyPrice();        
    }

    //adds name of equipable item in inventory to button
    public void populateUseButtonData(UsableItemClass item)
    {
        useBuying = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = useBuying.GetName();
        itemTexts[1].text = "Cost: " + useBuying.GetBuyPrice();
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
