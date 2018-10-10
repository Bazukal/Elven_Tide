using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleItem : MonoBehaviour {

    private UsableItem selectedItem;

    public Text itemsName;
    public Text itemQuantity;

    public void itemName(UsableItem item)
    {
        selectedItem = item;
        itemsName.text = item.name;
        itemQuantity.text = "x" + item.quantity.ToString();
    }

    public void useItem()
    {
        BattleSkillsItems.view.charSelect(null, selectedItem);
        BattleScript.battleOn.itemAction(selectedItem);
    }
}
