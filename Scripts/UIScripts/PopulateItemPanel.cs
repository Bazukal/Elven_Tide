using UnityEngine;
using UnityEngine.UI;

public class PopulateItemPanel : MonoBehaviour {

    UsableItemClass usableSent = null;
    EquipableItemClass equipableSent = null;

    //adds name of item in inventory to button
	public void populateUsableData(UsableItemClass item)
    {
        usableSent = item;
        gameObject.GetComponentInChildren<Text>().text = usableSent.GetName() + " x " + usableSent.GetQuantity();        
    }

    //adds name of item in inventory to button
    public void populateEquipableData(EquipableItemClass item)
    {
        equipableSent = item;
        gameObject.GetComponentInChildren<Text>().text = equipableSent.GetName() + " x " + equipableSent.GetQuantity();
    }

    //adds name of item to sell shop button
    public void populateUsableSellData(UsableItemClass item)
    {
        usableSent = item;
        gameObject.GetComponentInChildren<Text>().text = usableSent.GetName();
    }

    //adds name of item to sell shop button
    public void populateEquipSellData(EquipableItemClass item)
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
