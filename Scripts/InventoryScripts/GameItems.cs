using System.Collections.Generic;
using UnityEngine;

public class GameItems : MonoBehaviour {

    public static GameItems gItems;
    
    private List<EquipmentClass> equipableItems = new List<EquipmentClass>();
    private List<ItemClass> usableItems = new List<ItemClass>();

    private void Awake()
    {
        gItems = this;
    }

    public void setItems(List<EquipmentClass> equip, List<ItemClass> use)
    {
        equipableItems = equip;
        usableItems = use;
    }

    //gets lists of items in lists
    public List<EquipmentClass> getAllEquipable() { return equipableItems; }
    public List<ItemClass> getAllUsable() { return usableItems; }

    //gets list of equipable items based on the average level of the party
    public List<EquipmentClass> getEquipableInRange(int aveLvl)
    {
        List<EquipmentClass> equipables = new List<EquipmentClass>();
        
        foreach(EquipmentClass item in equipableItems)
        {
            int min = item.GetMinLevel();
            int max = item.GetMaxLevel();

            if( aveLvl >= min && aveLvl <= max)
            {
                equipables.Add(item);
            }
        }

        return equipables;
    }

    //gets list of usable items based on the average level of the party
    public List<ItemClass> getUsableInRange(int aveLvl)
    {
        List<ItemClass> usables = new List<ItemClass>();

        foreach (ItemClass item in usableItems)
        {
            int min = item.GetMinLevel();
            int max = item.GetMaxLevel();

            if (aveLvl >= min && aveLvl <= max)
            {
                usables.Add(item);
            }
        }

        return usables;
    }

    public EquipmentClass findEquipItem(string name)
    {
        foreach (EquipmentClass item in equipableItems)
        {
            if (item.GetName().Equals(name))
            {
                return item;
            }
        }
        return null;
    }

    public ItemClass findUseItem(string name)
    {
        foreach (ItemClass item in usableItems)
        {
            if (item.GetName().Equals(name))
            {
                return item;
            }
        }
        return null;
    }
}
