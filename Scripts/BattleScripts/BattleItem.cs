using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleItem : MonoBehaviour {

    private UsableItemClass selectedItem;

    public Text itemsName;
    public Text itemQuantity;

    public void itemName(UsableItemClass item)
    {
        Debug.Log("Item Sent Over: " + item.GetName());
        selectedItem = item;
        itemsName.text = item.GetName();
        itemQuantity.text = "x" + item.GetQuantity().ToString();
    }

    public void useItem()
    {
        BattleSkillsItems.view.charSelect(null, selectedItem);
        Battle.battle.itemAction(selectedItem);
    }
}
