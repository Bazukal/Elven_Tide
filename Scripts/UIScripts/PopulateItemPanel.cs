using UnityEngine;
using UnityEngine.UI;

public class PopulateItemPanel : MonoBehaviour {

    ItemClass usableSent = null;
    EquipmentClass equipableSent = null;

    //adds name of item in inventory to button
	public void populateUsableData(ItemClass item)
    {
        usableSent = item;
        gameObject.GetComponentInChildren<Text>().text = usableSent.GetName() + " x " + usableSent.GetQuantity();        
    }

    //adds name of item in inventory to button
    public void populateEquipableData(EquipmentClass item)
    {
        equipableSent = item;
        gameObject.GetComponentInChildren<Text>().text = equipableSent.GetName();
    }

    //adds name of item to sell shop button
    public void populateUsableSellData(ItemClass item)
    {
        usableSent = item;
        gameObject.GetComponentInChildren<Text>().text = usableSent.GetName();
    }

    //adds name of item to sell shop button
    public void populateEquipSellData(EquipmentClass item)
    {
        equipableSent = item;
        gameObject.GetComponentInChildren<Text>().text = equipableSent.GetName();
    }

    //activates equip item stat window with item
    public void itemToStat()
    {
        if (equipableSent != null)
            UseEquipItem.equipItem.activateEquipItemStat(equipableSent);
        else
            UseEquipItem.equipItem.activateUseItemStat(usableSent);
    }
}
