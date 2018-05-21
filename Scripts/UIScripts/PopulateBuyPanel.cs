using UnityEngine;
using UnityEngine.UI;

public class PopulateBuyPanel : MonoBehaviour {

    EquipmentClass equipBuying;
    ItemClass useBuying;
    Text[] itemTexts;

    //adds name of equipable item in inventory to button
    public void populateEquipButtonData(EquipmentClass item)
    {
        equipBuying = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = equipBuying.GetName();
        itemTexts[1].text = "Cost: " + equipBuying.GetBuyPrice();        
    }

    //adds name of equipable item in inventory to button
    public void populateUseButtonData(ItemClass item)
    {
        useBuying = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = useBuying.GetName();
        itemTexts[1].text = "Cost: " + useBuying.GetBuy();
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
