using UnityEngine;
using UnityEngine.UI;

public class PopulateItemPanel : MonoBehaviour {

    UsableItem usableSent = null;
    EquipableItem equipableSent = null;

    //adds name of item in inventory to button
	public void populateUsableData(UsableItem item)
    {
        usableSent = item;
        gameObject.GetComponentInChildren<Text>().text = usableSent.name + " x " + usableSent.quantity;        
    }

    //adds name of item in inventory to button
    public void populateEquipableData(EquipableItem item)
    {
        equipableSent = item;
        gameObject.GetComponentInChildren<Text>().text = equipableSent.name;
    }

    //adds name of item to sell shop button
    public void populateUsableSellData(UsableItem item)
    {
        usableSent = item;
        gameObject.GetComponentInChildren<Text>().text = usableSent.name;
    }

    //adds name of item to sell shop button
    public void populateEquipSellData(EquipableItem item)
    {
        equipableSent = item;
        gameObject.GetComponentInChildren<Text>().text = equipableSent.name;
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
