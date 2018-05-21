using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleItem : MonoBehaviour {

    private ItemClass selectedItem;

    public Text itemsName;
    public Text itemQuantity;

    public void itemName(ItemClass item)
    {
        selectedItem = item;
        itemsName.text = item.GetName();
        itemQuantity.text = "x" + item.GetQuantity().ToString();
    }

    public void useItem()
    {
        BattleSkillsItems.view.charSelect(null, selectedItem);
        StateMachine.state.itemAction(selectedItem);
    }
}
