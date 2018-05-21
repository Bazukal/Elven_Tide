using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopSell : MonoBehaviour {

    public static shopSell shSell;

    public GameObject sellPanel;
    public GameObject scrollContent;

    private void Awake()
    {
        shSell = this;
    }

    //populate sell item screen
    public void shopSellPanel()
    {
        try
        {
            List<EquipmentClass> equipableItems = CharacterInventory.charInven.getEquipableInventory();
            List<ItemClass> usableItems = CharacterInventory.charInven.getUsableInventory();

            foreach (Transform child in scrollContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            foreach (ItemClass item in usableItems)
            {
                GameObject invenItem = (GameObject)Instantiate(sellPanel) as GameObject;
                invenItem.transform.SetParent(scrollContent.transform, false);

                populateSellPanel popItem = invenItem.GetComponent<populateSellPanel>();

                popItem.populateUseButtonData(item);
            }

            foreach (EquipmentClass item in equipableItems)
            {
                GameObject invenItem = (GameObject)Instantiate(sellPanel) as GameObject;
                invenItem.transform.SetParent(scrollContent.transform, false);

                populateSellPanel popItem = invenItem.GetComponent<populateSellPanel>();

                popItem.populateEquipButtonData(item);
            }
        }
        catch
        { return; }
    }    
}
