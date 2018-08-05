using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopBuy : MonoBehaviour {

    public static shopBuy sBuy;

    public GameObject equipPanel;
    public GameObject usePanel;
    public GameObject scrollContent;

    public Dropdown typeDropdown;

    private void Awake()
    {
        sBuy = this;
    }

    public void shopBuyPanel()
    {
        CloseBuyPanel.closeBuyPanel.updateGold();
        int aveLvl = Manager.manager.AveLevel();
        GameObject inRange = Manager.manager.getObject();
        string objectTag = inRange.tag;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        typeDropdown.GetComponent<Dropdown>();

        //displays items for sell based on if the player is near the blacksmith or the healer
        switch(objectTag)
        {
            case "Healer":
                //clears any item buttons in the scroll view
                foreach (Transform child in scrollContent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                List<UsableItem> usableItems = Manager.manager.getUsableInRange(aveLvl);
                List<string> healerDropdown = new List<string>() { "Healer" };
                //sets dropdown list for shop
                typeDropdown.ClearOptions();
                typeDropdown.AddOptions(healerDropdown);

                //populates items available for sell into the scroll view
                foreach (UsableItem item in usableItems)
                {
                    bool bought = item.bought;

                    if(bought == true)
                    {
                        GameObject invenItem = (GameObject)Instantiate(usePanel) as GameObject;
                        invenItem.transform.SetParent(scrollContent.transform, false);

                        PopulateBuyPanel popItem = invenItem.GetComponent<PopulateBuyPanel>();

                        popItem.populateUseButtonData(item);
                    }                    
                }
                break;
            case "Blacksmith":
                List<EquipableItem> equipableItems = Manager.manager.getEquipableInRange(aveLvl);
                List<string> blacksmithDropdown = new List<string>() { "Weapon", "Armor", "Accessory" };
                List<EquipableItem> weapons = new List<EquipableItem>();
                List<EquipableItem> armors = new List<EquipableItem>();
                List<EquipableItem> accessories = new List<EquipableItem>();
                string typeSelected;
                List<Dropdown.OptionData> menuOptions;

                typeDropdown.ClearOptions();
                typeDropdown.AddOptions(blacksmithDropdown);

                int index = typeDropdown.GetComponent<Dropdown>().value;
                menuOptions = typeDropdown.GetComponent<Dropdown>().options;
                typeSelected = menuOptions[index].text;
                                
                foreach (EquipableItem item in equipableItems)
                {
                    string itemType = item.type;

                    bool bought = item.bought;

                    if (bought == true)
                    {
                        switch (itemType)
                        {
                            case "Weapon":
                                weapons.Add(item);
                                break;
                            case "Armor":
                                armors.Add(item);
                                break;
                            case "Accessory":
                                accessories.Add(item);
                                break;
                        }
                    }                        
                }

                foreach (Transform child in scrollContent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                switch (typeSelected)
                {
                    case "Weapon":
                        foreach (EquipableItem weaponItem in weapons)
                        {
                            GameObject invenItem = (GameObject)Instantiate(equipPanel) as GameObject;
                            invenItem.transform.SetParent(scrollContent.transform, false);

                            PopulateBuyPanel popItem = invenItem.GetComponent<PopulateBuyPanel>();

                            popItem.populateEquipButtonData(weaponItem);
                        }
                        break;
                    case "Armor":
                        foreach (EquipableItem armorItem in armors)
                        {
                            GameObject invenItem = (GameObject)Instantiate(equipPanel) as GameObject;
                            invenItem.transform.SetParent(scrollContent.transform, false);

                            PopulateBuyPanel popItem = invenItem.GetComponent<PopulateBuyPanel>();

                            popItem.populateEquipButtonData(armorItem);
                        }
                        break;
                    case "Accessory":
                        foreach (EquipableItem accessoryItem in accessories)
                        {
                            GameObject invenItem = (GameObject)Instantiate(equipPanel) as GameObject;
                            invenItem.transform.SetParent(scrollContent.transform, false);

                            PopulateBuyPanel popItem = invenItem.GetComponent<PopulateBuyPanel>();

                            popItem.populateEquipButtonData(accessoryItem);
                        }
                        break;
                }                
                break;
        }
    }      
}
