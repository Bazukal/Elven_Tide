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
        CloseSellPanel.closeSellPanel.updateGold();
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        try
        {
            SortedDictionary<int, EquipableItem> equipableItems = Manager.manager.getEquipableInventory();
            Dictionary<string, UsableItem> usableItems = Manager.manager.getUsableInventory();

            foreach (Transform child in scrollContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            foreach (KeyValuePair<string, UsableItem> item in usableItems)
            {
                string key = item.Key;

                GameObject invenItem = (GameObject)Instantiate(sellPanel) as GameObject;
                invenItem.transform.SetParent(scrollContent.transform, false);

                populateSellPanel popItem = invenItem.GetComponent<populateSellPanel>();

                popItem.populateUseButtonData(usableItems[key]);
            }

            foreach (KeyValuePair<int, EquipableItem> item in equipableItems)
            {
                int key = item.Key;

                GameObject invenItem = (GameObject)Instantiate(sellPanel) as GameObject;
                invenItem.transform.SetParent(scrollContent.transform, false);

                populateSellPanel popItem = invenItem.GetComponent<populateSellPanel>();

                popItem.populateEquipButtonData(equipableItems[key]);
            }
        }
        catch
        { return; }
    }    
}
