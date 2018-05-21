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
        int aveLvl = Manager.manager.AveLevel();
        string whosInRange = Manager.manager.getInRange();
        typeDropdown.GetComponent<Dropdown>();

        //displays items for sell based on if the player is near the blacksmith or the healer
        switch(whosInRange)
        {
            case "Healer":
                //clears any item buttons in the scroll view
                foreach (Transform child in scrollContent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                List<ItemClass> usableItems = GameItems.gItems.getUsableInRange(aveLvl);
                List<string> healerDropdown = new List<string>() { "Healer" };
                //sets dropdown list for shop
                typeDropdown.ClearOptions();
                typeDropdown.AddOptions(healerDropdown);

                //populates items available for sell into the scroll view
                foreach (ItemClass item in usableItems)
                {
                    bool bought = item.IsBought();

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
                List<EquipmentClass> equipableItems = GameItems.gItems.getEquipableInRange(aveLvl);
                List<string> blacksmithDropdown = new List<string>() { "Weapon", "Armor", "Accessory" };
                List<EquipmentClass> weapons = new List<EquipmentClass>();
                List<EquipmentClass> armors = new List<EquipmentClass>();
                List<EquipmentClass> accessories = new List<EquipmentClass>();
                string typeSelected;
                List<Dropdown.OptionData> menuOptions;

                typeDropdown.ClearOptions();
                typeDropdown.AddOptions(blacksmithDropdown);

                int index = typeDropdown.GetComponent<Dropdown>().value;
                menuOptions = typeDropdown.GetComponent<Dropdown>().options;
                typeSelected = menuOptions[index].text;
                                
                foreach (EquipmentClass item in equipableItems)
                {
                    string itemType = item.GetItemType();

                    bool bought = item.IsBought();

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
                        foreach (EquipmentClass weaponItem in weapons)
                        {
                            GameObject invenItem = (GameObject)Instantiate(equipPanel) as GameObject;
                            invenItem.transform.SetParent(scrollContent.transform, false);

                            PopulateBuyPanel popItem = invenItem.GetComponent<PopulateBuyPanel>();

                            popItem.populateEquipButtonData(weaponItem);
                        }
                        break;
                    case "Armor":
                        foreach (EquipmentClass armorItem in armors)
                        {
                            GameObject invenItem = (GameObject)Instantiate(equipPanel) as GameObject;
                            invenItem.transform.SetParent(scrollContent.transform, false);

                            PopulateBuyPanel popItem = invenItem.GetComponent<PopulateBuyPanel>();

                            popItem.populateEquipButtonData(armorItem);
                        }
                        break;
                    case "Accessory":
                        foreach (EquipmentClass accessoryItem in accessories)
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
