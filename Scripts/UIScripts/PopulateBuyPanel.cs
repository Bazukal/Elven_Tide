using UnityEngine;
using UnityEngine.UI;

public class PopulateBuyPanel : MonoBehaviour {

    EquipableItem equipBuying;
    UsableItem useBuying;
    Text[] itemTexts;

    //adds name of equipable item in inventory to button
    public void populateEquipButtonData(EquipableItem item)
    {
        equipBuying = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = equipBuying.name;
        itemTexts[1].text = "Cost: " + equipBuying.buyValue;        
    }

    //adds name of equipable item in inventory to button
    public void populateUseButtonData(UsableItem item)
    {
        useBuying = item;
        itemTexts = this.GetComponentsInChildren<Text>();
        itemTexts[0].text = useBuying.name;
        itemTexts[1].text = "Cost: " + useBuying.buyValue;
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
