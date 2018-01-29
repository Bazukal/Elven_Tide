using System.Collections.Generic;
using UnityEngine;

public class GameItems : MonoBehaviour {

    public static GameItems gItems;
    
    private List<EquipableItemClass> equipableItems = new List<EquipableItemClass>();
    private List<UsableItemClass> usableItems = new List<UsableItemClass>();

    private void Awake()
    {
        gItems = this;
    }

    public void setItems(List<EquipableItemClass> equip, List<UsableItemClass> use)
    {
        equipableItems = equip;
        usableItems = use;
    }

    //gets lists of items in lists
    public List<EquipableItemClass> getAllEquipable() { return equipableItems; }
    public List<UsableItemClass> getAllUsable() { return usableItems; }

    //gets list of equipable items based on the average level of the party
    public List<EquipableItemClass> getEquipableInRange(int aveLvl)
    {
        List<EquipableItemClass> equipables = new List<EquipableItemClass>();
        
        foreach(EquipableItemClass item in equipableItems)
        {
            bool isEquip = item.getEquipable();
            int min = item.getMinLevel();
            int max = item.getMaxLevel();

            if( isEquip == true && aveLvl >= min && aveLvl <= max)
            {
                equipables.Add(item);
            }
        }

        return equipables;
    }

    //gets list of usable items based on the average level of the party
    public List<UsableItemClass> getUsableInRange(int aveLvl)
    {
        List<UsableItemClass> usables = new List<UsableItemClass>();

        foreach (UsableItemClass item in usableItems)
        {
            bool isUsable = item.getUsable();
            int min = item.getMinLevel();
            int max = item.getMaxLevel();

            if (isUsable == true && aveLvl >= min && aveLvl <= max)
            {
                usables.Add(item);
            }
        }

        return usables;
    }

    public EquipableItemClass findEquipItem(string name)
    {
        foreach (EquipableItemClass item in equipableItems)
        {
            if (item.getName().Equals(name))
            {
                return item;
            }
        }
        return null;
    }

    public UsableItemClass findUseItem(string name)
    {
        foreach (UsableItemClass item in usableItems)
        {
            if (item.getName().Equals(name))
            {
                return item;
            }
        }
        return null;
    }
}
